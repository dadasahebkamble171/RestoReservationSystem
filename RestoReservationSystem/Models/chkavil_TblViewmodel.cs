using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class chkavil_TblViewmodel
    {
        public CheckAvailable chk {  get; set; } 

        public List<Table> tablesInfo { get; set; }
    }
}