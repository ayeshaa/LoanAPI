using ChargeAfter.Areas.Admin.ViewModels;
using ChargeAfter.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ChargeAfter.Models.Service
{
    public class OfferService
    {
        private LoanInfo loaninfo;
        public List<OfferViewModel> GetOffers(String LoadnAmount, string grossIncome, Applicant applicant)
        {
            string endPoint = @"https://stage.mktplacegateway.com/gateway/v1/loans/publish";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            string authInfo = "cWo2ckxqSlFScHVrNzZwMTp5RWNHNlNXRVZ1bDcza1BaZzA3bzNhb0tTVFk2ZEc5OT0=";

            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            httpWebRequest.Headers["PartnerKey"] = "testMerchantPartner123";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    OfferRequest offers = new OfferRequest();
                    offers.loanCategory = "CONSUMER_POS";
                    offers.loanType = "POINT_OF_SALE";
                    offers.isPosLoan = true;
                    offers.loanAmount = LoadnAmount;
                    Consumerloan consumerLoan = new Consumerloan();
                    consumerLoan.grossIncome = grossIncome;
                    offers.consumerLoan = consumerLoan;

                    applicant.firstName = "MIMIC";
                    applicant.lastName = "APPROVE";

                    offers.applicant = applicant;
                    String json = new JavaScriptSerializer().Serialize(offers);
                    
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var file = @"F:/ChargeAfter/APICode/ChargeAfter-API_Refund/ChargeAfter/ChargeAfter/LogFile/APIRequestLog.txt";
                using (StreamWriter writer = new FileInfo(file).AppendText())
                {
                    writer.WriteLine("------ API Call Request------");
                    writer.WriteLine("-----------------------------");
                    OfferRequest offers = new OfferRequest();
                    offers.loanCategory = "CONSUMER_POS";
                    offers.loanType = "POINT_OF_SALE";
                    offers.isPosLoan = true;
                    offers.loanAmount = LoadnAmount;
                    Consumerloan consumerLoan = new Consumerloan();
                    consumerLoan.grossIncome = grossIncome;
                    offers.consumerLoan = consumerLoan;
                    applicant.firstName = "MIMIC";
                    applicant.lastName = "APPROVE";

                    offers.applicant = applicant;
                    String data = new JavaScriptSerializer().Serialize(offers);
                    writer.WriteLine(data);
                    writer.WriteLine("-----------------------------");
                    writer.WriteLine(Environment.NewLine);


                }

                String jsona = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    jsona = streamReader.ReadToEnd();
                    loaninfo = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LoanInfo>(jsona);
                }

            }
            catch (Exception e)
            {

            }



            string html = string.Empty;

           //string url = @"https://stage.mktplacegateway.com/gateway/v1/loans/" + loaninfo.loanId.ToString() + "/offer";
            string url = "https://stage.mktplacegateway.com/gateway/v1/loans/4430bb2e-2326-4f57-a363-3542a741ff85/offer";
            //string url = "https://stage.mktplacegateway.com/gateway/v1/loans/0b9595cc-ff55-4545-9212-89a6f2f72ce1/offer";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Headers["Authorization"] = "Basic " + authInfo;
            request.Headers["PartnerKey"] = "testMerchantPartner123";
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            var path = "F:/ChargeAfter/APICode/ChargeAfter-API_Refund/ChargeAfter/ChargeAfter/LogFile/APIRequestLog.txt";
            using (StreamWriter writer = new FileInfo(path).AppendText())
            {
                writer.WriteLine("------ API Call Response------");
                writer.WriteLine("-----------------------------");

                String data = new JavaScriptSerializer().Serialize(html);
                writer.WriteLine(data); 
                writer.WriteLine("-----------------------------");
                writer.WriteLine(Environment.NewLine);
                writer.WriteLine(Environment.NewLine);
                writer.WriteLine(Environment.NewLine);
                writer.WriteLine(Environment.NewLine);
            }
            Rootobject Rootobject = new Rootobject();
            Offer offer = new Offer();
            List<OfferViewModel> viewmodellist = new List<OfferViewModel>();
            try
            {
                Rootobject = new JavaScriptSerializer().Deserialize<Rootobject>(html);

            }
            catch (Exception ex)
            {


                OfferViewModel viewmodel = new OfferViewModel();
                viewmodel.Lender = "No Lender Avaialable";
                viewmodel.Amount = "Sorry Your Loan Ammount is not Accepted";
                viewmodel.Duration = " ";
                viewmodel.Intrest = " ";
                ////viewmodel.loanId = "f8c5774a-d532-49eb-8cac-c5cdb5a6b52c";
                viewmodel.loanId = "0";
                viewmodel.offerId = "0";
                //viewmodel.monthlyPayment = "0";
                viewmodellist.Add(viewmodel);

                //viewmodel.loanAmount = 0;
                //viewmodel.annualFee = 0;
                //viewmodel.eligibility = false;
                //viewmodel.isPreQualifyOffer = false;
                ////viewmodel.minApr = ;
                ////viewmodel.offerExpirationDate = ;
                ////viewmodel.pendingOffers = ;
                //viewmodel.status = "No Offer";
                //viewmodel.term = 0;
                //viewmodel.dateCreated = " ";
                ////viewmodel.declineReasons = ;

                //viewmodel.apr = 0;
                ////viewmodel.monthlyPayment = item.monthlyPayment;
                //viewmodel.term = 0;


                return viewmodellist;
            }



            if (Rootobject.offers.Count() == 0)
            {
                OfferViewModel viewmodels = new OfferViewModel();
                viewmodels.Lender = "No Lender Avaialable";
                viewmodels.Amount = "Sorry Your Loan Ammount is not Accepted";
                viewmodels.Duration = " ";
                viewmodels.Intrest = " ";
                ////viewmodel.loanId = "f8c5774a-d532-49eb-8cac-c5cdb5a6b52c";
                viewmodels.loanId = "0";
                viewmodels.offerId = "0";
                //viewmodel.monthlyPayment = "0";
                viewmodellist.Add(viewmodels);

                return viewmodellist;
            }
            foreach (var item in Rootobject.offers)
            {

               
                
                OfferViewModel viewmodel = new OfferViewModel();
                viewmodel.Lender = "Fortiva";
                viewmodel.Amount = Rootobject.loanAmount.ToString();
                viewmodel.Duration = item.term.ToString();
                viewmodel.Intrest = item.interestRate.ToString();
                //viewmodel.loanId = "f8c5774a-d532-49eb-8cac-c5cdb5a6b52c";
                viewmodel.loanId = loaninfo.loanId;
                viewmodel.offerId = item.offerId.ToString();



                //new Fields
                viewmodel.loanAmount = item.loanAmount;
                viewmodel.annualFee = item.annualFee;
                viewmodel.eligibility = item.eligibility;
                viewmodel.isPreQualifyOffer = item.isPreQualifyOffer;
                viewmodel.minApr = item.minApr;
                viewmodel.offerExpirationDate = item.offerExpirationDate;
                viewmodel.pendingOffers = item.pendingOffers;
                viewmodel.status = item.status;
                viewmodel.term = item.term;
                viewmodel.dateCreated = item.dateCreated;
                viewmodel.declineReasons = item.declineReasons;

                viewmodel.apr = item.apr;
                viewmodel.monthlyPayment = item.monthlyPayment;
                viewmodel.term = item.term;
              
                //extra
                //    viewmodel.extra.accountId = item.extra.accountId.ToString();
                //viewmodel.extra.applicationUID = item.extra.applicationUID.ToString();

                //viewmodel.extra.latePaymentFee = item.extra.latePaymentFee.ToString();
                //viewmodel.extra.offerId = item.extra.offerId;
                //viewmodel.extra.penaltyAPR = item.extra.penaltyAPR.ToString();
                //viewmodel.extra.manualUnderwriting = item.extra.manualUnderwriting.ToString();
                //viewmodel.extra.transactionFee = item.extra.transactionFee.ToString();


                //viewmodel.legalDisclosure = item.legalDisclosure;
                //viewmodel.maxMonthlyPayment = item.maxMonthlyPayment;

                viewmodel.legalDisclosure = item.legalDisclosure;
                viewmodel.maxMonthlyPayment = item.maxMonthlyPayment;



                viewmodellist.Add(viewmodel);

                


            }


            return viewmodellist;
        }

      


        public SubmitOffer ConfirmOffer(string LoanID, String OfferID)
        {
            string endPoint = @"https://stage.mktplacegateway.com/gateway/v1/loans/pos/apply";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            string authInfo = "cWo2ckxqSlFScHVrNzZwMTp5RWNHNlNXRVZ1bDcza1BaZzA3bzNhb0tTVFk2ZEc5OT0=";
            String jsona = "";
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            httpWebRequest.Headers["PartnerKey"] = "testMerchantPartner123";
            httpWebRequest.Headers["Location-Key"] = "SUPbranch_new";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            try
            {
                string json = "";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //string json = "{\"loanId\" : \"3029c28a-f296-44cf-b3ed-6a83617c74d0\" ,";
                    //json += "\"offerId\" : \"75ed2b01-fc37-46ae-a2d9-b23d9e6c916e\"}";

                    SubmitRequest req = new SubmitRequest();
                    req.loanId = LoanID;
                    req.offerId = OfferID;

                    json = new JavaScriptSerializer().Serialize(req);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    jsona = streamReader.ReadToEnd();
                }

            }
            catch (Exception e)
            {

            }

            SubmitOffer Rootobject = new JavaScriptSerializer().Deserialize<SubmitOffer>(jsona);




            return Rootobject;
        }
        public List<TransactionViewModel> Refund()
        {
            string jsonS = " ";
            string LoanID = "3029c28a-f296-44cf-b3ed-6a83617c74d0";


            string authInfo = "cWo2ckxqSlFScHVrNzZwMTp5RWNHNlNXRVZ1bDcza1BaZzA3bzNhb0tTVFk2ZEc5OT0=";
            //string endPoint = @"https://stage.mktplacegateway.com/gateway/v1/pos/"+LoanID+"/transaction/submit";

            string url = @"https://stage.mktplacegateway.com/gateway/v1/loans/pos/827ef3e8-4f83-4d58-a7de-14cee4cefb34/transactions";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Headers["Authorization"] = "Basic " + authInfo;
            request.Headers["PartnerKey"] = "testMerchantPartner123";
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonS = reader.ReadToEnd();
            }
            TransactionList Transactions = new TransactionList();





            try
            {
                Transactions = new JavaScriptSerializer().Deserialize<TransactionList>(jsonS);

            }
            catch (Exception ex)
            {

            }

            List<TransactionViewModel> trans = new List<TransactionViewModel>();
            TransactionViewModel dummyData = new TransactionViewModel();
            foreach (var item in trans)
            {
                if (item.count == "0")
                {

                    dummyData.count = "0";
                    dummyData.laonId = "0";

                    TransactionViewModel.Transactions temp = new TransactionViewModel.Transactions();
                    temp.parentTransactionId = "0";
                    temp.transactionAmount = "0";
                    temp.transactionDate = "No date found";
                    temp.transactionType = "No TYPE";
                    temp.transactionId = "No Id";
                    temp.transactionError = "No Transaction Found";
                    dummyData.transactions = temp;

                    trans.Add(dummyData);
                    return trans;
                }



            }




            return trans;
        }

    }
}




