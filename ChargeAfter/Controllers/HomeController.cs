using ChargeAfter.Models;
using ChargeAfter.Models.Service;
using ChargeAfter.Models.ViewModel;
using ChargeAfter.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ChargeAfter.Controllers
{
    public class HomeController : Controller
    {
        ChargeAfterEntities db;

        private HttpContextBase Context { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult Agreement()
        {
            return View();
        }
        public ActionResult Computer()
        {
            return View();
        }
        public ActionResult Laptop()
        {
            return View();
        }
        public ActionResult GamingEditing()
        {
            using (db = new ChargeAfterEntities())
            {
                var product = (from p in db.Products where p.Name.Contains("Dell 15.6") select p).FirstOrDefault();
                return View(product);
            }
        }


        public ActionResult Product()
        {
            using (db = new ChargeAfterEntities())
            {
                var product = (from p in db.Products select p).ToList();
                return View(product);
            }
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult BeginCheckout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            using (db = new ChargeAfterEntities())
            {
                try
                {
                    ViewData["Awesome"] = "Awesome";
                    Session["Name"] = form["firstName"] + " " + form["lastName"];
                    Session["email"] = form["email"];
                    Session["phone"] = form["phone"];
                    Session["country"] = form["country"];
                    Session["address"] = form["address1"] + form["address2"] + form["address3"];
                    Session["City"] = form["city"];
                    Session["state"] = form["state"];
                    Session["Zip"] = form["zip"];
                    TempData["Name"] = form["firstName"] + " " + form["lastName"];
                    TempData["phone"] = form["phone"];
                    TempData["address"] = form["address1"] + form["address2"] + form["address3"];
                    TempData["city"] = form["city"];
                    TempData["state"] = form["state"];
                    TempData["Zip"] = form["zip"];
                    TempData["country"] = form["country"];
                    return View("ConfirmShipping");
                }
                catch (Exception ex)
                {
                    ViewData["Awesome"] = "Oops " + ex.Message;
                    return View();

                }


            }
        }
        public ActionResult ConfirmShipping()
        {
            using (db = new ChargeAfterEntities())
            {
                TempData["name"] = TempData["name"];
                TempData["address"] = TempData["address"];
                TempData["city"] = TempData["city"];
                TempData["state"] = TempData["state"];
                TempData["Zip"] = TempData["Zip"];
                TempData["country"] = TempData["country"];
                return View();
            }
        }
        public ActionResult Payment()
        {
            using (db = new ChargeAfterEntities())
            {
                TempData["name"] = Session["Name"];
                TempData["email"] = Session["email"];
                TempData["phone"] = Session["phone"];
                return View();
            }
        }
        [HttpPost]
        public ActionResult Payment(FormCollection form)
        {
            using (db = new ChargeAfterEntities())
            {
                Session["chargeAName"] = form["name"];
                Session["ChargAmobile"] = form["mobile"];
                Session["ChargAemail"] = form["email"];
                Session["ChargADOB"] = Convert.ToDateTime(form["dob"]);
                Session["ChargASSN"] = form["ssn"];

            }
            return RedirectToAction("ConfirmPayment");
        }
        public ActionResult ConfirmPayment()
        {
            using (db = new ChargeAfterEntities())
            {
                TempData["name"] = Session["chargeAName"];
                TempData["mobile"] = Session["ChargAmobile"];
                TempData["email"] = Session["ChargAemail"];
                TempData["DOB"] = Session["ChargADOB"];
                TempData["SSN"] = Session["ChargASSN"];
                return View();
            }
        }



        int tempID;

        [HttpPost]
        public ActionResult ConfirmPayment(FormCollection form)
        {
            

            var items = (List<Item>)Session["cart"];
            float subTot = 0;
            float total = 0;
            if (items != null)
            {
                foreach (Item itemPro in (List<Item>)Session["cart"])
                {
                    float price = Convert.ToSingle(itemPro.Pro.Price) * 1;
                    subTot = subTot + price;
                }
                total = subTot + 100;
            }
            decimal amount = Math.Round((decimal)total, 0);


            try
            {
                Applicant applicant = new Applicant();
                Address address = new Address();
                //var loanAmount = form["loanAmmount"];

                applicant.firstName = form["name"];
                applicant.lastName = form["name"];

                string tempDate = (form["DOB"]);
                string createddate = Convert.ToDateTime(tempDate).ToString("yyyy-MM-dd");
                applicant.dateOfBirth = createddate;
                applicant.mobile = form["mobile"];
                applicant.email = form["email"];
                applicant.identificationNumber = form["ssn"];
                if (form["housingType"] != null)
                {
                    applicant.housingType = form["housingType"];
                }
                else
                {
                    applicant.housingType = "";
                }
                applicant.salaryFrequency = form["salaryFrequency"];
                applicant.employmentStatus = form["employmentStatus"];


                address.street1 = Session["address"].ToString();
                address.city = Session["City"].ToString();
                address.state = Session["state"].ToString();
                address.zipCode = Session["Zip"].ToString();
                address.country = Session["country"].ToString();

                applicant.address = address;

                var grossIncome = form["grossIncome"];




                TempData["matches"] = new OfferService().GetOffers(amount.ToString(), grossIncome, applicant);


                //   IEnumerable<OfferViewModel> ResponceOffer = new OfferService().GetOffers(loanAmount, grossIncome, applicant);



                if (TempData["matches"] != null)
                {

                    using (db = new ChargeAfterEntities())
                    {
                        var user = new User();
                        var userAddress = new UserAdress();
                        var req = new Request();
                        var name = form["name"];
                        var email = form["email"];
                        user.FirstName = form["name"];
                        user.Mobile = form["mobile"];
                        user.Email = form["email"];
                        user.DOB = Convert.ToDateTime(form["DOB"]);
                        user.EstimatedCreditRange = form["CreditScore"];
                        user.EmploymentStatus = form["employmentStatus"];
                        user.HousingStatus = form["housingType"];
                        user.GrossAnnualIndividual = form["grossIncome"];
                        user.NetMonthlyIncome = form["netMonthly"];
                        user.SalaryFrequency = form["salaryFrequency"];




                        db.Users.Add(user);
                        db.SaveChanges();

                        userAddress.UserId = user.Id;
                        tempID = user.Id;
                        Session["id"] = user.Id;
                        userAddress.Country = Session["country"].ToString();
                        userAddress.Adress = Session["address"].ToString();
                        userAddress.City = Session["City"].ToString();
                        userAddress.State = Session["state"].ToString();
                        userAddress.PostalCode = Session["Zip"].ToString();
                        db.UserAdresses.Add(userAddress);



                        req.UserID = user.Id;
                        req.RequestDate = DateTime.Now;
                        req.RequestAmount = amount;
                        req.RequestStatus = "Pending";
                        TempData["id"] = user.Id;
                        db.Requests.Add(req);
                        db.SaveChanges();

                        ViewData["message"] = "Sucessful";

                    }
                    using (db = new ChargeAfterEntities())
                    {
                        var offer = new RequestOffer();
                        var item = (List<Item>)Session["cart"];
                        float subTots = 0;
                        float pay = 0;
                        decimal approvedAmount = 0;
                        if (item != null)
                        {
                            foreach (Item itemPro in (List<Item>)Session["cart"])
                            {
                                float price = Convert.ToSingle(@itemPro.Pro.Price) * 1;
                                subTots = subTots + price;
                                Session["Subtotal"] = subTots;

                            }

                            pay = Convert.ToSingle(100 + subTots);
                            approvedAmount = Math.Round((decimal)pay, 0);
                        }
                        decimal monthly = approvedAmount / 12;
                        decimal monthlyPay = Math.Round((decimal)monthly, 0);


                        IEnumerable<OfferViewModel> offered = (IEnumerable<OfferViewModel>)TempData["matches"];


                        foreach (var itms in offered)
                        {
                            if (offered.Count() == 0)
                            {
                                offer.Lender = "No Lender Avaialable";
                                offer.Ammount = 0;
                                offer.Duration = " ";
                                offer.Intrest = " ";
                                ////viewmodel.loanId = "f8c5774a-d532-49eb-8cac-c5cdb5a6b52c";
                                offer.LoanId = "0";
                                offer.OffID = 0;
                            }

                            offer.UserID = long.Parse(tempID.ToString());
                            offer.TotalPayback = itms.term;
                            offer.IncreasePercent = int.Parse(itms.interestRate.ToString());
                            offer.ApprovedAmmount = decimal.Parse(itms.loanAmount.ToString());
                            try
                            {
                                offer.MonthlyAmount = decimal.Parse(itms.monthlyPayment.ToString());
                            }


                            catch (Exception ex)
                            {
                                offer.MonthlyAmount = 0;

                            }
                            offer.annualFee = itms.annualFee;
                            offer.Duration = itms.Duration;
                            offer.eligibility = itms.eligibility;
                            offer.Intrest = itms.Intrest;
                            offer.minApr = itms.apr.ToString();
                            offer.isPreQualifyOffer = itms.isPreQualifyOffer;
                            //offer.legalDisclosure = itms.legalDisclosure.ToString();
                            offer.LoanId = itms.loanId;
                            //offer.maxMonthlyPayment = itms.maxMonthlyPayment.ToString();
                            //offer.minApr = itms.minApr.ToString();
                            offer.MonthCount = itms.term;
                            //offer.pendingOffers = itms.pendingOffers.ToString();
                            offer.status = itms.status;
                            offer.term = itms.term;
                            offer.Lender = itms.Lender;


                            db.RequestOffers.Add(offer);
                            db.SaveChanges();
                        }
                        return RedirectToAction("Offers");
                    }




                }
                else return View();
            }
            catch (Exception ex)
            {
                ViewData["message"] = "Sorry " + ex.Message;
                return View();
            }

        }
        public ActionResult Approved()
        {
            using (db = new ChargeAfterEntities())
            {
                var reqID = "f267e96e-49e5-4c59-9f18-d39f4af27c1a";
                if(TempData["ConfirmOffer"] != null)
                {
                    reqID = (TempData["ConfirmOffer"]).ToString();
                }
                var ConfirmOffer = (from r in db.RequestOffers where r.LoanId == reqID select r).FirstOrDefault();
                return View(ConfirmOffer);
            }
        }
        public ActionResult Offers()
        {

            TempData["id"] = Session["id"];
            TempData["Uemail"] = Session["id"];
            TempData["matches1"] = TempData["matches"];
            return View(TempData["matches"]);
        }

        public ActionResult OfferFortiva(int id)
        {
            using (db = new ChargeAfterEntities())
            {
                var offer = new RequestOffer();
                var item = (List<Item>)Session["cart"];
                float subTot = 0;
                float pay = 0;
                decimal approvedAmount = 0;
                if (item != null)
                {
                    foreach (Item itemPro in (List<Item>)Session["cart"])
                    {
                        float price = Convert.ToSingle(@itemPro.Pro.Price) * 1;
                        subTot = subTot + price;
                        Session["Subtotal"] = subTot;

                    }

                    pay = Convert.ToSingle(100 + subTot);
                    approvedAmount = Math.Round((decimal)pay, 0);
                }
                decimal monthly = approvedAmount / 6;
                decimal monthlyPay = Math.Round((decimal)monthly, 0);
                // var Oid = user
                offer.UserID = id;

                offer.TotalPayback = 6;
                offer.IncreasePercent = 0;
                offer.ApprovedAmmount = approvedAmount;
                offer.MonthlyAmount = monthlyPay;
                offer.Lender = "Fortiva";
                db.RequestOffers.Add(offer);
                var req = (from r in db.Requests where r.UserID == id select r).FirstOrDefault();
                long reqID = req.ReqID;
                Request model = db.Requests.Find(reqID);
                model.RequestStatus = "Authorized";
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            TempData["PayBack"] = "6";
            Session["PayBack"] = "6";
            return RedirectToAction("Approved");
        }
        public ActionResult OfferProsper(int id)
        {
            using (db = new ChargeAfterEntities())
            {
                var offer = new RequestOffer();
                var item = (List<Item>)Session["cart"];
                float subTot = 0;
                float pay = 0;
                decimal approvedAmount = 0;
                if (item != null)
                {
                    foreach (Item itemPro in (List<Item>)Session["cart"])
                    {
                        float price = Convert.ToSingle(@itemPro.Pro.Price) * 1;
                        subTot = subTot + price;
                        Session["Subtotal"] = subTot;

                    }

                    pay = Convert.ToSingle(100 + subTot);
                    approvedAmount = Math.Round((decimal)pay, 0);
                }
                decimal monthly = approvedAmount / 12;
                decimal monthlyPay = Math.Round((decimal)monthly, 0);
                offer.UserID = id;
                offer.TotalPayback = 12;
                offer.IncreasePercent = 0;
                offer.ApprovedAmmount = approvedAmount;
                offer.MonthlyAmount = monthlyPay;
                offer.Lender = "Prosper";
                db.RequestOffers.Add(offer);
                var req = (from r in db.Requests where r.UserID == id select r).FirstOrDefault();
                long reqID = req.ReqID;
                Request model = db.Requests.Find(reqID);
                model.RequestStatus = "Authorized";
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["PayBack"] = null;
                Session["PayBack"] = null;
                return RedirectToAction("Approved");
            }
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Pro.Id == id)
                    return i;
            return -1;
        }

        public ActionResult ReviewSubmit()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult AddToCart(int id)
        {
            if (Session["cart"] == null)
            {
                using (db = new ChargeAfterEntities())
                {
                    List<Item> cart = new List<Item>();
                    var data = (from d in db.Products where d.Id == id select d).FirstOrDefault();
                    cart.Add(new Item(data, 1));
                    Session["cart"] = cart;
                }
            }
            else
            {
                using (db = new ChargeAfterEntities())
                {
                    List<Item> cart = (List<Item>)Session["cart"];
                    int index = isExisting(id);
                    if (index == -1)
                        cart.Add(new Item(db.Products.Find(id), 1));
                    else
                        cart[index].Quantity++;
                    Session["cart"] = cart;
                }
            }
            TempData["Cart"] = "cart is not empty";
            //ViewData["AddedCart"] = "cart is not empty";
            return Redirect(Request.UrlReferrer.PathAndQuery);

            //using (db = new ChargeAfterEntities())
            //{
            //    var product = (from p in db.Products select p).ToList();
            //    return View("Product", product);
            //}
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult Choose(String loanId, String offerid, int id)
        {
            SubmitOffer offerData = new OfferService().ConfirmOffer(loanId, offerid);
            using (db = new ChargeAfterEntities())
            {
                //int ids = Convert.ToInt32(Session["id"]);
                var req = (from r in db.Requests where r.UserID == id select r).FirstOrDefault();
                long reqID = req.ReqID;

                Request model = db.Requests.Find(reqID);
                model.RequestStatus = "Authorized";
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();


                //Stroing Request Responce
                SubmitOfferResponce sOR = new SubmitOfferResponce();
                sOR.userId = id;
                sOR.loanId = offerData.loanId;
                sOR.applicationId = offerData.loanId;
                //sOR.type = offerData.type;
                sOR.message = offerData.message;
                //sOR.reason = offerData.reason;

                db.SubmitOfferResponces.Add(sOR);
                db.SaveChanges();

                
                TempData["ConfirmOffer"] = loanId;
            }
            
            return RedirectToAction("Approved");


        }
    }
}