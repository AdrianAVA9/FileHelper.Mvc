namespace FileHelperLibrary.Entities
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public string Code { get; set; }

        public ErrorMessage(string message, string code)
        {
            Message = message;
            Code = code;
        }
    }
}
