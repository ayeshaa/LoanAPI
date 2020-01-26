using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.Models
{
    public class OfferRequest
    {
        public string loanCategory { get; set; }
        public string loanType { get; set; }
        public bool isPosLoan { get; set; }
        //public string desiredLoanMonths { get; set; }
        public Consumerloan consumerLoan { get; set; }
        public string loanAmount { get; set; }
        public Applicant applicant { get; set; }
    }

    public class Consumerloan
    {
        //public string employer { get; set; }
        public string grossIncome { get; set; }
        //public string monthlyNetIncome { get; set; }
    }

    public class Applicant
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string identificationNumber { get; set; }
        //public string selfReportedCreditScore { get; set; }
        public string housingType { get; set; }
        //public string annualGrossIncome { get; set; }
        public string employmentStatus { get; set; }

        public Address address { get; set; }
        public string mobile { get; set; }
        //public string title { get; set; }
        public string email { get; set; }
        public string salaryFrequency { get; set; }

        
    }

    public class Address
    {
        public string zipCode { get; set; }
        public string city { get; set; }
        public string street1 { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }


}