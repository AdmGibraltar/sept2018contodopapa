//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaModelo_CC.CuentasCoorporativas
{
    using System;
    using System.Collections.Generic;
    
    public partial class CatAcys_Productos
    {
        public int Id_Prd { get; set; }
        public int Id_ACYS { get; set; }
        public int Id_TG { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<double> Subtotal { get; set; }
        public Nullable<int> Frecuencia { get; set; }
        public Nullable<bool> Lun { get; set; }
        public Nullable<bool> Mar { get; set; }
        public Nullable<bool> Mie { get; set; }
        public Nullable<bool> Jue { get; set; }
        public Nullable<bool> Vie { get; set; }
        public Nullable<bool> Sab { get; set; }
        public string Documento { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string Presentacion { get; set; }
    
        public virtual CatCNac_ACYS CatCNac_ACYS { get; set; }
    }
}
