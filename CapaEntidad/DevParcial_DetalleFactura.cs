using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DevParcial_DetalleFactura
    {
        int id_Fac;
        int id_FacDet;
        int id_Ter;
        string Territorio;
        int id_Prod;
        string Descripcion;
        string Present;
        int Cantidad;
        double Precio;
        double Importe;
        bool devuelto;
        int cantDevuelta;
        private int _Prd_Agrupador;

        public int Prd_Agrupador
        {
            get { return _Prd_Agrupador; }
            set { _Prd_Agrupador = value; }
        }

        public int CantDevuelta
        {
            get { return cantDevuelta; }
            set { cantDevuelta = value; }
        }

        public bool Devuelto
        {
            get { return devuelto; }
            set { devuelto = value; }
        }

        public int Id_Fac
        {
            get { return id_Fac; }
            set { id_Fac = value; }
        }

        public int Id_FacDet
        {
            get { return id_FacDet; }
            set { id_FacDet = value; }
        }

        public int Id_Ter
        {
            get { return id_Ter; }
            set { id_Ter = value; }
        }
        
        public string Territorio1
        {
            get { return Territorio; }
            set { Territorio = value; }
        }

        public int Id_Prod
        {
            get { return id_Prod; }
            set { id_Prod = value; }
        }

        public string Descripcion1
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }

        public string Present1
        {
            get { return Present; }
            set { Present = value; }
        }

        public int Cantidad1
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }

        public double Precio1
        {
            get { return Precio; }
            set { Precio = value; }
        }

        public double Importe1
        {
            get { return Importe; }
            set { Importe = value; }
        }
    }
}
