using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class SolConvenioDet
    {
       public int? FiltroId_Sol { get; set; }
       public string FiltroPc_NoConvenio { get; set; }
       public string FiltroSol_Estatus { get; set; }
       public string Id_Unique { get; set; }


       public int Id_Sol { get; set; }
       public int Id_Cd { get; set; }
       public int Id_PC { get; set; }
       public int Id_Cte { get; set; }
       public int Id_Ter { get; set; }
       public string Sol_CteNombre { get; set; }
       public string SolTer_Nombre { get; set; }
       public string Sol_UsuFinal { get; set; }
       public string SolD_Estatus { get; set; }
       public string CDI { get; set; }
       public string Sol_UNombre { get; set; }
       public string U_Nombre { get; set; }

    }
}
