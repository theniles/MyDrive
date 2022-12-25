using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDrive.Maui.Behaviors.Validation
{
    internal class MongoDatabaseNameValidation : ValidationBehavior<string>
    {
        //https://www.geeksforgeeks.org/mongodb-database-collection-and-document/
        public static readonly char[] IllegalCharacters = new[]
        { '/', '\\', '.', ' ', '"', '$', '*', ':', '|', '?', '<', '>', '\0'};

        protected override async Task<bool> ValidateGenericAsync(string value)
        {
            return await Task.Run(() =>
            {
                if (value == null || value.Length > 63)
                    return false;
                foreach (var character in IllegalCharacters)
                {
                    if (value.Contains(character))
                        return false;
                }
                return true;
            });
        }
    }
}
