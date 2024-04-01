using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldatovaCRUD.classes
{
    internal class currentuser
    {
        public static int UserRole { get; set; }
        public static bool Activesession { get; set; }
        public static DateTime AppBooted { get; set; }
        public static int ActiveUserID { get; set; }
    }
   
}
