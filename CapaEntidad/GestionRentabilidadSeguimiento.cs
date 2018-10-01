using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class GestionRentabilidadSeguimiento
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

    }
}
