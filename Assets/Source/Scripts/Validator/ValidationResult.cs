using System.Collections.Generic;

namespace Source.Scripts.Gameplay.Controller
{
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