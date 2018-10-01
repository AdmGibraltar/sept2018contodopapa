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
    public partial class CatRik
    {
        public CatRik()
        {
            this.CapAcys = new HashSet<CapAcy>();
            this.CapRIKNotificacions = new HashSet<CapRIKNotificacion>();
            this.CatClientes = new HashSet<CatCliente>();
            this.CatTerritorios = new HashSet<CatTerritorio>();
            this.CrmProspectoes = new HashSet<CrmProspecto>();
        }
    
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Rik { get; set; }
        public Nullable<int> Id_Reg { get; set; }
        public string Rik_Nombre { get; set; }
        public string Rik_Calle { get; set; }
        public string Rik_Numero { get; set; }
        public string Rik_Colonia { get; set; }
        public string Rik_Tel { get; set; }
        public Nullable<System.DateTime> Rik_FecAlta { get; set; }
        public Nullable<double> Rik_Contribucion { get; set; }
        public Nullable<double> Rik_Compesacion { get; set; }
        public Nullable<bool> Rik_Pertenece { get; set; }
        public Nullable<int> Rik_Gte { get; set; }
        public Nullable<bool> Rik_Activo { get; set; }
        public Nullable<int> Tipo_Rep { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapAcy> CapAcys { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapRIKNotificacion> CapRIKNotificacions { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatCentroDistribucion CatCentroDistribucion { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CatCliente> CatClientes { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CatTerritorio> CatTerritorios { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CrmProspecto> CrmProspectoes { get; set; }
    }
}
