namespace HotelBookingAPI.Models
{
    public class HotelBooking
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string GuestName { get; set; }
        public DateTime checkInData { get; set; }
        public DateTime checkOutData { get; set; }

    }
}
