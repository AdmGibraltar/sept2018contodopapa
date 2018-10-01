using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Remision
    {

        private int _UsuNum;
        public int UsuNum
        {
            get { return _UsuNum; }
            set { _UsuNum = value; }
        }
        private string _UsuNom;
        public string UsuNom
        {
            get { return _UsuNom; }
            set { _UsuNom = value; }
        }

        private string _Rem_ManAutStr;
        public string Rem_ManAutStr
        {
            get { return _Rem_ManAutStr; }
            set { _Rem_ManAutStr = value; }
        }

        private string _Rem_TipoStr;
        public string Rem_TipoStr
        {
            get { return _Rem_TipoStr; }
            set { _Rem_TipoStr = value; }
        }

        private string _Rem_EstatusStr;
        public string Rem_EstatusStr
        {
            get { return _Rem_EstatusStr; }
            set { _Rem_EstatusStr = value; }
        }

        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }


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
        private double _Rem_Subtotal;
        public double Rem_Subtotal
        {
            get { return _Rem_Subtotal; }
            set { _Rem_Subtotal = value; }
        }

        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Rem_CteCuentaNacional;
        public int Rem_CteCuentaNacional
        {
            get { return _Rem_CteCuentaNacional; }
            set { _Rem_CteCuentaNacional = value; }
        }


        private int _Rem_CteCuentaContNacional;
        public int Rem_CteCuentaContNacional
        {
            get { return _Rem_CteCuentaContNacional; }
            set { _Rem_CteCuentaContNacional = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }


        private string _Rem_Tipo;
        public string Rem_Tipo
        {
            get { return _Rem_Tipo; }
            set { _Rem_Tipo = value; }
        }

        private DateTime _Rem_Fecha;
        public DateTime Rem_Fecha
        {
            get { return _Rem_Fecha; }
            set { _Rem_Fecha = value; }
        }

        private DateTime? _Rem_FechaHr;
        public DateTime? Rem_FechaHr
        {
            get { return _Rem_FechaHr; }
            set { _Rem_FechaHr = value; }
        }

        private int _Id_Tm;
        public int Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }

        private int _Id_Ped;
        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
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

        private int _Id_Cco;
        public int Id_Cco
        {
            get { return _Id_Cco; }
            set { _Id_Cco = value; }
        }
        private string _Rem_OrdenCompra;
        public string Rem_OrdenCompra
        {
            get { return _Rem_OrdenCompra; }
            set { _Rem_OrdenCompra = value; }
        }

        /// <summary>
        /// Esta prpiedad es para guardar el numero de contrato 
        /// para imnpresión en el reporte de contrato de comodato
        /// </summary>
        private string _NumContratoImpresion;
        public string NumContratoImpresion
        {
            get { return _NumContratoImpresion; }
            set { _NumContratoImpresion = value; }
        }

        private string _Rem_Calle;
        public string Rem_Calle
        {
            get { return _Rem_Calle; }
            set { _Rem_Calle = value; }
        }

        private string _Rem_Numero;
        public string Rem_Numero
        {
            get { return _Rem_Numero; }
            set { _Rem_Numero = value; }
        }

        private string _Rem_Cp;
        public string Rem_Cp
        {
            get { return _Rem_Cp; }
            set { _Rem_Cp = value; }
        }

        private string _Rem_Colonia;
        public string Rem_Colonia
        {
            get { return _Rem_Colonia; }
            set { _Rem_Colonia = value; }
        }

        private string _Rem_Municipio;
        public string Rem_Municipio
        {
            get { return _Rem_Municipio; }
            set { _Rem_Municipio = value; }
        }

        private string _Rem_Ciudad;
        public string Rem_Ciudad
        {
            get { return _Rem_Ciudad; }
            set { _Rem_Ciudad = value; }
        }

        private string _Rem_Estado;
        public string Rem_Estado
        {
            get { return _Rem_Estado; }
            set { _Rem_Estado = value; }
        }

        private string _Rem_Rfc;
        public string Rem_Rfc
        {
            get { return _Rem_Rfc; }
            set { _Rem_Rfc = value; }
        }

        private string _Rem_Telefono;
        public string Rem_Telefono
        {
            get { return _Rem_Telefono; }
            set { _Rem_Telefono = value; }
        }

        private string _Rem_Contacto;
        public string Rem_Contacto
        {
            get { return _Rem_Contacto; }
            set { _Rem_Contacto = value; }
        }


        private string _Rem_Conducto;
        public string Rem_Conducto
        {
            get { return _Rem_Conducto; }
            set { _Rem_Conducto = value; }
        }

        private string _Rem_Guia;
        public string Rem_Guia
        {
            get { return _Rem_Guia; }
            set { _Rem_Guia = value; }
        }

        private DateTime? _Rem_FechaEntrega;
        public DateTime? Rem_FechaEntrega
        {
            get { return _Rem_FechaEntrega; }
            set { _Rem_FechaEntrega = value; }
        }

        private string _Rem_HoraEntrega;
        public string Rem_HoraEntrega
        {
            get { return _Rem_HoraEntrega; }
            set { _Rem_HoraEntrega = value; }
        }

        private string _Rem_Nota;
        public string Rem_Nota
        {
            get { return _Rem_Nota; }
            set { _Rem_Nota = value; }
        }


        private double _Rem_Iva;
        public double Rem_Iva
        {
            get { return _Rem_Iva; }
            set { _Rem_Iva = value; }
        }

        private double _Rem_Total;
        public double Rem_Total
        {
            get { return _Rem_Total; }
            set { _Rem_Total = value; }
        }

        private string _Rem_Estatus;
        public string Rem_Estatus
        {
            get { return _Rem_Estatus; }
            set { _Rem_Estatus = value; }
        }


        private int _ZonIva;
        public int ZonIva
        {
            get { return _ZonIva; }
            set { _ZonIva = value; }
        }





        private int _TMITipo;
        public int TMITipo
        {
            get { return _TMITipo; }
            set { _TMITipo = value; }
        }

        private string _TMINombre;
        public string TMINombre
        {
            get { return _TMINombre; }
            set { _TMINombre = value; }
        }

        private int _RemPedCli;
        public int RemPedCli
        {
            get { return _RemPedCli; }
            set { _RemPedCli = value; }
        }

        private string _RemCtto;
        public string RemCtto
        {
            get { return _RemCtto; }
            set { _RemCtto = value; }
        }

        private DateTime _RemFVig;
        public DateTime RemFVig
        {
            get { return _RemFVig; }
            set { _RemFVig = value; }
        }

        private List<RemisionDet> _ListRemisionDetalle;
        public List<RemisionDet> ListRemisionDetalle
        {
            get { return _ListRemisionDetalle; }
            set { _ListRemisionDetalle = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private string _Cte_Calle;
        public string Cte_Calle
        {
            get { return _Cte_Calle; }
            set { _Cte_Calle = value; }
        }

        private string _Cte_Numero;
        public string Cte_Numero
        {
            get { return _Cte_Numero; }
            set { _Cte_Numero = value; }
        }

        private string _Cte_Colonia;
        public string Cte_Colonia
        {
            get { return _Cte_Colonia; }
            set { _Cte_Colonia = value; }
        }

        private int _Cte_CondPago;
        public int Cte_CondPago
        {
            get { return _Cte_CondPago; }
            set { _Cte_CondPago = value; }
        }

        private int _Rem_ManAut;
        public int Rem_ManAut
        {
            get { return _Rem_ManAut; }
            set { _Rem_ManAut = value; }
        }

        private string _Tm_Nombre;
        public string Tm_Nombre
        {
            get { return _Tm_Nombre; }
            set { _Tm_Nombre = value; }
        }


        private string _Rik_Calle;
        public string Rik_Calle
        {
            get { return _Rik_Calle; }
            set { _Rik_Calle = value; }
        }

        private string _Rik_Numero;

        public string Rik_Numero
        {
            get { return _Rik_Numero; }
            set { _Rik_Numero = value; }
        }
        private string _Rik_Colonia;

        public string Rik_Colonia
        {
            get { return _Rik_Colonia; }
            set { _Rik_Colonia = value; }
        }

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        private string _Emp_Nombre;
        public string Emp_Nombre
        {
            get { return _Emp_Nombre; }
            set { _Emp_Nombre = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }


        private int _Cant;
        public int Cant
        {
            get { return _Cant; }
            set { _Cant = value; }
        }


        private string _Rem_ResumenInversion;
        public string Rem_ResumenInversion
        {
            get { return _Rem_ResumenInversion; }
            set { _Rem_ResumenInversion = value; }
        }



        private string _Rem_TablaAmortizacion;
        public string Rem_TablaAmortizacion
        {
            get { return _Rem_TablaAmortizacion; }
            set { _Rem_TablaAmortizacion = value; }
        }


        private string _Rem_KardexAmortizacion;
        public string Rem_KardexAmortizacion
        {
            get { return _Rem_KardexAmortizacion; }
            set { _Rem_KardexAmortizacion = value; }
        }


        private DateTime? _Rem_FecCan;
        public DateTime? Rem_FecCan
        {
            get { return _Rem_FecCan; }
            set { _Rem_FecCan = value; }
        }

        public int? IdTg
        {
            get
            {
                return _idTg;
            }
            set
            {
                _idTg = value;
            }
        }


        private int? _idTg;


        #region Propiedades extras o calculadas
        private string _UCorreo;

        public string UCorreo
        {
            get { return _UCorreo; }
            set { _UCorreo = value; }
        }
        private string _NombreCliente;
        private int? _Id_Vap;
        private byte[] _PDF;
        private int _Rem_Especial;

        public int Rem_Especial
        {
            get { return _Rem_Especial; }
            set { _Rem_Especial = value; }
        }

        public byte[] PDF
        {
            get { return _PDF; }
            set { _PDF = value; }
        }

        public int? Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }

        public string NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
        }

        #endregion
    }
}
