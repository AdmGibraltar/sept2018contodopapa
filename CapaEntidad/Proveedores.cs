using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Proveedores
    {
        int _Empresa;
        int _Id;
        string _Descripcion;
        string _Calle;
        string _Numero;
        string _CP;
        string _Colonia;
        string _Municipio;
        string _Telefono;
        string _RFC;
        string _Fax;
        string _Correo;
        string _Estado;
        string _Contacto;
        bool _Local;
        string _Pais;
        bool _Estatus;
        bool _Franquicia;
        int _Tipo;
        bool _Habilitar;

   

        string _EstatusStr;
        int _Centro;

        public int Centro
        {
            get { return _Centro; }
            set { _Centro = value; }
        }
        public int Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public bool Habilitar
        {
            get { return _Habilitar; }
            set { _Habilitar = value; }
        }

        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string CP
        {
            get { return _CP; }
            set { _CP = value; }
        }
        public string Colonia
        {
            get { return _Colonia; }
            set { _Colonia = value; }
        }
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public string Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public string Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }
        public bool Local
        {
            get { return _Local; }
            set { _Local = value; }
        }
        public string Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }

        public bool Franquicia
        {
            get { return _Franquicia; }
            set { _Franquicia = value; }
        }

    }
}
