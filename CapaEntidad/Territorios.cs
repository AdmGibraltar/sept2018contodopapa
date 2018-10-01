using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Territorios
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ter;
        int _Id_TerNuevo;
        int _Id_TerAnt;
        int _Cve_Terr;
        string _Descripcion;
        int _Id_Uen;
        string _Uen_Descripcion;
        int _Id_Rik;
        string _Rik_Nombre;
        int _Id_Seg;
        string _Seg_Nombre;
        int _Id_TipoCliente;
        string _TipoCliente_Nombre;
        string _Id_Local;
        int _Id_TipoRepresentante;
        string _TipoRepresentante_Nombre;
        bool _Estatus;
        string _EstatusStr;
        int _Consecutivo;

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

        public int Id_TerNuevo
        {
            get { return _Id_TerNuevo; }
            set { _Id_TerNuevo = value; }
        }

        
        public int Id_TerAnt
        {
            get { return _Id_TerAnt; }
            set { _Id_TerAnt = value; }
        }


        public int Cve_Terr
        {
            get { return _Cve_Terr; }
            set { _Cve_Terr = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        public string Uen_Descripcion
        {
            get { return _Uen_Descripcion; }
            set { _Uen_Descripcion = value; }
        }

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        public string Seg_Nombre
        {
            get { return _Seg_Nombre; }
            set { _Seg_Nombre = value; }
        }

        public int Id_TipoCliente
        {
            get { return _Id_TipoCliente; }
            set { _Id_TipoCliente = value; }
        }

        public string TipoCliente_Nombre
        {
            get { return _TipoCliente_Nombre; }
            set { _TipoCliente_Nombre = value; }
        }

        public string Id_Local
        {
            get { return _Id_Local; }
            set { _Id_Local = value; }
        }

        public int Id_TipoRepresentante
        {
            get { return _Id_TipoRepresentante; }
            set { _Id_TipoRepresentante = value; }
        }

        public string TipoRepresentante_Nombre
        {
            get { return _TipoRepresentante_Nombre; }
            set { _TipoRepresentante_Nombre = value; }
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

        public int Consecutivo
        {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }       


      
    }
}
