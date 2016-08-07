using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;

namespace RabbitPublisher.Bus
{
    public class AmqpMessagingService
    {
        private string _hostName = "172.22.32.31";
        private string _userName = "guest";
        private string _password = "guest";
        private string _exchangeName = "/";
       // private string _oneWayMessageQueueName = "OneWayMessageQueue";
       

        public IConnection GetRabbitMqConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = _hostName;
            connectionFactory.UserName = _userName;
            connectionFactory.Password = _password;

            return connectionFactory.CreateConnection();
        }

       
    }
}