using DataLayer.Conexion;
using DataLayer.EntityModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Logica
{
    public class CategoriaLog
    {
        private string id_cat;
        private int estado;

        public CategoriaLog()
        {
        }

        public CategoriaLog(string id_cat, int estado)
        {
            this.id_cat = id_cat;
            this.estado = estado;
        }

        public bool desactivarCategoriaSP(ref categoriaModel listC)
        {
            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("DelCat", "", "");
                objStoreProc.Add_Par_VarChar_Input("@id_cat",this.id_cat);
                objStoreProc.Add_Par_Int_Input("@estado", this.estado);
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    DataRow rows = data.Rows[0];
                    int varia = Convert.ToInt32(rows["msg"]);
                    if(varia==1) {

                        listC.Mensaje = "Cambio de estado Correctamente";
                        res = true;
                    }
                    else
                    {
                        listC.Mensaje = "Cambio de estado Fallido";
                        res = false;
                    }
                    
                }
                else
                {

                    listC.Mensaje = msgResEjecucion;
                    res = false;

                }


            }
            catch (Exception ex)
            {

                listC.Mensaje = "Hubo un Error " + ex;
                res = false;
            }

            return res;
        }

        public bool ListarCategorias(ref List<categoriaModel> listC)
        {
            DataLayer.EntityModel.categoriaModel cat = new DataLayer.EntityModel.categoriaModel();
            bool res = false;



            try
            {

                using (SqlConnection connection = new SqlConnection(DataLayer.Conexion.EjectProcAlm.ConexioDB.strSql))
                {
                    string query = "SELECT * FROM T_CATEGORIA WHERE Estado = 1";
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
                                        cat = new categoriaModel();
                                        cat.Id_Categoria = reader.GetString(0);
                                        cat.Nombre = reader.GetString(1);
                                        cat.Descripcion = reader.GetString(2);
                                        cat.Fecha_Creacion = Convert.ToString(reader.GetDateTime(3));
                                        cat.Fecha_Modificacion = Convert.ToString(reader.GetDateTime(4));
                                        if (!reader.IsDBNull(5))
                                        {
                                            cat.Usuario_Creacion = reader.GetString(5);
                                        }
                                        else
                                        {
                                            cat.Usuario_Creacion = "";
                                        }
                                        if (!reader.IsDBNull(6))
                                        {
                                            cat.Usuario_Modificacion = reader.GetString(6);
                                        }
                                        else
                                        {
                                            cat.Usuario_Modificacion = "";
                                        }
                                        cat.Estado = Convert.ToInt32(reader.GetBoolean(7));
                                        listC.Add(cat);



                                    }
                                    res = true;
                                }
                                else

                                {
                                    res = false;
                                    cat.Mensaje = "No Hay Registros";
                                    listC.Add(cat);


                                }
                            }
                        }
                        else
                        {
                            connection.Close();
                            res = false;
                            cat.Mensaje = "Hubo un problema en la ejecucion";
                            listC.Add(cat);


                        }
                    }
                    connection.Close();
                }




            }
            catch (Exception ex)
            {
                res = false;
                cat.Mensaje = "Hubo un Error " + ex;
                listC.Add(cat);
            }

            return res;
        }

        public bool ListarCategoriasSP(ref List<categoriaModel> listC)
        {
            bool res = false;
            DataLayer.EntityModel.categoriaModel cat = new DataLayer.EntityModel.categoriaModel();
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("listCat", "", "");
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            cat = new categoriaModel();
                            cat.Id_Categoria = Convert.ToString(row["ID_CATEGORIA"]);
                            cat.Nombre = Convert.ToString(row["NOMBRE"]);
                            cat.Descripcion = Convert.ToString(row["DESCRIPCION"]);

                            cat.Fecha_Creacion = Convert.ToString(row["FECHA _CREACION"]);
                            cat.Fecha_Modificacion = Convert.ToString(row["FECHA_MODIFICACION"]);
                            cat.Usuario_Creacion = Convert.ToString(row["USUARIO_CREACION"]);
                            cat.Usuario_Modificacion = Convert.ToString(row["USUARIO_MODIFICACION"]);
                            cat.Estado = Convert.ToInt32(row["ESTADO"]);
                            listC.Add(cat);
                        }

                        res = true;
                    }
                    else
                    {

                        cat.Mensaje = "No Hay Registros";
                        listC.Add(cat);
                        res = false;
                    }
                }
                else
                {

                    cat.Mensaje = msgResEjecucion;
                    listC.Add(cat);
                    res = false;
                    
                }


            }
            catch (Exception ex)
            {

                cat.Mensaje = "Hubo un Error " + ex;
                listC.Add(cat);
                res = false;
            }

            return res;
        }
    }
}
