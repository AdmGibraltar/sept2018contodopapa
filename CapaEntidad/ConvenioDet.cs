using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class ConvenioDet
    {
       public string PC_Nombre { get; set; }
       public int Id_PC { get; set; }
       public int Id_Prd { get; set; }
       public string PCD_ClaveProv { get; set; }
       public string Prd_Descripcion { get; set; }
       public double? PCD_PrecioVtaMin { get; set; }
       public double? PCD_PrecioVtaMax { get; set; }
       public double? PCD_CantidadMax { get; set; }
       public int Id_Moneda { get; set; }
       public string Id_MonedaStr { get; set; }
       public string PCD_CatDesp { get; set; }
       public double? PCD_PrecioAAAEsp { get; set; }
       public DateTime? PCD_FechaInicio { get; set; }
       public double? PCD_PrecioAAAEspA { get; set; }
       public DateTime? PCD_FechaInicioA { get; set; }
       public DateTime? PCD_FechaFinA { get; set; }
       public double? PCD_PrecioAAAEspB { get; set; }
       public DateTime? PCD_FechaInicioB { get; set; }
       public double? PCD_PrecioAAAEspC { get; set; }
       public DateTime? PCD_FechaInicioC { get; set; }
       public DateTime? PCD_FechaFinC { get; set; }
       public DateTime? PCD_FechaFin { get; set; }
       public DateTime? PCD_FechaFinVer { get; set; }

       public int Id_Emp { get; set; }
       public int Id_Cd { get; set; }
       public int Id_Cte { get; set; }
       public string PC_NoConvenio { get; set; }


    }
}
