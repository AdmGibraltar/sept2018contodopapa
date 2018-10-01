using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Ruta
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Rut;
        private int _Id_Rut_Ant;
        private string _Rut_Descripcion;
        private bool _Rut_Activo;
        private string _Rut_ActivoStr;

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

        public int Id_Rut
        {
            get { return _Id_Rut; }
            set { _Id_Rut = value; }
        }

        public int Id_Rut_Ant
        {
            get { return _Id_Rut_Ant; }
            set { _Id_Rut_Ant = value; }
        }

        public string Rut_Descripcion
        {
            get { return _Rut_Descripcion; }
            set { _Rut_Descripcion = value; }
        }

        public bool Rut_Activo
        {
            get { return _Rut_Activo; }
            set { _Rut_Activo = value; }
        }

        public string Rut_ActivoStr
        {
            get { return _Rut_ActivoStr; }
            set { _Rut_ActivoStr = value; }
        }
    }
}
