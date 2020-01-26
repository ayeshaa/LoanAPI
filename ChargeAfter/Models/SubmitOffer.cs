using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.Models
{
    public class SubmitOffer
    {
        public string message { get; set; }
        public string applicationId { get; set; }
        public string loanId { get; set; }

        public string reason { get; set; }

        public string type { get; set; }

    }
}