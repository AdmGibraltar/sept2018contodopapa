using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class GestionRentabilidadSimulador
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

        private string _Cpr_Descripcion;
        public string Cpr_Descripcion
        {
            get { return _Cpr_Descripcion; }
            set { _Cpr_Descripcion = value; }
        }


        private int? _Id_Prd;
        public int? Id_Prd
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

        private decimal? _cantidad;
        public decimal? cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private decimal? _PrecioVenta;
        public decimal? PrecioVenta
        {
            get { return _PrecioVenta; }
            set { _PrecioVenta = value; }
        }


        private decimal? _PrecioDistribuidor;
        public decimal? PrecioDistribuidor
        {
            get { return _PrecioDistribuidor; }
            set { _PrecioDistribuidor = value; }
        }

        private decimal? _venta;
        public decimal? venta
        {
            get { return _venta; }
            set { _venta = value; }
        }

        private decimal? _Costo;
        public decimal? Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }
        
        
        private decimal? _UtilidadBruta;
        public decimal? UtilidadBruta
        {
            get { return _UtilidadBruta; }
            set { _UtilidadBruta = value; }
        }


        private decimal? _PorcUBReal;
        public decimal? PorcUBReal
        {
            get { return _PorcUBReal; }
            set { _PorcUBReal = value; }
        }


        
        private string _Accion;
        public string Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }
        
        private decimal? _Id_PrdP;
        public decimal? Id_PrdP
        {
            get { return _Id_PrdP; }
            set { _Id_PrdP = value; }
        }


        private string _Prd_DescripcionP;
        public string Prd_DescripcionP
        {
            get { return _Prd_DescripcionP; }
            set { _Prd_DescripcionP = value; }
        }
        
        private decimal? _cantidadP;
        public decimal? cantidadP
        {
            get { return _cantidadP; }
            set { _cantidadP = value; }
        }
        
        private decimal? _PrecioVentaP;
        public decimal? PrecioVentaP
        {
            get { return _PrecioVentaP; }
            set { _PrecioVentaP = value; }
        }
        
        private decimal? _PrecioDistribuidorP;
        public decimal? PrecioDistribuidorP
        {
            get { return _PrecioDistribuidorP; }
            set { _PrecioDistribuidorP = value; }
        }
        
        
        private decimal? _ventaP;
        public decimal? ventaP
        {
            get { return _ventaP; }
            set { _ventaP = value; }
        }

        private decimal? _CostoP;
        public decimal? CostoP
        {
            get { return _CostoP; }
            set { _CostoP = value; }
        }


        private decimal? _UtilidadBrutaP;
        public decimal? UtilidadBrutaP
        {
            get { return _UtilidadBrutaP; }
            set { _UtilidadBrutaP = value; }
        }

        private decimal? _PorcUBRealP;
        public decimal? PorcUBRealP
        {
            get { return _PorcUBRealP; }
            set { _PorcUBRealP = value; }
        }


        


        private int? _AnioAccion;
        public int? AnioAccion
        {
            get { return _AnioAccion; }
            set { _AnioAccion = value; }
        }


        private string _MesAccionNumero;
        public string MesAccionNumero
        {
            get { return _MesAccionNumero; }
            set { _MesAccionNumero = value; }
        }

        private string _MesAccionNombre;
        public string MesAccionNombre
        {
            get { return _MesAccionNombre; }
            set { _MesAccionNombre = value; }
        }


        
    }

}
