using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RutaEntrega
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id;
        int _Id_Ant;
        string _Descripcion;
        int _Id_Ter;
        int _Sem_Ini;
        int _Incidencia;
        int _Dia;
        bool _Estatus;
        string _EstatusStr;

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
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Sem_Ini
        {
            get { return _Sem_Ini; }
            set { _Sem_Ini = value; }
        }
        public int Incidencia
        {
            get { return _Incidencia; }
            set { _Incidencia = value; }
        }
        public int Dia
        {
            get { return _Dia; }
            set { _Dia = value; }
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
