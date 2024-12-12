namespace bookStoreApp;

public partial class HomePage : ContentPage
{
    private readonly databaseService _database;
    public HomePage(databaseService database)
	{
		InitializeComponent();

        _database = database;
        LoadData();
    }
    private async void LoadData()
    {
        var books = await _database.GetBooksAsync();
        BooksScrollView.BindingContext = books;
        RecommendedScrollView.BindingContext = books;

        GenreCarousel.ItemsSource = new List<string> { "Horror", "Romance", "Sci-Fi", "Fantasy" };
    }
    private async void OnGenreClicked(object sender, EventArgs e)
    {
        string genre = ((Button)sender).Text;
        await Navigation.PushAsync(new GenrePage(_database, genre));
    }

    private async void OnBookClicked(object sender, EventArgs e)
    {
        var book = (Book)((Button)sender).CommandParameter;
        await Navigation.PushAsync(new BookDetailPage(_database, book));
    }
}