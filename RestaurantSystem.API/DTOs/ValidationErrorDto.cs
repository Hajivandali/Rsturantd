namespace RestaurantSystem.API.DTOs
{
    public class ValidationErrorDto
    {
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? AttemptedValue { get; set; }
    }

    public class ValidationResultDto
    {
        public bool IsValid { get; set; }
        public List<ValidationErrorDto> Errors { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }
}
