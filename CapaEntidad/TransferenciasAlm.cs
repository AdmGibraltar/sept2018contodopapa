using System;
using System.Text;

namespace CapaEntidad
{
   public class TransferenciasAlm
    {

       public int Id_Cd { get; set; }
       public int? Id_CteIni { get; set; }
       public int? Id_CteFin { get; set; }
       public int? Id_Rem { get; set; }
       public DateTime? Rem_FechaIni { get; set; }
       public DateTime? Rem_FechaFin { get; set; }
       public string Rem_Estatus { get; set; }

       public int? Id_CdOrigenIni { get; set; }
       public int? Id_CdOrigenFin { get; set; }
       public int? Id_RemOrigen { get; set; }
       public DateTime? Tra_FechaIni { get; set; }
       public DateTime? Tra_FechaFin { get; set; }
       public string Tra_Estatus { get; set; }

       public int Id_Emp { get; set; }
       public int Id_Tra { get; set; }
       public int Id_CdOrigen { get; set; }
       public string Cd_Nombre { get; set; }
 
       public double Tra_Importe { get; set; }
 
       public string Tra_EstatusStr { get; set; }
       public DateTime Tra_RemFecha { get; set; }
       public DateTime Tra_FechaEnvio{ get; set; }
       public DateTime? Tra_FechaRecepcion { get; set; }
       public string Id_CdOrigenStr { get; set; }
       public string Tra_Notas { get; set; }
       public double CD_IVA { get; set; }
       public int? Id_Es { get; set; }
       public int Id_U { get; set; }


    }
}
