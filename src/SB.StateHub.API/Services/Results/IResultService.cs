using SB.StateHub.API.DTOs.Results;

namespace SB.StateHub.API.Services.Results
{
    public interface IResultService
    {
        SuccessResultDto CreateSuccessResult();
        SuccessResultDto CreateSuccessResult(dynamic? data);
        ErrorResultDto CreateErrorResult(string message);
        ErrorResultDto CreateErrorResult(string[] messages);
    }
}