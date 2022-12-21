using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;

namespace HotelBookingApp.Client.Services.Interfaces
{
    public interface ICheckLogin
    {
        User ValidateLogin(LoginViewModel model);
    }
}
