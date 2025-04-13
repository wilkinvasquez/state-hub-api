namespace SB.StateHub.API.DTOs.Results
{
    public class ResultDto
    {
        public bool Success { get; set; }

        public ResultDto(bool success)
        {
            Success = success;
        }
    }
}