using Helpers.Models;
using RabbitMQ.Client;

namespace Common.Messaging.Queues
{
    public interface IMessagingQueueHandler
    {
        ResponseDetails<T, ulong> PullMessage<T>(IModel channel);

        ResponseDetailsBase PushMessage<T>(T message, IModel channel);

        ResponseDetailsBase AckNowledge(ulong deleviryTag, IModel channel);

        IModel CreateConnection();
    }
}