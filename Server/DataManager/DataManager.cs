

using CarteleraBlazor.Shared.Entidades;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace CarteleraBlazor.Server.DataManager
{
    public class DataManager
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public DataManager(string connectionString, IConfiguration configuration)
        {
            this.connectionString = connectionString;
            this.configuration = configuration;
        }

        public List<AfiliadoReclamacion> listaConsultaAfiliado(string proveedor)
        {

            List<AfiliadoReclamacion> recs = new List<AfiliadoReclamacion>();

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand("dbaper.pkg_programas_Ws.BuscarAfiliadosByPrestador", connection);
                    command.BindByName = true;
                    command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("p_prestador", OracleDbType.Varchar2).Value = proveedor;
                    command.Parameters.Add("p_resul", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                                        OracleDataReader reader = command.ExecuteReader();
                  
                    while (reader.Read())
                    {
                        var existe = false;
                        AfiliadoReclamacion newR = new AfiliadoReclamacion();
                       
                        foreach (AfiliadoReclamacion r in recs)
                        {
                            if (r.Cedula.Trim() == "'" + reader["CDIDEPER"].ToString().Trim())
                            {
                                existe = true;
                            }

                        }
                        newR.Codigo = reader["CODIGO"].ToString();
                        newR.Nombre = reader["AFILIADO"].ToString();
                        newR.Programa = reader["PROGRAMA"].ToString();
                        newR.Sexo = reader["SEXO"].ToString();
                        newR.cdperson = reader["CDPERSON"].ToString();
                        newR.Fecha_Inicio = Convert.ToDateTime(reader["FECHA_INI"].ToString()).ToString("dd /MM/yyyy");//reader.GetDateTime(3).ToString("dd /MM/yyyy");
                        try
                        {
                            newR.Fecha_Reclamacion = Convert.ToDateTime(reader["FEC_ULT_RECLAMACION"].ToString()).ToString("dd /MM/yyyy"); //reader.GetDateTime(4).ToString("dd /MM/yyyy");
                        }
                        catch (Exception)
                        {

                            newR.Fecha_Reclamacion = "";
                        }

                        newR.Edad = reader["EDAD"].ToString();
                        newR.Cedula = "'" + reader["CDIDEPER"].ToString();
                        newR.Periodo = reader["PERIODO"].ToString();
                        newR.cant_act_edu = reader["cant_act_edu"].ToString();
                        if (existe == false)
                        {
                            recs.Add(newR);
                        }
                        // cuenta++;
                    }
                }

            }
            catch (Exception e)
            {
                //resultado.errCode = "99";
                //resultado.errMsg = e.Message;
            }
            return recs;
        }

        public async Task guardarGenero(Genero genero)
        {
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GUARDAR_GENERO", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                    cmd.Parameters.Add("P_DESCRIPCION", OracleDbType.Varchar2).Value = genero.descripcion;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int64).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["P_ID"].Value = genero.id;
                    var dr = await cmd.ExecuteNonQueryAsync();
                    var id = cmd.Parameters["P_ID"].Value.ToString();
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }
        public async Task<List<Genero>> getGeneros()
        {
            var generos= new List<Genero>();
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GET_GENEROS", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                 
                    cmd.Parameters.Add("P_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        generos.Add(new Genero
                        {
                            descripcion = dr["descripcion"].ToString()!,
                            id = Convert.ToInt32( dr["id"].ToString())
                        });
                    }
                    return generos;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }
        public async Task<Genero> getGenero(string id)
        {
            var genero = new Genero();
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GET_GENERO_BY_ID", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int64).Value = id;
                    cmd.Parameters.Add("P_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        genero = new Genero
                        {
                            descripcion = dr["descripcion"].ToString()!,
                            id = Convert.ToInt32(dr["id"].ToString()),
                         


                        };
                    }
                    return genero;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }


        public async Task<List<Actor>> getActores()
        {
            var generos = new List<Actor>();
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GET_ACTORES", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;

                    cmd.Parameters.Add("P_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        generos.Add(new Actor
                        {
                            nombre = dr["NOMBRE"].ToString()!,
                            id = Convert.ToInt32(dr["id"].ToString()),
                            foto= dr["foto"].ToString(),
                            fechaNancimiento= DateTime.Parse( dr["Fecha_Nacimiento"].ToString()!),
                            biografia= dr["Biografia"].ToString() ?? ""


                        });
                    }
                    return generos;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }

        public async Task<Actor> getActor(string id)
        {
            var actor = new Actor();
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GET_ACTOR_BY_ID", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int64).Value = id;
                    cmd.Parameters.Add("P_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                       actor =new Actor
                        {
                           personaje= dr["personaje"].ToString(),
                            nombre = dr["NOMBRE"].ToString()!,
                            id = Convert.ToInt32(dr["id"].ToString()),
                            foto = dr["foto"].ToString(),
                            fechaNancimiento = DateTime.Parse(dr["Fecha_Nacimiento"].ToString()!),
                            biografia = dr["Biografia"].ToString() ?? ""


                        };
                    }
                    return actor;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }


        public async Task guardarActor(Actor actor)
        {
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GUARDAR_ACTOR", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                    cmd.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2).Value = actor.nombre;
                    cmd.Parameters.Add("P_BIOGRAFIA", OracleDbType.Varchar2).Value = actor.biografia;
                    cmd.Parameters.Add("P_FOTO", OracleDbType.Varchar2).Value = actor.foto;
                    cmd.Parameters.Add("P_PERSONAJE", OracleDbType.Varchar2).Value = actor.personaje;
                    cmd.Parameters.Add("P_FECHA_NACIMIENTO", OracleDbType.Date).Value = actor.fechaNancimiento;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int64).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["P_ID"].Value = actor.id;
                    var dr = await cmd.ExecuteNonQueryAsync();
                    var id = cmd.Parameters["P_ID"].Value.ToString();
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }

        public async Task<int> guardarPelicula(Shared.Entidades.Pelicula pelicula)
        {
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GUARDAR_PELICULA", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                    cmd.Parameters.Add("P_TITULO", OracleDbType.Varchar2).Value = pelicula.titulo;
                    cmd.Parameters.Add("P_RESUMEN", OracleDbType.Varchar2).Value = pelicula.resumen;
                    cmd.Parameters.Add("P_POSTER", OracleDbType.Varchar2).Value = pelicula.poster;
                    cmd.Parameters.Add("P_TRAILER", OracleDbType.Varchar2).Value = pelicula.trailer;
                    cmd.Parameters.Add("P_ENCARTELERA", OracleDbType.Varchar2).Value =  pelicula.enCartelera? "S" : "N";
                    cmd.Parameters.Add("P_FECHA_LANZAMIENTO", OracleDbType.Date).Value = pelicula.fechaLanzamiento;
                    
                    cmd.Parameters.Add("P_ID", OracleDbType.Int64).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["P_ID"].Value = pelicula.id;
                    var dr = await cmd.ExecuteNonQueryAsync();
                    var id = Convert.ToInt32( cmd.Parameters["P_ID"].Value.ToString());
                    return id;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }

        public async Task<Shared.Entidades.Pelicula> getPelicula(string id)
        {
            var pelicula = new Shared.Entidades.Pelicula();
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GET_PELICULA_BY_ID", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int64).Value = id;
                    cmd.Parameters.Add("P_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        var carterlera = dr["enCartelera"].ToString();
                        pelicula = new Shared.Entidades.Pelicula
                        {
                            titulo = dr["titulo"].ToString()!,
                            id = Convert.ToInt32(dr["id"].ToString()),
                            resumen = dr["resumen"].ToString()!,
                            poster = dr["poster"].ToString(),
                            trailer = dr["trailer"].ToString(),
                            fechaLanzamiento = DateTime.Parse(dr["fecha_lanzamiento"].ToString()!)


                        };
                        if( carterlera == "N" )
                        {
                            pelicula.enCartelera = false;
                           return pelicula;
                        }
                        pelicula.enCartelera = true;
                    }
                    return pelicula;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }


        public async Task<List<Shared.Entidades.Pelicula>> getPeliculas()
        {
            var generos = new List<Shared.Entidades.Pelicula>();
            using OracleConnection connection = new OracleConnection(connectionString);
            await connection.OpenAsync();
            using (OracleCommand cmd = new OracleCommand($"DBAPER.PKG_PELICULAS_BLAZOR.GET_PELICULAS", connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;

                    cmd.Parameters.Add("P_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        generos.Add(new Shared.Entidades.Pelicula
                        {
                            titulo= dr["titulo"].ToString(),



                        });
                    }
                    return generos;
                }
                catch (Exception e)
                {

                    throw e;
                }



            }

        }

        //public async Task<List<Remitente>> ConsultaRemitenteAsync(string remitente)
        //{
        //    List<Remitente> remitentes = new List<Remitente>();
        //    using OracleConnection connection = new OracleConnection(connectionString);
        //    await connection.OpenAsync();
        //    using (OracleCommand cmd = new OracleCommand($"dbaper.web_pkg_app.p_consulta_remitente", connection))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure; cmd.BindByName = true;
        //        cmd.Parameters.Add("p_cadena", OracleDbType.Varchar2).Value = remitente;
        //        cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //        var dr = await cmd.ExecuteReaderAsync();

        //        while (dr.Read())
        //        {
        //            remitentes.Add(new Remitente
        //            {
        //                Codigo = dr["CODIGO"].ToString()!,
        //                Descripcion = dr["nombre"].ToString()!
        //            });
        //        }
        //    }

        //    return remitentes;
        //}

    }
}
