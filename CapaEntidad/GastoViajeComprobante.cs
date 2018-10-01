using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class GastoViajeComprobante
    {
        private int _id_Emp;

        public int Id_Emp
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        private int _id_Cd;

        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }

        private int _id_GV;
        public int Id_GV
        {
            get { return _id_GV; }
            set { _id_GV = value; }
        }

        private int _id_GVComprobante;
        public int Id_GVComprobante
        {
            get { return _id_GVComprobante; }
            set { _id_GVComprobante = value; }
        }
        private DateTime? _gVComprobante_Fecha;

        public DateTime? GVComprobante_Fecha
        {
            get { return _gVComprobante_Fecha; }
            set { _gVComprobante_Fecha = value; }
        }
        private int _id_GVComprobanteTipo;

        public int Id_GVComprobanteTipo
        {
            get { return _id_GVComprobanteTipo; }
            set { _id_GVComprobanteTipo = value; }
        }

        private string _gVComprobanteTipo_Descripcion;

        public string GVComprobanteTipo_Descripcion
        {
            get { return _gVComprobanteTipo_Descripcion; }
            set { _gVComprobanteTipo_Descripcion = value; }
        }
        private bool _gVComprobante_ConComprobante;

        public bool GVComprobante_ConComprobante
        {
            get { return _gVComprobante_ConComprobante; }
            set { _gVComprobante_ConComprobante = value; }
        }

        private string _gVComprobante_ConComprobanteDescripcion;

        public string GVComprobante_ConComprobanteDescripcion
        {
            get { return _gVComprobante_ConComprobanteDescripcion; }
            set { _gVComprobante_ConComprobanteDescripcion = value; }
        }
        private decimal _gVComprobante_Importe;

        public decimal GVComprobante_Importe
        {
            get { return _gVComprobante_Importe; }
            set { _gVComprobante_Importe = value; }
        }

        private object _gVComprobante_Xml;
        public object GVComprobante_Xml
        {
            get { return _gVComprobante_Xml; }
            set { _gVComprobante_Xml = value; }
        }

        public byte[] GVComprobante_XmlStream { get; set; }
        
        private byte[] _gVComprobante_Pdf;
        public byte[] GVComprobante_Pdf
        {
            get { return _gVComprobante_Pdf; }
            set { _gVComprobante_Pdf = value; }
        }

        private string _gVComprobante_Observaciones;
        public string GVComprobante_Observaciones
        {
            get { return _gVComprobante_Observaciones; }
            set { _gVComprobante_Observaciones = value; }
        }

        //JFCV 16 oct agregue serie, folio, centa cc, cuentapago 

        private string _gVComprobante_Serie;
        public string GVComprobante_Serie
        {
            get { return _gVComprobante_Serie; }
            set { _gVComprobante_Serie = value; }
        }

        private string _gVComprobante_Folio;
        public string GVComprobante_Folio
        {
            get { return _gVComprobante_Folio; }
            set { _gVComprobante_Folio = value; }
        }

        private string _gVComprobante_GV_Cuenta;
        public string GVComprobante_GV_Cuenta
        {
            get { return _gVComprobante_GV_Cuenta; }
            set { _gVComprobante_GV_Cuenta = value; }
        }

        private string _gVComprobante_GV_Cc;
        public string GVComprobante_GV_Cc
        {
            get { return _gVComprobante_GV_Cc; }
            set { _gVComprobante_GV_Cc = value; }
        }

        private string _gVComprobante_GV_CuentaPago;
        public string GVComprobante_GV_CuentaPago
        {
            get { return _gVComprobante_GV_CuentaPago; }
            set { _gVComprobante_GV_CuentaPago = value; }
        }

        private string _gVComprobante_GV_Numero;
        public string GVComprobante_GV_Numero
        {
            get { return _gVComprobante_GV_Numero; }
            set { _gVComprobante_GV_Numero = value; }
        }

        private string _gVComprobante_GV_SubCuenta;
        public string GVComprobante_GV_SubCuenta
        {
            get { return _gVComprobante_GV_SubCuenta; }
            set { _gVComprobante_GV_SubCuenta = value; }
        }

        private string _gVComprobante_GV_SubSubCuenta;
        public string GVComprobante_GV_SubSubCuenta
        {
            get { return _gVComprobante_GV_SubSubCuenta; }
            set { _gVComprobante_GV_SubSubCuenta = value; }
        }


         private string _gVComprobante_GV_PagElec_Rfc;
        public string GVComprobante_GV_PagElec_Rfc
        {
            get { return _gVComprobante_GV_PagElec_Rfc; }
            set { _gVComprobante_GV_PagElec_Rfc = value; }
        }

         private string _gVComprobante_GV_PagElec_Soporte_Tipo;
        public string GVComprobante_GV_PagElec_Soporte_Tipo
        {
            get { return _gVComprobante_GV_PagElec_Soporte_Tipo; }
            set { _gVComprobante_GV_PagElec_Soporte_Tipo = value; }
        }

         private string _gVComprobante_GV_PagElec_Soporte_Nombre;
        public string GVComprobante_GV_PagElec_Soporte_Nombre
        {
            get { return _gVComprobante_GV_PagElec_Soporte_Nombre; }
            set { _gVComprobante_GV_PagElec_Soporte_Nombre = value; }
        }


         private string _gVComprobante_GV_PagElec_Id_PagElecCuenta;
        public string GVComprobante_GV_PagElec_Id_PagElecCuenta
        {
            get { return _gVComprobante_GV_PagElec_Id_PagElecCuenta; }
            set { _gVComprobante_GV_PagElec_Id_PagElecCuenta = value; }
        }

         
    }
}
