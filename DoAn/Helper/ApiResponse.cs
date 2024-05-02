namespace DoAn.Helper
{
    public class ApiResponse
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }

        public ApiResponse(int statusCode, string message = "", object data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Success = statusCode >= 200 && statusCode < 300;
            Data = data;
        }
    }
}
