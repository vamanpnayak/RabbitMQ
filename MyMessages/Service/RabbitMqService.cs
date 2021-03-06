﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace MyMessages.Service
{
    public class RabbitMqService
    {
        public IConnection GetRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "172.22.32.31",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };

            return connectionFactory.CreateConnection();
        }
    }
}
