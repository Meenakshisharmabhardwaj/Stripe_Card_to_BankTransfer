using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe_Payment_Testing_Application.Modals;
using Stripe_Payment_Testing_Application.Services;

namespace Stripe_Payment_Testing_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PaymentController : Controller
    {
        IConfiguration configuration;
        public PaymentController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public ResponseModel Payment([FromBody]PaymentModel paymentModel)
        {
            ResponseModel response = new ResponseModel();
            PaymentServices paymentServices = new PaymentServices(configuration);
            if (ModelState.IsValid)
            {
                var data = paymentServices.Payment(paymentModel);
                if (data != null)
                {
                    response.data ="Success";
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                }
                else
                {
                    response.data = null;
                    response.Message = Constants.Messages.SOMETHING_WENT_WRONG;
                    response.StatusCode = Constants.StatusCodes.InternalServerError;

                }
            }
            else
            {
                List<string> obj = new List<string>();
               
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        obj.Add(error.ErrorMessage);
                    }
                }
                response.data = obj;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = Constants.Messages.SOMETHING_WENT_WRONG; 
                return response;
            }
            return response;
           
        }

        
    }
}
