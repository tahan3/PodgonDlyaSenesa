using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Config;
using Source.Scripts.Gameplay.Model;

namespace Source.Scripts.Gameplay.Controller
{
    public class BoardValidator
    {
        private readonly RuntimeData _runtimeData;
        private readonly LevelConfig _levelConfig;
        
        //Observable<ValidationResult>
        
        public BoardValidator(RuntimeData runtimeData, LevelConfig levelConfig)
        {
            _runtimeData = runtimeData;
            _levelConfig = levelConfig;
        }
        
        public void Validate()
        {
            ValidationResult result = ValidateBoard();
            //Observable<ValidationResult>?.OnNext
        }

        private ValidationResult ValidateBoard()
        {
            var correctResults = new bool[_levelConfig.Words.Count];
            var incorrectResultsCounter = 0;
            
            for (var i = 0; i < _runtimeData.WordSlots.Count; i++)
            {
                if (!_runtimeData.WordSlots[i].IsFull)
                {
                    return new ValidationResult(false, "There are unfilled words");
                }

                if (_levelConfig.Words.Contains(_runtimeData.WordSlots[i].GetCurrentWord()))
                {
                    correctResults[i] = true;
                }
                else
                {
                    incorrectResultsCounter++;
                }
            }

            if (incorrectResultsCounter > 0)
            {
                return new ValidationResult(false, $"There are {incorrectResultsCounter} incorrect words");
            }
            

            return new ValidationResult(true, "All words are correct");
        }
    }
    
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string Message { get; }
        public List<int> InvalidSlotIndices { get; }

        public ValidationResult(bool isValid, string message, List<int> invalidSlotIndices = null)
        {
            IsValid = isValid;
            Message = message;
            InvalidSlotIndices = invalidSlotIndices ?? new List<int>();
        }
    }
}