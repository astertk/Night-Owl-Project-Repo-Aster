using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SendGrid;
using SendGrid.Helpers.Mail;
using Moq;
using DWC_NightOwlTests.SendGridApiService;

namespace DWC_NightOwlTests
{
    public class SendGridApiTests
    {
        [Test]
        public async Task SendEmail_Should_Return_Ok_Status_Code()
        {
            //Arrange
            var mockClient = new Mock<ISendGridClient>();
            mockClient.Setup(x => x.SendEmailAsync(
                It.IsAny<SendGridMessage>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response(System.Net.HttpStatusCode.Accepted, null, null));


            var message = new SendGridMessage
            {
                From = new EmailAddress("oneilmagno@gmail.com", "From Oneil"),
                Subject = "Test Email",
                HtmlContent = "<p>This is a test email</p>"
            };
            message.AddTo(new EmailAddress("onel29gaming@gmail.com", "To Other Oneil"));

            var sendGridService = new SendGridService(mockClient.Object);

            //Act
            var response = await sendGridService.SendEmailAsync(message);

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.Accepted, response.StatusCode);
            mockClient.VerifyAll();
        }
    }
}
