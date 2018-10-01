using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PagoElectronico
    {
        public PagoElectronico()
        {
            this.PagElecArchivo = new List<PagoElectronicoArchivo>();
        }

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
        private int _id_PagElec;

        public int Id_PagElec
        {
            get { return _id_PagElec; }
            set { _id_PagElec = value; }
        }

        private int _id_PagElecTipo;
        public int Id_PagElecTipo
        {
            get { return _id_PagElecTipo; }
            set { _id_PagElecTipo = value; }
        }

        public int Id_PagElecSubTipo { get; set; }

        public int Id_PagElecCuenta { get; set; }

        private int _id_Acr;
        private int? _id_Acr_Filtro;

        public int? Id_Acr_Filtro
        {
            get { return _id_Acr_Filtro; }
            set { _id_Acr_Filtro = value; }
        }
        private int? _id_PagElecTipo_Filtro;

        public int? Id_PagElecTipo_Filtro
        {
            get { return _id_PagElecTipo_Filtro; }
            set { _id_PagElecTipo_Filtro = value; }
        }

        public int? Id_PagElecCuenta_Filtro { get; set; }

        public int Id_AcrCheque { get; set; }

        public int Id_Acr
        {
            get { return _id_Acr; }
            set { _id_Acr = value; }
        }
        private string _pagElec_Solicitante;

        public string PagElec_Solicitante
        {
            get { return _pagElec_Solicitante; }
            set { _pagElec_Solicitante = value; }
        }
        private DateTime? _pagElec_FechaRequiere;

        public DateTime? PagElec_FechaRequiere
        {
            get { return _pagElec_FechaRequiere; }
            set { _pagElec_FechaRequiere = value; }
        }
        private string _pagElec_Cuenta;

        public string PagElec_Cuenta
        {
            get { return _pagElec_Cuenta; }
            set { _pagElec_Cuenta = value; }
        }
        private string _pagElec_Cc;

        public string PagElec_Cc
        {
            get { return _pagElec_Cc; }
            set { _pagElec_Cc = value; }
        }
        private string _pagElec_Numero;

        public string PagElec_Numero
        {
            get { return _pagElec_Numero; }
            set { _pagElec_Numero = value; }
        }
        private decimal _pagElec_Importe;

        public decimal PagElec_Importe
        {
            get { return _pagElec_Importe; }
            set { _pagElec_Importe = value; }
        }
        private string _pagElec_Observaciones;

        public string PagElec_Observaciones
        {
            get { return _pagElec_Observaciones; }
            set { _pagElec_Observaciones = value; }
        }

        private object _pagElec_Xml;
        public object PagElec_Xml
        {
            get { return _pagElec_Xml; }
            set { _pagElec_Xml = value; }
        }

        private byte[] _pagElec_Pdf;
        public byte[] PagElec_Pdf
        {
            get { return _pagElec_Pdf; }
            set { _pagElec_Pdf = value; }
        }

        public List<PagoElectronicoArchivo> PagElecArchivo { get; set; }

        private int _pagElec_IdU;
        public int PagElec_IdU
        {
            get { return _pagElec_IdU; }
            set { _pagElec_IdU = value; }
        }
        private DateTime _pagElec_FechaRegistro;

        private string _acr_Nombre;
        private string _pagElecTipo_Descrpcion;
        private string _pagElec_SubCuenta;
        private string _pagElec_SubSubCuenta;
        private string _pagElec_CuentaPago;
        private bool _pagElec_Autorizado;
        private byte[] _pagElec_Soporte;
        private string _pagElec_NumeroReferencia;
        private int _id_PagElecEstatus;
        private string _pagElecEstatus_Descripcion;
        private int? _id_PagElecEstatus_Filtro;
        private DateTime? _pagElec_FechaSalida;
        private DateTime? _pagElec_FechaUltMod;
        private string _pagElec_Destino;
        private string _pagElec_MotivoRechazo;
        private DateTime? _pagElec_FechaRegreso;
        private int? _PagElec_Dias;
        private int? _Id_GV;
        private decimal _pagElec_SoporteImporte;
        private string _pagElec_SubTipoDescripcion;

        public decimal PagElec_SoporteImporte
        {
            get { return _pagElec_SoporteImporte; }
            set { _pagElec_SoporteImporte = value; }
        }
        public DateTime PagElec_FechaRegistro
        {
            get { return _pagElec_FechaRegistro; }
            set { _pagElec_FechaRegistro = value; }
        }

        public string pagElecCuenta_Descripcion { get; set; }

        public string AcrCheque_Nombre { get; set; }

        public string Acr_Nombre
        {
            get { return _acr_Nombre; }
            set { _acr_Nombre = value; }
        }

        public string PagElecTipo_Descrpcion
        {
            get { return _pagElecTipo_Descrpcion; }
            set { _pagElecTipo_Descrpcion = value; }
        }

        public string PagElec_SubCuenta
        {
            get { return _pagElec_SubCuenta; }
            set { _pagElec_SubCuenta = value; }
        }
 
        public string PagElec_SubSubCuenta
        {
            get { return _pagElec_SubSubCuenta; }
            set { _pagElec_SubSubCuenta = value; }
        }
  
        public string PagElec_CuentaPago
        {
            get { return _pagElec_CuentaPago; }
            set { _pagElec_CuentaPago = value; }
        }

        public bool PagElec_Autorizado
        {
            get { return _pagElec_Autorizado; }
            set { _pagElec_Autorizado = value; }
        }

        public byte[] PagElec_Soporte
        {
            get { return _pagElec_Soporte; }
            set { _pagElec_Soporte = value; }
        }

        public string PagElec_Soporte_Nombre { get; set; }
        public string PagElec_Soporte_Tipo { get; set; }

        //JFCV 29-09-2015 cambiar de tipo int por string
    
        public string PagElec_NumeroReferencia
        {
            get { return _pagElec_NumeroReferencia; }
            set { _pagElec_NumeroReferencia = value; }
        }

        public int Id_PagElecEstatus
        {
            get { return _id_PagElecEstatus; }
            set { _id_PagElecEstatus = value; }
        }

        public string PagElecEstatus_Descripcion
        {
            get { return _pagElecEstatus_Descripcion; }
            set { _pagElecEstatus_Descripcion = value; }
        }

        public int? Id_PagElecEstatus_Filtro
        {
            get { return _id_PagElecEstatus_Filtro; }
            set { _id_PagElecEstatus_Filtro = value; }
        }

        public string Acr_NumeroGenerado { get; set; }
        //JFCV 03 nov 2015 agregar fecha salida y fecha ult mod 

        public DateTime? PagElec_FechaSalida
        {
            get { return _pagElec_FechaSalida; }
            set { _pagElec_FechaSalida = value; }
        }

        public DateTime? PagElec_FechaUltMod
        {
            get { return _pagElec_FechaUltMod; }
            set { _pagElec_FechaUltMod = value; }
        }
        //JFCV 17 dic 2015 agregar motivo rechazo y destino 
      
        public string PagElec_Destino
        {
            get { return _pagElec_Destino; }
            set { _pagElec_Destino = value; }
        }

        public string PagElec_MotivoRechazo
        {
            get { return _pagElec_MotivoRechazo; }
            set { _pagElec_MotivoRechazo = value; }
        }

        public DateTime? PagElec_FechaRegreso
        {
            get { return _pagElec_FechaRegreso; }
            set { _pagElec_FechaRegreso = value; }
        }

   
        public int? PagElec_Dias
        {
            get { return _PagElec_Dias; }
            set { _PagElec_Dias = value; }
        }



        public int? Id_GV
        {
            get { return _Id_GV; }
            set { _Id_GV = value; }
        }

        //JFCV 18nov2016

        public string PagElec_SubTipoDescripcion
        {
            get { return _pagElec_SubTipoDescripcion; }
            set { _pagElec_SubTipoDescripcion = value; }
        }
    }
}
