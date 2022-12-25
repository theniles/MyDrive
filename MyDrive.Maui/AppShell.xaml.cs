using MyDrive.Maui.Views;

namespace MyDrive.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(AddDrivePage), typeof(AddDrivePage));
            Routing.RegisterRoute($"{nameof(AddDrivePage)}/{nameof(AddMongoDrivePage)}", typeof(AddMongoDrivePage));
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
        }
    }
}