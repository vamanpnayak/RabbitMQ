using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyNetQ;
using RabbitMQ.Client;

namespace RabbitPublisher.Bus
{
    public class AmqpMessagingService
    {
        private string _hostName = "";
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


        public IAdvancedBus GetRabbitMqBus()
        {
            //return RabbitHutch.CreateBus(";username=guest;password=guest").Advanced;
            var bus = RabbitHutch.CreateBus("host=spotted-monkey.rmq.cloudamqp.com;virtualHost=xswwhewh;username=xswwhewh;password=");
            return bus.Advanced;
        }

       
    }
}