using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.Behaviors.Validation
{
    internal class HostNameValidationBehavior : ValidationBehavior<string>
    {
        protected override async Task<bool> ValidateGenericAsync(string value)
        {
			try
			{
				await Dns.GetHostEntryAsync(value);
                return true;
			}
			catch (ArgumentException)
            {
                return false;
            }
			catch(SocketException)
            {
                return false;
            }
        }
    }
}
