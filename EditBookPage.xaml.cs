namespace bookStoreApp;

public partial class EditBookPage : ContentPage
{
    private readonly databaseService _database;
    private readonly Book _book;
    public EditBookPage(databaseService database, Book book)
	{
		InitializeComponent();
        _database = database;
        _book = book;

        TitleEntry.Text = book.Title;
        AuthorEntry.Text = book.Author;
        DescriptionEditor.Text = book.Description;
        PriceEntry.Text = book.Price.ToString();
    }
    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
            string.IsNullOrWhiteSpace(AuthorEntry.Text) ||
            string.IsNullOrWhiteSpace(DescriptionEditor.Text) ||
            !decimal.TryParse(PriceEntry.Text, out var price))
        {
            await DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
            return;
        }

        // Update the book details
        _book.Title = TitleEntry.Text;
        _book.Author = AuthorEntry.Text;
        _book.Description = DescriptionEditor.Text;
        _book.Price = price;

        await _database.UpdateBookAsync(_book);
        await DisplayAlert("Success", "Book updated successfully!", "OK");
        await Navigation.PopAsync(); // Return to AdminPage
    }
}