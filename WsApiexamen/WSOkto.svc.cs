using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;

namespace WsApiexamen
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WebServiceOkto : IWSOkto
    {
        public List<string> ActualizarExamen(int id, string Nombre, string Descripcion)
        {
            bool exito = false;
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DESKTOP-OKTO"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tblExamen SET Nombre = @Nombre, Descripcion = @Descripcion WHERE idExamen = @idExamen;", cn);
                cmd.Parameters.AddWithValue("@idExamen", id);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);

                try
                {
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        exito = true;
                        mensaje = "Se Actualizo El registro Con Exito.";
                    }
                    else
                    {
                        mensaje = "Registro No Encontrado";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error al actualizar el examen: " + ex.Message;
                }
            }

            List<string> resultado = new List<string>();
            resultado.Add(exito.ToString());
            resultado.Add(mensaje);

            return resultado;
        }

            public List<tblExamen> ConsultarExamen(int id, string Nombre, string Descripcion)
            {
                List<tblExamen> resultados = new List<tblExamen>();

                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DESKTOP-OKTO"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tblExamen WHERE idExamen = @idExamen AND Nombre = @Nombre AND Descripcion = @Descripcion;", cn);
                    cmd.Parameters.AddWithValue("@idExamen", id);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", Descripcion);

                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            tblExamen tbl = new tblExamen();
                            tbl.idExamen = Convert.ToInt32(dr["idExamen"]);
                            tbl.Nombre = dr["Nombre"].ToString();
                            tbl.Descripcion = dr["Descripcion"].ToString();
                            resultados.Add(tbl);
                        }
                    }
                    else 
                    {
                        throw new Exception("Registro No Encontrado");
                    }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception ("Error al consultar el examen: " + ex.Message);
                    }
                }

                return resultados;
            }

        public List<tblExamen> ConsultarExamenbyId(int id)
        {
            List<tblExamen> resultado = new List<tblExamen>();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DESKTOP-OKTO"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblExamen WHERE idExamen = @idExamen;", cn);
                cmd.Parameters.AddWithValue("@idExamen", id);

                try
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            tblExamen tbl = new tblExamen();
                            tbl.idExamen = Convert.ToInt32(dr["idExamen"]);
                            tbl.Nombre = dr["Nombre"].ToString();
                            tbl.Descripcion = dr["Descripcion"].ToString();
                            resultado.Add(tbl);
                        }
                    }
                    else
                    {
                        throw new Exception("Registro no Encontrado");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al consultar el examen: " + ex.Message);
                }
            }

            return resultado;
        }


        public List<string> EliminarExamen(int id)
        {
            bool exito = false;
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DESKTOP-OKTO"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tblExamen WHERE idExamen = @idExamen;", cn);
                cmd.Parameters.AddWithValue("@idExamen", id);

                try
                {
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        exito = true;
                        mensaje = "Registro Eliminado.";
                    }
                    else {
                        mensaje = "El Registro No Existe";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error al eliminar el examen: " + ex.Message;
                }
            }

            List<string> resultado = new List<string>();
            resultado.Add(exito.ToString());
            resultado.Add(mensaje);

            return resultado;
        }


        public List<tblExamen> GetTblExamens()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DESKTOP-OKTO"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * From tblExamen;", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<tblExamen> lista = new List<tblExamen>();
                tblExamen tbl;

                while (dr.Read())
                {

                    tbl = new tblExamen();
                    tbl.idExamen = Convert.ToInt32(dr[0]);
                    tbl.Nombre = dr[1].ToString();
                    tbl.Descripcion = dr[2].ToString();
                    lista.Add(tbl);
                }
                return lista;
            }
        }

        List<string> IWSOkto.AgregarExamen(int id, string Nombre, string Descripcion)
        {
            bool exito = false;
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DESKTOP-OKTO"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tblExamen (idExamen, Nombre, Descripcion) VALUES (@idExamen, @Nombre, @Descripcion);", cn);
                cmd.Parameters.AddWithValue("@idExamen", id);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);

                try
                {
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        exito = true;
                        mensaje = "Registro Agregado Con Exito.";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error al insertar el examen: " + ex.Message;
                }
            }
            List<string> resultado = new List<string>();
            resultado.Add(exito.ToString());
            resultado.Add(mensaje);

            return resultado;
        }
    }
}
