using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
namespace CapaDatos
{
    public class CD_CatTipoVenta
    {
        public CD_CatTipoVenta(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CatTipoVenta ConsultarPorNombre(string nombre)
        {
            CatTipoVenta ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_connectionString))
            {
                var result = ctx.spCatTipoVenta_ConsultaPorNombre(nombre).ToList();
                if (result.Count > 0)
                {
                    ret = result[0];
                }
            }

            return ret;
        }

        private string _connectionString;
    }
}
