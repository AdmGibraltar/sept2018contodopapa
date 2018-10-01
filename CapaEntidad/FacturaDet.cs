using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class FacturaDet
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

        private int _Id_FacDet;
        public int Id_FacDet
        {
            get { return _Id_FacDet; }
            set { _Id_FacDet = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _Id_TerStr;
        public string Id_TerStr
        {
            get { return _Id_TerStr; }
            set { _Id_TerStr = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int? _Id_CteExt;
        public int? Id_CteExt
        {
            get { return _Id_CteExt; }
            set { _Id_CteExt = value; }
        }

        private string _Id_CteExtStr;
        public string Id_CteExtStr
        {
            get { return _Id_CteExtStr; }
            set { _Id_CteExtStr = value; }
        }


        private float _Fac_CantE;
        public float Fac_CantE
        {
            get { return _Fac_CantE; }
            set { _Fac_CantE = value; }
        }


        private int _Fac_Cant;
        public int Fac_Cant
        {
            get { return _Fac_Cant; }
            set { _Fac_Cant = value; }
        }

        public double Fac_Precio_Original { get; set; }

        private double _Fac_Precio;
        public double Fac_Precio
        {
            get { return _Fac_Precio; }
            set { _Fac_Precio = value; }
        }

        private int? _Fac_Asignar;
        public int? Fac_Asignar
        {
            get { return _Fac_Asignar; }
            set { _Fac_Asignar = value; }
        }

        private bool _Fac_Devolucion;
        public bool Fac_Devolucion
        {
            get { return _Fac_Devolucion; }
            set { _Fac_Devolucion = value; }
        }

        public double Fac_Importe
        {
            get { return this._Fac_Cant * this._Fac_Precio; }
        }

        public double Fac_ImporteE
        {
            get { return this._Fac_CantE * this._Fac_Precio; }
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

        public Single Multiplicador { get; set; }
        public Single Precio_Venta { get; set; }
        public Single Totales { get; set; }

        #region Propiedades para calculos especiales

        private string _Clp_Release;
        public string Clp_Release
        {
            get { return _Clp_Release; }
            set { _Clp_Release = value; }
        }

        private double _amortizacionProducto;
        public double AmortizacionProducto
        {
            get { return _amortizacionProducto; }
            set { _amortizacionProducto = value; }
        }

        private int? _Id_Rem;
        public int? Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        private double? _Rem_Cant;
        private string _Prd_Unis;
        public object Id_PrdEsp;
        private int _Prd_Agrupador;

        public int Prd_Agrupador
        {
            get { return _Prd_Agrupador; }
            set { _Prd_Agrupador = value; }
        }

        public string Prd_Unis
        {
            get { return _Prd_Unis; }
            set { _Prd_Unis = value; }
        }
        public double? Rem_Cant
        {
            get { return _Rem_Cant; }
            set { _Rem_Cant = value; }
        }


        private string _Fac_ClaveProdServ;
        public string Fac_ClaveProdServ
        {
            get { return _Fac_ClaveProdServ; }
            set { _Fac_ClaveProdServ = value; }
        }


        private string _Fac_ClaveUnidad;
        public string Fac_ClaveUnidad
        {
            get { return _Fac_ClaveUnidad; }
            set { _Fac_ClaveUnidad = value; }
        }
        #endregion
    }
}
