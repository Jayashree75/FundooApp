using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MSMQListener
{
  public class Mail
  {
    public static bool SendTokenTomail(string token,string email)
    {
      if(!string.IsNullOrWhiteSpace(token)&&!string.IsNullOrWhiteSpace(email))
      {
        MailMessage message = new MailMessage("fundooapplication@gmail.com", email);
        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        message.Subject = "Forget Password Link";
        message.Body = "this is my link to reset password" + token;
        message.IsBodyHtml = true;
        smtp.UseDefaultCredentials = true;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential("fundooapplication@gmail.com", "Fundoo@123");
        try
        {
          smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
          smtp.Send(message);
          Console.WriteLine("Hola");
          return true;
        }
        catch (Exception e)
        {
          throw new Exception(e.Message);
        }
      }return false; 
    }
  }
}
