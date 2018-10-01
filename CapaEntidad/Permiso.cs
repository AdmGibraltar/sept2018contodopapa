using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Permiso
    {
        private int _Id_U;
        private Int32 _Id_TU;

        private Int32 _Sm_cve;
        private bool _Sp_PAccesar;
        private bool _Sp_PGrabar;
        private bool _Sp_PModificar;
        private bool _Sp_PEliminar;

        private bool _Sp_PImprimir;
        private bool _PAccesar;
        private bool _PGrabar;
        private bool _PModificar;
        private bool _PEliminar;

        private bool _PImprimir;
        private string _Menu;

        private Int32 _Id_Cd;
        private Int32 _Id_Emp;
        string _Id_Ctrl;

        bool _PDeshabilitar;

        public bool PDeshabilitar
        {
            get { return _PDeshabilitar; }
            set { _PDeshabilitar = value; }
        }
        bool _POcultar;

        public bool POcultar
        {
            get { return _POcultar; }
            set { _POcultar = value; }
        }


        public string Id_Ctrl
        {
            get { return _Id_Ctrl; }
            set { _Id_Ctrl = value; }
        }

        public Int32 Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public Int32 Id_TU
        {
            get { return _Id_TU; }
            set { _Id_TU = value; }
        }
        public Int32 Sm_cve
        {
            get { return _Sm_cve; }
            set { _Sm_cve = value; }
        }

        public bool Sp_PAccesar
        {
            get { return _Sp_PAccesar; }
            set { _Sp_PAccesar = value; }
        }
        public bool Sp_PGrabar
        {
            get { return _Sp_PGrabar; }
            set { _Sp_PGrabar = value; }
        }
        public bool Sp_PModificar
        {
            get { return _Sp_PModificar; }
            set { _Sp_PModificar = value; }
        }
        public bool Sp_PEliminar
        {
            get { return _Sp_PEliminar; }
            set { _Sp_PEliminar = value; }
        }
        public bool Sp_PImprimir
        {
            get { return _Sp_PImprimir; }
            set { _Sp_PImprimir = value; }
        }

        public bool PAccesar
        {
            get { return _PAccesar; }
            set { _PAccesar = value; }
        }
        public bool PGrabar
        {
            get { return _PGrabar; }
            set { _PGrabar = value; }
        }
        public bool PModificar
        {
            get { return _PModificar; }
            set { _PModificar = value; }
        }
        public bool PEliminar
        {
            get { return _PEliminar; }
            set { _PEliminar = value; }
        }
        public bool PImprimir
        {
            get { return _PImprimir; }
            set { _PImprimir = value; }
        }

        public string Menu
        {
            get { return _Menu.TrimEnd(); }
            set { _Menu = value.TrimEnd(); }
        }
        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

    }
}
