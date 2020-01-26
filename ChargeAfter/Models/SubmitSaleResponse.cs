using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.Models
{
    public class SubmitSaleResponse
    {
        public string message { get; set; }
        public string applicationId { get; set; }
        public string loanId { get; set; }
        public string status { get; set; }
        public string transactionId { get; set; }
    }

}