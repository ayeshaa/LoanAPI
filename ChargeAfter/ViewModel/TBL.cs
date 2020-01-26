using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChargeAfter.Models;
using ChargeAfter.Models.ViewModel;

namespace ChargeAfter.ViewModel
{
    public class TBL
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string lender { get; set; }
        public DateTime DOB { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public Nullable<int> ReqID { get; set; }
        public long UserID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? rDate { get; set; }
        public string Status { get; set; }
        public string EmpStatus { get; set; }
        public string HousingStatus { get; set; }
        public string GrossIndividual { get; set; }
        public string NetIncom { get; set; }
        public string SalaryFrequency { get; set; }
        public long OffID { get; set; }

        public decimal ApprovedAmmount { get; set; }
        public Nullable<decimal> TotalPayback { get; set; }
        public Nullable<int> IncreasePercent { get; set; }
        public Nullable<decimal> MonthlyAmount { get; set; }
        public Nullable<int> MonthCount { get; set; }


        public string Price { get; set; }
        public int Id { get; set; }


        public String Lender { get; set; }
        //public String Amount { get; set; }
        public String Duration { get; set; }
        public String Intrest { get; set; }

        public String loanId { get; set; }
        public String offerId { get; set; }




        //Change by azee

        public Nullable<decimal> Ammount { get; set; }
        public string LoanId { get; set; }
        public Nullable<double> annualFee { get; set; }
        public Nullable<bool> eligibility { get; set; }
        public Nullable<bool> isPreQualifyOffer { get; set; }
        public string minApr { get; set; }
        public string offerExpirationDate { get; set; }
        public string pendingOffers { get; set; }
        public string status { get; set; }
        public Nullable<int> term { get; set; }
        public string legalDisclosure { get; set; }
        public string maxMonthlyPayment { get; set; }

    }
}