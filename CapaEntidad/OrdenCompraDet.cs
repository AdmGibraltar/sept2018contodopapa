using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class OrdenCompraDet
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

        private int _Id_Ord;
        public int Id_Ord
        {
            get { return _Id_Ord; }
            set { _Id_Ord = value; }
        }

        private int _Id_OrdDet;
        public int Id_OrdDet
        {
            get { return _Id_OrdDet; }
            set { _Id_OrdDet = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Prd_AAA;
        public int Prd_AAA
        {
            get { return _Prd_AAA; }
            set { _Prd_AAA = value; }
        }

        private float _Ord_Cantidad;
        public float Ord_Cantidad
        {
            get { return _Ord_Cantidad; }
            set { _Ord_Cantidad = value; }
        }

        private float _Ord_CantidadGen;
        public float Ord_CantidadGen
        {
            get { return _Ord_CantidadGen; }
            set { _Ord_CantidadGen = value; }
        }

        private float _Ord_CantidadCump;
        public float Ord_CantidadCump
        {
            get { return _Ord_CantidadCump; }
            set { _Ord_CantidadCump = value; }
        }

        private Producto _producto;
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        private ProductoPrecios _productoPrecio;
        public ProductoPrecios ProductoPrecio
        {
            get { return _productoPrecio; }
            set { _productoPrecio = value; }
        }

        private int _ventaPromedio;
        public int ventaPromedio
        {
            get { return _ventaPromedio; }
            set { _ventaPromedio = value; }
        }

        private int _Id_Ptp;

        public int Id_Ptp
        {
            get { return _Id_Ptp; }
            set { _Id_Ptp = value; }
        }

        private bool _Prd_Autorizado;

        public bool Prd_Autorizado
        {
            get { return _Prd_Autorizado; }
            set { _Prd_Autorizado = value; }
        }

        private string _Prd_Presentacion;

        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        private int _ventaMes3;

        public int VentaMes3
        {
            get { return _ventaMes3; }
            set { _ventaMes3 = value; }
        }
        private int _ventaMes2;

        public int VentaMes2
        {
            get { return _ventaMes2; }
            set { _ventaMes2 = value; }
        }
        private int _ventaMes1;

        public int VentaMes1
        {
            get { return _ventaMes1; }
            set { _ventaMes1 = value; }
        }
        private int _ventaMes0;

        public int VentaMes0
        {
            get { return _ventaMes0; }
            set { _ventaMes0 = value; }
        }
        private int _prd_MaxExistencia;

        public int Prd_MaxExistencia
        {
            get { return _prd_MaxExistencia; }
            set { _prd_MaxExistencia = value; }
        }
        private int _ordenado;

        public int Ordenado
        {
            get { return _ordenado; }
            set { _ordenado = value; }
        }
    }
}
