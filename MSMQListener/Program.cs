using System;

namespace MSMQListener
{
  public class Program
  {
    public static void Main(string[] args)
    {
      string path = @".\Private$\MyQueue";
      MSMQListener msmqlistener = new MSMQListener(path);
      msmqlistener.Start();
      Console.WriteLine("started listening");
    }
  }
}
