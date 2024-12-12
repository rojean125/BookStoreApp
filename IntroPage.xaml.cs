namespace bookStoreApp;

public partial class IntroPage : ContentPage
{
    private readonly databaseService _database;
    public IntroPage(databaseService database)
	{
		InitializeComponent();

        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    private async void getStartButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new SignUpPage(_database));
    }

    private async void alreadyButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_database));
    }
}