namespace SB.StateHub.API.DTOs.Results
{
    public class ErrorResultDto : ResultDto
    {
        public string[]? Messages { get; set; }

        public ErrorResultDto(bool success, string[] messages) : base(success)
        {
            Success = success;
            Messages = messages;
        }
    }
}