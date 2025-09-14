using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;

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
        
        public virtual void InitLesson(JsonObject modeConfig, SongData song, GeneralConfig config)
        {
            _song   = song;
            _config = config;
            GenerateLookingForNote();
        }

        public virtual void GenerateLookingForNote()
        {
            float totalTime = 0;
            foreach (var note in _song.Notes)
            {
                // Dont looking for rest
                if (note.BaseId > 0)
                {
                    var lookingForNote = new LookingForNote
                                         {
                                             Note     = note,
                                             FromTime = totalTime - _config.HitWindow                 / 2,
                                             ToTime   = totalTime + note.Duration + _config.HitWindow / 2,
                                         };
                    _lookingForNotes.Add(lookingForNote);
                }

                totalTime += note.Duration;
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
                    GameEvents.OnNoteCorrect?.Invoke(note.Note);
                }
            }

            if (_lookingForNotes.Count == 0)
            {
                GameEvents.RequestEndLesson?.Invoke(_result);
            }
            else
            {
                var note = _lookingForNotes[0];
                if (_currentNoteTime > note.ToTime)
                {
                    _result.note_miss++;
                    GameEvents.OnNoteMiss?.Invoke(note.Note);
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
        public int      HitCount;
        public int      FalseHitCount;
    }
}