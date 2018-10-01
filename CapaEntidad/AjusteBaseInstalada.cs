using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AjusteBaseInstalada
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private string _Cd_Nombre;
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        private int _Id_Abi;
        public int Id_Abi
        {
            get { return _Id_Abi; }
            set { _Id_Abi = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private string _U_Nombre;
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        private DateTime _Abi_Fecha;
        public DateTime Abi_Fecha
        {
            get { return _Abi_Fecha; }
            set { _Abi_Fecha = value; }
        }

        private DateTime? _Abi_FechaAutoriza;
        public DateTime? Abi_FechaAutoriza
        {
            get { return _Abi_FechaAutoriza; }
            set { _Abi_FechaAutoriza = value; }
        }

        private string _Abi_Unique;
        public string Abi_Unique
        {
            get { return _Abi_Unique; }
            set { _Abi_Unique = value; }
        }

        private List<AjusteBaseInstaladaDet> _ListaAjusteBaseInstalada;
        public List<AjusteBaseInstaladaDet> ListaAjusteBaseInstalada
        {
            get { return _ListaAjusteBaseInstalada; }
            set { _ListaAjusteBaseInstalada = value; }
        }
    }
}
