namespace API.Model
{
    public class Account
    {
        public long AccountID { get; set; }

        public long CustId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string DisplayName { get; set; }
        public string MailServerName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
