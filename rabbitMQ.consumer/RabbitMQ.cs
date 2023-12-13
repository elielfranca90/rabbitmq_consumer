using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace rabbitMQ.consumer
{
    public class RabbitMQ
    {
        public void ConsumeQueue_ProdClient()
        {
            try
            {
                RBMQConnection rBMQConnection = new RBMQConnection();

                var product = new Product();
                var factory = rBMQConnection.RMQ_NewConnection();

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    #region EXCHANGE/QUEUE DE DEAD LETTERS

                    //channel.ExchangeDeclare("DeadLetterExchange", ExchangeType.Fanout);
                    //channel.QueueDeclare("DeadLetterQueue", true, false, false, null);
                    //channel.QueueBind("DeadLetterQueue", "DeadLetterExchange", "");

                    //var arguments = new Dictionary<string, object>()
                    //{
                    //    {"x-dead-letter-exchange", "DeadLetterExchange" }
                    //};

                    #endregion

                    channel.QueueDeclare(queue: "prodClient",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false);
                    //arguments: arguments);

                    //channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    Console.WriteLine("Aguardando mensagem...");

                    BuildAndRunConsumerForProdClient(channel, product);

                    //Console.WriteLine(" Press [enter] to exit.");
                    //Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }

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

        public void ConsumeQueue_HistAcess()
        {
            try
            {
                RBMQConnection rBMQConnection = new RBMQConnection();

                var product = new Product();
                //var acessHistory = new AcessHistory();

                var factory = rBMQConnection.RMQ_NewConnection();

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    #region EXCHANGE/QUEUE DE DEAD LETTERS

                    //channel.ExchangeDeclare("DeadLetterExchange", ExchangeType.Fanout);
                    //channel.QueueDeclare("DeadLetterQueue", true, false, false, null);
                    //channel.QueueBind("DeadLetterQueue", "DeadLetterExchange", "");

                    //var arguments = new Dictionary<string, object>()
                    //{
                    //    {"x-dead-letter-exchange", "DeadLetterExchange" }
                    //};

                    #endregion

                    channel.QueueDeclare(queue: "histAcess",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false);
                    //arguments: arguments);

                    //channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    Console.WriteLine("Aguardando mensagem...");
                    BuildAndRunConsumerForAcessHistory(channel, new AcessHistory("39104545850",
                                                                                acessHistDate: DateTime.Now.ToShortDateString(), 
                                                                                acessHistHour: DateTime.Now.ToShortTimeString(), 
                                                                                acessHistTotalProductsSent: 1, 
                                                                                acessHistTotalProductsReceived: 1));

                    //Console.WriteLine(" Press [enter] to exit.");
                    //Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }

        public static void BuildAndRunConsumerForAcessHistory(IModel channel, AcessHistory acessHistory)
        {
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
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queue: "histAcess", autoAck: false, consumer: consumer);
        }
    }
}
