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
    public partial class CatRecursoURL
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Recurso { get; set; }
        public int Id_U { get; set; }
        public int Id_RecURL { get; set; }
        public string RecURL_URL { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatRecurso CatRecurso { get; set; }
    }
}
