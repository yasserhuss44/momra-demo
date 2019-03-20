using Helpers.Models;
using RabbitMQ.Client;

namespace Common.Messaging.Queues
{
    public interface IMessagingQueue
    {
        //ResponseDetails<T, ulong> PullMessage<T>();

        //IModel Channel { get; set; }

        IModel CreateConnection(string configSectionName);

        //void DeclareQueue(string exchangeName,string queueName, string routingKey);

        //ResponseDetailsBase PushMessage<T>(T message, string exchange,string routingKey);

        //ResponseDetailsBase AckNowledge(ulong deleviryTag, IModel channel);        
    }
}