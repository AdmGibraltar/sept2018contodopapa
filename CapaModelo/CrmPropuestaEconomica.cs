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
    public partial class CrmPropuestaEconomica
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Rik { get; set; }
        public int Id_Val { get; set; }
        public int Id_Prd { get; set; }
        public string PropEc_Dilucion { get; set; }
        public string PropEc_CostoUso { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatProducto CatProducto { get; set; }
    }
}
