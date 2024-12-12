namespace bookStoreApp;

public partial class AdminPage : ContentPage
{
    private readonly databaseService _database;
    public AdminPage(databaseService database)
	{
		InitializeComponent();
        _database = database;
        LoadData();
    }
    private async void LoadData()
    {
        var books = await _database.GetBooksAsync();
        BooksCollection.ItemsSource = books;
    }
    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddBookPage(_database));
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var book = (Book)((Button)sender).CommandParameter;
        await Navigation.PushAsync(new EditBookPage(_database, book));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var book = (Book)((Button)sender).CommandParameter;
        await _database.DeleteBookAsync(book);
        LoadData();
    }
}