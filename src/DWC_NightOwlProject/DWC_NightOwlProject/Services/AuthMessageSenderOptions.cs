namespace DWC_NightOwlProject.Services
{
    public class AuthMessageSenderOptions
    {
        //public string SendGridUser { get; set; }
        public string? SendGridKey { get; set; }
        public AuthMessageSenderOptions()
        {
            // Set the SendGrid API key
            //SendGridKey = "YOUR_API_KEY_HERE";
        }
    }
}
