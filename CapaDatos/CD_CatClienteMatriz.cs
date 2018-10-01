using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using CapaModelo_CC.CuentasCoorporativas;


namespace CapaDatos
{
    public class CD_CatClienteMatriz
    {

        sianwebmty_CCEntities model;


        public CD_CatClienteMatriz(sianwebmty_CCEntities modelo)
        {
            model = modelo;
        }

        public CD_CatClienteMatriz()
        {
            model = new sianwebmty_CCEntities();
        }


        public List<CatCNac_Matriz> ConsultarTodos()
        {
            var res = model.CatCNac_Matriz.ToList();
            return res;
        }

        public CatCNac_Matriz ConsultarMatriz(int id_matriz)
        {

            var res = model.CatCNac_Matriz.Where(x => x.Id == id_matriz).FirstOrDefault();
            return res;
        }

        public CatCNac_Matriz ConsultarMatrizItem(int id_matriz)
        {

            var res = model.CatCNac_Matriz.Where(x => x.Id == id_matriz).FirstOrDefault();
            return res;
        }




        public List<CatACYS_DirFiscales> ConsutarDirFiscales()
        {
            return model.CatACYS_DirFiscales.ToList();
        }

        public List<CatACYS_DirFiscales> ConsutarDirFiscales(int idMatriz)
        {
            return model.CatACYS_DirFiscales.Where(x=>x.Id_ClienteMatriz==idMatriz).ToList();
        }



        public List<CatAcys_Productos> ConsultarProductos(int id_TG, int id_Acys)
        {
            return model.CatAcys_Productos.Where(x => x.Id_TG == id_TG && x.Id_ACYS==id_Acys).ToList();
        }

        public List<CatCNac_PermisosCamposACYS> ConsultaPermisosCampos()
        {
            return model.CatCNac_PermisosCamposACYS.ToList();
        }

        public Boolean EsClienteVinculado(int Id_cte)
        {
            var cliente = model.CatCNac_Estructura.Where(x => x.Id_Cte == Id_cte).FirstOrDefault();

            if (cliente != null) return true;
            else return false;

        }

    }
}
