using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.Models
{
    public class SubmitRequest
    {
        public string loanId { get; set; }

        public string offerId { get; set; }
    }
}