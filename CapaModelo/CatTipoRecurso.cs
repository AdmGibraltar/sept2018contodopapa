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
    public partial class CatTipoRecurso
    {
        public CatTipoRecurso()
        {
            this.CatRecursoes = new HashSet<CatRecurso>();
        }
    
        public int Id_TipoRecurso { get; set; }
        public string TipoRec_Nombre { get; set; }
        public string TipoRec_Descripcion { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CatRecurso> CatRecursoes { get; set; }
    }
}
