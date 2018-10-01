using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class NoConformidades
    {
        int _Id_Emp;
        int _Id_Nco;
        int _Id_Nco_Ant;
        string _Nco_Descripcion;
        string _Nco_Aplica;
        int _Nco_Tipo;
        string _Nco_TipoStr;
        bool _Nco_Activo;
        string _Nco_ActivoStr;
        public int Nco_TipoAnt;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Nco
        {
            get { return _Id_Nco; }
            set { _Id_Nco = value; }
        }
        public int Id_Nco_Ant
        {
            get { return _Id_Nco_Ant; }
            set { _Id_Nco_Ant = value; }
        }
        public string Nco_Descripcion
        {
            get { return _Nco_Descripcion; }
            set { _Nco_Descripcion = value; }
        }
        public string Nco_Aplica
        {
            get { return _Nco_Aplica; }
            set { _Nco_Aplica = value; }
        }
        public int Nco_Tipo
        {
            get { return _Nco_Tipo; }
            set { _Nco_Tipo = value; }
        }
        public string Nco_TipoStr
        {
            get { return _Nco_TipoStr; }
            set { _Nco_TipoStr = value; }
        }
        public bool Nco_Activo
        {
            get { return _Nco_Activo; }
            set { _Nco_Activo = value; }
        }
        public string Nco_ActivoStr
        {
            get { return _Nco_ActivoStr; }
            set { _Nco_ActivoStr = value; }
        }
    }
}
