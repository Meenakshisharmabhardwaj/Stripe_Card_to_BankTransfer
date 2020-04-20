using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Payment_Testing_Application.Modals
{
    public class PaymentModel
    {
        [Required]
        public string card { get; set; }
        [Required]
        public string ExpiryDetail { get; set; }
        public object Phone { get; set; }
        public int ExpMonth { get; set; }
        
        public int ExpYear { get; set; }
        [Required]
        public string Cvc { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BookingAmount { get; set; }
        
        public string InstructionMessage { get; set; }
    }
}
