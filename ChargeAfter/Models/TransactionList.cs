using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.Models
{
    public class TransactionList
    {
        public string laonId { get; set; }
        public string count { get; set; }

        public Transactions[] transactions { get; set; }


    }
    public class Transactions
    {
        public string transactionId { get; set; }
        public string transactionStatus { get; set; }
        public string transactionAmount { get; set; }
        public string transactionType   { get; set; }
        public string parentTransactionId { get; set; }

        public string transactionError { get; set; }

        public string transactionDate { get; set; }

    }
   
}