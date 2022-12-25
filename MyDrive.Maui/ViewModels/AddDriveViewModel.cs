using MyDrive.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyDrive.Maui.ViewModels
{
    internal class AddDriveViewModel : ObservableViewModel
    {
        public ICommand AddMongoDriveCommand { get; }
        public AddDriveViewModel()
        {
            AddMongoDriveCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AddMongoDrivePage));
            });
        }
    }
}
