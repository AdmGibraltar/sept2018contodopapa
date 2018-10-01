using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Acreedor
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
        private int _id_Acr;

        public int Id_Acr
        {
            get { return _id_Acr; }
            set { _id_Acr = value; }
        }
        private string _acr_Nombre;

        public string Acr_Nombre
        {
            get { return _acr_Nombre; }
            set { _acr_Nombre = value; }
        }
        private string _acr_Calle;

        public string Acr_Calle
        {
            get { return _acr_Calle; }
            set { _acr_Calle = value; }
        }
        private string _acr_Numero;

        public string Acr_Numero
        {
            get { return _acr_Numero; }
            set { _acr_Numero = value; }
        }
        private string _acr_NumInterior;

        public string Acr_NumInterior
        {
            get { return _acr_NumInterior; }
            set { _acr_NumInterior = value; }
        }
        private string _acr_CP;

        public string Acr_CP
        {
            get { return _acr_CP; }
            set { _acr_CP = value; }
        }
        private string _acr_Colonia;

        public string Acr_Colonia
        {
            get { return _acr_Colonia; }
            set { _acr_Colonia = value; }
        }
        private string _acr_Municipio;

        public string Acr_Municipio
        {
            get { return _acr_Municipio; }
            set { _acr_Municipio = value; }
        }
        private string _acr_Estado;

        public string Acr_Estado
        {
            get { return _acr_Estado; }
            set { _acr_Estado = value; }
        }
        private string _acr_RFC;

        public string Acr_RFC
        {
            get { return _acr_RFC; }
            set { _acr_RFC = value; }
        }

        private bool _acr_Autorizado;

        public bool Acr_Autorizado
        {
            get { return _acr_Autorizado; }
            set { _acr_Autorizado = value; }
        }

        private string _acr_Telefono;

        public string Acr_Telefono
        {
            get { return _acr_Telefono; }
            set { _acr_Telefono = value; }
        }
        private string _acr_Correo;

        public string Acr_Correo
        {
            get { return _acr_Correo; }
            set { _acr_Correo = value; }
        }
        private string _acr_Contacto;

        public string Acr_Contacto
        {
            get { return _acr_Contacto; }
            set { _acr_Contacto = value; }
        }
        private int _acr_CondPago;

        public int Acr_CondPago
        {
            get { return _acr_CondPago; }
            set { _acr_CondPago = value; }
        }
        private string _acr_Clave;

        public string Acr_Clave
        {
            get { return _acr_Clave; }
            set { _acr_Clave = value; }
        }
        private int _acr_Tipo;

        public int Acr_Tipo
        {
            get { return _acr_Tipo; }
            set { _acr_Tipo = value; }
        }

        private string _acr_NumeroGenerado;

        public string Acr_NumeroGenerado
        {
            get { return _acr_NumeroGenerado; }
            set { _acr_NumeroGenerado = value; }
        }

        private string _acr_Banco;
        public string Acr_Banco
        {
            get { return _acr_Banco; }
            set { _acr_Banco = value; }
        }

        private string _acr_Cuenta;
        public string Acr_Cuenta
        {
            get { return _acr_Cuenta; }
            set { _acr_Cuenta = value; }
        }


        private bool _acr_Estatus;

        public bool Acr_Estatus
        {
            get { return _acr_Estatus; }
            set { _acr_Estatus = value; }
        }

         

          private string _acr_EstatusDescripcion;

        public string Acr_EstatusDescripcion
        {
            get { return _acr_EstatusDescripcion; }
            set { _acr_EstatusDescripcion = value; }
        }


       

    }
}
