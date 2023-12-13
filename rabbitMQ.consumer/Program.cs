using System.Threading;

namespace rabbitMQ.consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RabbitMQ mQ = new RabbitMQ();

            while (true)
            {
                //mQ.ConsumeQueue_ProdClient();

                mQ.ConsumeQueue_HistAcess();

                Thread.Sleep(1500);

                System.Console.WriteLine("Estou girando !!!!");
            }
        }
    }
}
