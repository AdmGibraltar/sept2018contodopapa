using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class InvRotacion
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }


        private int _Ano;
        public int Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }


        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
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

        private float _Prd_Presentacion;
        public float Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }


        private string _Id_Uni;
        public string Id_Uni
        {
            get { return _Id_Uni; }
            set { _Id_Uni = value; }
        }

        private int _Prd_InvFinal;
        public int Prd_InvFinal
        {
            get { return _Prd_InvFinal; }
            set { _Prd_InvFinal = value; }
        }

        private float _Prd_PrecioAAA;
        public float Prd_PrecioAAA
        {
            get { return _Prd_PrecioAAA; }
            set { _Prd_PrecioAAA = value; }
        }

        private float _ImporteInventario;
        public float ImporteInventario
        {
            get { return _ImporteInventario; }
            set { _ImporteInventario = value; }
        }


        private int _Antepenultimo;
        public int Antepenultimo
        {
            get { return _Antepenultimo; }
            set { _Antepenultimo = value; }
        }

        private int _Penultimo;
        public int Penultimo
        {
            get { return _Penultimo; }
            set { _Penultimo = value; }
        }

        private int _Ultimo;
        public int Ultimo
        {
            get { return _Ultimo; }
            set { _Ultimo = value; }
        }

        private float _Promedio;
        public float Promedio
        {
            get { return _Promedio; }
            set { _Promedio = value; }
        }



        private float _CostoPromedio;
        public float CostoPromedio
        {
            get { return _CostoPromedio; }
            set { _CostoPromedio = value; }
        }


        private float _Rotacion;
        public float Rotacion
        {
            get { return _Rotacion; }
            set { _Rotacion = value; }
        }


      


        private float _Vigente;
        public float Vigente
        {
            get { return _Vigente; }
            set { _Vigente = value; }
        }


        private float _Vencido;
        public float Vencido
        {
            get { return _Vencido; }
            set { _Vencido = value; }
        }

    }
}
