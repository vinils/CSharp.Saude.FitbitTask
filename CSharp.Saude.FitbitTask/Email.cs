namespace CSharp.Saude.FitbitTask
{
    using System.Net;
    using System.Net.Mail;

    public class Email
    {
        public static void Send(string myEmail, string myPassword, string body)
        {
            //var fromAddress = new MailAddress("from@example.com", "From Name");
            var fromAddress = new MailAddress(myEmail);
            var toAddress = new MailAddress(myEmail);
            const string subject = "Erro fitbit task";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, myPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
