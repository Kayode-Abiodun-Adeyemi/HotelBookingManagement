using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApp.Client.Models
{
    public class PaymentModel
    {
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public string CVC { get; set; }
        public int Year { get; set; }
        public int Value { get; set; }

    }
}
