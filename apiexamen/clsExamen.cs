using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using apiexamen.WSROkto;
using System.Linq;


namespace apiexamen
{
    public class clsExamen
    {
        private string connectionString = "Data Source=DESKTOP-OKTO;Initial Catalog=BdiExamen;Integrated Security=True";
        private WSOktoClient webService;

        public clsExamen()
        {
            webService = new WSOktoClient();
        }

        public List<tblExamen> ConsultarDatos(string nombre, string descripcion)
        {
            List<tblExamen> listaExamenes = new List<tblExamen>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string storedProcedure = "spConsultar";
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Descripcion", descripcion);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tblExamen examen = new tblExamen();
                        examen.idExamen = Convert.ToInt32(reader["idExamen"]);
                        examen.Nombre = reader["Nombre"].ToString();
                        examen.Descripcion = reader["Descripcion"].ToString();

                        listaExamenes.Add(examen);
                    }
                }
            }
            return listaExamenes;
        }

        public (bool, string) AgregarDatos(int idExamen, string nombre, string descripcion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string storedProcedure = "spAgregar";
                    SqlCommand command = new SqlCommand(storedProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idExamen", idExamen);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);

                    command.ExecuteNonQuery();

                    return (true, "Datos agregados correctamente.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error al agregar datos: " + ex.Message);
            }
        }

        public List<tblExamen> GetTblExamens()
        {
            return webService.GetTblExamens().ToList();
        }

        public List<tblExamen> ConsultarExamen(int id, string nombre, string descripcion)
        {
            return webService.ConsultarExamen(id, nombre, descripcion).ToList();
        }

        public (bool, string) ActualizarExamen(int id, string nombre, string descripcion)
        {
            List<string> resultado = webService.ActualizarExamen(id, nombre, descripcion).ToList();
            bool exito = Convert.ToBoolean(resultado[0]);
            string mensaje = resultado[1];
            return (exito, mensaje);
        }

        public (bool, string) EliminarExamen(int id)
        {
            List<string> resultado = webService.EliminarExamen(id).ToList();
            bool exito = Convert.ToBoolean(resultado[0]);
            string mensaje = resultado[1];
            return (exito, mensaje);
        }

        public (bool, string) AgregarExamen(int id, string nombre, string descripcion)
        {
            List<string> resultado = webService.AgregarExamen(id, nombre, descripcion).ToList();
            bool exito = Convert.ToBoolean(resultado[0]);
            string mensaje = resultado[1];
            return (exito, mensaje);
        }
    }
}
