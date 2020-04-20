using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Payment_Testing_Application.Constants
{
    public static class StatusCodes
    {
        public static int Success = 200;
        public static int BadRequest = 404;
        public static int InternalServerError = 500;
    }
    public static class Messages
    {
        public static string SOMETHING_WENT_WRONG = "Something went wrong";
        public static string PAYMENT_SUCCESSFULL = "Payment tranfer sucessfully!!!";
    }
}
