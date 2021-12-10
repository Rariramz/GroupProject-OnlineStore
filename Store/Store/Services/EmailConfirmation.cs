using Store.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Store.Services
{
    public class EmailConfirmation
    {
        public string CredentialEmail { get; set; }
        public string CredentialPassword { get; set; }
        public string Url { get; set; }

        public NetworkCredential networkCredential { get; set; }
        public EmailConfirmation(string credentialEmail, string credentialPassword, string url)
        {
            CredentialEmail = credentialEmail;
            CredentialPassword = credentialPassword;
            networkCredential = new NetworkCredential(credentialEmail, credentialPassword);
            Url = url;
        }

        public string BeginConfirmation(string email)
        {
            string code = GenerateCode(32);
            MailAddress from = new MailAddress(CredentialEmail);
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Email confirmation";
            StringBuilder body = new StringBuilder();
            body.Append("<h2>Confirm your email for Candleaf.<h2/>");
            body.Append("<br/>");
            body.AppendLine($"<a href=\"{Url}/api/Account/Confirm?email={email}&code={code}\">Press here</a>");
            message.Body = body.ToString();
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = networkCredential;
            smtpClient.Send(message);
            

            return code;
        }

        public void ConfirmOrder(string email, OrderData orderEmailData)
        {
            MailAddress from = new MailAddress(CredentialEmail);
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Thank you for your order!";
            StringBuilder body = new StringBuilder();
            body.AppendLine("<h2>You just ordered the following items: </h2>");
            body.AppendLine("<ul>");
            foreach(OrderItemData orderItemData in orderEmailData.ItemDatas)
            {
                body.Append("<li>");
                body.Append($"{orderItemData.Count}x {orderItemData.ItemData.Name}");
                body.AppendLine("</li>");
            }
            body.AppendLine("</ul>");
            body.AppendLine($"<h3>Total price: {orderEmailData.TotalPrice}</h3>");

            body.AppendLine("<br/>");
            body.AppendLine($"<h4>Date of order: {orderEmailData.InitialDate}</h4>");
            body.AppendLine($"<h4>Delivery address: {orderEmailData.AddressData.AddressString}</h4>");

            message.Body = body.ToString();
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = networkCredential;
            smtpClient.Send(message);
        }

        private string GenerateCode(int length)
        {
            StringBuilder buffer = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                buffer.Append((char)random.Next('A', 'Z' + 1));
            }
            return buffer.ToString();
        }
    }
}
