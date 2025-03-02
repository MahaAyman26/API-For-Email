namespace API.Model
{
    public class Emailcontent
    {
        public required string FromEmail { set; get; }

        public required string ToEmail { set; get; }
        public string? CcEmail { set; get; } = string.Empty;

        public required string Subject { set; get; }
        public required string Body { set; get; }
        public required string BodyFormat { set; get; }

       public string? Attachment { set; get; } = string.Empty;
        public int Importance { get; set; }
        //By default= 2 (Normal) 3 - High 1 - Low
    }
}
