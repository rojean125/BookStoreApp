namespace bookStoreApp;

public partial class AddBookPage : ContentPage
{
    private readonly databaseService _database;
    private string _selectedImagePath;
    public AddBookPage(databaseService database)
    {
        InitializeComponent();
        _database = database;
    }
    private async void OnAddBookClicked(object sender, EventArgs e)
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

        // Create the new book object
        var newBook = new Book
        {
            Title = TitleEntry.Text,
            Author = AuthorEntry.Text,
            Description = DescriptionEditor.Text,
            Price = price,
            Genre = GenreEntry.Text ?? "Unknown", // Use entered genre or default to "Unknown"
            Quantity = 10, // Default quantity
            Image = _selectedImagePath ?? "default_book.png" // Use selected image path or a default image
        };

        // Add the book to the database
        await _database.AddBookAsync(newBook);

        // Display success message
        await DisplayAlert("Success", "Book added successfully!", "OK");

        // Navigate back to the AdminPage
        await Navigation.PopAsync();
    }
    private async void OnChooseImageClicked(object sender, EventArgs e)
    {
        try
        {
            // Open media picker to select an image from gallery
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select a Book Cover"
            });

            if (result != null)
            {
                // Store the image path
                _selectedImagePath = result.FullPath;

                // Display the selected image in the Image view
                BookCoverImage.Source = ImageSource.FromFile(_selectedImagePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to pick an image: " + ex.Message, "OK");
        }
    }
}