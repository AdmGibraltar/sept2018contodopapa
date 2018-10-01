using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eTerritorio
    {
        private int _Id_Emp { get; set; }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd { get; set; }
        public int Id_Cd 
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Ter { get; set; }
        public int Id_Ter 
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        
        private string _Ter_Nombre { get; set; }
        public string Ter_Nombre 
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private int _Id_Uen { get; set; }
        public int Id_Uen 
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private int _Id_Rik { get; set; }
        public int Id_Rik 
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private int _Id_Seg { get; set; }
        public int Id_Seg 
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private bool _Ter_Activo { get; set; }
        public bool Ter_Activo 
        {
            get { return _Ter_Activo; }
            set { _Ter_Activo = value; }
        }
        
        private int _Id_TipoCliente { get; set; }
        public int Id_TipoCliente 
        {
            get { return _Id_TipoCliente; }
            set { _Id_TipoCliente = value; }
        }
        
        private string _Id_Local { get; set; }
        public string Id_Local 
        {
            get { return _Id_Local; }
            set { _Id_Local = value; }
        }

        private int _Id_TipoRepresentante { get; set; }
        public int Id_TipoRepresentante 
        {
            get { return _Id_TipoRepresentante; }
            set { _Id_TipoRepresentante = value; }
        }

        private int _Cve_Terr { get; set; }
        public int Cve_Terr 
        {
            get { return _Cve_Terr; }
            set { _Cve_Terr = value; }
        }

        private int _Id_TerAnt { get; set; }
        public int Id_TerAnt 
        {
            get { return _Id_TerAnt; }
            set { _Id_TerAnt = value; }
        }
        //
    }
}
