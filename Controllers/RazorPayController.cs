using AutomobileInsuranceManagement_AIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    public class RazorPayController : Controller
    {
        AIMEntities dataconnection=new AIMEntities();
        public ActionResult CreateOrder(ActivePolicy _activePolicy)
        {
            Random random = new Random();
            string transactionId = random.Next(10000, 10000).ToString();
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_xPSgWk2BqWWbX5", "drfZAYMtn4LooQhfnGe5LLPr");
            Dictionary<string,object> option=new Dictionary<string,object>();
            option.Add("amount", _activePolicy.premium * 100);
            option.Add("receipt", transactionId);
            option.Add("currency", "INR");
            option.Add("payment_capture", 0);
            Razorpay.Api.Order orderResponse=client.Order.Create(option);
            string orderId = orderResponse["id"].ToString();
            user userData = dataconnection.users.Find(_activePolicy.userId);
            Policy chosenPolicyData=dataconnection.Policies.Find(_activePolicy.policyId);
            transaction tran = new transaction
            {
                automobileId = _activePolicy.automobileId,
                razorKey = "rzp_test_xPSgWk2BqWWbX5",
                premium = Convert.ToDouble(_activePolicy.premium * 100),
                policyId=_activePolicy.policyId,
                createdDate = DateTime.Now,
                renewalDate=DateTime.Now.AddYears(Convert.ToInt32(chosenPolicyData.years)),
                currency="INR",
                userId=_activePolicy.userId,
                name=userData.userName.ToString(),
                mobileNumber=userData.mobile,
                generatedID=orderId,
                //success=false
            };
            dataconnection.transactions.Add(tran);
            dataconnection.SaveChanges();
            Session["CurrentOrderId"] = tran.generatedID;
            Session["CurrentActiveId"] = _activePolicy.ActiveId;
            return View("Complete", tran);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = Request.Params["rzp_paymentid"];

            // This is orderId
            string orderId = Request.Params["rzp_orderid"];
            

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_xPSgWk2BqWWbX5", "drfZAYMtn4LooQhfnGe5LLPr");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            
            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                
                string transactionId = Session["CurrentOrderId"] as string;
                int? activePolicyId = (int?)Session["CurrentActiveId"];
                if (transactionId != null && activePolicyId!=null)
                {
                    //dataconnection.SP_Payment_Success(transactionId,activePolicyId);
                    return RedirectToAction("DashBoard","Home");
                }
              
            }
            else
            {
                return RedirectToAction("Failed");
            }
            return Content("Error");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }
    }

}
