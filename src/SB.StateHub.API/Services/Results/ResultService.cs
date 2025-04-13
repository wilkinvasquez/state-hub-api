using SB.StateHub.API.DTOs.Results;

namespace SB.StateHub.API.Services.Results
{
    public class ResultService : IResultService
    {
        public SuccessResultDto CreateSuccessResult()
        {
            return new SuccessResultDto(true);
        }

        public SuccessResultDto CreateSuccessResult(dynamic? data)
        {
            return new SuccessResultDto(true, data);
        }

        public ErrorResultDto CreateErrorResult(string message)
        {
            return new ErrorResultDto(false, new string[] { message });
        }

        public ErrorResultDto CreateErrorResult(string[] messages)
        {
            return new ErrorResultDto(false, messages);
        }
    }
}