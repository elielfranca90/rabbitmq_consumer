using RabbitMQ.Client;

namespace rabbitMQ.consumer
{
    public class RBMQConnection
    {
        /// <summary>
        /// Cria uma nova conexão com determinado servidor do rbMQ
        /// </summary>
        /// <returns></returns>
        public ConnectionFactory RMQ_NewConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = AmqpTcpEndpoint.UseDefaultPort, //:DefaultPort 5672
                UserName = "guest",
                Password = "guest"
            };

            return factory;
        }
    }
}
