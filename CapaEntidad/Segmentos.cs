using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Segmentos
    {
        int _Id_Emp;
        int _Id_Seg;
        int _Id_Seg_Ant;
        string _Descripcion;
        int _Id_UEN;
        string _Unidades;
        double _Valor;
        bool _Estatus;
        string _EstatusStr;
        string _Seg_IdXUen;
        
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        public int Id_Seg_Ant
        {
            get { return _Id_Seg_Ant; }
            set { _Id_Seg_Ant = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public int Id_UEN
        {
            get { return _Id_UEN; }
            set { _Id_UEN = value; }
        }
        public string Unidades
        {
            get { return _Unidades; }
            set { _Unidades = value; }
        }
        public double Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
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
        private int? _Id_U;
        public int? Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public string Seg_IdXUen
        {
            get { return _Seg_IdXUen; }
            set { _Seg_IdXUen = value; }
        }
    }
}
