using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
 


    public class PagoElectronicoArchivo
    {
        public int? Id_Emp { get; set; }
        public int? Id_Cd { get; set; }
        public int? Id_PagElec { get; set; }
        public int? id_RowFactura { get; set; }
        public byte[] PagElec_PDFStream { get; set; }
        public byte[] PagElec_XMLStream { get; set; }
        public DateTime? FechaFactura { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public decimal? Importe { get; set; }
        public string PagElec_Cuenta   { get; set; }    
        public string PagElec_Cc   { get; set; }    
        public string PagElec_Numero   { get; set; }    
        public string PagElec_SubCuenta   { get; set; }    
        public string PagElec_SubSubCuenta   { get; set; }    
        public string PagElec_CuentaPago   { get; set; }
        public string PagElec_Observaciones { get; set; }
        public int? Id_PagElecCuenta { get; set; }
        public string PagElec_RFC { get; set; }
        public string PagElec_UUID { get; set; }
        public decimal? PagElec_Subtotal { get; set; }
        public decimal? PagElec_Iva { get; set; }
        public decimal? PagElec_ImpRetenido { get; set; }
        public decimal? PagElec_IvaRetenido { get; set; }
        public string PagElec_ConSoporte { get; set; }
        //JFCV 02 feb 2016 
     

        public PagoElectronicoArchivo()
        { 
            this.Id_Emp = null;
            this.Id_Cd = null;
            this.Id_PagElec = null;
            this.id_RowFactura = null;
            this.PagElec_PDFStream = null;
            this.PagElec_XMLStream = null;
            this.FechaFactura = null;
            this.Serie = null;
            this.Folio = null;
            this.Importe = null;
            this.PagElec_Cuenta = null;
            this.PagElec_Cc = null;
            this.PagElec_Numero = null;  
            this.PagElec_SubCuenta = null;  
            this.PagElec_SubSubCuenta = null;  
            this.PagElec_CuentaPago = null;  
            this.PagElec_Observaciones = null;
            this.Id_PagElecCuenta = null;
            this.PagElec_RFC = null;
            this.PagElec_UUID = null;
            this.PagElec_Subtotal = null;
            this.PagElec_Iva = null;
            this.PagElec_ImpRetenido = null;
            this.PagElec_IvaRetenido = null;
            this.PagElec_ConSoporte = null;
       }
        //02 feb 2016
         
        public PagoElectronicoArchivo(
            int? id_RowFactura, 
            byte[] PagElec_PDFStreamList, 
            byte[] PagElec_XMLStreamList, 
            DateTime? FechaFacturaList, 
            string SerieList, 
            string FolioList, 
            decimal? ImporteList,
            string PagElec_CuentaList,
            string PagElec_CcList, 
            string PagElec_NumeroList,
            string PagElec_SubCuentaList,
            string PagElec_SubSubCuentaList,
            string PagElec_CuentaPagoList,
            string PagElec_ObservacionesList,
            int Id_PagElecCuentaList,
            string PagElec_RFCList,
            string PagElec_UUIDList,
            decimal? PagElec_SubtotalList,
            decimal? PagElec_IvaList,
            decimal? PagElec_ImpRetenidoList,
            decimal? PagElec_IvaRetenidoList, 
            string PagElec_ConSoporteList
            )
            : this(null, null, null, id_RowFactura, PagElec_PDFStreamList, PagElec_XMLStreamList, FechaFacturaList, SerieList, FolioList, ImporteList, PagElec_CuentaList, PagElec_CcList, PagElec_NumeroList, PagElec_SubCuentaList, PagElec_SubSubCuentaList, PagElec_CuentaPagoList, PagElec_ObservacionesList, Id_PagElecCuentaList, PagElec_RFCList, PagElec_UUIDList, PagElec_SubtotalList, PagElec_IvaList, PagElec_ImpRetenidoList, PagElec_IvaRetenidoList, PagElec_ConSoporteList) { }
     
        public PagoElectronicoArchivo(
            int? Id_Emp, 
            int? Id_Cd, 
            int? Id_PagElec, 
            int? id_RowFactura, 
            byte[] PagElec_PDFStreamList, 
            byte[] PagElec_XMLStreamList, 
            DateTime? FechaFacturaList, 
            string SerieList, 
            string FolioList, 
            decimal? ImporteList,
            string PagElec_CuentaList,
            string PagElec_CcList, 
            string PagElec_NumeroList,
            string PagElec_SubCuentaList,
            string PagElec_SubSubCuentaList,
            string PagElec_CuentaPagoList,
            string PagElec_ObservacionesList,
            int? Id_PagElecCuentaList,
            string PagElec_RFCList,
            string PagElec_UUIDList,
            decimal? PagElec_SubtotalList,
            decimal? PagElec_IvaList,
            decimal? PagElec_ImpRetenidoList,
            decimal? PagElec_IvaRetenidoList, 
            string PagElec_ConSoporteList
        )
        {
            this.Id_Emp = Id_Emp;
            this.Id_Cd = Id_Cd;
            this.Id_PagElec = Id_PagElec;
            this.id_RowFactura = id_RowFactura;
            this.PagElec_PDFStream = PagElec_PDFStreamList;
            this.PagElec_XMLStream = PagElec_XMLStreamList;
            this.FechaFactura = FechaFacturaList;
            this.Serie = SerieList;
            this.Folio = FolioList;
            this.Importe = ImporteList;
            this.PagElec_Cuenta = PagElec_CuentaList;
            this.PagElec_Cc = PagElec_CcList;
            this.PagElec_Numero = PagElec_NumeroList;  
            this.PagElec_SubCuenta = PagElec_SubCuentaList;  
            this.PagElec_SubSubCuenta = PagElec_SubSubCuentaList;  
            this.PagElec_CuentaPago = PagElec_CuentaPagoList;  
            this.PagElec_Observaciones = PagElec_ObservacionesList;
            this.Id_PagElecCuenta = Id_PagElecCuentaList;
            this.PagElec_RFC = PagElec_RFCList;
            this.PagElec_UUID = PagElec_UUIDList;
            this.PagElec_Subtotal = PagElec_SubtotalList;
            this.PagElec_Iva = PagElec_IvaList;
            this.PagElec_ImpRetenido = PagElec_ImpRetenidoList;
            this.PagElec_IvaRetenido = PagElec_IvaRetenidoList;
            this.PagElec_ConSoporte = PagElec_ConSoporteList;
        }
    }
}
