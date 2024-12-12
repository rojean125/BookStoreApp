namespace bookStoreApp;

public partial class GenrePage : ContentPage
{
    private readonly databaseService _database;
    private readonly string _genre;
    public GenrePage(databaseService database, string genre)
	{
		InitializeComponent();
        _database = database;
        _genre = genre;
        Title = $"{_genre} Books";
        LoadData();
    }
    private async void LoadData()
    {
        var books = await _database.GetBooksByGenreAsync(_genre);
        BooksCollection.ItemsSource = books;
    }
}