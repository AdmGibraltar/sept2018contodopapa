//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaModelo.SIANCentral
{
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    using System.Web.Script.Serialization;
    using Newtonsoft.Json;
    //[Serializable]
    public partial class CatCuotasCrm
    {
        public int Id_Cd { get; set; }
        public int Id_rik { get; set; }
        public int Cuo_Anio { get; set; }
        public int Cuo_Mes { get; set; }
        public Nullable<double> Cuo_MontoProy { get; set; }
        public Nullable<double> Cuo_MontoCierre { get; set; }
        public Nullable<int> Cuo_NumProy { get; set; }
        public Nullable<int> Cuo_NumProyCierre { get; set; }
    }
}