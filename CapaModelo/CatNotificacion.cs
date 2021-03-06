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
    public partial class CatNotificacion
    {
        public CatNotificacion()
        {
            this.CapRIKNotificacions = new HashSet<CapRIKNotificacion>();
            this.CapUsuarioNotificacions = new HashSet<CapUsuarioNotificacion>();
        }
    
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Notificacion { get; set; }
        public int Id_TipoNotificacion { get; set; }
        public string Notif_Contenido { get; set; }
        public bool Notif_Leida { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapRIKNotificacion> CapRIKNotificacions { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatEmpresa CatEmpresa { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapUsuarioNotificacion> CapUsuarioNotificacions { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatCentroDistribucion CatCentroDistribucion { get; set; }
    }
}
