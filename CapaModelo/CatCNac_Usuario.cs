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
    
    public partial class CatCNac_Usuario
    {
        public CatCNac_Usuario()
        {
            this.CatCNac_Solicitudes = new HashSet<CatCNac_Solicitudes>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdMatriz { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string CDIK { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }
        public Nullable<int> Rol_Auditorias { get; set; }
        public Nullable<int> Rol_Ecommerce { get; set; }
        public Nullable<bool> AdminCliente { get; set; }
    
        public virtual ICollection<CatCNac_Solicitudes> CatCNac_Solicitudes { get; set; }
    }
}
