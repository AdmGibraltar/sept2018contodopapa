using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ExpClienteCaracNecesidad
    {
        private Int32 _Id_Cd;
        private Int32 _Id_Cli;
        private Int32 _Id_CN;
        private Int32 _Id_Gpo;
        private Int32 _Id_SGpo;
        private Int32 _Id_DSGpo;
        private string _Id_GpoStr;
        private string _Id_SGpoStr;
        private string _Id_DSGpoStr;

        private bool _CaracteristicaNecesidad;

        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public Int32 Id_Cli
        {
            get { return _Id_Cli; }
            set { _Id_Cli = value; }
        }
        public Int32 Id_CN
        {
            get { return _Id_CN; }
            set { _Id_CN = value; }
        }
        public Int32 Id_Gpo
        {
            get { return _Id_Gpo; }
            set { _Id_Gpo = value; }
        }
        public Int32 Id_SGpo
        {
            get { return _Id_SGpo; }
            set { _Id_SGpo = value; }
        }
        public Int32 Id_DSGpo
        {
            get { return _Id_DSGpo; }
            set { _Id_DSGpo = value; }
        }
        public string Id_GpoStr
        {
            get { return _Id_GpoStr; }
            set { _Id_GpoStr = value; }
        }
        public string Id_SGpoStr
        {
            get { return _Id_SGpoStr; }
            set { _Id_SGpoStr = value; }
        }
        public string Id_DSGpoStr
        {
            get { return _Id_DSGpoStr; }
            set { _Id_DSGpoStr = value; }
        }
        public bool CaracteristicaNecesidad
        {
            get { return _CaracteristicaNecesidad; }
            set { _CaracteristicaNecesidad = value; }
        }

    }
}
