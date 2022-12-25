using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Updates
{
    public class DateUpdate
    {
        public DateUpdate(DateTime newDate)
        {
            NewDate = newDate;
        }

        public DateTime NewDate { get; set; }
    }
}
