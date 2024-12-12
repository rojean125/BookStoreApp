namespace bookStoreApp;

public partial class PaymentPage : ContentPage
{
    private readonly databaseService _database;
    private readonly Book _book;
    public PaymentPage(databaseService database, Book book)
	{
		InitializeComponent();
        _database = database;
        _book = book;
    }
    private async void OnBuyBookClicked(object sender, EventArgs e)
    {  
        _book.Quantity -= 1;
        await _database.UpdateBookAsync(_book);

        await DisplayAlert("Success", "Payment successful! Thank you for your purchase.", "OK");
        await Navigation.PushAsync(new HomePage(_database));
    }
}