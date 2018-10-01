using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Region
    {
        int _Id_Emp;
        int _Id_Reg;
        string _Reg_Descripcion;
        bool _Reg_Activo;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set {  _Id_Emp = value; }
        }

        public int Id_Reg
        {
            get { return _Id_Reg; }
            set {_Id_Reg  = value; }
        }

        public string Reg_Descripcion
        {
            get { return _Reg_Descripcion; }
            set {  _Reg_Descripcion= value; }
        }

        public bool Reg_Activo
        {
            get { return _Reg_Activo; }
            set { _Reg_Activo = value; }
        }
    }
}
