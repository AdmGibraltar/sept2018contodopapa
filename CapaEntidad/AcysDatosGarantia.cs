using System;
using System.Collections.Generic;

namespace CapaEntidad
{
   
    public class AcysDatosGarantia
    {
        public int Id_Emp { get; set; }
        public int Id_Acs { get; set; }
        public int Id_Cd { get; set; }
        public int Id_AcsVersion { get; set; }
        public int Id_TG { get; set; }
        public double FactorGarantia { get; set; }
        public double UPrimaNeta { get; set; }
        public DateTime FechaCorte { get; set; }

        public Dictionary<int, DateTime> Fechas_Corte { get; set; }

    }

}