using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class GestionRentabilidadLista
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

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        private int? _Id_Prd;
        public int? Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Nombre;
        public string Prd_Nombre
        {
            get { return _Prd_Nombre; }
            set { _Prd_Nombre = value; }
        }


        private decimal? _unidades;
        public decimal? unidades
        {
            get { return _unidades; }
            set { _unidades = value; }
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

        private decimal? _CostoNuevo;
        public decimal? CostoNuevo
        {
            get { return _CostoNuevo; }
            set { _CostoNuevo = value; }
        }



        private decimal? _UtilidadBruta;
        public decimal? UtilidadBruta
        {
            get { return _UtilidadBruta; }
            set { _UtilidadBruta = value; }
        }


        private decimal? _UtilidadBrutaNueva;
        public decimal? UtilidadBrutaNueva
        {
            get { return _UtilidadBrutaNueva; }
            set { _UtilidadBrutaNueva = value; }
        }



        private decimal? _PorcUBReal;
        public decimal? PorcUBReal
        {
            get { return _PorcUBReal; }
            set { _PorcUBReal = value; }
        }




        private string _CrearProyecto;
        public string CrearProyecto
        {
            get { return _CrearProyecto; }
            set { _CrearProyecto = value; }
        }



        private decimal? _Variacion;
        public decimal? Variacion
        {
            get { return _Variacion; }
            set { _Variacion = value; }
        }


        private decimal? _ImpactoUB;
        public decimal? ImpactoUB
        {
            get { return _ImpactoUB; }
            set { _ImpactoUB = value; }
        }

        private decimal? _PrecioVenta;
        public decimal? PrecioVenta
        {
            get { return _PrecioVenta; }
            set { _PrecioVenta = value; }
        }

    }

}
