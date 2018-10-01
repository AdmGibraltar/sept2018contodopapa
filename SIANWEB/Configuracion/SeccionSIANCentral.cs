using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace SIANWEB.Configuracion
{
    public class SeccionSIANCentral
        : ConfigurationSection
    {
        [ConfigurationProperty("conexiones", IsRequired=true, IsDefaultCollection=true)]
        [ConfigurationCollection(typeof(ConexionSIANWebCollection))]
        public ConexionSIANWebCollection ConexionesSIANWeb
        {
            get
            {
                return (ConexionSIANWebCollection)this["conexiones"];
            }
            set
            {
                this["conexiones"]=value;
            }
        }
    }

    public class ConexionSIANWeb
        : ConfigurationElement
    {
        public ConexionSIANWeb()
        {
        }

        [ConfigurationProperty("nombre", IsKey=true, IsRequired=true)]
        public string Nombre
        {
            get
            {
                return (string)base["nombre"];
            }
            set
            {
                base["nombre"] = value;
            }
        }

        [ConfigurationProperty("cadenaConexionEF", IsRequired = true)]
        public string ConexionEF
        {
            get
            {
                return (string)base["cadenaConexionEF"];
            }
            set
            {
                base["cadenaConexionEF"] = value;
            }
        }
    }

    public class ConexionSIANWebCollection
        : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConexionSIANWeb();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConexionSIANWeb)element).Nombre;
        }


    }

    public class SeccionSIANCentralConfig
    {
        private SeccionSIANCentralConfig()
        {
        }

        static SeccionSIANCentralConfig()
        {
            _conexiones = ((SeccionSIANCentral)(System.Configuration.ConfigurationManager.GetSection("sianCentralGroup/seccionSIANCentral"))).ConexionesSIANWeb;
        }

        public static ConexionSIANWeb ObtenerConexionEF(string nombreSIANWeb)
        {
            var conexiones = from ConexionSIANWeb c in _conexiones
                             where c.Nombre==nombreSIANWeb
                             select c;
            if (conexiones.Count() > 0)
                return conexiones.First();
            return null;
        }

        public static ConexionSIANWebCollection Conexiones
        {
            get
            {
                return _conexiones;
            }
        }

        private static ConexionSIANWebCollection _conexiones = null;
    }
}