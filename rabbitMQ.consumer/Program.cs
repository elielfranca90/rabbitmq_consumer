using System.Threading;

namespace rabbitMQ.consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                RabbitMQ.BuildAndRunConsumerForAcessHistory();

                Thread.Sleep(1000);
            }
        }
    }
}
