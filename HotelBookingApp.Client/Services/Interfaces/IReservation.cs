using HotelBookingApp.Client.Models.DTO;

namespace HotelBookingApp.Client.Services.Interfaces
{
    public interface IReservation
    {
        bool CheckAvailability(AvailabilityDTO availabilityDTO);
        bool BookRoom(RoomBookingDto roomBookingDto);

        bool CancelReservation(int BookingId);

        bool ReduceNumberofRoomsAfterBooking(AvailabilityDTO availabilityDTO);
    }

}
