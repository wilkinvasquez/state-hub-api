namespace SB.StateHub.API.DTOs.Results
{
    public class SuccessResultDto : ResultDto
    {
        public dynamic? Data { get; set; }

        public SuccessResultDto(bool success) : base(success)
        {
            Success = success;
        }

        public SuccessResultDto(bool success, dynamic? data) : base(success)
        {
            Success = success;
            Data = data;
        }
    }
}