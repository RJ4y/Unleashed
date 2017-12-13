using System;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class MessengerService : IMessagingCenter
    {
        IMessagingCenter _messagingService = MessagingCenter.Instance;

        public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            _messagingService.Send<TSender, TArgs>(sender, message, args);
        }

        internal void Send(object sender, string v)
        {
            _messagingService.Send(sender, v);
        }

        public void Send<TSender>(TSender sender, string message) where TSender : class
        {
            _messagingService.Send<TSender>(sender, message);
        }

        public void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback, TSender source = null) where TSender : class
        {
            _messagingService.Subscribe<TSender, TArgs>(subscriber, message, callback, source);
        }

        public void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback, TSender source = null) where TSender : class
        {
            _messagingService.Subscribe<TSender>(subscriber, message, callback, source);
        }

        public void Unsubscribe<TSender, TArgs>(object subscriber, string message) where TSender : class
        {
            _messagingService.Unsubscribe<TSender, TArgs>(subscriber, message);
        }

        public void Unsubscribe<TSender>(object subscriber, string message) where TSender : class
        {
            _messagingService.Unsubscribe<TSender>(subscriber, message);
        }
    }
}
