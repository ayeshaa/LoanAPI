using ChargeAfter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChargeAfter.ViewModel;
using ChargeAfter.Areas.Admin.ViewModels;
using ChargeAfter.Models.Service;
using System.Net;
using System.IO;

namespace ChargeAfter.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        ChargeAfterEntities db;
        public ActionResult Index()
        {
            using (db = new ChargeAfterEntities())
            {
                //var query = (from u in db.Users
                //             join r in db.Requests
                //             on u.Id equals r.UserID // into bases
                //             //from sb in bases.DefaultIfEmpty()
                //             join ro in db.RequestOffers
                //             on r.UserID equals ro.UserID //into ess
                //from es in ess.DefaultIfEmpty()
                var query = (from u in db.Users
                             from ua in db.UserAdresses.Where(c => c.UserId == u.Id).DefaultIfEmpty()
                             from r in db.Requests.Where(f => f.UserID == u.Id).DefaultIfEmpty()
                             from ro in db.RequestOffers.Where(comp => comp.UserID == u.Id).DefaultIfEmpty()
                             select new TBL
                             {
                                 Id = u.Id,
                                 //DOB = u.DOB.Value,
                                 Mobile = u.Mobile,
                                 Phone = u.Phone,
                                 FirstName = u.FirstName,
                                 //LastName = u.LastName,
                                 Email = u.Email,
                                 //Username = u.FirstName,
                                 lender = ro.Lender,
                                 //ReqID = (int)u.Id,
                                 Amount = ro.ApprovedAmmount,
                                 rDate = r.RequestDate ?? DateTime.Now,
                                 Status = r.RequestStatus

                             }).OrderByDescending(x => x.Id);

                return View(query.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            using (db = new ChargeAfterEntities())
            {
                var query = (from u in db.Users
                             from ua in db.UserAdresses.Where(c => c.UserId == u.Id).DefaultIfEmpty()
                             from r in db.Requests.Where(f => f.UserID == u.Id).DefaultIfEmpty()
                             from ro in db.RequestOffers.Where(comp => comp.UserID == u.Id).DefaultIfEmpty()
                             where u.Id == id
                             select new TBL
                             {
                                 Id = u.Id,
                                 //DOB = (DateTime?)u.DOB.Value ?? DateTime.Now,
                                 Mobile = u.Mobile,
                                 Phone = u.Phone,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 address = ua.Adress,
                                 city = ua.City,
                                 country = ua.Country,
                                 zip = ua.PostalCode,
                                 state = ua.State,
                                 Email = u.Email,
                                 Username = u.FirstName,
                                 lender = ro.Lender,
                                 EmpStatus = u.EmploymentStatus,
                                 HousingStatus = u.HousingStatus,
                                 GrossIndividual = u.GrossAnnualIndividual,
                                 NetIncom = u.NetMonthlyIncome,
                                 Amount = ro.ApprovedAmmount,
                                 Date = r.RequestDate.Value,
                                 Status = r.RequestStatus,
                                 SalaryFrequency = u.SalaryFrequency,

                                 annualFee = ro.annualFee,
                                 //ApprovedAmmount = (decimal)ro.ApprovedAmmount,
                                 eligibility = ro.eligibility,
                                 Intrest = ro.Intrest,
                                 IncreasePercent = ro.IncreasePercent,
                                 Duration = ro.Duration,
                                 isPreQualifyOffer = ro.isPreQualifyOffer,
                                 offerId = ro.OffID.ToString(),
                                 MonthCount = ro.MonthCount,
                                 loanId = ro.LoanId,
                                 term = ro.term,


                             }).FirstOrDefault();

                return Json(new { success = true, query }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Refund_Old()
        {

            List<TransactionViewModel> list = new OfferService().Refund();
            TransactionViewModel dummyData = new TransactionViewModel();
            if (list.Count == 0)
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
                temp.transactionStatus = "No Transaction Performed";
                dummyData.transactions = temp;

                list.Add(dummyData);
                return View(list);

            }




            return View(list);
        }

        public ActionResult Refund()
        {
            string endPoint = @"https://stage.mktplacegateway.com/gateway/v1/loans/pos/3029c28a-f296-44cf-b3ed-6a83617c74d0/transaction/submit";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            string authInfo = "cWo2ckxqSlFScHVrNzZwMTp5RWNHNlNXRVZ1bDcza1BaZzA3bzNhb0tTVFk2ZEc5OT0=";
            SubmitSaleResponse info = new SubmitSaleResponse();
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            httpWebRequest.Headers["PartnerKey"] = "testMerchantPartner123";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    String json = "{\"transactionAmount\" : \"500.0\",\"type\" : \"GOODS_SALE\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                String jsona = "";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    jsona = streamReader.ReadToEnd();
                    info = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SubmitSaleResponse>(jsona);
                }


                endPoint = @"https://stage.mktplacegateway.com/gateway/v1/loans/pos/3029c28a-f296-44cf-b3ed-6a83617c74d0/transaction/submit";

                httpWebRequest = (HttpWebRequest)WebRequest.Create(endPoint);
                authInfo = "cWo2ckxqSlFScHVrNzZwMTp5RWNHNlNXRVZ1bDcza1BaZzA3bzNhb0tTVFk2ZEc5OT0=";

                httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                httpWebRequest.Headers["PartnerKey"] = "testMerchantPartner123";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        String json = "{\"transactionAmount\" : \"500.0\",\"type\" : \"REFUND\",\"parentTransactionId\" : " + info.transactionId + "}";

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    jsona = "";
                    //SubmitSaleResponse info = new SubmitSaleResponse();
                    httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        jsona = streamReader.ReadToEnd();
                        info = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SubmitSaleResponse>(jsona);
                    }
                ViewBag.transactioniD = info.transactionId;

                return RedirectToAction("Success","User", new { area = "Admin" });
            }

                catch
                {

                }
               

               
                return View();
            }

        public ActionResult Success()
        {
            return View();
        }


    
    }
}