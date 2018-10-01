using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Convenio
    {
       public int Id_PC { get; set; }
       public int Id_PCAnterior { get; set; }
       public string PC_NoConvenio { get; set; }
       public string PC_Nombre { get; set; }
       public int Id_Cat { get; set; }
       public int Cat_Consecutivo { get; set; }
       public int Id_ULider { get; set; }
       public int Id_UEjecutivo { get; set; }
       public double PC_Margen { get; set; }
       public string PC_Notas { get; set; }
       public DateTime PC_FechaMod { get; set; }
       public DateTime PC_FechaCreo { get; set; }
       public string Cat_DescCorta { get; set; }
       public string Estatus { get; set; }
       public int Id_U { get; set; }
       public int Filtro_TipoFiltro { get; set; }
       public int Filtro_Vencido { get; set; }
       public int Filtro_Id_Cat { get; set; }
       public string Filtro_Valor { get; set; }
       public int Filtro_Id_Cd { get; set; }

       public int Id_Tipo { get; set; }
       public int Id_Cd { get; set; }
       public string Cd_Nombre { get; set; }
       public bool Seleccionado { get; set; }
       public bool PCD_Ver { get; set; }
       public bool PCD_Usar { get; set; }


       public int Pue_Admin1 {get;set;}
	   public int Pue_Admin2 {get;set;}
	   public int Pue_CteMacola {get;set;}
	   public int Pue_CteIntranet {get;set;}
	   public int Pue_EqComodato {get;set;}
       public int Pue_VerTodo { get; set; }

    }
}
