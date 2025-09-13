using AppointmentSimulator.Pages;

namespace AppointmentSimulator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnNewAppointmentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddNewAppointmentPage());
    }
}
