namespace AppointmentSimulator.Models
{
    public class Appointment
    {
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateOnly AppointmentDate { get; set; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan EndingTime { get; set; }
    }
}
