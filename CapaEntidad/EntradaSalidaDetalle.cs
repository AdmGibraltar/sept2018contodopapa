using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntradaSalidaDetalle
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Es;
        private int _Id_EsDet;
        private string _Id_EsDetStr;
        private int _EsDet_Naturaleza;
        private int _Id_Prd;
        private string _Prd_Descripcion;
        private string _Prd_Unidad;
        private string _Prd_Presentacion;
        private int _Id_Ter;
        private bool _Es_BuenEstado;
        private int _Es_Cantidad;
        private double _Es_Costo;
        private bool _Afct_OrdCompra;
        private int _Prd_AgrupadoSpo;
        private int _Id_Rem;
        private int _Es_CantidadRem;
        private int _Es_Usado;

        public int Es_CantidadRem
        {
            get { return _Es_CantidadRem; }
            set { _Es_CantidadRem = value; }
        }

        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }
        //--------------------

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
        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }


        public int Id_EsDet
        {
            get { return _Id_EsDet; }
            set { _Id_EsDet = value; }
        }
        public string Id_EsDetStr
        {
            get { return _Id_EsDetStr; }
            set { _Id_EsDetStr = value; }
        }
        public int EsDet_Naturaleza
        {
            get { return _EsDet_Naturaleza; }
            set { _EsDet_Naturaleza = value; }
        }

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        public string Prd_Unidad
        {
            get { return _Prd_Unidad; }
            set { _Prd_Unidad = value; }
        }

        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public bool Es_BuenEstado
        {
            get { return _Es_BuenEstado; }
            set { _Es_BuenEstado = value; }
        }



        public int Es_Usado
        {
            get { return _Es_Usado; }
            set { _Es_Usado = value; }
        }




        public int Es_Cantidad
        {
            get { return _Es_Cantidad; }
            set { _Es_Cantidad = value; }
        }
        public double Es_Costo
        {
            get { return _Es_Costo; }
            set { _Es_Costo = value; }
        }
        public bool Afct_OrdCompra
        {
            get { return _Afct_OrdCompra; }
            set { _Afct_OrdCompra = value; }
        }
        public int Prd_AgrupadoSpo
        {
            get { return _Prd_AgrupadoSpo; }
            set { _Prd_AgrupadoSpo = value; }
        }

        private string _Ter_Nombre;

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private string _Presentacion;

        public string Presentacion
        {
            get { return _Presentacion; }
            set { _Presentacion = value; }
        }

        private bool _afecta;

        public bool Afecta
        {
            get { return _afecta; }
            set { _afecta = value; }
        }

        

        private double _importe;

        public double Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }



        private int _TipoSalida;
        public int TipoSalida
        {
            get { return _TipoSalida; }
            set { _TipoSalida = value; }
        }


        private int _ConceptoTipoSalida;
        public int ConceptoTipoSalida
        {
            get { return _ConceptoTipoSalida; }
            set { _ConceptoTipoSalida = value; }
        }


        private string _DescTipoSalida;
        public string DescTipoSalida
        {
            get { return _DescTipoSalida; }
            set { _DescTipoSalida = value; }
        }


        private string _DescConceptoTipoSalida;
        public string DescConceptoTipoSalida
        {
            get { return _DescConceptoTipoSalida; }
            set { _DescConceptoTipoSalida = value; }
        }        

    }
}
