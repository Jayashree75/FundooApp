//-----------------------------------------------------------------------
// <copyright file="Send.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.MSMQ
{
using Experimental.System.Messaging;
using System;

  /// <summary>
  /// This is the class for sendingMSMQ.
  /// </summary>
  public class Send
  {
    public static void SendMSMQ(string token,string email)
    {
      MessageQueue messageQueue = null;
      string messge = token;
      string path = @".\Private$\MyQueue";
      try
      {
        if (MessageQueue.Exists(path))
        {
          messageQueue = new MessageQueue(path);
          
        }
        else
        {
          MessageQueue.Create(path);
          messageQueue = new MessageQueue(path);
        
        }
        messageQueue.Label = email;
        Message message1 = new Message(messge);
        message1.Formatter = new BinaryMessageFormatter();
        messageQueue.Send(message1, email);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}
