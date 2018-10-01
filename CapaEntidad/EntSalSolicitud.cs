using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntSalSolicitud
    {
        public int? Id_ESolIni { get; set; }
        public int? Id_ESolFin { get; set; }
        public DateTime? ESol_FechaIni { get; set; }
        public DateTime? ESol_FechaFin { get; set; }
        public string ESol_Estatus { get; set; }
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_ESol { get; set; }
        public string ESol_Unique { get; set; }
        public string ESol_EstatusStr { get; set; }
        public string Id_TmStr { get; set; }
        public string Id_UCreoStr { get; set; }
        public DateTime ESol_Fecha { get; set; }
        public int Id_UEnviar { get; set; }
        public int ESol_Naturaleza { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Tm { get; set; }
        public int Id_Pvd { get; set; }
        public int Id_Ter { get; set; }
        public string ESol_Referencia { get; set; }
        public int Id_Rem { get; set; }
        public int Id_Fac { get; set; }
        public int Id_UCC { get; set; }
        public string ESol_Notas { get; set; }
        public int ESol_CteCuentaNacional { get; set; }
        public int ESol_CteCuentaContNacional { get; set; }
        public DateTime? ESol_FechaReferencia { get; set; }
        public double ESol_Subtotal { get; set; }
        public double ESol_Impuesto { get; set; }
        public double ESol_Total { get; set; }
        public int Id_UCreo { get; set; }
        public string Cte_NomComercial { get; set; }
        public string Ter_Nombre { get; set; }
        public string Id_EsStr { get; set; }
        public string ESol_CorreoDest { get; set; }
        public string ESol_CorreoCC { get; set; }
        public int Id_Ord { get; set; }
    }
}
