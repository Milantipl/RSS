using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary
{

    public class ConnectionClass
    {
        private readonly SqlConnection Connection;
        public ConnectionClass()
        {
            Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ToString());
            Connection.Open();
        }
        public int InsertScope(string strQuery)
        {
            try
            {

                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }
                var cmd = new SqlCommand(strQuery, Connection);

                var r = cmd.ExecuteScalar();

                return Convert.ToInt32(r);
                //return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();


            }
        }
        public T ExecuteProcedure<T>(string procedureName, List<Tuple<string, string, SqlDbType, int?>> parameters)
        {
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }

                using (SqlCommand sqlCommand = new SqlCommand(procedureName, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            if (!string.IsNullOrWhiteSpace(parameter.Item1))
                            {
                                if (parameter.Item4 != null)
                                {
                                    var param = new SqlParameter(parameter.Item1, parameter.Item3, Convert.ToInt32(parameter.Item4)) { Value = parameter.Item2 };
                                    sqlCommand.Parameters.Add(param);
                                }
                                else
                                {
                                    var param = new SqlParameter(parameter.Item1, parameter.Item3) { Value = parameter.Item2 };
                                    sqlCommand.Parameters.Add(param);
                                }
                            }
                        }
                    }

                    if (typeof(T) == typeof(DataTable))
                    {
                        var dt = new DataTable();
                        var dataAdaper = new SqlDataAdapter(sqlCommand);
                        sqlCommand.CommandTimeout = 999999;
                        dataAdaper.Fill(dt);
                        return (T)Convert.ChangeType(dt, typeof(T));
                    }
                    else
                    {
                        return (T)sqlCommand.ExecuteScalar();
                    }
                }
                return default(T);
            }
            catch (SqlException e)
            {

                return default(T);
            }
            finally
            {
                Connection.Close();
            }
        }
        public T ExecuteProcedure<T>(string procedureName, List<Tuple<string, string, SqlDbType, int?>> parameters, out string Error)
        {
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }

                using (SqlCommand sqlCommand = new SqlCommand(procedureName, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            if (!string.IsNullOrWhiteSpace(parameter.Item1))
                            {
                                if (parameter.Item4 != null)
                                {
                                    var param = new SqlParameter(parameter.Item1, parameter.Item3, Convert.ToInt32(parameter.Item4)) { Value = parameter.Item2 };
                                    sqlCommand.Parameters.Add(param);
                                }
                                else
                                {
                                    var param = new SqlParameter(parameter.Item1, parameter.Item3) { Value = parameter.Item2 };
                                    sqlCommand.Parameters.Add(param);
                                }
                            }
                        }
                    }

                    if (typeof(T) == typeof(DataSet))
                    {
                        var dt = new DataSet();
                        var dataAdaper = new SqlDataAdapter(sqlCommand);
                        sqlCommand.CommandTimeout = 999999;
                        dataAdaper.Fill(dt);
                        Error = null;
                        return (T)Convert.ChangeType(dt, typeof(T));
                    }
                    else
                    {
                        Error = null;
                        return (T)sqlCommand.ExecuteScalar();
                    }
                }
                return default(T);
            }
            catch (SqlException e)
            {
                Error = e.Message.ToString();
                return default(T);
            }
            finally
            {
                Connection.Close();
            }
        }
        public T ExecuteProcedure<T>(string procedureName, DataTable data = null)
        {
            try
            {

                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }

                using (SqlCommand sqlCommand = new SqlCommand(procedureName, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    if (data != null && !string.IsNullOrWhiteSpace(data.TableName))
                    {
                        sqlCommand.Parameters.AddWithValue(data.TableName, data);
                    }

                    if (typeof(T) == typeof(DataTable))
                    {
                        var dt = new DataTable();
                        var dataAdaper = new SqlDataAdapter(sqlCommand);
                        sqlCommand.CommandTimeout = 99999999;
                        dataAdaper.Fill(dt);
                        return (T)Convert.ChangeType(dt, typeof(T));
                    }
                    if (typeof(T) == typeof(DataSet))
                    {
                        var dt = new DataSet();
                        var dataAdaper = new SqlDataAdapter(sqlCommand);

                        sqlCommand.CommandTimeout = 99999999;
                        dataAdaper.Fill(dt);
                        return (T)Convert.ChangeType(dt, typeof(T));
                    }
                    else
                    {
                        if (Connection.State != System.Data.ConnectionState.Open)
                            Connection.Open();
                        //sqlCommand.ExecuteNonQuery();
                        //return default(T);
                        return (T)sqlCommand.ExecuteScalar();
                    }
                }
                return default(T);
            }
            catch (SqlException e)
            {

                //System.Windows.Forms.MessageBox.Show(e.Message.ToString() + e.StackTrace.ToString() + "192.168.10.99");
                return default(T);
            }
            finally
            {
                Connection.Close();
            }
        }
        public DataTable Select(string strQuery)
        {
            var dt = new DataTable();
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, Connection))
                {
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }
        }
        public DataTable Select(string strQuery, out string Error)
        {
            var dt = new DataTable();
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, Connection))
                {
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                Error = null;
                return dt;
            }
            catch (Exception ex)
            {

                Error = ex.Message;
                return null;
            }
            finally
            {

                Connection.Close();
            }
        }

        public int Insert(string strQuery)
        {
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }
               
                using (SqlCommand cmd = new SqlCommand(strQuery, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }
        }

        public int Update(string strQuery)
        {
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }
        }

        public int Delete(string strQuery)
        {
            try
            {
                if (ConnectionState.Closed == Connection.State)
                {
                    Connection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }
        }
        public T ExecuteProcedure<T>(string procedureName)
        {
            try
            {
                var sqlCommand = new SqlCommand(procedureName, Connection) { CommandType = CommandType.StoredProcedure };

                if (typeof(T) == typeof(DataTable))
                {
                    var dt = new DataTable();
                    sqlCommand.CommandTimeout = 10000;
                    var dataAdaper = new SqlDataAdapter(sqlCommand);
                    dataAdaper.Fill(dt);
                    return (T)Convert.ChangeType(dt, typeof(T));
                }
                //else if (typeof(T) == typeof(DataSet))
                //{
                //}
                else
                {
                    return (T)sqlCommand.ExecuteScalar();
                }
            }
            catch (SqlException e)
            {
                return default(T);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
