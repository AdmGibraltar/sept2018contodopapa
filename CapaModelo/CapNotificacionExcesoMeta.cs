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
    public partial class CapNotificacionExcesoMeta
    {
        public int Id { get; set; }
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public string Nem_Asunto { get; set; }
        public System.DateTime Nem_FechaEnvio { get; set; }
        public string Nem_Cuerpo { get; set; }
        public byte[] Nem_contenidoArchivoAdjunto { get; set; }
    }
}
