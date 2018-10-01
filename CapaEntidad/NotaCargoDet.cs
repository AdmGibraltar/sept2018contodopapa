using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class NotaCargoDet
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

        private int _Id_Nca;
        public int Id_Nca
        {
            get { return _Id_Nca; }
            set { _Id_Nca = value; }
        }

        private int _Id_NcaDet;
        public int Id_NcaDet
        {
            get { return _Id_NcaDet; }
            set { _Id_NcaDet = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private int _Id_Rik;
        public int Id_Rik
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

        private int _Id_Prd;
        public int Id_Prd
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

        private int _Nca_Cant;
        public int Nca_Cant
        {
            get { return _Nca_Cant; }
            set { _Nca_Cant = value; }
        }

        private double _Nca_Importe;
        public double Nca_Importe
        {
            get { return _Nca_Importe; }
            set { _Nca_Importe = value; }
        }
        private int _Id_CteExt;
        public int Id_CteExt
        {
            get { return _Id_CteExt; }
            set { _Id_CteExt = value; }
        }
        private string _Clp_Release;
        public string Clp_Release
        {
            get { return _Clp_Release; }
            set { _Clp_Release = value; }
        }
        private float _Nca_Precio;
        public float Nca_Precio
        {
            get { return _Nca_Precio; }
            set { _Nca_Precio = value; }
        }
        private Producto _producto;
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private float _Nca_Importe1;
        private string _Prd_Unis;

        public string Prd_Unis
        {
            get { return _Prd_Unis; }
            set { _Prd_Unis = value; }
        }

        private string _Prd_Presentacion;
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        public float Nca_Importe1
        {
            get { return _Nca_Importe1; }
            set { _Nca_Importe1 = value; }
        }


        private string _Nca_ClaveProdServ;
        public string Nca_ClaveProdServ
        {
            get { return _Nca_ClaveProdServ; }
            set { _Nca_ClaveProdServ = value; }
        }


        private string _Nca_ClaveUnidad;
        public string Nca_ClaveUnidad
        {
            get { return _Nca_ClaveUnidad; }
            set { _Nca_ClaveUnidad = value; }
        }

    }
}
