using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class NotaCreditoDet
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

        private int _Id_Ncr;
        public int Id_Ncr
        {
            get { return _Id_Ncr; }
            set { _Id_Ncr = value; }
        }

        private int _Id_NcrDet;
        public int Id_NcrDet
        {
            get { return _Id_NcrDet; }
            set { _Id_NcrDet = value; }
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

        private double _Ncr_Importe;
        public double Ncr_Importe
        {
            get { return _Ncr_Importe; }
            set { _Ncr_Importe = value; }
        }

        private int _Ncr_Cant;
        public int Ncr_Cant
        {
            get { return _Ncr_Cant; }
            set { _Ncr_Cant = value; }
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
        private float _Ncr_Precio;

        public float Ncr_Precio
        {
            get { return _Ncr_Precio; }
            set { _Ncr_Precio = value; }
        }
      
        private string _Prd_UniNs;
        public string Prd_UniNs
        {
            get { return _Prd_UniNs; }
            set { _Prd_UniNs = value; }
        }

        private string _Prd_Presentacion;
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        private Producto _producto;
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }


        private string _Ncr_ClaveProdServ;
        public string Ncr_ClaveProdServ
        {
            get { return _Ncr_ClaveProdServ; }
            set { _Ncr_ClaveProdServ = value; }
        }


        private string _Ncr_ClaveUnidad;
        public string Ncr_ClaveUnidad
        {
            get { return _Ncr_ClaveUnidad; }
            set { _Ncr_ClaveUnidad = value; }
        }
    }
}
