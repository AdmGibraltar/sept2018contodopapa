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
    
    using System.Runtime.Serialization;
    using System.Web.Script.Serialization;
    using Newtonsoft.Json;
    //[Serializable]
    public partial class CatAplicacion
    {
        public CatAplicacion()
        {
            this.CrmCatalogoUnicoes = new HashSet<CrmCatalogoUnico>();
            this.CrmOportunidadesProductos = new HashSet<CrmOportunidadesProducto>();
            this.CrmOportunidadesAplicacions = new HashSet<CrmOportunidadesAplicacion>();
            this.CapAplicacionProductoes = new HashSet<CapAplicacionProducto>();
        }
    
        public int Id_Emp { get; set; }
        public int Id_Apl { get; set; }
        public string Apl_Descripcion { get; set; }
        public Nullable<int> Id_Sol { get; set; }
        public Nullable<double> Apl_Potencial { get; set; }
        public Nullable<bool> Apl_Limpieza { get; set; }
        public Nullable<bool> Apl_Activo { get; set; }
        public Nullable<int> Clave_Aplicacion { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CrmCatalogoUnico> CrmCatalogoUnicoes { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CrmOportunidadesProducto> CrmOportunidadesProductos { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CrmOportunidadesAplicacion> CrmOportunidadesAplicacions { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatSolucion CatSolucion { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapAplicacionProducto> CapAplicacionProductoes { get; set; }
    }
}