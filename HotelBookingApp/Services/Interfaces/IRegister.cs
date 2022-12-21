using HotelBookingApp.Models.DTO;

namespace HotelBookingApp.Services.Interfaces
{
    public interface IRegister
    {
        bool Register(RegisterViewModel registerViewModel);
    }
}
