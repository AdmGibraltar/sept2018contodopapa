using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Factura
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Fac;
        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }

        private int? _Id_Cfe;
        public int? Id_Cfe
        {
            get { return _Id_Cfe; }
            set { _Id_Cfe = value; }
        }

        private string _Id_FacSerie;
        public string Id_FacSerie
        {
            get { return _Id_FacSerie; }
            set { _Id_FacSerie = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private int _Id_Tm;
        public int Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }

        private int _Id_Tm_Rem;
        public int Id_Tm_Rem
        {
            get { return _Id_Tm_Rem; }
            set { _Id_Tm_Rem = value; }
        }

        private int? _Fac_PedNum;
        public int? Fac_PedNum
        {
            get { return _Fac_PedNum; }
            set { _Fac_PedNum = value; }
        }

        private string _Fac_PedDesc;
        public string Fac_PedDesc
        {
            get { return _Fac_PedDesc; }
            set { _Fac_PedDesc = value; }
        }

        private string _Fac_Req;
        public string Fac_Req
        {
            get { return _Fac_Req; }
            set { _Fac_Req = value; }
        }

        private DateTime _Fac_Fecha;
        public DateTime Fac_Fecha
        {
            get { return _Fac_Fecha; }
            set { _Fac_Fecha = value; }
        }


        private DateTime _Fac_FechaHr;
        public DateTime Fac_FechaHr
        {
            get { return _Fac_FechaHr; }
            set { _Fac_FechaHr = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private int? _Id_Ter;
        public int? Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int? _Id_Rik;
        public int? Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        private int? _Id_Mon;
        public int? Id_Mon
        {
            get { return _Id_Mon; }
            set { _Id_Mon = value; }
        }

        private bool? _Fac_DesgIva;
        public bool? Fac_DesgIva
        {
            get { return _Fac_DesgIva; }
            set { _Fac_DesgIva = value; }
        }

        private bool? _Fac_RetIva;
        public bool? Fac_RetIva
        {
            get { return _Fac_RetIva; }
            set { _Fac_RetIva = value; }
        }

        private string _Fac_CteNombre;
        public string Fac_CteNombre
        {
            get { return _Fac_CteNombre; }
            set { _Fac_CteNombre = value; }
        }


        private string _Fac_CteCalle;
        public string Fac_CteCalle
        {
            get { return _Fac_CteCalle; }
            set { _Fac_CteCalle = value; }
        }

        private string _Fac_CteNumero;
        public string Fac_CteNumero
        {
            get { return _Fac_CteNumero; }
            set { _Fac_CteNumero = value; }
        }

        private string _Fac_CteNumeroInterior;
        public string Fac_CteNumeroInterior
        {
            get { return _Fac_CteNumero; }
            set { _Fac_CteNumero = value; }
        }


        private string _Fac_CteCp;
        public string Fac_CteCp
        {
            get { return _Fac_CteCp; }
            set { _Fac_CteCp = value; }
        }

        private string _Fac_CteColonia;
        public string Fac_CteColonia
        {
            get { return _Fac_CteColonia; }
            set { _Fac_CteColonia = value; }
        }

        private string _Fac_CteMunicipio;
        public string Fac_CteMunicipio
        {
            get { return _Fac_CteMunicipio; }
            set { _Fac_CteMunicipio = value; }
        }


        private string _Fac_CteEstado;
        public string Fac_CteEstado
        {
            get { return _Fac_CteEstado; }
            set { _Fac_CteEstado = value; }
        }

        private string _Fac_CteRfc;
        public string Fac_CteRfc
        {
            get { return _Fac_CteRfc; }
            set { _Fac_CteRfc = value; }
        }

        private string _Fac_CteTel;
        public string Fac_CteTel
        {
            get { return _Fac_CteTel; }
            set { _Fac_CteTel = value; }
        }

        private string _Fac_OrdEntrega;
        public string Fac_OrdEntrega
        {
            get { return _Fac_OrdEntrega; }
            set { _Fac_OrdEntrega = value; }
        }

        private string _Fac_CondEntrega;
        public string Fac_CondEntrega
        {
            get { return _Fac_CondEntrega; }
            set { _Fac_CondEntrega = value; }
        }

        private int? _Fac_NumEntrega;
        public int? Fac_NumEntrega
        {
            get { return _Fac_NumEntrega; }
            set { _Fac_NumEntrega = value; }
        }

        private string _Fac_Notas;
        public string Fac_Notas
        {
            get { return _Fac_Notas; }
            set { _Fac_Notas = value; }
        }

        private double? _Fac_DescPorcen1;
        public double? Fac_DescPorcen1
        {
            get { return _Fac_DescPorcen1; }
            set { _Fac_DescPorcen1 = value; }
        }

        private string _Fac_Desc1;
        public string Fac_Desc1
        {
            get { return _Fac_Desc1; }
            set { _Fac_Desc1 = value; }
        }

        private double? _Fac_DescPorcen2;
        public double? Fac_DescPorcen2
        {
            get { return _Fac_DescPorcen2; }
            set { _Fac_DescPorcen2 = value; }
        }

        private string _Fac_Desc2;
        public string Fac_Desc2
        {
            get { return _Fac_Desc2; }
            set { _Fac_Desc2 = value; }
        }

        private string _Fac_Tipo;
        public string Fac_Tipo
        {
            get { return _Fac_Tipo; }
            set { _Fac_Tipo = value; }
        }

        private string _Fac_Conducto;
        public string Fac_Conducto
        {
            get { return _Fac_Conducto; }
            set { _Fac_Conducto = value; }
        }

        private string _Fac_NumeroGuia;
        public string Fac_NumeroGuia
        {
            get { return _Fac_NumeroGuia; }
            set { _Fac_NumeroGuia = value; }
        }

        private int? _Fac_CanNum;
        public int? Fac_CanNum
        {
            get { return _Fac_CanNum; }
            set { _Fac_CanNum = value; }
        }

        private DateTime? _Fac_FecCan;
        public DateTime? Fac_FecCan
        {
            get { return _Fac_FecCan; }
            set { _Fac_FecCan = value; }
        }

        private double? _Fac_Pagado;
        public double? Fac_Pagado
        {
            get { return _Fac_Pagado; }
            set { _Fac_Pagado = value; }
        }

        private DateTime? _Fac_FecPag;
        public DateTime? Fac_FecPag
        {
            get { return _Fac_FecPag; }
            set { _Fac_FecPag = value; }
        }

        private string _Fac_Estatus;
        public string Fac_Estatus
        {
            get { return _Fac_Estatus; }
            set { _Fac_Estatus = value; }
        }

        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        private string _consig_Dir;

        public string Consig_Dir
        {
            get { return _consig_Dir; }
            set { _consig_Dir = value; }
        }
        private string _consig_Num;

        public string Consig_Num
        {
            get { return _consig_Num; }
            set { _consig_Num = value; }
        }
        private string _consig_mun;

        public string Consig_mun
        {
            get { return _consig_mun; }
            set { _consig_mun = value; }
        }
        private string _consig_colonia;

        public string Consig_colonia
        {
            get { return _consig_colonia; }
            set { _consig_colonia = value; }
        }

        private string _consig_pais;

        public string Consig_pais
        {
            get { return _consig_pais; }
            set { _consig_pais = value; }
        }

        private bool? _PDF;
        public bool? PDF
        {
            get { return _PDF; }
            set { _PDF = value; }
        }

        private bool? _FXML;
        public bool? FXML
        {
            get { return _FXML; }
            set { _FXML = value; }
        }

        #region facturaciones especiales

        private int? _Id_Ped;
        public int? Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }

        private int? _Id_Es;
        public int? Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }

        //private string _Id_Rem_Lista;
        //public string Id_Rem_Lista
        //{
        //    get { return _Id_Rem_Lista; }
        //    set { _Id_Rem_Lista = value; }
        //}

        #endregion

        #region Depuracion

        private bool? _Fac_Depuracion;

        private string _Fac_DepuracionStr;

        public string Fac_DepuracionStr
        {
            get { return _Fac_DepuracionStr; }
            set { _Fac_DepuracionStr = value; }
        }


        public bool? Fac_Depuracion
        {
            get { return _Fac_Depuracion; }
            set { _Fac_Depuracion = value; }
        }

        private string _Fac_DepuracionMotivo;

        public string Fac_DepuracionMotivo
        {
            get { return _Fac_DepuracionMotivo; }
            set { _Fac_DepuracionMotivo = value; }
        }

        private string _Fac_DepuracionAutorizo;

        public string Fac_DepuracionAutorizo
        {
            get { return _Fac_DepuracionAutorizo; }
            set { _Fac_DepuracionAutorizo = value; }
        }

        private DateTime? _Fac_DepuracionFecha;

        public DateTime? Fac_DepuracionFecha
        {
            get { return _Fac_DepuracionFecha; }
            set { _Fac_DepuracionFecha = value; }
        }

        #endregion

        #region Propiedades extras o propiedades calculadas


        private string _Fac_Sello;
        public string Fac_Sello
        {
            get { return _Fac_Sello; }
            set { _Fac_Sello = value; }
        }

        private object _Fac_Xml;
        public object Fac_Xml
        {
            get { return _Fac_Xml; }
            set { _Fac_Xml = value; }
        }

        private byte[] _Fac_Pdf;
        public byte[] Fac_Pdf
        {
            get { return _Fac_Pdf; }
            set { _Fac_Pdf = value; }
        }

        private string _Serie;
        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private int? _Cd_IvaPedidosFacturacion;
        public int? Cd_IvaPedidosFacturacion
        {
            get { return _Cd_IvaPedidosFacturacion; }
            set { _Cd_IvaPedidosFacturacion = value; }
        }


        //esta propiedad sirve para el momento de la impresion de factura, consulta si tiene embarque
        private int? _Id_Emb;
        public int? Id_Emb
        {
            get { return _Id_Emb; }
            set { _Id_Emb = value; }
        }


        private int _Dias;
        public int Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }



        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        private string _Cte_Email;
        public string Cte_Email
        {
            get { return _Cte_Email; }
            set { _Cte_Email = value; }
        }

        private string _Fac_EstatusStr;
        public string Fac_EstatusStr
        {
            get { return _Fac_EstatusStr; }
            set { _Fac_EstatusStr = value; }
        }

        private string _Mon_Descripcion;
        public string Mon_Descripcion
        {
            get { return _Mon_Descripcion; }
            set { _Mon_Descripcion = value; }
        }

        private double _Mon_TipCambio;
        public double Mon_TipCambio
        {
            get { return _Mon_TipCambio; }
            set { _Mon_TipCambio = value; }
        }

        private bool? _TienePagos;
        public bool? TienePagos
        {
            get { return _TienePagos; }
            set { _TienePagos = value; }
        }

        private string _Fac_TipoStr;
        public string Fac_TipoStr
        {
            get { return _Fac_TipoStr; }
            set { _Fac_TipoStr = value; }
        }

        private string _U_Nombre;
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        private double? _Fac_Importe;
        public double? Fac_Importe
        {
            get { return _Fac_Importe; }
            set { _Fac_Importe = value; }
        }

        private double? _Fac_ImporteRetencion;
        public double? Fac_ImporteRetencion
        {
            get { return _Fac_ImporteRetencion; }
            set { _Fac_ImporteRetencion = value; }
        }

        private double? _Fac_SubTotal;
        public double? Fac_SubTotal
        {
            get { return _Fac_SubTotal; }
            set { _Fac_SubTotal = value; }
        }

        private double? _Fac_ImporteIva;
        public double? Fac_ImporteIva
        {
            get { return _Fac_ImporteIva; }
            set { _Fac_ImporteIva = value; }
        }

        private double? _Fac_Saldo;
        private string _Fac_Contacto;
        public string Mon_Unidad;
        public string Ter_Nombre;
        public int Folio_cancelacion;

        public string Fac_Contacto
        {
            get { return _Fac_Contacto; }
            set { _Fac_Contacto = value; }
        }
        public double? Fac_Saldo
        {
            get { return _Fac_Saldo; }
            set { _Fac_Saldo = value; }
        }

        #endregion

        private string _Fac_Refactura;
        public string Fac_UDigitos;
        public string Fac_FPago;

        public string Fac_Refactura
        {
            get { return _Fac_Refactura; }
            set { _Fac_Refactura = value; }
        }

        //Gerardo Ponce
        //Se agrega lo propiedad Fac_FolioFiscal
        private string _fac_FolioFiscal;

        public string Fac_FolioFiscal
        {
            get { return _fac_FolioFiscal; }
            set { _fac_FolioFiscal = value; }
        }

        private string _Fac_SelloCN;

        public string Fac_SelloCN
        {
            get { return _Fac_SelloCN; }
            set { _Fac_SelloCN = value; }
        }
        private string _fac_FolioFiscalCN;

        public string Fac_FolioFiscalCN
        {
            get { return _fac_FolioFiscalCN; }
            set { _fac_FolioFiscalCN = value; }
        }

        private byte[] _Fac_PdfCN;

        public byte[] Fac_PdfCN
        {
            get { return _Fac_PdfCN; }
            set { _Fac_PdfCN = value; }
        }

        private string _Fac_CteAdeNombre;

        public string Fac_CteAdeNombre
        {
            get { return _Fac_CteAdeNombre; }
            set { _Fac_CteAdeNombre = value; }
        }
        private int _Fac_CteAdeId;

        public int Fac_CteAdeId
        {
            get { return _Fac_CteAdeId; }
            set { _Fac_CteAdeId = value; }
        }

        private object _Fac_XmlCN;

        public object Fac_XmlCN
        {
            get { return _Fac_XmlCN; }
            set { _Fac_XmlCN = value; }
        }
        private bool? _FXMLCN;

        public bool? FXMLCN
        {
            get { return _FXMLCN; }
            set { _FXMLCN = value; }
        }

        private int? _Fac_FolioCN;

        public int? Fac_FolioCN
        {
            get { return _Fac_FolioCN; }
            set { _Fac_FolioCN = value; }
        }


        private string _Fac_EsRefactura;
        public string Fac_EsRefactura
        {
            get { return _Fac_EsRefactura; }
            set { _Fac_EsRefactura = value; }
        }

        private string _Fac_IdCausaRef;
        public string Fac_IdCausaRef
        {
            get { return _Fac_IdCausaRef; }
            set { _Fac_IdCausaRef = value; }
        }

        private DateTime? _Fac_FechaRef;
        public DateTime? Fac_FechaRef
        {
            get { return _Fac_FechaRef; }
            set { _Fac_FechaRef = value; }
        }

        private string _Fac_IdUsuRef;
        public string Fac_IdUsuRef
        {
            get { return _Fac_IdUsuRef; }
            set { _Fac_IdUsuRef = value; }
        }

        private string _Fac_TipoRef;
        public string Fac_TipoRef
        {
            get { return _Fac_TipoRef; }
            set { _Fac_TipoRef = value; }
        }

    }
}
