using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CapaModelo_CC.CuentasCoorporativas;


namespace CapaDatos
{
    public class CD_CatCNac_Matriz
    {
        sianwebmty_CCEntities model = new sianwebmty_CCEntities();
        //public CD_CatCNac_Matriz(sianwebmty_gEntities modelo)
        //{
        //    model = modelo;
        //}


        public List<CatCNac_Matriz> ConsultarTodos()
        {
            var res = model.CatCNac_Matriz.ToList();
            return res;
        }


        public List<CatCNac_Estructura> ConsultarEstructura(int idMatriz, int Id_Emp, int Id_Cd)
        {
            var res = model.CatCNac_Estructura.Where(x => x.Id_Matriz == idMatriz && x.Nivel_ACYS != null && x.Sucursal==Id_Cd).ToList();
            return res;
        }


        public List<CatCNac_Estructura> ConsultarEstructura()
        {
            var res = model.CatCNac_Estructura.Where(x=>x.Id_Matriz!=null && x.Nivel_ACYS !=null).ToList();
            return res;
        }


        public CatCliente ConsultaCliente(int idCliente, int Id_Emp, int Id_Cd)
        {
            var res = model.CatCliente.Where(x => x.Id_Cte == idCliente && x.Id_Emp== Id_Emp && x.Id_Cd== Id_Cd).FirstOrDefault();
            return res;

        }

        public List<CatACYS_DirFiscales> ConsultaDireccionesFiscales(int idMatriz)
        {
            var res = model.CatACYS_DirFiscales.Where(x => x.Id_ClienteMatriz == idMatriz).ToList();
            return res;
        }

        public List<spCatCNac_DireccionesFiscales_Result> ConsultaDireccionesFiscales_SP(string clienteSIAN)
        {
            var res = model.spCatCNac_DireccionesFiscales(clienteSIAN).ToList();
            return res;
        }


        public List<Intra_CFD_CuentaNacional> ConsultaIntranetCuentaNacional(int idMatriz, int DireccionId)
        {

            var res = model.Intra_CFD_CuentaNacional.Where(x => x.id_Matriz == idMatriz && x.DireccionID == DireccionId).ToList();
            return res;
        }


        public List<CatCNac_Usuario> ComboAsesores(int idMatriz)
        {
            var res = model.CatCNac_Usuario.Where(x => x.IdMatriz == idMatriz).ToList();
            return res;
        }

        public List<CatCNac_RemisionesMov80> ComboRemisionesMov80()
        {
            var res = model.CatCNac_RemisionesMov80.ToList();
            return res;
        }

        public Boolean GuardarSolicitud(CatCNac_Solicitudes solicitud)
        {
            model.CatCNac_Solicitudes.Add(solicitud);
            model.SaveChanges();
            return true;
        }

        public List<CatCNac_Solicitudes> ConsultarSolicitudes(string usuario)
        {
            var res = model.CatCNac_Solicitudes.Where(x=>x.Usuario==usuario).OrderBy(x=>x.Estatus).ToList();
            return res;
        }


        public CatCNac_Solicitudes ConsultarSolicitudes(int idEstructura)
        {
            var res = model.CatCNac_Solicitudes.Where(x => x.Id_Estructura == idEstructura && (x.Estatus==1 || x.Estatus==2)).FirstOrDefault();
            return res;
        }


        public Boolean CancelarSolicitud(int idEstructura)
        {
            var sol = model.CatCNac_Solicitudes.Where(x => x.Id_Estructura == idEstructura && x.Estatus==1).FirstOrDefault();

            sol.Estatus = 4;
            model.Entry<CatCNac_Solicitudes>(sol).State = EntityState.Modified;
            model.SaveChanges();
            return true;
        }


    }
}
