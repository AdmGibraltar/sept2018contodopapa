using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo_CC.CuentasCoorporativas;


namespace CapaNegocios
{
    public class CN_CatClienteMatriz
    {
    


        public CN_CatClienteMatriz()
        {
         
        }


       public List<CatCNac_Matriz> ConsultarTodos()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarTodos();
        }

       


        public CatCNac_Matriz ConsultarMatriz(int id_matriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarMatriz(id_matriz);
        }


        public List<CatACYS_DirFiscales> ConsutarDirFiscales()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();

            return CMatriz.ConsutarDirFiscales();
        }

        public List<CatACYS_DirFiscales> ConsutarDirFiscales(int idMatriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();

            return CMatriz.ConsutarDirFiscales(idMatriz);
        }


        public List<CatAcys_Productos> ConsultarProductos(int id_TG, int id_Acys)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();

            return CMatriz.ConsultarProductos(id_TG, id_Acys);
        }




        public CatCNac_Matriz ConsultarMatrizItem(int id_matriz)
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultarMatrizItem(id_matriz);
        }


         public List<CatCNac_PermisosCamposACYS> ConsultaPermisosCampos()
        {
            CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
            return CMatriz.ConsultaPermisosCampos();
        }

         public Boolean EsClienteVinculado(int Id_cte)
         {
             CD_CatClienteMatriz CMatriz = new CD_CatClienteMatriz();
             return CMatriz.EsClienteVinculado(Id_cte);

         }
    }
}
