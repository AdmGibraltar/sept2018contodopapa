using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CapaModelo_CC.CuentasCoorporativas;


namespace CapaDatos
{
    public class CD_CatCNac_Solicitudes
    {


        sianwebmty_CCEntities model = new sianwebmty_CCEntities();
        //public CD_CatCNac_Solicitudes(sianwebmty_gEntities modelo)
        //{
        //    model = modelo;
        //}

        public List<CatCNac_Solicitudes> ConsultarTodos()
        {
            var res = model.CatCNac_Solicitudes.ToList();
            return res;
        }

        public CatCNac_Solicitudes ConsultarItem(int id)
        {
            var res = model.CatCNac_Solicitudes.Where(x => x.Id == id).FirstOrDefault();
            return res;
        }
    }

}