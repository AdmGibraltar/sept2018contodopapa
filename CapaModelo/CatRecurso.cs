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
    public partial class CatRecurso
    {
        public CatRecurso()
        {
            this.CrmPropuestaTecnicas = new HashSet<CrmPropuestaTecnica>();
            this.CrmPropuestaTecnicas1 = new HashSet<CrmPropuestaTecnica>();
            this.CatRecursoArchivoes = new HashSet<CatRecursoArchivo>();
            this.CatRecursoURLs = new HashSet<CatRecursoURL>();
            this.CapBibliotecaNodoes = new HashSet<CapBibliotecaNodo>();
        }
    
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Recurso { get; set; }
        public int Id_TipoRecurso { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatTipoRecurso CatTipoRecurso { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CrmPropuestaTecnica> CrmPropuestaTecnicas { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CrmPropuestaTecnica> CrmPropuestaTecnicas1 { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CatRecursoArchivo> CatRecursoArchivoes { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CatRecursoURL> CatRecursoURLs { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapBibliotecaNodo> CapBibliotecaNodoes { get; set; }
    }
}
