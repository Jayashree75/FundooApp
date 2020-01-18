﻿using Experimental.System.Messaging;
using System;

namespace FundooCommonLayer.MSMQ
{
  public class Send
  {
    public static void SendMSMQ(string token)
    {
      MessageQueue messageQueue = null;

      string description = "This is a test queue.";

      string messge = token;

      string path = @".\Private$\MyQueue";
      try
      {
        if (MessageQueue.Exists(path))
        {
          messageQueue = new MessageQueue(path);
          messageQueue.Label = description;

        }
        else
        {
          MessageQueue.Create(path);
          messageQueue = new MessageQueue(path);
          messageQueue.Label = description;
        }
        Message message1 = new Message(messge);
        message1.Formatter = new BinaryMessageFormatter();
        messageQueue.Send(message1);

      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}