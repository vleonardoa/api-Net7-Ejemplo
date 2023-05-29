
using DataLayer.Conexion;
using DataLayer.EntityModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LogicLayer.Logica
{
    public class Listar
    {
        public bool ListarCategorias(ref List<categoriaModel> listC)
        {
            throw new NotImplementedException();
        }

        public bool ListarRoles(ref List<DataLayer.EntityModel.ListarRol> listR)
        {
           DataLayer.EntityModel.ListarRol rol = new DataLayer.EntityModel.ListarRol();
            bool res = false;


       
            try
            {
                
                using (SqlConnection connection = new SqlConnection(DataLayer.Conexion.EjectProcAlm.ConexioDB.strSql))
                {
                    string query = "SELECT * FROM CS_ROL WHERE Estado = 1";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        if (command.Parameters.Count == 0)
                       {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        rol = new ListarRol();
                                        rol.Id_Rol = reader.GetInt32(0);
                                        rol.Descripcion = reader.GetString(1);
                                        rol.Estado = Convert.ToInt32(reader.GetBoolean(2));
                                        rol.Fecha_Creacion = Convert.ToString(reader.GetDateTime(3));
                                        rol.Fecha_Modificacion = Convert.ToString(reader.GetDateTime(4));
                                        rol.Usuario_Creacion = reader.GetString(5);
                                        rol.Usuario_Modificacion = reader.GetString(6);

                                        listR.Add(rol);



                                    }
                                    res = true; 
                                }
                                else

                                {
                                    res = false;
                                    rol.Mensaje = "No Hay Registros";
                                    listR.Add(rol);


                                }
                            }
                        }
                        else
                        {
                            connection.Close();
                            res = false;
                            rol.Mensaje = "Hubo un problema en la ejecucion";
                            listR.Add(rol);


                        }
                    }
                    connection.Close();
                }
                



            }
            catch (Exception ex)
            {
                res = false;
                rol.Mensaje = "Hubo un Error "+ ex ;
                listR.Add(rol);
            }

            return res;
        }

        public bool ListarRolesSP(ref List<ListarRol> listR)
        {
          bool res = false;
            DataLayer.EntityModel.ListarRol rol = new DataLayer.EntityModel.ListarRol();
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_ROLES", "", "");
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            rol = new ListarRol();
                            rol .Id_Rol = Convert.ToInt32(row["Id_Rol"]);
                            rol .Descripcion = Convert.ToString(row["Descripcion"]);
                            rol .Estado = Convert.ToInt32(row["Estado"]);
                            rol .Fecha_Creacion = Convert.ToString(row["Fecha_Creacion"]);
                            rol .Fecha_Modificacion = Convert.ToString(row["Fecha_Modificacion"]);
                            rol .Usuario_Creacion = Convert.ToString(row["Usuario_Creacion"]);
                            rol .Usuario_Modificacion = Convert.ToString(row["Usuario_Modificacion"]);

                            listR.Add(rol);
                        }

                        res = true;
                    }
                    else
                    {

                        rol.Mensaje= "No Hay Registros";
                        listR.Add(rol);
                        res = false;
                    }
                }


            }
            catch (Exception ex)
            {
               
                rol.Mensaje = "Hubo un Error " + ex;
                listR.Add(rol);
                res = false;
            }

            return res;
        }
    }
}