namespace OnlineQueuing.Entities
{
    public class Appointment
    {
        public long AppointmentId { get; set; }
        public User User { get; set; }
        public int TimeSlot { get; set; }
        public string ServiceType { get; set; }
        public string Date { get; set; }
    }
}
