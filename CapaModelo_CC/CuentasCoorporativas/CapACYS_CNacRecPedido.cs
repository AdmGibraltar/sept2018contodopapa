//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaModelo_CC.CuentasCoorporativas
{
    using System;
    using System.Collections.Generic;
    
    public partial class CapACYS_CNacRecPedido
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Acs { get; set; }
        public int Id_AcsVersion { get; set; }
        public Nullable<bool> RecRevLunes { get; set; }
        public Nullable<bool> RecRevMartes { get; set; }
        public Nullable<bool> RecRevMiercoles { get; set; }
        public Nullable<bool> RecRevJueves { get; set; }
        public Nullable<bool> RecRevViernes { get; set; }
        public Nullable<bool> RecRevSabado { get; set; }
        public string RecRevHora1Ini { get; set; }
        public string RecRevHora1Fin { get; set; }
        public string RecRevHora2Ini { get; set; }
        public string RecRevHora2Fin { get; set; }
        public string RecPersonaRecibe { get; set; }
        public string RecPuesto { get; set; }
        public Nullable<bool> RecCitaMismoDia { get; set; }
        public Nullable<bool> RecCitaSinCita { get; set; }
        public Nullable<bool> RecCitaPrevia { get; set; }
        public string RecCitaContacto { get; set; }
        public string RecCitaTelefono { get; set; }
        public Nullable<int> RecCitaDiasdeAnticipacion { get; set; }
        public Nullable<bool> RecAreaPropia { get; set; }
        public Nullable<bool> RecAreaPlaza { get; set; }
        public Nullable<bool> RecAreaCalle { get; set; }
        public Nullable<bool> RecAreaAvTransitada { get; set; }
        public Nullable<bool> RecEstCortesia { get; set; }
        public Nullable<bool> RecEstCosto { get; set; }
        public Nullable<double> RecEstMonto { get; set; }
        public Nullable<bool> RecDocFactFranquiciaEnt { get; set; }
        public Nullable<int> RecDocFactFranquiciaEntCop { get; set; }
        public Nullable<bool> RecDocFactFranquiciaRec { get; set; }
        public Nullable<int> RecDocFactFranquiciaRecCop { get; set; }
        public Nullable<bool> RecDocFactKeyEnt { get; set; }
        public Nullable<int> RecDocFactKeyEntCop { get; set; }
        public Nullable<bool> RecDocFactKeyRec { get; set; }
        public Nullable<int> RecDocFactKeyRecCop { get; set; }
        public Nullable<bool> RecDocOrdCompraEnt { get; set; }
        public Nullable<int> RecDocOrdCompraEntCop { get; set; }
        public Nullable<bool> RecDocOrdCompraRec { get; set; }
        public Nullable<int> RecDocOrdCompraRecCop { get; set; }
        public Nullable<bool> RecDocOrdReposEnt { get; set; }
        public Nullable<int> RecDocOrdReposEntCop { get; set; }
        public Nullable<bool> RecDocOrdReposRec { get; set; }
        public Nullable<int> RecDocOrdReposRecCop { get; set; }
        public Nullable<bool> RecDocCopPedidoEnt { get; set; }
        public Nullable<int> RecDocCopPedidoEntCop { get; set; }
        public Nullable<bool> RecDocCopPedidoRec { get; set; }
        public Nullable<int> RecDocCopPedidoRecCop { get; set; }
        public Nullable<bool> RecDocRemisionEnt { get; set; }
        public Nullable<int> RecDocRemisionEntCop { get; set; }
        public Nullable<bool> RecDocRemisionRec { get; set; }
        public Nullable<int> RecDocRemisionRecCop { get; set; }
        public Nullable<bool> RecDocFolioEnt { get; set; }
        public Nullable<int> RecDocFolioEntCop { get; set; }
        public Nullable<bool> RecDocFolioRec { get; set; }
        public Nullable<int> RecDocFolioRecCop { get; set; }
        public Nullable<bool> RecDocContraRecEnt { get; set; }
        public Nullable<int> RecDocContraRecEntCop { get; set; }
        public Nullable<bool> RecDocContraRecRec { get; set; }
        public Nullable<int> RecDocContraRecRecCop { get; set; }
        public Nullable<bool> RecDocEntAlmacenEnt { get; set; }
        public Nullable<int> RecDocEntAlmacenEntCop { get; set; }
        public Nullable<bool> RecDocEntAlmacenRec { get; set; }
        public Nullable<int> RecDocEntAlmacenRecCop { get; set; }
        public Nullable<bool> RecDocSopServicioEnt { get; set; }
        public Nullable<int> RecDocSopServicioEntCop { get; set; }
        public Nullable<bool> RecDocSopServicioRec { get; set; }
        public Nullable<int> RecDocSopServicioRecCop { get; set; }
        public Nullable<bool> RecDocNomFirmaEnt { get; set; }
        public Nullable<int> RecDocNomFirmaEntCop { get; set; }
        public Nullable<bool> RecDocNomFirmaoRec { get; set; }
        public Nullable<int> RecDocNomFirmaRecCop { get; set; }
        public Nullable<bool> RecCitaEnt { get; set; }
        public Nullable<int> RecCitaEntCop { get; set; }
        public Nullable<bool> RecCitaRec { get; set; }
        public Nullable<int> RecCitaRecCop { get; set; }
    
        public virtual CapAcy CapAcys { get; set; }
    }
}
