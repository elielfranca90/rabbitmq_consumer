using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace rabbitMQ.consumer
{
    public class RabbitMQ
    {
        public static void BuildAndRunConsumerForProdClient(IModel channel, Product product)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(message);

                    Console.WriteLine("Product: " + product.Id + "|" + product.InternalCode + "|" + product.UserCNPJ + "|" + product.ProdDescription);

                    //Confirma que a mensagem foi recepcionada
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro de consumo: {ex.StackTrace}");

                    //Refaz o envio da mensagem para a queue ou não (true ou false)
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queue: "prodClient", autoAck: false, consumer: consumer);
        }

        public static void BuildAndRunConsumerForAcessHistory()
        {
            AcessHistory acessHistory = new AcessHistory();
            RBMQConnection rBMQConnection = new RBMQConnection();
            
            var factory = rBMQConnection.RMQ_NewConnection();

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    acessHistory = Newtonsoft.Json.JsonConvert.DeserializeObject<AcessHistory>(message);

                    Console.WriteLine("AcessHistory: " + acessHistory.CNPJ + "|" + acessHistory.AcessHistDate + "|" + acessHistory.AcessHistHour + "|" + acessHistory.AcessHistTotalProductsSent + "|" + acessHistory.AcessHistTotalProductsReceived);

                    //Confirma que a mensagem foi recepcionada
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro de consumo: {ex.StackTrace}");

                    //Refaz o envio da mensagem para a queue ou não (true ou false)
                    //channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queue: "histAcess", autoAck: true, consumer: consumer);
            
        }
    }
}
