using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntSalSolicitudDet
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_ESol;
        private int _Id_ESolD;
        private string _Id_EsDetStr;
        private int _ESol_Naturaleza;
        private int _Id_Prd;
        private string _Prd_Descripcion;
        private string _Prd_Unidad;
        private string _Prd_Presentacion;
        private int _Id_Ter;
        private bool _ESol_BuenEstado;
        private int _ESol_Cantidad;
        private double _ESol_Costo;
        private bool _Afct_OrdCompra;
        private int _Prd_AgrupadoSpo;
        private int _Id_Rem;
        private int _Es_CantidadRem;

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
        public int Id_ESol
        {
            get { return _Id_ESol; }
            set { _Id_ESol = value; }
        }


        public int Id_ESolD
        {
            get { return _Id_ESolD; }
            set { _Id_ESolD = value; }
        }
        public string Id_EsDetStr
        {
            get { return _Id_EsDetStr; }
            set { _Id_EsDetStr = value; }
        }
        public int ESol_Naturaleza
        {
            get { return _ESol_Naturaleza; }
            set { _ESol_Naturaleza = value; }
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
        public bool ESol_BuenEstado
        {
            get { return _ESol_BuenEstado; }
            set { _ESol_BuenEstado = value; }
        }
        public int ESol_Cantidad
        {
            get { return _ESol_Cantidad; }
            set { _ESol_Cantidad = value; }
        }
        public double ESol_EsCosto
        {
            get { return _ESol_Costo; }
            set { _ESol_Costo = value; }
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
    }
}
