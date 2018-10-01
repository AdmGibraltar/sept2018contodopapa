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
    public partial class CatBiblioteca
    {
        public CatBiblioteca()
        {
            this.CapBibliotecaNodoes = new HashSet<CapBibliotecaNodo>();
            this.CapBibliotecaUsuarios = new HashSet<CapBibliotecaUsuario>();
        }
    
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Biblioteca { get; set; }
        public string Biblio_Nombre { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapBibliotecaNodo> CapBibliotecaNodoes { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual ICollection<CapBibliotecaUsuario> CapBibliotecaUsuarios { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatEmpresa CatEmpresa { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatCentroDistribucion CatCentroDistribucion { get; set; }
    }
}