using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Posicion
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
        private int _Id_Pos;

        public int Id_Pos
        {
            get { return _Id_Pos; }
            set { _Id_Pos = value; }
        }
        private string _Pos_Descripcion;

        public string Pos_Descripcion
        {
            get { return _Pos_Descripcion; }
            set { _Pos_Descripcion = value; }
        }
        private int _Id_Seg;

        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        private int _Id_Uen;

        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }
        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        private string _EstatusStr;
        
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        private int _Id_Est;

        public int Id_Est
        {
            get { return _Id_Est; }
            set { _Id_Est = value; }
        }

    }
}
