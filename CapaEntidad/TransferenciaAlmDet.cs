using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaEntidad
{
    public class TransferenciaAlmDet
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Tra { get; set; }
        public int Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public string Prd_Presentacion { get; set; }
        public int TraD_Cant { get; set; }
        public int TraD_CantRec { get; set; }
        public int TraD_Diferencia { get; set; }
        public double TraD_Costo { get; set; }
        public double TraD_TotalEnv { get; set; }
        public double TraD_TotalRec { get; set; }
        public string UniqueID { get; set; }

    }
}
