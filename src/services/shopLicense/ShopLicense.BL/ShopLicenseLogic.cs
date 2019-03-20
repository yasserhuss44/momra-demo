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
            Recieve();
        }

        public void GetLicenseList()
        {
           
        }

        public void Recieve()
        {
            var channel= this._messagingQueueHandler.CreateConnection("RabbitMqServiceBus");
            channel.Recieve<UserCreatedMessage>("UsersCreatedEventQueue",(x)=> {
                HandleMessage(x);
            });
            //var consumer = new EventingBasicConsumer(channel);
            //consumer.Received += (ch, ea) =>
            //{
            //    var body = ea.Body;
            //    // ... process the message
            //    channel.BasicAck(ea.DeliveryTag, false);
            //};
            //String consumerTag = channel.BasicConsume(queueName, false, consumer);
        }
        public void HandleMessage(UserCreatedMessage userCreatedMessage)
        {

        }
    }
}
