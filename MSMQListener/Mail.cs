using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MSMQListener
{
  public class Mail
  {
    public static bool SendTokenTomail(string token, string email)
    {
      try {
        if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(email))
        {
          MailMessage message = new MailMessage("fundooapplication@gmail.com", email);
          SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
          message.Subject = "Forget Password Link";
          string url = "http://localhost:4200/ResetPassword/" + token;
          message.Body = "this is my link to reset password<br>" + url;
          message.IsBodyHtml = true;
          smtp.UseDefaultCredentials = true;
          smtp.EnableSsl = true;
          smtp.Credentials = new NetworkCredential("fundooapplication@gmail.com", "Fundoo@123");
          smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
          smtp.Send(message);
          Console.WriteLine("Hola");
          return true;
        }
        return false;
      }
      catch(Exception e)
      {
        throw new Exception(e.Message);
      }
    
    }
  }
}
