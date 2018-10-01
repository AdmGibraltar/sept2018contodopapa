using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PagoElectronicoCuenta
    {
        private int _id_Emp;

        public int Id_Emp
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        private int _id_Cd;

        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }
        private int _id_PagElecCuenta;

        public int Id_PagElecCuenta
        {
            get { return _id_PagElecCuenta; }
            set { _id_PagElecCuenta = value; }
        }
        private string _pagElecCuenta_CC;

        public string PagElecCuenta_CC
        {
            get { return _pagElecCuenta_CC; }
            set { _pagElecCuenta_CC = value; }
        }
        private string _pagElecCuenta_Numero;

        public string PagElecCuenta_Numero
        {
            get { return _pagElecCuenta_Numero; }
            set { _pagElecCuenta_Numero = value; }
        }
        private string _pagElecCuenta_Descripcion;

        public string PagElecCuenta_Descripcion
        {
            get { return _pagElecCuenta_Descripcion; }
            set { _pagElecCuenta_Descripcion = value; }
        }

        private string _pagElecCuenta_SubCuenta;

        public string PagElecCuenta_SubCuenta
        {
            get { return _pagElecCuenta_SubCuenta; }
            set { _pagElecCuenta_SubCuenta = value; }
        }
        private string _pagElecCuenta_SubSubCuenta;

        public string PagElecCuenta_SubSubCuenta
        {
            get { return _pagElecCuenta_SubSubCuenta; }
            set { _pagElecCuenta_SubSubCuenta = value; }
        }
        private string _pagElecCuenta_CuentaPago;

        public string PagElecCuenta_CuentaPago
        {
            get { return _pagElecCuenta_CuentaPago; }
            set { _pagElecCuenta_CuentaPago = value; }
        }

        private Boolean _flete;
        public Boolean Flete
        {
            get { return _flete; }
            set { _flete = value; }
        }

        private Boolean _noInventariable;
        public Boolean NoInventariable
        {
            get { return _noInventariable; }
            set { _noInventariable = value; }
        }
        private Boolean _compraLocal;
        public Boolean CompraLocal
        {
            get { return _compraLocal; }
            set { _compraLocal = value; }
        }
        private Boolean _Servicios;
        public Boolean Servicios
        {
            get { return _Servicios; }
            set { _Servicios = value; }
        }
        private Boolean _Otros;
        public Boolean Otros
        {
            get { return _Otros; }
            set { _Otros = value; }
        }
        private Boolean _Honorarios;
        public Boolean Honorarios
        {
            get { return _Honorarios; }
            set { _Honorarios = value; }
        }

          private Boolean _Arrendamientos;
        public Boolean Arrendamientos
        {
            get { return _Arrendamientos; }
            set { _Arrendamientos = value; }
        }


        private int _id_Subtipo;

        public int Id_Subtipo
        {
            get { return _id_Subtipo; }
            set { _id_Subtipo = value; }
        }

     
    }
}
