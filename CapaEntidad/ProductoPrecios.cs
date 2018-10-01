using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ProductoPrecios
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

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Id_Pre;
        public int Id_Pre
        {
            get { return _Id_Pre; }
            set { _Id_Pre = value; }
        }

        private bool _Prd_Actual;
        public bool Prd_Actual
        {
            get { return _Prd_Actual; }
            set { _Prd_Actual = value; }
        }

        private object _Prd_FechaInicio;
        public object Prd_FechaInicio
        {
            get { return _Prd_FechaInicio; }
            set { _Prd_FechaInicio = value; }
        }

        private object _Prd_FechaFin;
        public object Prd_FechaFin
        {
            get { return _Prd_FechaFin; }
            set { _Prd_FechaFin = value; }
        }

        private string _Prd_PreDescripcion;
        public string Prd_PreDescripcion
        {
            get { return _Prd_PreDescripcion; }
            set { _Prd_PreDescripcion = value; }
        }

        private string _Pre_Descripcion;
        public string Pre_Descripcion
        {
            get { return _Pre_Descripcion; }
            set { _Pre_Descripcion = value; }
        }

        private float _Prd_Pesos;
        public float Prd_Pesos
        {
            get { return _Prd_Pesos; }
            set { _Prd_Pesos = value; }
        }

    }
}
