using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntradaSalida
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_U;
        private int _Id_Es;
        private int _Es_Naturaleza;
        private string _Es_NaturalezaStr;//
        private DateTime _Es_Fecha;
        private int _Id_Tm;
        private string _Tm_Nombre;//
        private int _Id_Cte;
        private string _Nombre_Cliente;//
        private string _Calle;//
        private string _Numero;//
        private string _Colonia;//
        private string _Municipio;//
        private string _Estado;//        
        private string _Cte_NomComercial;
        private int _Id_Pvd;
        private string _Pvd_Descripcion;
        private string _Es_Referecia;
        private string _Es_Notas;
        private double _Es_Subtotal;
        private double _Es_Iva;
        private double _Es_Total;
        private string _Es_Estatus;
        private string _Es_EstatusStr;
        private bool _ManAut;
        private string _ManAutStr;
        private int _Id_Ter;//
        private int _Id_Rik;//
        private int _Id_Rem;//
        private int _Es_CteCuentaNacional;//
        private int _Es_CteCuentaContNacional;
        private DateTime? _Es_FechaReferencia;
        private int _Id_Ord;


        public int Es_CteCuentaNacional
        {
            get { return _Es_CteCuentaNacional; }
            set { _Es_CteCuentaNacional = value; }
        }

        public DateTime? Es_FechaReferencia
        {
            get { return _Es_FechaReferencia; }
            set { _Es_FechaReferencia = value; }
        }


        public int Es_CteCuentaContNacional
        {
            get { return _Es_CteCuentaContNacional; }
            set { _Es_CteCuentaContNacional = value; }
        }

        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }
        //Tabla CapEntSalDet

        //------------------

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }
        /// <summary>
        /// 0 entrada, 1 salida
        /// </summary>
        public int Es_Naturaleza
        {
            get { return _Es_Naturaleza; }
            set { _Es_Naturaleza = value; }
        }
        public string Es_NaturalezaStr
        {
            get { return _Es_NaturalezaStr; }
            set { _Es_NaturalezaStr = value; }
        }
        public DateTime Es_Fecha
        {
            get { return _Es_Fecha; }
            set { _Es_Fecha = value; }
        }
        public int Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }
        public string Tm_Nombre
        {
            get { return _Tm_Nombre; }
            set { _Tm_Nombre = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Nombre_Cliente
        {
            get { return _Nombre_Cliente; }
            set { _Nombre_Cliente = value; }
        }
        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Colonia
        {
            get { return _Colonia; }
            set { _Colonia = value; }
        }
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }
        public string Pvd_Descripcion
        {
            get { return _Pvd_Descripcion; }
            set { _Pvd_Descripcion = value; }
        }

        public string Es_Referencia
        {
            get { return _Es_Referecia; }
            set { _Es_Referecia = value; }
        }
        public string Es_Notas
        {
            get { return _Es_Notas; }
            set { _Es_Notas = value; }
        }
        public double Es_SubTotal
        {
            get { return _Es_Subtotal; }
            set { _Es_Subtotal = value; }
        }
        public double Es_Iva
        {
            get { return _Es_Iva; }
            set { _Es_Iva = value; }
        }
        public double Es_Total
        {
            get { return _Es_Total; }
            set { _Es_Total = value; }
        }
        /// <summary>
        /// capturado(c), baja(b), (i)impreso
        /// </summary>
        public string Es_Estatus
        {
            get { return _Es_Estatus; }
            set { _Es_Estatus = value; }
        }
        public string Es_EstatusStr
        {
            get { return _Es_EstatusStr; }
            set { _Es_EstatusStr = value; }
        }
        /// <summary>
        /// Manual 1 , Automatico 0
        /// </summary>
        public bool ManAut
        {
            get { return _ManAut; }
            set { _ManAut = value; }
        }
        public string ManAutStr
        {
            get { return _ManAutStr; }
            set { _ManAutStr = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }


        private List<EntradaSalidaDetalle> _ListaDetalle;
        private string _Id_PvdStr;

        public string Id_PvdStr
        {
            get { return _Id_PvdStr; }
            set { _Id_PvdStr = value; }
        }
        private string _Id_CteStr;



        public string Id_CteStr
        {
            get { return _Id_CteStr; }
            set { _Id_CteStr = value; }
        }
        public List<EntradaSalidaDetalle> ListaDetalle
        {
            get { return _ListaDetalle; }
            set { _ListaDetalle = value; }
        }

        private string _Ter_Nombre;

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        public int Id_Ord
        {
            get { return _Id_Ord; }
            set { _Id_Ord = value; }
        }

        private DateTime _Es_FechaHr;
        public DateTime Es_FechaHr
        {
            get { return _Es_FechaHr; }
            set { _Es_FechaHr = value; }
        }

        private List<EntradaSalidaDetalle> _ListEntradaSalidaDetalle;
        public List<EntradaSalidaDetalle> ListEntradaSalidaDetalle
        {
            get { return _ListEntradaSalidaDetalle; }
            set { _ListEntradaSalidaDetalle = value; }
        }


    }
}
