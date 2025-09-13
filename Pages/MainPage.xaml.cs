using AppointmentSimulator.Models;

namespace AppointmentSimulator.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AppointmentsCollectionView.ItemsSource = GlobalData.Appointments;
        }

    }
}
