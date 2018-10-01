using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Rentabilidad
    {
        int _Id_Emp;
        int _Id_Ren;
        string _Nivel;
        string _Descripcion;
        bool _Estatus;
        string _EstatusStr;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Ren
        {
            get { return _Id_Ren; }
            set { _Id_Ren = value; }
        }
        public string Nivel
        {
            get { return _Nivel; }
            set { _Nivel = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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
