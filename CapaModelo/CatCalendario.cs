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
    public partial class CatCalendario
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cal { get; set; }
        public int Cal_Año { get; set; }
        public Nullable<int> Cal_Mes { get; set; }
        public Nullable<System.DateTime> Cal_FechaIni { get; set; }
        public Nullable<System.DateTime> Cal_FechaFin { get; set; }
        public Nullable<bool> Cal_Actual { get; set; }
        public Nullable<bool> Cal_Activo { get; set; }
        public Nullable<System.DateTime> Cal_FechaExtemporaneo { get; set; }
    }
}
