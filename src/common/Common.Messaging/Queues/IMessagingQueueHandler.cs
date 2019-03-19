using Helpers.Models;
using RabbitMQ.Client;

namespace Common.Messaging.Queues
{
    public interface IMessagingQueueHandler
    {
        ResponseDetails<T, ulong> PullMessage<T>();

        ResponseDetailsBase PushMessage<T>(T message);

        ResponseDetailsBase AckNowledge(ulong deleviryTag, IModel channel);        
    }
}