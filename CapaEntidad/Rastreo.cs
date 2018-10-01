using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Rastreo
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Ras_TipoDoc;
        private string _Ras_Doc;
        private int _Id_U;
        private string _U_Nombre;
        private string _Doc_TipoMov;
        private int _Id_Doc;
        private DateTime _Doc_Fecha;
        private double _Doc_Importe;
        private string _Doc_Estatus;
        private string _Doc_EstatusStr;
        public string Ras_SerieDoc;       
        private string _Actividad;
        private int _Id_Relacion;

        public string U_Actividad
        {
            get { return _Actividad; }
            set { _Actividad = value; }
        }


        public int Id_Relacion
        {
            get { return _Id_Relacion; }
            set { _Id_Relacion = value; }
        }

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
        public int Ras_TipoDoc
        {
            get { return _Ras_TipoDoc; }
            set { _Ras_TipoDoc = value; }
        }
        public string Ras_Doc
        {
            get { return _Ras_Doc; }
            set { _Ras_Doc = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }
        public string Doc_TipoMov
        {
            get { return _Doc_TipoMov; }
            set { _Doc_TipoMov = value; }
        }
        public int Id_Doc
        {
            get { return _Id_Doc; }
            set { _Id_Doc = value; }
        }
        public DateTime Doc_Fecha
        {
            get { return _Doc_Fecha; }
            set { _Doc_Fecha = value; }
        }
        public double Doc_Importe
        {
            get { return _Doc_Importe; }
            set { _Doc_Importe = value; }
        }
        public string Doc_Estatus
        {
            get { return _Doc_Estatus; }
            set { _Doc_Estatus = value; }
        }
        public string Doc_EstatusStr
        {
            get { return _Doc_EstatusStr; }
            set { _Doc_EstatusStr = value; }
        }

        public string Cd_Externo { get; set; }

        private string _ras_FolioFiscal;

        public string Ras_FolioFiscal
        {
            get { return _ras_FolioFiscal; }
            set { _ras_FolioFiscal = value; }
        }
    }
}
