using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
   public class CN_Convenio
    {
       public void ConsultaListaConvenios(Convenio conv, ref List<Convenio> ListUtil, ref List<Convenio> ListNoUtil, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaListaConvenios(conv, ref ListUtil, ref ListNoUtil, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConsecutivo(int Id_Cat, ref Convenio conv, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaConsecutivo(Id_Cat, ref conv,Conexion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarConvenio(Convenio conv, List<ConvenioDet> List, ref int Verificador,ref int Id_PC, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.InsertarConvenio(conv, List, ref Verificador,ref Id_PC, Conexion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void BajaConvenio(int Id_PC, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.BajaConvenio(Id_PC, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConvenio(int Id_PC, ref Convenio conv, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaConvenio(Id_PC, ref conv, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConvenioDet(int Id_PC, ref List<ConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaConvenioDet(Id_PC, ref List, Conexion);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ModificaConvenio(Convenio conv, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ModificaConvenio(conv, ref Verificador, Conexion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarConvenioDet(int Id_PC, List<ConvenioDet> List, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.InsertarConvenioDet(Id_PC, List, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaProConvSucursal(int Id_PC, ref List<Convenio> List, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaProConvSucursal(Id_PC, ref List, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarProConvSucursal(List<Convenio> List, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.InsertarProConvSucursal ( List , ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaUsuariosEspeciales(ref Convenio conv, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaUsuariosEspeciales(ref conv, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ModificaUsuariosEspeciales(Convenio conv, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ModificaUsuariosEspeciales(conv, ref Verificador, Conexion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaPermisosSucursal(int Id_U, ref List<Convenio> List, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaPermisosSucursal(Id_U, ref List, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarPermisosSucursal(List<Convenio> List, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.InsertarPermisosSucursal(List, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConsecutivoSolicitud(ref int Id_Sol, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaConsecutivoSolicitud(ref Id_Sol, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaCapturaUsuario(string Cat_DescCorta, ref string Cat_CapturaUsuario, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConsultaCapturaUsuario(Cat_DescCorta, ref Cat_CapturaUsuario, Conexion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_Insertar(SolConvenio sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_Insertar(sol, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_InsertarDet(int Id_Sol, SolConvenio sol, List<SolConvenioDet> List, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_InsertarDet(Id_Sol, sol, List, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_Consulta(int Id_Sol, ref SolConvenio sol, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_Consulta(Id_Sol, ref sol, Conexion);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_ConsultaAt(string Sol_Unique, ref SolConvenio sol, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_ConsultaAt(Sol_Unique, ref sol, Conexion);


           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSolicitud_ConsultaDet(int Id_Sol, ref  List<SolConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_ConsultaDet(Id_Sol, ref List, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_Cancelar(int Id_Sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_Cancelar(Id_Sol, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
 
       }
       public void ConvenioSolicitud_Modificar(SolConvenio sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_Modificar(sol, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_ConsultaDetAt(int Id_Sol, ref  List<SolConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_ConsultaDetAt(Id_Sol, ref List, Conexion);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSolicitud_Atender(int Id_Sol, List<SolConvenioDet> List, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_Atender(Id_Sol, List, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_EnviarCorreoCreoSol(int Id_Sol, ref int Verificador, string Conexion)
       {
           try
           {
                CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_EnviarCorreoCreoSol (Id_Sol,ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_EnviarCorreoAtendio(int Id_Sol, ref int Verificador, string Conexion)
       {
           try
           {
                CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSolicitud_EnviarCorreoAtendio (Id_Sol,ref Verificador, Conexion);
           }
           catch (Exception)
           {
               
               throw;
           }
       }
       public void Convenio_ConsultaVinculados(int Id_PC, int Id_CD, ref List<SolConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.Convenio_ConsultaVinculados(Id_PC, Id_CD, ref List, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void Convenio_DesvinculaUno(SolConvenioDet sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.Convenio_DesvinculaUno(sol, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioCreo_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioCreo_EnviarCorreo(Id_PC, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioModifico_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioModifico_EnviarCorreo(Id_PC, Conexion);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSustituyo_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSustituyo_EnviarCorreo(Id_PC, Conexion);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioCancelo_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioCancelo_EnviarCorreo(Id_PC, Conexion);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioDesvinculo_EnviarCorreo(SolConvenioDet sol, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioDesvinculo_EnviarCorreo(sol, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSustituyo_ActualizaClientes(int Id_PC, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.ConvenioSustituyo_ActualizaClientes(Id_PC, ref Verificador, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void Convenion_ConsultaVinculadosTodos(int Id_Cd, ref DataTable dt, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.Convenion_ConsultaVinculadosTodos(Id_Cd, ref dt, Conexion);
           }
           catch (Exception)
           {
               
               throw;
           }
       }
       public void Convenio_ConsultaListaDet(Convenio conv, ref DataTable dt, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.Convenio_ConsultaListaDet(conv, ref dt, Conexion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       public void Convenio_ConsultaPrecio(ConvenioDet conv, ref ConvenioDet convdet, string Conexion)
       {
           try
           {
               CD_Convenio cd_conv = new CD_Convenio();
               cd_conv.Convenio_ConsultaPrecio(conv,ref convdet, Conexion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

    }
}
