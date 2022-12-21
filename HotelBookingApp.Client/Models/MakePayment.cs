using Stripe;
using System.Threading.Tasks;

namespace HotelBookingApp.Client.Models
{
    public class MakePayment
    {
        public static async Task<dynamic> PayAsync(string cardnumber, int month, int year, string CVC)
        {
            try
            {
                StripeConfiguration.ApiKey ="sk_test_51MEVRMGw1sVYJqne1YziZmObwp9yHVQXPoLLGEnEApgkE6rUZr9COK0Myzl8VbntKGhDhTd5GxwG9H4YeP7VsbMD008jRKLYpC";
                var optionstoken = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = cardnumber,
                        ExpMonth = month,
                        ExpYear = year,
                        Cvc = CVC
                    }
                };

                    var servicetoken = new TokenService();
                Token stripetoken = await servicetoken.CreateAsync(optionstoken);
                var options = new ChargeCreateOptions
                {
                    Amount = long.MaxValue,
                    Currency = "usd",
                    Description = "Test",
                    Source = stripetoken.Id

                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);
                if(charge.Paid)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
        }

        //public async Task<dynamic> Pay(Models.PaymentModel pm)
        //{
        //    return await MakePayment.PayAsync(pm.CardNumber, pm.Month, pm.Year, pm.CVC)
        //}

        
    }
}

