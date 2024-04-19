using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _connectionFactory;

        public MessageBusService(IConfiguration configuration)
        {
            _connectionFactory = new ConnectionFactory
            {
                //HostName = configuration.GetConnectionString("")
                HostName = "localhost"

            };
        }


        public void Publish(string queue, byte[] message)
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                
                using(var chanel = connection.CreateModel())
                {
                    // Garantir que a fila esta criada
                    chanel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    // Publicar a mensagem
                    chanel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message
                        );
                }


            }

            //throw new NotImplementedException();
        }
    }
}
