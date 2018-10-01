using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CalendarioControl
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Cal_Año { get; set; }

        public int Id_Acs { get; set; }
        public int Id_AcsVersion { get; set; }

        public int Semana { get; set; }
        public int IdProd { get; set; }

        public int Id_TG { get; set; }
    }


}