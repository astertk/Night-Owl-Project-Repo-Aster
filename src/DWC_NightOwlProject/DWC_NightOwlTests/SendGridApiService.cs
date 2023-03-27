using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DWC_NightOwlTests.SendGridApiService
{
    public class SendGridService
    {
        private readonly ISendGridClient _client;

        public SendGridService(ISendGridClient client)
        {
            _client = client;
        }

        public async Task<Response> SendEmailAsync(SendGridMessage message)
        {
            return await _client.SendEmailAsync(message);
        }
    }
}
