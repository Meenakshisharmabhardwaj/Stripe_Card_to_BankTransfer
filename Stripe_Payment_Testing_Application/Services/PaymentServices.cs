using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe_Payment_Testing_Application.Modals;

namespace Stripe_Payment_Testing_Application.Services
{
    public class PaymentServices
    {
        IConfiguration configuration;
        public PaymentServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public ResponseModel Payment(PaymentModel paymentModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                CreditCardOptions card = new CreditCardOptions();
                card.Name = "Rafeal Esteves"; // For indian account it's neccessary
                card.AddressCity = "LA";
                card.AddressState = "CA";
                card.AddressCountry = "United States";
                card.AddressLine1 = "5765 Valley Ave Suite 100";
                card.AddressZip = "94566";
                var stripekey = configuration.GetValue<string>("Stripe:secretkey");
                StripeConfiguration.ApiKey = stripekey;
                //card.Number = model.card;
                card.Number = paymentModel.card;
                string str = Convert.ToString(paymentModel.ExpiryDetail);
                string[] tokens = str.Split("/");
                card.ExpMonth = Convert.ToInt64(tokens[0]);
                card.ExpYear = Convert.ToInt64(tokens[1]);
                card.Cvc = paymentModel.Cvc;
                //Assign Card to Token Object and create Token
                TokenCreateOptions token = new TokenCreateOptions();
                token.Card = card;
                TokenService serviceToken = new TokenService();
                Token newToken = serviceToken.Create(token);

                //Create Customer Object and Register it on Stripe 
                CustomerCreateOptions myCustomer = new CustomerCreateOptions();
                myCustomer.Email = paymentModel.Email;
                myCustomer.Source = newToken.Id;
                var customerService = new CustomerService();
                Customer stripeCustomer = customerService.Create(myCustomer);

                //Create Charge Object with details of Charge 
                var options = new ChargeCreateOptions();
                options.Amount = Convert.ToInt64(paymentModel.BookingAmount) * 100;//options.Amount = Convert.ToInt64("1");
                options.Currency = configuration.GetValue<string>("Currency"); ;
                options.ReceiptEmail = paymentModel.Email;
                options.Customer = stripeCustomer.Id;
                options.Description = paymentModel.InstructionMessage;
                options.Capture = false;

                //and Create Method of this object is doing the payment execution. 
                var service = new ChargeService();

                var charge = service.Create(options); // This will do the Payment    


                if (charge.Status == "succeeded")
                {
                    response.data = true;
                    response.StatusCode = Constants.StatusCodes.Success;
                    response.Message = Constants.Messages.PAYMENT_SUCCESSFULL;
                }
                else
                {
                    response.data = false;
                    response.StatusCode = Constants.StatusCodes.InternalServerError;
                    response.Message = Constants.Messages.SOMETHING_WENT_WRONG;
                }
                return response;
            }catch(StripeException ex)
            {
                response.Message = ex.Message;
                response.data = null;
                response.StatusCode = Constants.StatusCodes.InternalServerError;
                return response;
            }

        }
    }
}
