using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class NotaCredito
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
        private int _Id_Ncr;
        public int Id_Ncr
        {
            get { return _Id_Ncr; }
            set { _Id_Ncr = value; }
        }

        private int? _Id_Cfe;
        public int? Id_Cfe
        {
            get { return _Id_Cfe; }
            set { _Id_Cfe = value; }
        }

        private string _Id_NcrSerie;
        public string Id_NcrSerie
        {
            get { return _Id_NcrSerie; }
            set { _Id_NcrSerie = value; }
        }

        private int? _Id_Reg;
        public int? Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private int? _Id_Tm;
        public int? Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }

        private int? _Id_Cte;
        public int? Id_Cte
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

        private int? _Ncr_Tipo;
        public int? Ncr_Tipo
        {
            get { return _Ncr_Tipo; }
            set { _Ncr_Tipo = value; }
        }

        private string _Ncr_TipoStr;
        public string Ncr_TipoStr
        {
            get { return _Ncr_TipoStr; }
            set { _Ncr_TipoStr = value; }
        }

        private DateTime _Ncr_Fecha;
        public DateTime Ncr_Fecha
        {
            get { return _Ncr_Fecha; }
            set { _Ncr_Fecha = value; }
        }


        private DateTime _Ncr_FechaHr;
        public DateTime Ncr_FechaHr
        {
            get { return _Ncr_FechaHr; }
            set { _Ncr_FechaHr = value; }
        }

        private int? _Ncr_Movimiento;
        public int? Ncr_Movimiento
        {
            get { return _Ncr_Movimiento; }
            set { _Ncr_Movimiento = value; }
        }

        private int? _Ncr_Referencia;
        public int? Ncr_Referencia
        {
            get { return _Ncr_Referencia; }
            set { _Ncr_Referencia = value; }
        }

        private double? _Ncr_Saldo;
        public double? Ncr_Saldo
        {
            get { return _Ncr_Saldo; }
            set { _Ncr_Saldo = value; }
        }

        private bool? _Ncr_Desgloce;
        public bool? Ncr_Desgloce
        {
            get { return _Ncr_Desgloce; }
            set { _Ncr_Desgloce = value; }
        }

        private bool? _Ncr_DesglocePartidas;
        public bool? Ncr_DesglocePartidas
        {
            get { return _Ncr_DesglocePartidas; }
            set { _Ncr_DesglocePartidas = value; }
        }

        private string _Ncr_Notas;
        public string Ncr_Notas
        {
            get { return _Ncr_Notas; }
            set { _Ncr_Notas = value; }
        }

        private bool _Ncr_CteDIVA;
        public bool Ncr_CteDIVA
        {
            get { return _Ncr_CteDIVA; }
            set { _Ncr_CteDIVA = value; }
        }

        private double? _Ncr_Subtotal;
        public double? Ncr_Subtotal
        {
            get { return _Ncr_Subtotal; }
            set { _Ncr_Subtotal = value; }
        }

        private double? _Ncr_Iva;
        public double? Ncr_Iva
        {
            get { return _Ncr_Iva; }
            set { _Ncr_Iva = value; }
        }

        private double? _Ncr_Total;
        public double? Ncr_Total
        {
            get { return _Ncr_Total; }
            set { _Ncr_Total = value; }
        }

        private double? _Ncr_Pagado;
        public double? Ncr_Pagado
        {
            get { return _Ncr_Pagado; }
            set { _Ncr_Pagado = value; }
        }

        private DateTime? _Ncr_FecPagado;
        public DateTime? Ncr_FecPagado
        {
            get { return _Ncr_FecPagado; }
            set { _Ncr_FecPagado = value; }
        }

        private string _Ncr_Estatus;
        public string Ncr_Estatus
        {
            get { return _Ncr_Estatus; }
            set { _Ncr_Estatus = value; }
        }

        private string _Ncr_EstatusStr;
        public string Ncr_EstatusStr
        {
            get { return _Ncr_EstatusStr; }
            set { _Ncr_EstatusStr = value; }
        }

        private List<NotaCreditoDet> _ListaNotaCredito;
        public List<NotaCreditoDet> ListaNotaCredito
        {
            get { return _ListaNotaCredito; }
            set { _ListaNotaCredito = value; }
        }

        private string _campo;
        public string Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        string _Ade_Campo;

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

        private int id_prd;
        public int Id_Prd
        {
            get { return id_prd; }
            set { id_prd = value; }
        }

        private object _Ncr_Xml;
        public object Ncr_Xml
        {
            get { return _Ncr_Xml; }
            set { _Ncr_Xml = value; }
        }

        private byte[] _Ncr_Pdf;
        public byte[] Ncr_Pdf
        {
            get { return _Ncr_Pdf; }
            set { _Ncr_Pdf = value; }
        }

        private bool? _PDF;
        public bool? PDF
        {
            get { return _PDF; }
            set { _PDF = value; }
        }

        private bool? _NcrXML;
        public bool? NcrXML
        {
            get { return _NcrXML; }
            set { _NcrXML = value; }
        }

        #region Propiedades calculadas

        private string _Ncr_Sello;
        public string Ncr_Sello
        {
            get { return _Ncr_Sello; }
            set { _Ncr_Sello = value; }
        }
    
        private string _Serie;
        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private int? _Ncr_EmpleadoNumNomina;
        public int? Ncr_EmpleadoNumNomina
        {
            get { return _Ncr_EmpleadoNumNomina; }
            set { _Ncr_EmpleadoNumNomina = value; }
        }

        private string _Ncr_EmpleadoNombre;
        public string Ncr_EmpleadoNombre
        {
            get { return _Ncr_EmpleadoNombre; }
            set { _Ncr_EmpleadoNombre = value; }
        }

        private string _Ncr_CtaContable;
        public string Ncr_ReferenciaSerie;
        public string Ter_Nombre;
        public string Tm_Nombre;
        public string Ncr_CtaContable
        {
            get { return _Ncr_CtaContable; }
            set { _Ncr_CtaContable = value; }
        }

        #endregion


        //Gerardo Ponce
        //Se agrego propiedad para Folio Fiscal
        private string _ncr_FolioFiscal;

        public string Ncr_FolioFiscal
        {
            get { return _ncr_FolioFiscal; }
            set { _ncr_FolioFiscal = value; }
        }
    }
}
