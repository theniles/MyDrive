using Microsoft.Maui.Controls;
using MyDrive.Maui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyDrive.Maui.ViewModels
{
    internal class HomeViewModel : ObservableViewModel
    {
        private ObservableCollection<DriveViewModel> m_Drives;
        public ObservableCollection<DriveViewModel> Drives { get { return m_Drives; } set { SetProperty(ref m_Drives, value); } }

        public ICommand AddDriveCommand { get; }

        public HomeViewModel()
        {
            Drives = new ObservableCollection<DriveViewModel>();

            AddDriveCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AddDrivePage));
            });
        }
    }
}
