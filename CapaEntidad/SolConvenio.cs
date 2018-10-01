using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class SolConvenio
    {
       public int? FiltroId_Sol { get; set; }
       public string FiltroPc_NoConvenio { get; set; }
       public string FiltroSol_Estatus { get; set; }
       public int? FiltroId_CD { get; set; }

       public int Id_Sol { get; set; }
       public int Id_Cd { get; set; }
       public int Id_PC { get; set; }
       public int Id_U { get; set; }
       public string PC_NoConvenio { get; set; }
       public string PC_Nombre { get; set; }
       public string Sol_EstatusStr { get; set; }
       public string Sol_Estatus { get; set; }
       public int Sol_IdUCreo { get; set; }
       public string Sol_UNombre { get; set; }
       public string Sol_UCorreo { get; set; }
       public DateTime Sol_Fecha { get; set; }
       public string CD_Nombre { get; set; }
       public string Cat_DescCorta { get; set; }
       public string Sol_Unique { get; set; }


    }
}
