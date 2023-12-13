using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbitMQ.consumer
{
    public class AcessHistory
    {
        public AcessHistory(string cNPJ, string acessHistDate, string acessHistHour, int acessHistTotalProductsSent, int acessHistTotalProductsReceived)
        {
            CNPJ = cNPJ;
            AcessHistDate = acessHistDate;
            AcessHistHour = acessHistHour;
            AcessHistTotalProductsSent = acessHistTotalProductsSent;
            AcessHistTotalProductsReceived = acessHistTotalProductsReceived;
        }

        public string CNPJ { get; set; }

        public string AcessHistDate { get; set; }

        public string AcessHistHour { get; internal set; }

        public int AcessHistTotalProductsSent { get; set; }

        public int AcessHistTotalProductsReceived { get; set; }

    }
}
