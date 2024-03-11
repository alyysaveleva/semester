using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoldatovaCRUD.Models;

namespace SoldatovaCRUD.classes
{
    /// <summary>
    /// подключение к БД
    /// </summary>
    /// <param name="connect"></param>
    /// <returns></returns>
    internal class connect
    {
        public static SoldatovaCRUDEntities2 modelbd;
    }
}
