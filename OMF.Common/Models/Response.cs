namespace OMF.Common.Models
{
    public class Response
    {
        public Response(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}