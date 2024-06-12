﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Mango.MessageBus
{
    public class MessageBus : IMessageBus
    {

        private readonly string connectionString = @"Endpoint=sb://mdmangoweb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=f6dI7SKAIXdHITF8TqkOz0Db+loa3Yxm7+ASbKxmVt4=";


        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            await using var client=new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_Name);


            var jsonMessage=JsonConvert.SerializeObject(message);


            ServiceBusMessage finalMessage = new(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId=Guid.NewGuid().ToString()
            };


            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();



        }
    }
}
