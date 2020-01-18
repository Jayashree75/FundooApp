using Experimental.System.Messaging;
using FundooCommonLayer.ModelRequest;
using System;
using System.Net;
using System.Net.Mail;


namespace FundooCommonLayer.MSMQ
{
  public class Receive
  {
    public static void ReceiveMail(ForgetPassword forgetPassword)
    {
      string path = @".\Private$\MyQueue";
      MessageQueue messageQueue;
      messageQueue = new MessageQueue(path);
      Message mytoken = messageQueue.Receive();
      mytoken.Formatter = new BinaryMessageFormatter();
      

      MailMessage message = new MailMessage("fundooapplication@gmail.com", forgetPassword.Email);
      SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

      message.Subject = "Forget Password Link";
      message.Body = "this is my link to reset password"+mytoken.Body.ToString();
      message.IsBodyHtml = true;
      smtp.UseDefaultCredentials = true;

      smtp.EnableSsl = true;


      smtp.Credentials = new NetworkCredential("fundooapplication@gmail.com", "Fundoo@123");

      try
      {
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        Console.WriteLine("Hola");
      }
      catch (Exception e)
      {

      }
    }
  }

  }
