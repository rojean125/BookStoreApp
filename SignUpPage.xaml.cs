namespace bookStoreApp;

public partial class SignUpPage : ContentPage
{
    private readonly databaseService _database;
    public SignUpPage(databaseService database)
	{
		InitializeComponent();
        _database = database;
    }
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        var newUser = new User
        {
            Username = username,
            Email = email,
            Password = password
        };

        await _database.AddUserAsync(newUser);
        await DisplayAlert("Success", "Account created successfully.", "OK");
        await Navigation.PushAsync(new LoginPage(_database));
    }

    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IntroPage(_database));
    }
}