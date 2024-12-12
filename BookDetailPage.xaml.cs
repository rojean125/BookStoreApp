namespace bookStoreApp;

public partial class BookDetailPage : ContentPage
{
    private readonly databaseService _database;
    private readonly Book _book;
    public BookDetailPage(databaseService database, Book book)
	{
		InitializeComponent();
        _database = database;
        _book = book;

        BookImage.Source = book.Image;
        BookTitle.Text = book.Title;
        BookAuthor.Text = $"{book.Author}";
        BookDescription.Text = book.Description;
        BookPrice.Text = $"{book.Price:C}";
    }
    private async void OnBuyBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PaymentPage(_database, _book));
    }

    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}