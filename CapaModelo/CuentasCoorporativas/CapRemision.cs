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
    
    public partial class CapRemision
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Rem { get; set; }
        public string Rem_Tipo { get; set; }
        public Nullable<System.DateTime> Rem_Fecha { get; set; }
        public Nullable<int> Id_Tm { get; set; }
        public Nullable<int> Id_Ped { get; set; }
        public int Id_Cte { get; set; }
        public Nullable<int> Id_Ter { get; set; }
        public string Rem_Calle { get; set; }
        public string Rem_Numero { get; set; }
        public string Rem_Cp { get; set; }
        public string Rem_Colonia { get; set; }
        public string Rem_Municipio { get; set; }
        public string Rem_Ciudad { get; set; }
        public string Rem_Estado { get; set; }
        public string Rem_Rfc { get; set; }
        public string Rem_Telefono { get; set; }
        public string Rem_Contacto { get; set; }
        public string Rem_Conducto { get; set; }
        public string Rem_Guia { get; set; }
        public Nullable<System.DateTime> Rem_FechaEntrega { get; set; }
        public string Rem_HoraEntrega { get; set; }
        public string Rem_Nota { get; set; }
        public Nullable<double> Rem_Subtotal { get; set; }
        public Nullable<double> Rem_Iva { get; set; }
        public Nullable<double> Rem_Total { get; set; }
        public string Rem_Estatus { get; set; }
        public Nullable<int> ZonIva { get; set; }
        public Nullable<int> Id_U { get; set; }
        public Nullable<int> TMITipo { get; set; }
        public string TMINombre { get; set; }
        public Nullable<int> RemPedCli { get; set; }
        public string RemCtto { get; set; }
        public Nullable<System.DateTime> RemFVig { get; set; }
        public Nullable<int> Rem_ManAut { get; set; }
        public Nullable<int> Id_Vap { get; set; }
        public byte[] Rem_PDF { get; set; }
        public string Rem_OrdCompra { get; set; }
        public Nullable<System.DateTime> Rem_FechaHr { get; set; }
        public Nullable<int> Rem_CteCuentaNAcional { get; set; }
        public Nullable<int> Rem_CteCuentaContNacional { get; set; }
        public Nullable<System.DateTime> Rem_FecCan { get; set; }
        public Nullable<int> Id_UCancelo { get; set; }
        public Nullable<int> Id_TG { get; set; }
    
        public virtual CatCliente CatCliente { get; set; }
        public virtual CatTerritorio CatTerritorio { get; set; }
    }
}
