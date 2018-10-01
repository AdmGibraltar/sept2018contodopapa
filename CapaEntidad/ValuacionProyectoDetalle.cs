using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ValuacionProyectoDetalle
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

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private int _Id_Vap;
        public int Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }

        private int _Id_VapDet;
        public int Id_VapDet
        {
            get { return _Id_VapDet; }
            set { _Id_VapDet = value; }
        }

        private int _Vap_Tipo;
        public int Vap_Tipo
        {
            get { return _Vap_Tipo; }
            set { _Vap_Tipo = value; }
        }

        private string _Vap_TipoStr;
        public string Vap_TipoStr
        {
            get { return _Vap_TipoStr; }
            set { _Vap_TipoStr = value; }
        }
        
        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private string _Prd_Presentacion;
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        private string _Prd_UniNe;
        public string Prd_UniNe
        {
            get { return _Prd_UniNe; }
            set { _Prd_UniNe = value; }
        }
        
        private int _Vap_Cantidad;
        public int Vap_Cantidad
        {
            get { return _Vap_Cantidad; }
            set { _Vap_Cantidad = value; }
        }

        private double _Vap_Costo;
        public double Vap_Costo
        {
            get { return _Vap_Costo; }
            set { _Vap_Costo = value; }
        }

        private double _Vap_Precio;
        public double Vap_Precio
        {
            get { return _Vap_Precio; }
            set { _Vap_Precio = value; }
        }

        private bool _Autorizado;
        public bool Autorizado
        {
            get { return _Autorizado; }
            set { _Autorizado = value; }
        }

        private bool _Rechazado;
        public bool Rechazado
        {
            get { return _Rechazado; }
            set { _Rechazado = value; }
        }
        
        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _Det_FecAut;
        public string Det_FecAut
        {
            get { return _Det_FecAut; }
            set { _Det_FecAut = value; }
        }

        private double? _Vap_PrecioEspecial;
        public double? Vap_PrecioEspecial
        {
            get { return _Vap_PrecioEspecial; }
            set { _Vap_PrecioEspecial = value; }
        }        

        private Producto _Producto;
        public Producto Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }
    }
}
