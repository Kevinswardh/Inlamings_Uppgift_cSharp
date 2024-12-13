using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Helpers
{
    public static class GuidGenerator
    {
        // Generates a new GUID for the user
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();  // Generate a new GUID and convert it to string
        }
    }
}
