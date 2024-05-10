using AppiAutos.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AppiAutos.Data
{
    public class AutoData
    {
        private readonly string conexion;
        public AutoData (IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }
        public async Task <List<Auto>> Lista()
        {
            List<Auto> lista = new List<Auto>();
            using ( var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaAutos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) {
                        lista.Add(new Auto
                        {
                            IdAuto = Convert.ToInt32(reader["idAuto"]),
                            Marca = reader["Marca"].ToString(),
                            Cilindraje = Convert.ToInt32(reader["Cilindraje"]),
                            Color =  reader["Color"].ToString(),
                            Propietario = reader["Propietario"].ToString(),
                            Estado = Convert.ToByte(reader["Estado"])


                        });                   
                    }
                }
            }   return lista;
        }
        public async Task<Auto> Obtener(int Id)
        {
            Auto objeto = new Auto();
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerAuto", con);
                cmd.Parameters.AddWithValue("@idAuto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto= new Auto
                        {
                            IdAuto = Convert.ToInt32(reader["idAuto"]),
                            Marca = reader["Marca"].ToString(),
                            Cilindraje = Convert.ToInt32(reader["Cilindraje"]),
                            Color = reader["Color"].ToString(),
                            Propietario = reader["Propietario"].ToString(),
                            Estado = Convert.ToByte(reader["Estado"])


                        };
                    }
                }
            }
            return objeto;
        }
        public async Task<bool> Crear(Auto objeto)
        {
            bool respuesta= true;
            using (var con = new SqlConnection(conexion))
            {
              
                SqlCommand cmd = new SqlCommand("sp_crearAuto", con);
                
                cmd.Parameters.AddWithValue("@Marca", objeto.Marca);
                cmd.Parameters.AddWithValue("@Cilindraje", objeto.Cilindraje);
                cmd.Parameters.AddWithValue("@Color", objeto.Color);
                cmd.Parameters.AddWithValue("@Propietario", objeto.Propietario);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync()>0 ? true: false;
                }
                catch { respuesta = false; }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Auto objeto)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {
              
                SqlCommand cmd = new SqlCommand("sp_editarAuto", con);

                cmd.Parameters.AddWithValue("@idAuto", objeto.IdAuto);
                cmd.Parameters.AddWithValue("@Marca", objeto.Marca);
                cmd.Parameters.AddWithValue("@Cilindraje", objeto.Cilindraje);
                cmd.Parameters.AddWithValue("@Color", objeto.Color);
                cmd.Parameters.AddWithValue("@Propietario", objeto.Propietario);
                cmd.Parameters.AddWithValue("@Estado", objeto.Estado);  
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch { respuesta = false; }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {
                
                SqlCommand cmd = new SqlCommand("sp_eliminarAuto", con);

                cmd.Parameters.AddWithValue("@idAuto", id);
                cmd.CommandType= CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch { respuesta = false; }
            }
            return respuesta;
        }

        
    }
}
