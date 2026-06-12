namespace CURE.Application.DTOs.Booking
{
    public class BookingHistoryDto
    {
        public string OldStatus { get; set; }

        public string NewStatus { get; set; }

        public string ChangedBy { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}
