using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class SegmentoProducto
    {
        int _Id_Emp;
        int _id_Seg;
        int _Id_Pds_Ant;
        int _Id_Prd;
        int _Id_Cd;
        string _Pds_Descripcion;
        double _Pds_Contribucion;
        bool _Pds_Activo;
        string _Pds_ActivoStr;
        
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int id_Seg
        {
            get { return _id_Seg; }
            set { _id_Seg = value; }
        }
        public int Id_Pds_Ant
        {
            get { return _Id_Pds_Ant; }
            set { _Id_Pds_Ant = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public string Pds_Descripcion
        {
            get { return _Pds_Descripcion; }
            set { _Pds_Descripcion = value; }
        }
        public double Pds_Contribucion
        {
            get { return _Pds_Contribucion; }
            set { _Pds_Contribucion = value; }
        }
        public bool Pds_Activo
        {
            get { return _Pds_Activo; }
            set { _Pds_Activo = value; }
        }
        public string Pds_ActivoStr
        {
            get { return _Pds_ActivoStr; }
            set { _Pds_ActivoStr = value; }
        }
    }
}
