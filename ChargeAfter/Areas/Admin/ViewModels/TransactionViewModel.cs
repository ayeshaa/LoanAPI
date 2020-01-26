using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChargeAfter.Models;

namespace ChargeAfter.Areas.Admin.ViewModels
{
    public class TransactionViewModel
    {
        public class Transactions
        {
            public string transactionId { get; set; }
            public string transactionStatus { get; set; }
            public string transactionAmount { get; set; }
            public string transactionType { get; set; }
            public string parentTransactionId { get; set; }

            public string transactionError { get; set; }

            public string transactionDate { get; set; }

           
        }
    
   
        public string laonId { get; set; }
        public string count { get; set; }

        public Transactions transactions { get; set; }


    
 }
}