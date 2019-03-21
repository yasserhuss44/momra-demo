using Common.Messaging.Models;
using Common.Messaging.Queues;
using RabbitMQ.Client.Events;
using ShopLicense.Model;
using System;

namespace ShopLicense.BL
{
    public class ShopLicenseLogic:IShopLicenseLogic
    {
        IMessagingQueue _messagingQueueHandler;
        public ShopLicenseLogic(IMessagingQueue messagingQueueHandler)
        {
            this._messagingQueueHandler = messagingQueueHandler;
            ConsumeMessageBroker();
        }

        public void GetLicenseList()
        {
           
        }

        public void ConsumeMessageBroker()
        {
            var channel= this._messagingQueueHandler.CreateConnection("RabbitMqServiceBus");
            channel.Recieve<UserCreatedMessage>("UsersCreatedEventQueue",(x)=> {
                HandleMessage(x);
            });
        }
        public void HandleMessage(UserCreatedMessage userCreatedMessage)
        {

        }
    }
}
