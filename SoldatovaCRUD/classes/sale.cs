using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoldatovaCRUD.classes
{
    class sale
    {

        /// <summary>
        /// проверяет наличие скидки в столбце 
        /// </summary>
        /// <param name="IsSale"></param>
        /// <returns></returns>
        public bool IsSale
        {
            get
            {
                var saleObj = connect.modelbd.Merches.FirstOrDefault();
                if (Convert.ToDouble(saleObj) != 0) return true;
                else return false;

            }

        }
    }
}
