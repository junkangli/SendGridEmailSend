namespace SendGridEmailSend
{
    class EmailSendSettings
    {
        public string FromEmailAddress { get; set; } = "no-reply@singtelmybusiness.com";
        public string FromEmailName { get; set; } = "no-reply";
        public string ToEmailAddress { get; set; }
        public string ToEmailName { get; set; } = "Tester";
        public string Subject { get; set; } = "Test";
    }
}
