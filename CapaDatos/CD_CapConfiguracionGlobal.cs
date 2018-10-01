using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
namespace CapaDatos
{
    public class CD_CapConfiguracionGlobal
    {
        public CD_CapConfiguracionGlobal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CapConfiguracionGlobal ObtenerPorLlave(string llave)
        {
            CapConfiguracionGlobal ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_connectionString))
            {
                var res = ctx.spCapConfiguracionGlobal_ConsultarPorLlave(llave).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        public CapConfiguracionGlobal ObtenerPorLlave(string llave, sianwebmty_gEntities ctx)
        {
            CapConfiguracionGlobal ret = null;
            var res = ctx.spCapConfiguracionGlobal_ConsultarPorLlave(llave).ToList();
            if (res.Count > 0)
            {
                ret = res[0];
            }
            return ret;
        }

        public void GuardarValor(string llave, string valor)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_connectionString))
            {
                CapConfiguracionGlobal r=ObtenerPorLlave(llave, ctx);
                if (r != null)
                {
                    r.Conf_valor = valor;
                    ctx.SaveChanges();
                }
                else
                {
                    throw new Exception(String.Format("La llave {0} no existe", llave));
                }
            }
        }

        private string _connectionString;
    }
}
