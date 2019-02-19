using Helpers.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Common.Messaging.Queues
{
    public class MessagingQueueHandler : IMessagingQueueHandler
    {
        private readonly ILogger _logger;
        private readonly IConfiguration Configuration;
        private readonly string amqbUrl;
        private readonly string queueName;
        public MessagingQueueHandler(IConfiguration configuration, ILogger<MessagingQueueHandler> logger)
        {
            Configuration = configuration;
            var rabbitMqConfSection = Configuration.GetSection("RabbitMqServiceBus");
            amqbUrl = rabbitMqConfSection.GetValue<string>("AMQPURL");
            queueName = rabbitMqConfSection.GetValue<string>("QueueName");
            _logger = logger;
        }

        public IModel  CreateConnection ()
        {
            ConnectionFactory connFactory = new ConnectionFactory
            {
                Uri = new Uri(amqbUrl.Replace("amqp://", "amqps://"))
            };
            var conn = connFactory.CreateConnection();
            var channel = conn.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, null);
            return channel;
        }
        public ResponseDetails<T, ulong> PullMessage<T>(IModel channel)
        {
            var response = new ResponseDetails<T, ulong>();
            try
            {
                _logger.LogInformation("Pull Message From Service Bus Is Starting");
                     
                    var data = channel.BasicGet(queueName, false);
                    if (data == null)
                    {
                        _logger.LogInformation("No Messages Exist");
                        response.StatusCode = CommonEnums.ResponseStatusCode.NotFound;
                        return response;
                    }
                    var message = Encoding.UTF8.GetString(data.Body);
                    var command = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
                    response.DetailsObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
                    response.StatusCode = CommonEnums.ResponseStatusCode.Success;
                    response.SecondDetailsObject = data.DeliveryTag;
                    _logger.LogInformation("Pull Message From Service Bus Is Completed");
                    return response;
         

            }
            catch (Exception ex)
            {
                response = new ResponseDetails<T, ulong>(ex);
                return response;
            }
        }

        public ResponseDetailsBase PushMessage<T>(T message, IModel channel)
        {
            var response = new ResponseDetailsBase();
            try
            {
                _logger.LogInformation("Pull Message From Service Bus Is Starting");

                    string serializedMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(serializedMessage);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "VehiclePing",
                                         basicProperties: null,
                                         body: body);
                    _logger.LogInformation("Push Message To Service Bus Comleted");
                    response.StatusCode = CommonEnums.ResponseStatusCode.Success;               
                return response;
         
            }
            catch (Exception ex)
            {
                response = new ResponseDetailsBase(ex);
                return response;
            }
        }

        public ResponseDetailsBase AckNowledge(ulong deleviryTag, IModel channel)
        {
            var response = new ResponseDetailsBase();
            try
            {
                _logger.LogInformation("Pull Message From Service Bus Is Starting");
                ConnectionFactory connFactory = new ConnectionFactory
                {
                    Uri = new Uri(amqbUrl.Replace("amqp://", "amqps://"))
                };
        
                    channel.QueueDeclare(queue: "VehiclePing",
                                                      durable: false,
                                                      exclusive: false,
                                                      autoDelete: false,
                                                      arguments: null);
                    channel.BasicAck(deleviryTag, true);
                    _logger.LogInformation("Push Message To Service Bus Comleted");
                    response.StatusCode = CommonEnums.ResponseStatusCode.Success;
                    return response;
          
            }
            catch (Exception ex)
            {  
                ///
                response = new ResponseDetailsBase(ex);
                return response;
            }
        }
    }
}