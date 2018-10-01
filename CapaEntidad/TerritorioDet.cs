using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TerritorioDet
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ter;
        int _Id_Ter_Det;
        int _Anyo;
        int _Mes;
        int _Presupuesto;

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
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Ter_Det
        {
            get { return _Id_Ter_Det; }
            set { _Id_Ter_Det = value; }
        }
        public int Anyo
        {
            get { return _Anyo; }
            set { _Anyo = value; }
        }
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }
        public int Presupuesto
        {
            get { return _Presupuesto; }
            set { _Presupuesto = value; }
        }
    }
}
