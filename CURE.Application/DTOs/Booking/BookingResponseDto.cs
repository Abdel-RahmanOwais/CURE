namespace CURE.Application.DTOs.Booking
{
    public class BookingResponseDto
    {
        public long Id { get; set; }

        public string PatientName { get; set; }

        public string NurseName { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }
    }
}
