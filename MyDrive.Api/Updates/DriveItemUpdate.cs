using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Api.Updates
{
    public class DriveItemUpdate
    {
        public DriveItemUpdate(StringUpdate nameUpdate, StringUpdate descriptionUpdate, TagUpdate tagUpdate)
        {
            NameUpdate = nameUpdate;
            DescriptionUpdate = descriptionUpdate;
            TagUpdate = tagUpdate;
        }

        public StringUpdate NameUpdate { get; }

        public StringUpdate DescriptionUpdate { get; }

        public TagUpdate TagUpdate { get; }
    }
}
