using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class GestionRentabilidad
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



        private decimal? _InversionSP;
        public decimal? InversionSP
        {
            get { return _InversionSP; }
            set { _InversionSP = value; }
        }



        private decimal? _InversionCT;
        public decimal? InversionCT
        {
            get { return _InversionCT; }
            set { _InversionCT = value; }
        }


        private decimal? _PorcUBReal;
        public decimal? PorcUBReal
        {
            get { return _PorcUBReal; }
            set { _PorcUBReal = value; }
        }



        private decimal? _PorcURem;
        public decimal? PorcURem
        {
            get { return _PorcURem; }
            set { _PorcURem = value; }
        }



     private decimal? _VentaQuimicos;
        public decimal? VentaQuimicos
        {
            get { return _VentaQuimicos; }
            set { _VentaQuimicos = value; }
        }


     private decimal? _UBQuimicos;
        public decimal? UBQuimicos
        {
            get { return _UBQuimicos; }
            set { _UBQuimicos = value; }
        }



        private decimal? _VentaPT;
        public decimal? VentaPT
        {
            get { return _VentaPT; }
            set { _VentaPT = value; }
        }



        private decimal? _UBPT;
        public decimal? UBPT
        {
            get { return _UBPT; }
            set { _UBPT = value; }
        }



        private decimal? _VentaSD;
        public decimal? VentaSD
        {
            get { return _VentaSD; }
            set { _VentaSD = value; }
        }

        private decimal? _UBSD;
        public decimal? UBSD
        {
            get { return _UBSD; }
            set { _UBSD = value; }
        }

        private decimal? _VentaJarceria;
        public decimal? VentaJarceria
        {
            get { return _VentaJarceria; }
            set { _VentaJarceria = value; }
        }


        private decimal? _UBJarceria;
        public decimal? UBJarceria
        {
            get { return _UBJarceria; }
            set { _UBJarceria = value; }
        }


     private decimal? _VentaAccesorios;
        public decimal? VentaAccesorios
        {
            get { return _VentaAccesorios; }
            set { _VentaAccesorios = value; }
        }

        private decimal? _UBAccesorios;
        public decimal? UBAccesorios
        {
            get { return _UBAccesorios; }
            set { _UBAccesorios = value; }
        }


        private decimal? _VentaBB;
        public decimal? VentaBB
        {
            get { return _VentaBB; }
            set { _VentaBB = value; }
        }




        private decimal? _UBBB;
        public decimal? UBBB
        {
            get { return _UBBB; }
            set { _UBBB = value; }
        }

        private string _CrearProyecto;
        public string CrearProyecto
        {
            get { return _CrearProyecto; }
            set { _CrearProyecto = value; }
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



        private decimal? _InversionSPP;
        public decimal? InversionSPP
        {
            get { return _InversionSPP; }
            set { _InversionSPP = value; }
        }



        private decimal? _InversionCTP;
        public decimal? InversionCTP
        {
            get { return _InversionCTP; }
            set { _InversionCTP = value; }
        }




        private decimal? _PorcURemP;
        public decimal? PorcURemP
        {
            get { return _PorcURemP; }
            set { _PorcURemP = value; }
        }



    }

}
