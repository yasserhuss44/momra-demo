using Helpers.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Common.Messaging.Queues
{
    public class MessagingQueue : IMessagingQueue
    {
        private readonly ILogger _logger;
        private readonly IConfiguration Configuration;
        public MessagingQueue(IConfiguration configuration, ILogger<MessagingQueue> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IModel CreateConnection(string configSectionName)
        {
            var rabbitMqConfSection = Configuration.GetSection(configSectionName);
            var amqbUrl = rabbitMqConfSection.GetValue<string>("AMQPURL");
            var userName = rabbitMqConfSection.GetValue<string>("UserName");
            var password = rabbitMqConfSection.GetValue<string>("Password");
            var virtualHost = rabbitMqConfSection.GetValue<string>("VirtualHost");
            var hostName = rabbitMqConfSection.GetValue<string>("HostName");           
            ConnectionFactory connFactory = new ConnectionFactory
            {
                UserName = userName,
                Password = password,
                VirtualHost = virtualHost,
                HostName = hostName,
            };

            var conn = connFactory.CreateConnection();
            var channel = conn.CreateModel();
            return channel;
        }

     

    
        //public ResponseDetailsBase AckNowledge(ulong deleviryTag, IModel channel)
        //{
        //    var response = new ResponseDetailsBase();
        //    try
        //    {
        //        _logger.LogInformation("Pull Message From Service Bus Is Starting");
        //        ConnectionFactory connFactory = new ConnectionFactory
        //        {
        //            Uri = new Uri(amqbUrl.Replace("amqp://", "amqps://"))
        //        };

        //        channel.QueueDeclare(queue: "VehiclePing",
        //                                          durable: false,
        //                                          exclusive: false,
        //                                          autoDelete: false,
        //                                          arguments: null);
        //        channel.BasicAck(deleviryTag, true);
        //        _logger.LogInformation("Push Message To Service Bus Comleted");
        //        response.StatusCode = CommonEnums.ResponseStatusCode.Success;
        //        return response;

        //    }
        //    catch (Exception ex)
        //    {
        //        ///
        //        response = new ResponseDetailsBase(ex);
        //        return response;
        //    }
        //}
    }

    public static class ChannelExtension
    {
        public static void  DeclareQueue(this  IModel channel, string exchangeName, string queueName, string routingKey)
        {            
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
        }

        public static ResponseDetailsBase PushMessage<T>(this IModel channel, string exchange, string routingKey, T message)
        {
            var response = new ResponseDetailsBase();
            try
            {
               // _logger.LogInformation("Pull Message From Service Bus Is Starting");

                string serializedMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(serializedMessage);

                channel.BasicPublish(exchange: exchange,
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
                //_logger.LogInformation("Push Message To Service Bus Comleted");
                response.StatusCode = CommonEnums.ResponseStatusCode.Success;
                return response;

            }
            catch (Exception ex)
            {
                response = new ResponseDetailsBase(ex);
                return response;
            }
        }
        public static void Recieve<T>(this IModel channel,string queueName, Action<T> callBack)
        {
            var consumer = new EventingBasicConsumer(channel);
         
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                var command = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
                callBack(command);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            String consumerTag = channel.BasicConsume(queueName, false, consumer);
        }
        //public ResponseDetails<T, ulong> PullMessage<T>()
        //{
        //    var response = new ResponseDetails<T, ulong>();
        //    try
        //    {
        //        _logger.LogInformation("Pull Message From Service Bus Is Starting");

        //        var data = Channel.BasicGet(exchangeName, false);
        //        if (data == null)
        //        {
        //            _logger.LogInformation("No Messages Exist");
        //            response.StatusCode = CommonEnums.ResponseStatusCode.NotFound;
        //            return response;
        //        }
        //        var message = Encoding.UTF8.GetString(data.Body);
        //        var command = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
        //        response.DetailsObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
        //        response.StatusCode = CommonEnums.ResponseStatusCode.Success;
        //        response.SecondDetailsObject = data.DeliveryTag;
        //        _logger.LogInformation("Pull Message From Service Bus Is Completed");
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response = new ResponseDetails<T, ulong>(ex);
        //        return response;
        //    }
        //}
    }
}