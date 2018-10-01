using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Unidad
    {
        string _Id;
        int _Id_Ant;
        int _Id_Emp;
        string _Descripcion;
        int _Tipo;
        bool _Estatus;
        string _EstatusStr;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int Id_Ant
        {
            get { return _Id_Ant; }
            set { _Id_Ant = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
    }
}
