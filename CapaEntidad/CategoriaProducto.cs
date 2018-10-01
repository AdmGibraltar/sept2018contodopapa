using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CategoriaProducto
    {
        int _Id_Emp;
        int _Id_Cpr;
        int _Id_Cpr_Ant;
        string _Cpr_Descripcion;
        bool _Cpr_Activo;
        string _Cpr_ActivoStr;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cpr
        {
            get { return _Id_Cpr; }
            set { _Id_Cpr = value; }
        }
        public int Id_Cpr_Ant
        {
            get { return _Id_Cpr_Ant; }
            set { _Id_Cpr_Ant = value; }
        }
        public string Cpr_Descripcion
        {
            get { return _Cpr_Descripcion; }
            set { _Cpr_Descripcion = value; }
        }
        public bool Cpr_Activo
        {
            get { return _Cpr_Activo; }
            set { _Cpr_Activo = value; }
        }
        public string Cpr_ActivoStr
        {
            get { return _Cpr_ActivoStr; }
            set { _Cpr_ActivoStr = value; }
        }
    }
}
