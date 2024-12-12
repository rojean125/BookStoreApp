namespace bookStoreApp;

public partial class LoginPage : ContentPage
{
    private readonly databaseService _database;

    public LoginPage(databaseService database)
    {
        InitializeComponent();
        _database = database;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter email and password.", "OK");
            return;
        }


        if (email == "admin" && password == "admin")
        {
            await Navigation.PushAsync(new AdminPage(_database));
            return;
        }


        var user = await _database.GetUserByEmailAndPasswordAsync(email, password);
        if (user != null)
        {
            await Navigation.PushAsync(new HomePage(_database));
        }
        else
        {
            await DisplayAlert("Error", "Invalid email or password.", "OK");
        }
    }

    private async void signUpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage(_database));
    }

    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IntroPage(_database));
    }
}
