using System.Collections.ObjectModel;

namespace AppointmentSimulator.Models
{
    public static class GlobalData
    {
        public static ObservableCollection<Appointment> Appointments { get; } = [
            new Appointment
            {
                Name = "Alejandro Barroeta",
                Subject = "Cambio de carrera",
                AppointmentDate = new DateOnly(2025,09,14),
                StartingTime = new TimeSpan(8,0,0),
                EndingTime = new TimeSpan(8,30,0)
            }
        ];
    }
}
