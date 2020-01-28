namespace MSMQListener
{
using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;


  public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);
  public class MSMQListener
  {
    private bool _listen;
    private MessageQueue _queue;
    public event MessageReceivedEventHandler MessageReceived;

    public MSMQListener(string queuePath)
    {
      _queue = new MessageQueue(queuePath);
    }

    public void Start()
    {
      _listen = true;
      _queue.Formatter = new BinaryMessageFormatter();
      _queue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
      _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);
      StartListening();
      Console.ReadKey();
    }
    public void Stop()
    {
      _listen = false;
      _queue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
      _queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveCompleted);

    }
    private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
    {
      Message msg = _queue.EndReceive(e.AsyncResult);
      Console.WriteLine(msg.Body.ToString() + " " + msg.Label.ToString());
      Mail.SendTokenTomail(msg.Body.ToString(), msg.Label.ToString());
      StartListening();

      FireRecieveEvent(msg.Body.ToString());
    }
    private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
    {
      _queue.EndPeek(e.AsyncResult);
      MessageQueueTransaction trans = new MessageQueueTransaction();
      Message msg = null;
      try
      {
        trans.Begin();
        msg = _queue.Receive(trans);
        trans.Commit();
        StartListening();
        FireRecieveEvent(msg.Body);
      }
      catch
      {
        trans.Abort();
      }
    }
    private void FireRecieveEvent(object body)
    {
      MessageReceived?.Invoke(this, new MessageEventArgs(body));
    }
    private void StartListening()
    {
      try
      {
        if (!_listen)     
          return;        
        if (_queue.Transactional)
          _queue.BeginPeek();        
        else
          _queue.BeginReceive();        
      }
      catch (Exception e)
      {

        throw new Exception(e.Message);
      }
    }
  }
  public class MessageEventArgs : EventArgs
  {
    private object _messageBody;
    public object MessageBody
    {
      get { return _messageBody; }
    }
    public MessageEventArgs(object body)
    {
      _messageBody = body;

    }
  }
}
