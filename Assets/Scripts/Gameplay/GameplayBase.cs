using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core
{
    public abstract class GameplayBase
    {
        public int LookingForCount = 3;

        protected SongData             _song;
        protected GeneralConfig        _config;
        protected List<LookingForNote> _lookingForNotes = new();
        protected float                _currentNoteTime;
        protected LessonResult         _result;
        protected float                _totalSongLength;
        
        public virtual void InitLesson(JObject modeConfig, SongData song, GeneralConfig config)
        {
            _song   = song;
            _config = config;
            _result = new();
        }

        public virtual void GenerateLookingForNote()
        {
            _totalSongLength = 0;
            for (var i = 0; i < _song.Notes.Count; i++)
            {
                var note = _song.Notes[i];
                // Dont looking for rest
                if (note.BaseId > 0)
                {
                    var lookingForNote = new LookingForNote
                                         {
                                             Note     = note,
                                             FromTime = _totalSongLength - _config.HitWindow                 / 2,
                                             ToTime   = _totalSongLength + note.Duration + _config.HitWindow / 2,
                                             Index    = i,
                                         };
                    _lookingForNotes.Add(lookingForNote);
                }

                _totalSongLength += note.Duration;
            }
        }

        public virtual void Update(float noteTime)
        {
            _currentNoteTime += noteTime;

            var playingNotes = GameEvents.GetCurrentPlayNotes?.Invoke();
            if(playingNotes == null) return;
            
            for (var i = 0; i < LookingForCount; ++i)
            {
                if(i >= _lookingForNotes.Count) break;
                var note = _lookingForNotes[i];
                
                if(_currentNoteTime< note.FromTime || _currentNoteTime > note.ToTime) continue;
                
                var index = playingNotes.FindIndex(x => x.Item2.BaseId == note.Note.BaseId);

                if (index >= 0)
                {
                    // Found and consume
                    _result.note_hit++;
                    _lookingForNotes.RemoveAt(i);
                    GameEvents.RequestConsumePlayNotes?.Invoke(index);
                    GameEvents.OnNoteCorrect?.Invoke(note.Index, note.Note);
                }
            }

            if (_currentNoteTime >= _totalSongLength)
            {
                _result.song_end = true;
                GameEvents.RequestEndLesson?.Invoke(_result);
            }
            else if(_lookingForNotes.Count > 0)
            {
                var note = _lookingForNotes[0];
                if (_currentNoteTime > note.ToTime)
                {
                    _result.note_miss++;
                    GameEvents.OnNoteMiss?.Invoke(note.Index, note.Note);
                    _lookingForNotes.RemoveAt(0);
                }
            }
        }
    }

    public struct LookingForNote
    {
        public NoteData Note;
        public float    FromTime;
        public float    ToTime;
        public int      Index;
    }
}