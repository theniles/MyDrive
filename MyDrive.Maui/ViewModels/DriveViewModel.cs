using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.ViewModels
{
    internal class DriveViewModel : ObservableViewModel
    {

        private string m_Name;
        public string Name { get { return m_Name; } set { SetProperty(ref m_Name, value); } }

        public DriveViewModel()
        {
        }
    }
}
