using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class SeguimientoProductos
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Prd;
        private int _Id_SegPrd;
        private DateTime _Seg_fecha;
        private string _Seg_Comentarios;

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
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public int Id_SegPrd
        {
            get { return _Id_SegPrd; }
            set { _Id_SegPrd = value; }
        }
        public DateTime Seg_fecha
        {
            get { return _Seg_fecha; }
            set { _Seg_fecha = value; }
        }
        public string Seg_Comentarios
        {
            get { return _Seg_Comentarios; }
            set { _Seg_Comentarios = value; }
        }
    }
}
