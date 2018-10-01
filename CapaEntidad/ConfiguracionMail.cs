using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ConfiguracionMail
    {
        private int _Id_Cd;
        private string _Mail_Servidor;
        private string _Mail_Usuario;
        private string _Mail_Contraseña;
        private string _Mail_Puerto;
        private string _Mail_Remitente;
        private string _Ofi_Logo;
        public string Ofi_Logo
        {
            get { return _Ofi_Logo; }
            set { _Ofi_Logo = value; }
        }
        public string Mail_Remitente
        {
            get { return _Mail_Remitente; }
            set { _Mail_Remitente = value; }
        }
        public string Mail_Puerto
        {
            get { return _Mail_Puerto; }
            set { _Mail_Puerto = value; }
        }
        public string Mail_Contraseña
        {
            get { return _Mail_Contraseña; }
            set { _Mail_Contraseña = value; }
        }
        public string Mail_Usuario
        {
            get { return _Mail_Usuario; }
            set { _Mail_Usuario = value; }
        }
        public string Mail_Servidor
        {
            get { return _Mail_Servidor; }
            set { _Mail_Servidor = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

    }
}
