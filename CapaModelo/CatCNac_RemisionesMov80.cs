//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaModelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class CatCNac_RemisionesMov80
    {
        public CatCNac_RemisionesMov80()
        {
            this.CatCNac_Solicitudes = new HashSet<CatCNac_Solicitudes>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<CatCNac_Solicitudes> CatCNac_Solicitudes { get; set; }
    }
}