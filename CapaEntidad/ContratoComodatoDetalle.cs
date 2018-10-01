using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ContratoComodatoDetalle
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

        private int _Id_Cco;
        public int Id_Cco
        {
            get { return _Id_Cco; }
            set { _Id_Cco = value; }
        }

        private int _Id_CcoDet;
        public int Id_CcoDet
        {
            get { return _Id_CcoDet; }
            set { _Id_CcoDet = value; }
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

        private int _Cco_Cantidad;
        public int Cco_Cantidad
        {
            get { return _Cco_Cantidad; }
            set { _Cco_Cantidad = value; }
        }

        private double _Cco_Precio;
        public double Cco_Precio
        {
            get { return _Cco_Precio; }
            set { _Cco_Precio = value; }
        }
    }
}
