using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using AppointmentSimulator.Models;
using Microsoft.Maui.Controls;

namespace AppointmentSimulator.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private string name;
        private string subject;
        private DateOnly appointmentDate = DateOnly.FromDateTime(DateTime.Now);
        private TimeSpan startingTime;
        private TimeSpan endingTime;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Subject
        {
            get => subject;
            set { subject = value; OnPropertyChanged(nameof(Subject)); }
        }

        public DateOnly AppointmentDate
        {
            get => appointmentDate;
            set { appointmentDate = value; OnPropertyChanged(nameof(AppointmentDate)); }
        }

        public TimeSpan StartingTime
        {
            get => startingTime;
            set { startingTime = value; OnPropertyChanged(nameof(StartingTime)); }
        }

        public TimeSpan EndingTime
        {
            get => endingTime;
            set { endingTime = value; OnPropertyChanged(nameof(EndingTime)); }
        }

        public ObservableCollection<Appointment> Appointments => GlobalData.Appointments;

        public ICommand AddAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }

        private Appointment selectedAppointment;
        public Appointment SelectedAppointment
        {
            get => selectedAppointment;
            set { selectedAppointment = value; OnPropertyChanged(nameof(SelectedAppointment)); }
        }

        public AppointmentViewModel()
        {
            AddAppointmentCommand = new Command(AddAppointment);
            DeleteAppointmentCommand = new Command(DeleteAppointment);
        }

        private async void AddAppointment()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Subject))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            if (AppointmentDate < DateOnly.FromDateTime(DateTime.Now))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La fecha no puede ser anterior a hoy", "OK");
                return;
            }

            if (StartingTime >= EndingTime)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La hora de inicio debe ser menor a la de término", "OK");
                return;
            }

            foreach (var a in Appointments)
            {
                if (a.AppointmentDate == AppointmentDate &&
                   ((StartingTime >= a.StartingTime && StartingTime < a.EndingTime) ||
                    (EndingTime > a.StartingTime && EndingTime <= a.EndingTime)))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "La cita se traslapa con otra existente", "OK");
                    return;
                }
            }

            var newAppointment = new Appointment
            {
                Name = Name,
                Subject = Subject,
                AppointmentDate = AppointmentDate,
                StartingTime = StartingTime,
                EndingTime = EndingTime
            };

            Appointments.Add(newAppointment);

            await Application.Current.MainPage.DisplayAlert("Éxito", "Cita agregada correctamente", "OK");

            Name = string.Empty;
            Subject = string.Empty;
            StartingTime = TimeSpan.Zero;
            EndingTime = TimeSpan.Zero;
        }

        private async void DeleteAppointment()
        {
            if (SelectedAppointment == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Selecciona una cita para eliminar", "OK");
                return;
            }

            Appointments.Remove(SelectedAppointment);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Cita eliminada correctamente", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
