using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Representantes
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Rik;
        string _Nombre;
        string _Calle;
        int _Numero;
        string _Colonia;
        string _Telefono;
        DateTime _Fecha_Alta;
        double _Contribucion;
        double _Compensacion;
        bool _Pertenece;
        int _Gte;
        bool _Estatus;
        string _EstatusStr;
        int _Tipo_Rep;
        string _Descr_Rep;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Colonia
        {
            get { return _Colonia; }
            set { _Colonia = value; }
        }
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }
        public DateTime Fecha_Alta
        {
            get { return _Fecha_Alta; }
            set { _Fecha_Alta = value; }
        }
        public double Contribucion
        {
            get { return _Contribucion; }
            set { _Contribucion = value; }
        }
        public double Compensacion
        {
            get { return _Compensacion; }
            set { _Compensacion = value; }
        }
        public bool Pertenece
        {
            get { return _Pertenece; }
            set { _Pertenece = value; }
        }
        public int Gte
        {
            get { return _Gte; }
            set { _Gte = value; }
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

        public int TipoRep
        {
            get { return _Tipo_Rep; }
            set { _Tipo_Rep = value; }
        }

        public string DescrRep
        {
            get { return _Descr_Rep; }
            set { _Descr_Rep = value; }
        }


        

        

    }
}
