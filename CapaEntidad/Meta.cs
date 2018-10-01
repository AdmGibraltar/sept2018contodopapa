using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Meta
   {
       private int _Id_Emp;
       private int _Id_Met;
       private int _Id_Cd;
       private int _Id_Rik;
       private string _Rik_Nombre;
       private string _Cd_Nombre;
       private int _Met_Proyectos;
       private double _Met_MontoProyecto;
       private int _Met_CantCerrado;
       private double _Met_MontCerrado;
       private double _Met_Avances;
       private int _Cantidad;
       private double _Monto;
       
       public int Id_Emp
       {
           get { return _Id_Emp; }
           set { _Id_Emp = value; }
       }
       public int Id_Met
       {
           get { return _Id_Met; }
           set { _Id_Met = value; }
       }
       public int Id_Cd
       {
           get { return _Id_Cd; }
           set { _Id_Cd = value; }
       }
       public int Id_Rik
       {
           get { return _Id_Rik; }
           set { _Id_Rik = value; }
       }
       public string Rik_Nombre
       {
           get { return _Rik_Nombre; }
           set { _Rik_Nombre = value; }
       }
       public string Cd_Nombre
       {
           get { return _Cd_Nombre; }
           set { _Cd_Nombre = value; }
       }
       public int Met_Proyectos
       {
           get { return _Met_Proyectos; }
           set { _Met_Proyectos = value; }
       }
       public double Met_MontoProyecto
       {
           get { return _Met_MontoProyecto; }
           set { _Met_MontoProyecto = value; }
       }
       public int Met_CantCerrado
       {
           get { return _Met_CantCerrado; }
           set { _Met_CantCerrado = value; }
       }
       public double Met_MontCerrado
       {
           get { return _Met_MontCerrado; }
           set { _Met_MontCerrado = value; }
       }
       public double Met_Avances
       {
           get { return _Met_Avances; }
           set { _Met_Avances = value; }
       }
       public int Cantidad
       {
           get { return _Cantidad; }
           set { _Cantidad = value; }
       }
       public double Monto
       {
           get { return _Monto; }
           set { _Monto = value; }
       }      
    }
}
