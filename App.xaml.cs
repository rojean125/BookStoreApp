namespace bookStoreApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "bookstore.db");
            var databaseService = new databaseService(dbPath);

            MainPage = new AppShell();
            MainPage = new NavigationPage(new IntroPage(databaseService));
        }
    }
}
