using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCatTerritorio
    {
        int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        int _Id_Uen;
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        bool _Ter_Activo;
        public bool Ter_Activo
        {
            get { return _Ter_Activo; }
            set { _Ter_Activo = value; }
        }

        int _Id_TipoCliente;
        public int Id_TipoCliente
        {
            get { return _Id_TipoCliente; }
            set { _Id_TipoCliente = value; }
        }

        string _Id_Local;
        public string Id_Local
        {
            get { return _Id_Local; }
            set { _Id_Local = value; }
        }

        int _Id_TipoRepresentante;
        public int Id_TipoRepresentante
        {
            get { return _Id_TipoRepresentante; }
            set { _Id_TipoRepresentante = value; }
        }

        int _Cve_Terr;
        public int Cve_Terr
        {
            get { return _Cve_Terr; }
            set { _Cve_Terr= value; }
        }

        int _Id_TerAnt;
        public int Id_TerAnt
        {
            get { return _Id_TerAnt; }
            set { _Id_TerAnt = value; }
        }
        //
    }
}
