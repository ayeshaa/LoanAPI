using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.Models.ViewModel
{
    public class OfferViewModel
    {
        public String Lender { get; set; }
        public String Amount { get; set; }
        public String Duration { get; set; }
        public String Intrest { get; set; }

        public String loanId { get; set; }
        public String offerId { get; set; }



        //new Offer Fields

        public object offerLastClickedOn { get; set; }
        public string originatorName { get; set; }
        public string redirectUrl { get; set; }
        public bool eligibility { get; set; }
        public object maxLoanAmount { get; set; }
        public bool isPreQualifyOffer { get; set; }
        public object minApr { get; set; }
        public object type { get; set; }
        public Products products { get; set; }
        public object maxApr { get; set; }
        public string dateCreated { get; set; }
        public string portalName { get; set; }
        public object monthlyPayment { get; set; }
        public Extra extra { get; set; }
        public string portalId { get; set; }
        public object maxMonthlyPayment { get; set; }
        public object selectedForFlag { get; set; }
        public object customMessage { get; set; }
        public int term { get; set; }
        public string originatorId { get; set; }
        public object declineReasons { get; set; }
        public object productType { get; set; }
        public object offerFirstClickedOn { get; set; }
        public string portalTrackingId { get; set; }
        public object pendingOffers { get; set; }
        public float interestRate { get; set; }
        public object maxTerm { get; set; }
        public float apr { get; set; }
        public object offerExpirationDate { get; set; }
        public float annualFee { get; set; }
        public float loanAmount { get; set; }
        public object gracePeriodForRefinancedLoan { get; set; }
        public object subProductType { get; set; }
        public object[] offerClickTimeStamp { get; set; }
        public object legalDisclosure { get; set; }
        public object minTerm { get; set; }
        //public string offerId { get; set; }
        public bool duplicateFlag { get; set; }
        public object originationFee { get; set; }
        public object originationFeeAmount { get; set; }
        public Portalinformation portalInformation { get; set; }
        public Errorreasons errorReasons { get; set; }
        public string status { get; set; }
        public object duplicateInitLoanId { get; set; }
    }
    public class Products
    {
    }

    public class Extra
    {
        public object budgetPayInd { get; set; }
        public string latePaymentFee { get; set; }
        public string returnedPaymentFee { get; set; }
        public object color { get; set; }
        public string applicationUID { get; set; }
        public object manualUnderwriting { get; set; }
        public Documentcontent[] documentContents { get; set; }
        public float minPaymentAmt { get; set; }
        public string accountId { get; set; }
        public string minPaymentPct { get; set; }
        public int introDuration { get; set; }
        public object transactionFee { get; set; }
        public object paidInStoreAmount { get; set; }
        public int offerId { get; set; }
        public object penaltyAPR { get; set; }
    }

    public class Documentcontent
    {
        public string documentCode { get; set; }
        public string documentImageHtml { get; set; }
        public string documentName { get; set; }
    }

    public class Portalinformation
    {
        public object minimumTerm { get; set; }
        public object minimumInterestRate { get; set; }
        public object maximumInterestRate { get; set; }
        public object approvalTime { get; set; }
        public string logoUrl { get; set; }
        public object minimumLoanAmount { get; set; }
        public object minimumOriginationFee { get; set; }
        public Disclaimerinformation disclaimerInformation { get; set; }
        public object maximumApr { get; set; }
        public string[] moreInformation { get; set; }
        public object maximumLoanAmount { get; set; }
        public object maximumOriginationFee { get; set; }
        public object maximumTerm { get; set; }
        public string contactNumber { get; set; }
        public object minimumApr { get; set; }
    }

    public class Disclaimerinformation
    {
    }

    public class Errorreasons
    {
    }
   

}