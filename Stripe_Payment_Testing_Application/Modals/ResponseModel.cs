using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Payment_Testing_Application.Modals
{
    public class ResponseModel
    {
        public object data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
