using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace DWC_NightOwlProject.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }
        public AuthMessageSenderOptions Options { get; }
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("oneilmagno@gmail.com", "DND World Creator - Confirm Your Email"),
                Subject = "DND World Creator - Confirm Your Email",
                PlainTextContent = message,
                HtmlContent = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">\r\n" +
                "<html data-editor-version=\"2\" class=\"sg-campaigns\" xmlns=\"http://www.w3.org/1999/xhtml\">\r\n    " +
                "<head>\r\n      " +
                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n      " +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1\">\r\n      " +
                "<!--[if !mso]><!-->\r\n      <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\">\r\n      " +
                "<!--<![endif]-->\r\n      " +
                "<!--[if (gte mso 9)|(IE)]>\r\n      " +
                "<xml>\r\n        " +
                "<o:OfficeDocumentSettings>\r\n          " +
                "<o:AllowPNG/>\r\n          " +
                "<o:PixelsPerInch>96</o:PixelsPerInch>\r\n        " +
                "</o:OfficeDocumentSettings>\r\n      " +
                "</xml>\r\n      " +
                "<![endif]-->\r\n      " +
                "<!--[if (gte mso 9)|(IE)]>\r\n  " +
                "<style type=\"text/css\">\r\n    " +
                "body {width: 600px;margin: 0 auto;}\r\n    " +
                "table {border-collapse: collapse;}\r\n    " +
                "table, td {mso-table-lspace: 0pt;mso-table-rspace: 0pt;}\r\n    " +
                "img {-ms-interpolation-mode: bicubic;}\r\n  " +
                "</style>\r\n<![endif]-->\r\n      " +
                "<style type=\"text/css\">\r\n    " +
                "body, p, div {\r\n      " +
                "font-family: inherit;\r\n      " +
                "font-size: 14px;\r\n    " +
                "}\r\n    " +
                "body {\r\n      " +
                "color: #000000;\r\n    " +
                "}\r\n    " +
                "body a {\r\n      " +
                "color: #000000;\r\n      " +
                "text-decoration: none;\r\n    " +
                "}\r\n    " +
                "p { margin: 0; padding: 0; }\r\n    " +
                "table.wrapper {\r\n      " +
                "width:100% !important;\r\n      " +
                "table-layout: fixed;\r\n      " +
                "-webkit-font-smoothing: antialiased;\r\n      " +
                "-webkit-text-size-adjust: 100%;\r\n      " +
                "-moz-text-size-adjust: 100%;\r\n      " +
                "-ms-text-size-adjust: 100%;\r\n    " +
                "}\r\n    " +
                "img.max-width {\r\n      " +
                "max-width: 100% !important;\r\n    " +
                "}\r\n    " +
                ".column.of-2 {\r\n      " +
                "width: 50%;\r\n    " +
                "}\r\n    " +
                ".column.of-3 {\r\n      " +
                "width: 33.333%;\r\n    " +
                "}\r\n    " +
                ".column.of-4 {\r\n      " +
                "width: 25%;\r\n    " +
                "}\r\n    " +
                "ul ul ul ul  {\r\n      " +
                "list-style-type: disc !important;\r\n    " +
                "}\r\n    " +
                "ol ol {\r\n      " +
                "list-style-type: lower-roman !important;\r\n    " +
                "}\r\n    " +
                "ol ol ol {\r\n      " +
                "list-style-type: lower-latin !important;\r\n    " +
                "}\r\n    " +
                "ol ol ol ol {\r\n      " +
                "list-style-type: decimal !important;\r\n    " +
                "}\r\n    " +
                "@media screen and (max-width:480px) {\r\n      " +
                ".preheader .rightColumnContent,\r\n      " +
                ".footer .rightColumnContent {\r\n        " +
                "text-align: left !important;\r\n      " +
                "}\r\n      " +
                ".preheader .rightColumnContent div,\r\n      " +
                ".preheader .rightColumnContent span,\r\n      " +
                ".footer .rightColumnContent div,\r\n      " +
                ".footer .rightColumnContent span {\r\n        " +
                "text-align: left !important;\r\n      " +
                "}\r\n      " +
                ".preheader .rightColumnContent,\r\n      " +
                ".preheader .leftColumnContent {\r\n        " +
                "font-size: 80% !important;\r\n        " +
                "padding: 5px 0;\r\n      " +
                "}\r\n      " +
                "table.wrapper-mobile {\r\n        " +
                "width: 100% !important;\r\n        " +
                "table-layout: fixed;\r\n      " +
                "}\r\n      " +
                "img.max-width {\r\n        " +
                "height: auto !important;\r\n        " +
                "max-width: 100% !important;\r\n      " +
                "}\r\n      " +
                "a.bulletproof-button {\r\n        " +
                "display: block !important;\r\n       " +
                " width: auto !important;\r\n        " +
                "font-size: 80%;\r\n        " +
                "padding-left: 0 !important;\r\n        " +
                "padding-right: 0 !important;\r\n      " +
                "}\r\n      " +
                ".columns {\r\n        " +
                "width: 100% !important;\r\n      " +
                "}\r\n      " +
                ".column {\r\n        " +
                "display: block !important;\r\n        " +
                "width: 100% !important;\r\n        " +
                "padding-left: 0 !important;\r\n        " +
                "padding-right: 0 !important;\r\n        " +
                "margin-left: 0 !important;\r\n        " +
                "margin-right: 0 !important;\r\n      " +
                "}\r\n      " +
                ".social-icon-column {\r\n        " +
                "display: inline-block !important;\r\n      " +
                "}\r\n    " +
                "}\r\n  " +
                "</style>\r\n    " +
                "<style>\r\n      " +
                "@media screen and (max-width:480px) {\r\n        " +
                "table\\0 {\r\n          " +
                "width: 480px !important;\r\n          " +
                "}\r\n      }\r\n    " +
                "</style>\r\n      " +
                "<!--user entered Head Start--><link href=\"https://fonts.googleapis.com/css?family=Viga&display=swap\" rel=\"stylesheet\"><style>\r\n    " +
                "body {font-family: 'Viga', sans-serif;}\r\n</style><!--End Head user entered-->\r\n    </head>\r\n    <body>\r\n      <center class=\"wrapper\" data-link-color=\"#000000\" data-body-style=\"font-size:14px; font-family:inherit; color:#000000; background-color:#FFFFFF;\">\r\n        <div class=\"webkit\">\r\n          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" class=\"wrapper\" bgcolor=\"#FFFFFF\">\r\n            <tr>\r\n              <td valign=\"top\" bgcolor=\"#FFFFFF\" width=\"100%\">\r\n                <table width=\"100%\" role=\"content-container\" class=\"outer\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                  <tr>\r\n                    <td width=\"100%\">\r\n                      <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                        <tr>\r\n                          <td>\r\n                            <!--[if mso]>\r\n    <center>\r\n    <table><tr><td width=\"600\">\r\n  <![endif]-->\r\n                                    <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; max-width:600px;\" align=\"center\">\r\n                                      <tr>\r\n                                        <td role=\"modules-container\" style=\"padding:0px 0px 0px 0px; color:#000000; text-align:left;\" bgcolor=\"#FFFFFF\" width=\"100%\" align=\"left\"><table class=\"module preheader preheader-hide\" role=\"module\" data-type=\"preheader\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;\">\r\n    <tr>\r\n      <td role=\"module-content\">\r\n        <p></p>\r\n      </td>\r\n    </tr>\r\n  </table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"100%\" role=\"module\" data-type=\"columns\" style=\"padding:0px 0px 0px 0px;\" bgcolor=\"#7b63c2\" data-distribution=\"1\">\r\n    <tbody>\r\n      <tr role=\"module-content\">\r\n        <td height=\"100%\" valign=\"top\"><table width=\"580\" style=\"width:580px; border-spacing:0; border-collapse:collapse; margin:0px 10px 0px 10px;\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\" border=\"0\" bgcolor=\"\" class=\"column column-0\">\r\n      <tbody>\r\n        <tr>\r\n          <td style=\"padding:0px;margin:0px;border-spacing:0;\"><table class=\"module\" role=\"module\" data-type=\"spacer\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"10cc50ce-3fd3-4f37-899b-a52a7ad0ccce\">\r\n    <tbody>\r\n      <tr>\r\n        <td style=\"padding:0px 0px 40px 0px;\" role=\"module-content\" bgcolor=\"\">\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class=\"wrapper\" role=\"module\" data-type=\"image\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"f8665f9c-039e-4b86-a34d-9f6d5d439327\">\r\n    <tbody>\r\n      <tr>\r\n        <td style=\"font-size:6px; line-height:10px; padding:0px 0px 0px 0px;\" valign=\"top\" align=\"center\">\r\n          <img class=\"max-width\" border=\"0\" style=\"display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;\" width=\"1043\" alt=\"\" data-proportionally-constrained=\"true\" data-responsive=\"false\" src=\"http://cdn.mcauto-images-production.sendgrid.net/361cb3409b3c8397/66477d14-2301-4c5c-b3fd-75125a46c305/1043x190.png\" height=\"190\">\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class=\"module\" role=\"module\" data-type=\"spacer\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"10cc50ce-3fd3-4f37-899b-a52a7ad0ccce.1\">\r\n    " +
                "<tbody>\r\n      " +
                "<tr>\r\n        " +
                "<td style=\"padding:0px 0px 30px 0px;\" role=\"module-content\" bgcolor=\"\">\r\n        " +
                "</td>\r\n      " +
                "</tr>\r\n    " +
                "</tbody>\r\n  " +
                "</table></td>\r\n        " +
                "</tr>\r\n      " +
                "</tbody>\r\n    </table></td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"bff8ffa1-41a9-4aab-a2ea-52ac3767c6f4\" data-mc-module-version=\"2019-10-22\">\r\n    <tbody>\r\n      <tr>\r\n        <td style=\"padding:18px 30px 18px 30px; line-height:40px; text-align:inherit; background-color:#7B63C2;\" height=\"100%\" valign=\"top\" bgcolor=\"#7B63C2\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: center\"><span style=\"font-size: 40px; font-family: inherit; color: #f6f6f6\">Thank you for registering!</span></div><div></div></div></td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class=\"module\" role=\"module\" data-type=\"text\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed;\" data-muid=\"2f94ef24-a0d9-4e6f-be94-d2d1257946b0\" data-mc-module-version=\"2019-10-22\">\r\n    <tbody>\r\n      <tr>\r\n        <td style=\"padding:18px 50px 18px 50px; line-height:22px; text-align:inherit; background-color:#7B63C2;\" height=\"100%\" valign=\"top\" bgcolor=\"#7B63C2\" role=\"module-content\"><div><div style=\"font-family: inherit; text-align: center\"><span style=\"font-size: 16px; font-family: inherit; color: #f8f8f8\">Confirm your email address to start forging your destiny!</span></div><div></div></div></td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"module\" data-role=\"module-button\" data-type=\"button\" role=\"module\" style=\"table-layout:fixed;\" width=\"100%\" data-muid=\"c7bd4768-c1ab-4c64-ba24-75a9fd6daed8\">\r\n      <tbody>\r\n        <tr>\r\n          <td align=\"center\" bgcolor=\"#7B63C2\" class=\"outer-td\" style=\"padding:10px 0px 20px 0px; background-color:#7B63C2;\">\r\n            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"wrapper-mobile\" style=\"text-align:center;\">\r\n              <tbody>\r\n                <tr>\r\n                <td align=\"center\" bgcolor=\"#eac96c\" class=\"inner-td\" style=\"border-radius:6px; font-size:16px; text-align:center; background-color:inherit;\">\r\n                 "+ message +               
                "</td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table><div data-role=\"module-unsubscribe\" class=\"module\" role=\"module\" data-type=\"unsubscribe\" style=\"color:#444444; font-size:12px; line-height:20px; padding:16px 16px 16px 16px; text-align:Center;\" data-muid=\"4e838cf3-9892-4a6d-94d6-170e474d21e5\"><div class=\"Unsubscribe--addressLine\"></div><p style=\"font-size:12px; line-height:20px;\"><a target=\"_blank\" class=\"Unsubscribe--unsubscribeLink zzzzzzz\" href=\"{{{unsubscribe}}}\" style=\"\">Unsubscribe</a> - <a href=\"{{{unsubscribe_preferences}}}\" target=\"_blank\" class=\"Unsubscribe--unsubscribePreferences\" style=\"\">Unsubscribe Preferences</a></p></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"module\" data-role=\"module-button\" data-type=\"button\" role=\"module\" style=\"table-layout:fixed;\" width=\"100%\" data-muid=\"188c3d22-338c-4a35-a298-a7d3957f579d\">\r\n      <tbody>\r\n        <tr>\r\n          <td align=\"center\" bgcolor=\"\" class=\"outer-td\" style=\"padding:0px 0px 20px 0px;\">\r\n            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"wrapper-mobile\" style=\"text-align:center;\">\r\n              <tbody>\r\n                <tr>\r\n                <td align=\"center\" bgcolor=\"#f5f8fd\" class=\"inner-td\" style=\"border-radius:6px; font-size:16px; text-align:center; background-color:inherit;\"><a href=\"https://www.sendgrid.com/?utm_source=powered-by&utm_medium=email\" style=\"background-color:#f5f8fd; border:1px solid #f5f8fd; border-color:#f5f8fd; border-radius:25px; border-width:1px; color:#a8b9d5; display:inline-block; font-size:10px; font-weight:normal; letter-spacing:0px; line-height:normal; padding:5px 18px 5px 18px; text-align:center; text-decoration:none; border-style:solid; font-family:helvetica,sans-serif;\" target=\"_blank\">♥ POWERED BY TWILIO SENDGRID</a></td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table></td>\r\n                                      </tr>\r\n                                    </table>\r\n                                    <!--[if mso]>\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n                            </center>\r\n                            <![endif]-->\r\n                          </td>\r\n                        </tr>\r\n                      </table>\r\n                    </td>\r\n                  </tr>\r\n                </table>\r\n              </td>\r\n            </tr>\r\n          </table>\r\n        </div>\r\n      </center>\r\n    </body>\r\n  </html>" +  message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
