//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaModelo.CuentasCoorporativas
{
    using System;
    using System.Collections.Generic;
    
    public partial class CatACYS_OtrosApoyos
    {
        public int Id { get; set; }
        public string Notas { get; set; }
        public string ContactoClientePagos { get; set; }
        public string ContactoClientePagosTel { get; set; }
        public string ContactoClientePagosEmail { get; set; }
        public string ContactoClientecompra { get; set; }
        public string ContactoClientecompraTel { get; set; }
        public string ContactoClientecompraEmail { get; set; }
        public string ContactoClientealmacen { get; set; }
        public string ContactoClientealmacenTel { get; set; }
        public string txtContactoClientealmacenEmail { get; set; }
        public string ContactoClienteMantenimiento { get; set; }
        public string ContactoClienteMantenimientoTel { get; set; }
        public string ContactoClienteMantenimientoEmail { get; set; }
        public string ContactoClienteOtro { get; set; }
        public string ContactoClienteOtroTel { get; set; }
        public string ContactoClienteOtroEmail { get; set; }
    
        public virtual CatCNac_ACYS CatCNac_ACYS { get; set; }
    }
}
