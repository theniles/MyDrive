using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyDrive.Maui.ViewModels
{
    internal class AddMongoDriveViewModel : ObservableViewModel
    {
        private string m_Host;
        public string Host { get { return m_Host; } set { SetProperty(ref m_Host, value); } }

        private string m_Port;
        public string Port { get { return m_Port; } set { SetProperty(ref m_Port, value); } }

        private string m_User;
        public string User { get { return m_User; } set { SetProperty(ref m_User, value); } }

        private string m_Password;
        public string Password { get { return m_Password; } set { SetProperty(ref m_Password, value); } }

        private string m_Database;
        public string Database { get { return m_Database; } set { SetProperty(ref m_Database, value); } }

        public ICommand SaveCommand { get; }

        public AddMongoDriveViewModel()
        {
            SaveCommand = new Command(() =>
            {
                var mongoDrive = new 
            });
        }
    }
}
