namespace RealEstatePlatform.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse() => Success = true;

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public string Message { get; set; }
        public bool Success { get; set; }

        public List<string>? ValidationsErrors { get; set; }
    }
}
