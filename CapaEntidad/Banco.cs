using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Banco
    {
        int _Id;
        int _Id_Ant;
        string _Descripcion;
        string _Ciudad;
        string _Estado;
        string _Cuenta;
        int _Empresa;
        int _Centro;
        bool _Estatus;
        string _EstatusStr;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int Id_Ant
        {
            get { return _Id_Ant; }
            set { _Id_Ant = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public string Ciudad
        {
            get { return _Ciudad; }
            set { _Ciudad = value; }
        }
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public string Cuenta
        {
            get { return _Cuenta; }
            set { _Cuenta = value; }
        }
        public int Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        public int Centro
        {
            get { return _Centro; }
            set { _Centro = value; }
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
    }
}
