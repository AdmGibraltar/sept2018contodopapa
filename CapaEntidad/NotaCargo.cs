using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class NotaCargo
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
        
        private int _Id_Nca;
        public int Id_Nca
        {
            get { return _Id_Nca; }
            set { _Id_Nca = value; }
        }

        private int? _Id_Cfe;
        public int? Id_Cfe
        {
            get { return _Id_Cfe; }
            set { _Id_Cfe = value; }
        }

        private string _Id_NcaSerie;
        public string Id_NcaSerie
        {
            get { return _Id_NcaSerie; }
            set { _Id_NcaSerie = value; }
        }

        private int _Id_Reg;
        public int Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private DateTime _Nca_Fecha;
        public DateTime Nca_Fecha
        {
            get { return _Nca_Fecha; }
            set { _Nca_Fecha = value; }
        }


        private DateTime _Nca_FechaHr;
        public DateTime Nca_FechaHr
        {
            get { return _Nca_FechaHr; }
            set { _Nca_FechaHr = value; }
        }

        private int _Nca_Tipo;
        public int Nca_Tipo
        {
            get { return _Nca_Tipo; }
            set { _Nca_Tipo = value; }
        }

        private int _Id_Tm;
        public int Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }

        private int? _Id_Ban;
        public int? Id_Ban
        {
            get { return _Id_Ban; }
            set { _Id_Ban = value; }
        }

        private string _Nca_CtaContable;
        public string Nca_CtaContable
        {
            get { return _Nca_CtaContable; }
            set { _Nca_CtaContable = value; }
        }

        private bool _Nca_Desgloce;
        public bool Nca_Desgloce
        {
            get { return _Nca_Desgloce; }
            set { _Nca_Desgloce = value; }
        }

        private int _Nca_Referencia;
        public int Nca_Referencia
        {
            get { return _Nca_Referencia; }
            set { _Nca_Referencia = value; }
        }

        private string _Nca_Notas;
        public string Nca_Notas
        {
            get { return _Nca_Notas; }
            set { _Nca_Notas = value; }
        }

        private double _Nca_RSubtotal;
        public double Nca_RSubtotal
        {
            get { return _Nca_RSubtotal; }
            set { _Nca_RSubtotal = value; }
        }

        private double _Nca_RIva;
        public double Nca_RIva
        {
            get { return _Nca_RIva; }
            set { _Nca_RIva = value; }
        }

        private double _Nca_RTotal;
        public double Nca_RTotal
        {
            get { return _Nca_RTotal; }
            set { _Nca_RTotal = value; }
        }

        private double _Nca_Subtotal;
        public double Nca_Subtotal
        {
            get { return _Nca_Subtotal; }
            set { _Nca_Subtotal = value; }
        }

        private double _Nca_Iva;
        public double Nca_Iva
        {
            get { return _Nca_Iva; }
            set { _Nca_Iva = value; }
        }

        private double _Nca_Total;
        public double Nca_Total
        {
            get { return _Nca_Total; }
            set { _Nca_Total = value; }
        }

        private DateTime? _Nca_FecPag;
        public DateTime? Nca_FecPag
        {
            get { return _Nca_FecPag; }
            set { _Nca_FecPag = value; }
        }

        private string _Nca_Estatus;
        public string Nca_Estatus
        {
            get { return _Nca_Estatus; }
            set { _Nca_Estatus = value; }
        }

        private double _Importe;
        public double Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        private double _Nca_Pagado;
        public double Nca_Pagado
        {
            get { return _Nca_Pagado; }
            set { _Nca_Pagado = value; }
        }

        private double _Nca_Saldo;
        public double Nca_Saldo
        {
            get { return _Nca_Saldo; }
            set { _Nca_Saldo = value; }
        }

        private List<NotaCargoDet> _ListaNotaCargo;
        public List<NotaCargoDet> ListaNotaCargo
        {
            get { return _ListaNotaCargo; }
            set { _ListaNotaCargo = value; }
        }

        private string _Serie;
        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private string _Nca_Sello;
        public string Nca_Sello
        {
            get { return _Nca_Sello; }
            set { _Nca_Sello = value; }
        }
        
        private string _Ade_Campo;

        public string Ade_Campo
        {
            get { return _Ade_Campo; }
            set { _Ade_Campo = value; }
        }
        private string _Ade_Longitud;

        public string Ade_Longitud
        {
            get { return _Ade_Longitud; }
            set { _Ade_Longitud = value; }
        }
        private string clp_Descripcion;

        public string Clp_Descripcion
        {
            get { return clp_Descripcion; }
            set { clp_Descripcion = value; }
        }

        private int id_prd;

        public int Id_Prd
        {
            get { return id_prd; }
            set { id_prd = value; }
        }

        private object _Nca_Xml;
        public object Nca_Xml
        {
            get { return _Nca_Xml; }
            set { _Nca_Xml = value; }
        }

        private byte[] _Nca_Pdf;
        public byte[] Nca_Pdf
        {
            get { return _Nca_Pdf; }
            set { _Nca_Pdf = value; }
        }

        private bool? _PDF;
        public bool? PDF
        {
            get { return _PDF; }
            set { _PDF = value; }
        }

        private bool? _NcaXML;
        public bool? NcaXML
        {
            get { return _NcaXML; }
            set { _NcaXML = value; }
        }

        #region Propiedades calculadas

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        
        private string _Nca_TipoStr;
        public string Nca_TipoStr
        {
            get { return _Nca_TipoStr; }
            set { _Nca_TipoStr = value; }
        }

        private string _Nca_EstatusStr;
        public string Ter_Nombre;
        private string _U_Nombre;
        public string Nca_FPago;
        public string Nca_UDigitos;
        public string Tm_Nombre;

        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }
        public string Nca_EstatusStr
        {
            get { return _Nca_EstatusStr; }
            set { _Nca_EstatusStr = value; }
        }
        

        #endregion

        //Gerardo Ponce
        //Se agrego propiedad para el Folio Fiscal

        private string _nca_FolioFiscal;

        public string Nca_FolioFiscal
        {
            get { return _nca_FolioFiscal; }
            set { _nca_FolioFiscal = value; }
        }
    }
}
