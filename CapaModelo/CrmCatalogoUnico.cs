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
    public partial class CrmCatalogoUnico
    {
        public int Id_Emp { get; set; }
        public int Id_Uen { get; set; }
        public int Id_Seg { get; set; }
        public int Id_Area { get; set; }
        public int Id_Sol { get; set; }
        public int Id_Apl { get; set; }
        public int Id_SubFam { get; set; }
        public int Id_Prd { get; set; }
        public Nullable<bool> CrmCU_EsQuimico { get; set; }
    
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatProducto CatProducto { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatSegmento CatSegmento { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatSolucion CatSolucion { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatUEN CatUEN { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatArea CatArea { get; set; }
    	[IgnoreDataMember]
    	[ScriptIgnore]
    	[JsonIgnore]
        public virtual CatAplicacion CatAplicacion { get; set; }
    }
}
