using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class classdata
{
    private string strcon;
    private string strcon_PM;
    private string strconkhamoshi;
    private string strconHRPDNDB;
    private SqlConnection con;
    private SqlConnection conkhamoshi;
    private SqlConnection conHRPDNDB;
    private SqlDataReader dr;
    
    public classdata()
    {
        strcon = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["pm"].ConnectionString;


        con = new SqlConnection(strcon);
 

        //strcon = "Data Source=.;Initial Catalog=TAVANIR;Integrated Security=True";
        //strconkhamoshi = "Data Source=.;Initial Catalog=CcrequesterSetad;Integrated Security=True";

        //strcon = "Data Source=172.16.100.57;Initial Catalog=TAVANIR;User ID=TAS;Password=tas1390";
        //strconkhamoshi = "Data Source=172.16.100.57;Initial Catalog=CcrequesterSetad;User ID=TAS;Password=tas1390";

        //strcon = "Data Source=ARMAN-VAIO\\SQLEXPRESS;Initial Catalog=TAVANIR;Integrated Security=True";
        //strconkhamoshi = "Data Source=ARMAN-VAIO\\SQLEXPRESS;Initial Catalog=CcrequesterSetad;Integrated Security=True";

        con = new SqlConnection(strcon);
        conkhamoshi = new SqlConnection(strconkhamoshi);
        conHRPDNDB = new SqlConnection(strconHRPDNDB);
    }

    public bool BulkCopy(string query, string connectionString, string destinationTableName, string[] sourceColumnNames, string[] destinationColumnNames)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            dr = cmd.ExecuteReader();

            SqlBulkCopy sbc = new SqlBulkCopy(connectionString);
            sbc.DestinationTableName = destinationTableName;

            for (int i = 0; i < sourceColumnNames.Length; i++)
            {
                SqlBulkCopyColumnMapping mapName =
                    new SqlBulkCopyColumnMapping(sourceColumnNames[i], destinationColumnNames[i]);
                sbc.ColumnMappings.Add(mapName);
            }

            sbc.WriteToServer(dr);

            sbc.Close();
            dr.Close();
            con.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }

    public bool BulkCopy(string connectionString, string TableName)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Select * FROM " + TableName, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            SqlBulkCopy sbc = new SqlBulkCopy(connectionString);
            sbc.DestinationTableName = TableName;

            sbc.WriteToServer(dr);

            sbc.Close();
            dr.Close();
            con.Close();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public string getConnection()
    {
        return strcon;
    }

    public void CloseConnection()
    {
        if (dr != null)
        {
            if (!dr.IsClosed)
                dr.Close();
        }

        if (con.State == ConnectionState.Open)
            con.Close();
    }

    public void OpenConnection()
    {
        if (con.State != ConnectionState.Open)
            con.Open();
    }
    public async Task<List<Dictionary<string, object>>> ExecuteSql( string sqlQuery)
    {
        if (string.IsNullOrWhiteSpace(sqlQuery))
        {
            return null;
        }

        try
        {
            using (var connection = new SqlConnection(strcon))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var results = new List<Dictionary<string, object>>();
                        while (await reader.ReadAsync())
                        {
                            var row = Enumerable.Range(0, reader.FieldCount)
                                .ToDictionary(reader.GetName, reader.GetValue);

                            results.Add(row);
                        }
                        return results;
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            return null;
        }
    }
    public async Task<List<Dictionary<string, object>>> ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
    {
        if (string.IsNullOrWhiteSpace(storedProcedureName))
        {
            return null;
        }

        try
        {
            using (var connection = new SqlConnection(strcon))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader); // Load all rows into a DataTable

                        var results = dataTable.AsEnumerable()
                            .Select(row => dataTable.Columns.Cast<DataColumn>()
                                .ToDictionary(col => col.ColumnName, col => row[col]))
                            .ToList();

                        return results;
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Handle exception (consider logging the error)
            return null;
        }
    }
    public Boolean Check_Duplicate(string table, string field, string value)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
 
            SqlCommand cmd = new SqlCommand("select " + field + " from " + table + " where " + field + "=" + value, con);
            
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            con.Close();
        }
    }

    public Boolean Check_Count(string table, string field, string value,int no)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();

            SqlCommand cmd = new SqlCommand("select * from " + table + " where " + field + "='" + value+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            con.Close();
        }
    }

    public Boolean Check_DuplicateWithProcedure(string ProcedureName, string WhereField, string WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            //SqlDataReader dr;
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p);

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            con.Close();
        }
    }

    public System.Data.DataTable SetTable(string TableName)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            System.Data.DataTable dt;
            dt = new System.Data.DataTable();
            System.Data.DataSet ds = new System.Data.DataSet();
            con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from " + TableName, con);
            da.Fill(dt);
            //dt = ds.Tables[0];

            return dt;
        }
        catch (SqlException ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public string OpenConnectionString()
    {
        try
        {
            con.Open();
            return "Connection Open Successfully";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public SqlDataReader BindTable(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.Connection = con;
            //SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }
        catch
        {
            //dr.Close();
            con.Close();
            return null;
        }
    }

    public System.Data.DataTable SetTable(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            cmd.Connection = con;
            System.Data.DataTable dt;
            dt = new System.Data.DataTable();
            System.Data.DataSet ds = new System.Data.DataSet();
            con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 360;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        catch (SqlException ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            con.Close();
            return null;
        }
    }

    public System.Data.DataTable SetTablekhamoshi(SqlCommand cmd)
    {
        try
        {
            if (conkhamoshi.State == ConnectionState.Open)
                conkhamoshi.Close();
            cmd.Connection = conkhamoshi;
            System.Data.DataTable dt;
            dt = new System.Data.DataTable();
            System.Data.DataSet ds = new System.Data.DataSet();
            conkhamoshi = new SqlConnection(strconkhamoshi);
            conkhamoshi.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conkhamoshi.Close();
            return dt;
        }
        catch (SqlException ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            conkhamoshi.Close();
            return null;
        }
    }

    public System.Data.DataTable SetTable(string TableName, string where)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.DataTable dt;
            dt = new System.Data.DataTable(TableName);
            System.Data.DataSet ds = new System.Data.DataSet();
            con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from " + TableName + where, con);
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public System.Data.DataTable SetTable(string TableName, string SqlString, string where)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            // con.Open();
            System.Data.DataTable dt;
            dt = new System.Data.DataTable(TableName);
            System.Data.DataSet ds = new System.Data.DataSet();
            con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(SqlString + where, con);
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public System.Data.DataTable GetTableSchema(string TableName)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            System.Data.DataTable dt;
            dt = new System.Data.DataTable(TableName);
            con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from " + TableName, con);
            da.FillSchema(dt, SchemaType.Mapped);
            con.Close();
            return dt;
        }
        catch (SqlException ex)
        {
            con.Close();
            return null;
        }
    }

    #region NewFunc
    public decimal GetDecimal(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            decimal result = -1;
            if (dr.Read())
                result = dr.GetDecimal(0);
            con.Close();

            return result;
        }
        catch
        {
            con.Close();

            return -1;
        }
    }
    public bool GetBoolean(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            bool result = false;
            if (dr.Read())
                result = Convert.ToBoolean(dr.GetValue(0));
            con.Close();

            return result;
        }
        catch
        {
            con.Close();
            return false;
        }
    }
   
    public int GetInt(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            int result = -1;
            if (dr.Read())
                result = Convert.ToInt32(dr.GetValue(0));
            con.Close();

            return result;
        }
        catch
        {
            con.Close();
            return -1;
        }
    }

    public string GetString(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            string result = null;
            if (dr.Read())
                result = Convert.ToString(dr.GetValue(0));
            con.Close();

            return result;
        }
        catch
        {
            con.Close();
            return null;
        }
    }

    public int DoExecute(SqlCommand cmd)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.Connection = con;
            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
        catch
        {
            con.Close();
            return -1;
        }
    }

    #endregion

    public decimal GetMaxID(string TableName, string FieldName)
    {
        //SqlDataReader dr;
        decimal id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand("select max(" + FieldName + ") from " + TableName + "", con);
            dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                dr.Read();
                if (dr.IsDBNull(0))
                    id = 1;
                else
                    id = System.Convert.ToDecimal(dr.GetValue(0)) + 1;
                dr.Close();
                con.Close();
                return id;
            }
            else
            {
                dr.Close();
                con.Close();
                return 1;
            }
        }
        catch (Exception ex)
        {
            dr.Close();
            con.Close();
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            return 1;
        }
        finally
        {

        }

    }

    public decimal GetMaxID(string TableName, string FieldName, string where)
    {
        //SqlDataReader dr;
        decimal id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand("select max(" + FieldName + ") from " + TableName + where, con);
            dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                //dr.Close();
                con.Close();
                return 1;
            }
            else
            {
                dr.Read();
                if (dr.IsDBNull(0))
                    id = 1;
                else
                    id = System.Convert.ToDecimal(dr.GetValue(0)) + 1;
                dr.Close();
                con.Close();
                return id;
            }
        }
        catch
        {
            dr.Close();
            con.Close();
            return 0;
        }
        finally
        {
            //con.Close();
        }

    }

    public decimal GetMaxID(string ProcedureName)
    {
        //SqlDataReader dr;
        decimal id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(ProcedureName, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                //dr.Close();
                con.Close();
                return 1;
            }
            else
            {
                dr.Read();
                if (dr.IsDBNull(0))
                    id = 1;
                else
                    id = System.Convert.ToDecimal(dr.GetValue(0)) + 1;
                dr.Close();
                con.Close();
                return id;
            }
        }
        catch
        {
            dr.Close();
            con.Close();
            return 0;
        }
        finally
        {
            //con.Close();
        }

    }

    public bool HasData(string Sql)
    {
        //SqlDataReader dr;
        //decimal id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(Sql, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                //dr.Close();
                con.Close();
                return false;
            }
            else
            {
                //dr.Close();
                con.Close();
                return true;
            }

        }
        catch
        {
            return false;
        }
        finally
        {
            //dr.Close();
            con.Close();
        }

    }

    public SqlDataReader BindDataReader(string Sql)
    {
        //SqlDataReader dr;
        //decimal id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(Sql, con);
            return cmd.ExecuteReader();

        }
        catch
        {
            con.Close();
            return null;
        }

    }

    public object LoadData(string Sql)
    {
        //SqlDataReader dr;
        //decimal id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(Sql, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                //dr.Close();
                con.Close();
                return null;
            }
            else
            {
                dr.Read();
                object data = dr.GetValue(0);
                dr.Close();
                con.Close();
                return data;
            }
        }
        catch
        {
            dr.Close();
            con.Close();
            return null;
        }

    }

    public object LoadOpenData(string Sql)
    {
          try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                System.Data.SqlClient.SqlCommand cmd = new SqlCommand(Sql, con);
                object res = cmd.ExecuteScalar();
                con.Close();
                return res;
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                return null;
            }
        }
    public object LoadOpenData2(string Sql)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(Sql, con);
            object res = cmd.ExecuteScalar();
            con.Close();
            return res;
        }
        catch
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return null;
        }

    }
    public int MovePre(System.Data.DataSet ds, int RowIndex)
    {
        if (RowIndex > 0)
            return RowIndex - 1;
        else
            return ds.Tables[0].Rows.Count - 1;

    }

    public int MoveNext(System.Data.DataSet ds, int RowIndex)
    {
        if (RowIndex < ds.Tables[0].Rows.Count)
            return RowIndex + 1;
        else
            return 0;

    }

    public bool Add(string strsql)
    {
        System.Data.SqlClient.SqlCommand cmd = new SqlCommand(strsql, con);
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (SqlException ex)
        {
            con.Close();
            return false;
        }
    }

    public bool AddOpenConnection(string strsql)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(strsql, con);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public bool Add(string ProcedureName, string[] Parameters, params object[] values)
    {

        SqlCommand cmd = new SqlCommand(ProcedureName, con);
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.AddWithValue(Parameters[i], values[i]);
            }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public SqlDataReader SetdrWithProcedure(string ProcedureName, string[] WhereFields, string[] WhereValues)
    {
        dr = null;
        try
        {

            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < WhereFields.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + WhereFields[i], WhereValues[i]);
                cmd.Parameters.Add(p);
            }
            cmd.CommandText = ProcedureName;
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                return dr;
            }
            else
            {
                //dr.Close();
                con.Close();
                return null;
            }
        }
        catch (SqlException ex)
        {

            // System.IO.File.WriteAllText("c:\\1.txt",ex.Message);
            //dr.Close();
            con.Close();
            return null;
        }
    }

    public void setdata(ref String tablename, ref SqlDataAdapter da, ref System.Data.DataTable dt, ref string strsql, ref string database)
    {
        if (con.State == ConnectionState.Open)
            con.Close();
        con.Open();
        //Dim con As SqlConnection
        dt = new DataTable();
        //strcon = "packet size=4096;integrated security=SSPI;data source=(local);persist security info=False;initial catalog=" & database & ""
        //con = New SqlClient.SqlConnection(strcon)
        //con.Open()
        da = new SqlDataAdapter(strsql, con);
        da.Fill(dt);
        con.Close();
    }

    public void setdata(String tablename, ref SqlDataAdapter da, ref System.Data.DataSet ds, string strsql, string database)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            //Dim con As SqlConnection
            ds = new DataSet();
            //strcon = "packet size=4096;integrated security=SSPI;data source=(local);persist security info=False;initial catalog=" & database & ""
            //con = New SqlClient.SqlConnection(strcon)
            //con.Open()
            da = new SqlDataAdapter(strsql, con);
            da.Fill(ds);
            con.Close();
        }
        catch
        {
            con.Close();
        }
    }

    public bool EditAndUpdate(string strsql)
    {
        System.Data.SqlClient.SqlCommand cmd = new SqlCommand(strsql, con);
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception ex)
        {
            con.Close();
            return false;
        }

    }
    public int ExecuteQuerywithresult(string strsql)
    {
        System.Data.SqlClient.SqlCommand cmd = new SqlCommand(strsql, con);
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
         object result=   cmd.ExecuteScalar();
            con.Close();
            return int.Parse(result.ToString());
        }
        catch (Exception ex)
        {
            con.Close();
            return 0;
        }

    }
    public DataTable SetTableWithProcedure(string ProcedureName)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        catch
        {
            con.Close();
            return null;
        }
    }

    public DataTable SetTableWithProcedure(string ProcedureName, string WhereField, string WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        catch
        {
            con.Close();
            return null;
        }
    }

    public DataTable SetTableWithProcedure(string ProcedureName, string[] WhereField, string[] WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < WhereField.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                cmd.Parameters.Add(p);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 360;
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        catch(Exception ex)
        {
            string s = "";
            s = ex.Message.ToString();
            con.Close();
            return null;
        }
    }

    public DataTable SetTableWithProcedureForHRPDNDB(string ProcedureName, string[] WhereField, string[] WhereValue)
    {
        try
        {
            if (conHRPDNDB.State == ConnectionState.Open)
                conHRPDNDB.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, conHRPDNDB);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < WhereField.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                cmd.Parameters.Add(p);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 360;
            DataTable dt = new DataTable();
            conHRPDNDB.Open();
            da.Fill(dt);
            conHRPDNDB.Close();
            return dt;
        }
        catch
        {
            conHRPDNDB.Close();
            return null;
        }
    }

    public bool AddWithProcedure(string ProcedureName, string[] Fileds, string[] Values)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Fileds.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + Fileds[i], Values[i]);
                cmd.Parameters.Add(p);
            }//end for                

            cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch(Exception ex)
        {
            con.Close();
            return false;
        }
    }

    public int GetCountWithProcedure(string ProcedureName, string[] WhereField, string[] WhereValue)
    {
        try
        {
            int count = new int();
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < WhereField.Length; i++)
            {
                SqlParameter p1 = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                cmd.Parameters.Add(p1);
            }//end for
            SqlDataReader dr1;
            //  cmd.CommandText = ProcedureName;
            con.Open();

            dr1 = cmd.ExecuteReader();
            dr1.Read();
            count = Convert.ToInt32(dr1[0]);
            con.Close();
            return count;
        }
        catch
        {
            con.Close();
            return 0;
        }
    }

    public bool AddWithProcedure(string ProcedureName, string[] Fileds, object[] Values)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Fileds.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + Fileds[i], Values[i]);
                cmd.Parameters.Add(p);
            }//end for                
            cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public bool UpdateWithProcedure(string ProcedureName, string[] Fileds, string[] Values, string WhereField, string WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Fileds.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + Fileds[i], Values[i]);
                cmd.Parameters.Add(p);
            }//end for
            SqlParameter p1 = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p1);
            con.Close();
            cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public bool UpdateWithProcedureO(string ProcedureName, string[] Fileds, object[] Values, string WhereField, string WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Fileds.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + Fileds[i], Values[i]);
                cmd.Parameters.Add(p);
            }//end for
            SqlParameter p1 = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p1);
            con.Close();
            cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public bool UpdateWithProcedure(string ProcedureName, string[] Fileds, string[] Values, string[] WhereField, string[] WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Fileds.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + Fileds[i], Values[i]);
                cmd.Parameters.Add(p);
            }//end for
            for (int i = 0; i < WhereField.Length; i++)
            {
                SqlParameter p1 = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                cmd.Parameters.Add(p1);
            }//end for
            con.Close();
            cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public bool DeleteWithProcedure(string ProcedureName, string WhereField, string WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p);

            //cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public bool DeleteWithProcedure(string ProcedureName, string[] WhereField, string[] WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < WhereField.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                cmd.Parameters.Add(p);
            }
            //cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            con.Close();
            return false;
        }
    }

    public SqlDataReader SetdrWithProcedure(string ProcedureName)
    {
        dr = null;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcedureName;
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                return dr;
            }
            else
            {
                //dr.Close();
                con.Close();
                return null;
            }
        }
        catch
        {
            //dr.Close();
            con.Close();
            return null;
        }
    }

    public SqlDataReader SetdrWithProcedure(string ProcedureName, string WhereField, string WhereValue)
    {
        dr = null;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            //System.IO.File.Create("C:\\1.txt");
            //System.IO.File.WriteAllText("C:\\1.txt", con.ConnectionTimeout.ToString());
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p);

            cmd.CommandText = ProcedureName;
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                return dr;
            }
            else
            {
                //dr.Close();
                con.Close();
                return null;
            }
        }
        catch (Exception ex)
        {
            //dr.Close();
            con.Close();
            return null;
        }
    }

    public string SearchwhitProcedure(string ProcedureName, string WhereField, string WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p = new SqlParameter("@" + WhereField, WhereValue);
            cmd.Parameters.Add(p);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                dr.Read();
                string id = dr.GetValue(0).ToString();
                dr.Close();
                con.Close();
                return id;
            }
            else
            {
                //dr.Close();
                con.Close();
                return "";
            }
        }
        catch
        {
            dr.Close();
            con.Close();
            return "";
        }
    }

    public string SearchwhitProcedure(string ProcedureName, string[] WhereField, string[] WhereValue)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < WhereField.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                cmd.Parameters.Add(p);
            }
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                dr.Read();
                string id = dr.GetValue(0).ToString();
                dr.Close();
                con.Close();
                return id;
            }
            else
            {
                //dr.Close();
                con.Close();
                return "";
            }
        }
        catch
        {
            dr.Close();
            con.Close();
            return "";
        }
    }

    public string Search(string TableName, string selectfield, string Where)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand("select " + selectfield + " from " + TableName + Where, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                dr.Read();
                string id = dr.GetValue(0).ToString();
                dr.Close();
                con.Close();
                return id;
            }
            else
            {
                //dr.Close();
                con.Close();
                return "";
            }
        }
        catch
        {
            dr.Close();
            con.Close();
            return "";
        }
    }

    public string GetMaxTarikh(string TableName, string FieldName)
    {
        //SqlDataReader dr;
        string id;
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand("select max(" + FieldName + ") from " + TableName + "", con);
            dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                dr.Read();
                if (dr.IsDBNull(0))
                    id = "1";
                else
                    id = dr.GetValue(0).ToString();
                dr.Close();
                con.Close();
                return id;
            }
            else
            {
                dr.Close();
                con.Close();
                return "1";
            }
        }
        catch (Exception ex)
        {
            dr.Close();
            con.Close();
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            return "1";
        }
        finally
        {

        }

    }

    public int AddWithProcedure(string ProcedureName, string[] Fileds, string[] Values, string OutputField)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand(ProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Fileds.Length; i++)
            {
                SqlParameter p = new SqlParameter("@" + Fileds[i], Values[i]);
                cmd.Parameters.Add(p);
            }//end for                
            SqlParameter q = new SqlParameter("@" + OutputField, SqlDbType.Int);
            q.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(q);

            cmd.CommandText = ProcedureName;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(cmd.Parameters["@" + OutputField].Value);
        }
        catch
        {
            con.Close();
            return 0;
        }
    }
    public object GetValue(string Sql)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(Sql, con);
            object res = cmd.ExecuteScalar();
            con.Close();
            return res;
        }
        catch
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return null;
        }
    }
    public DataTable GetTable(SqlCommand cmd)
    {
        try
        {
            DataTable dt = new DataTable();
            OpenConnection();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            CloseConnection();
            return dt;
        }
        catch
        {
            throw;
        }
    }
    public DataTable GetTable(string sql)
    {
        try
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            return GetTable(cmd);
        }
        catch
        {
            throw;
        }
    }



}




//using System;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection;

//public class DynamicQueryExample
//{
//    public static void Main(string[] args)
//    {
//        using (var context = new YourDbContext())
//        {
//            // Assume you want to query the tblPayehFFM table
//            string entityName = "tblPayehFFM"; // This could come from user input

//            // Execute the dynamic query
//            var results = ExecuteDynamicQuery(context, entityName, 88);

//            // Process the results
//            foreach (var item in results)
//            {
//                Console.WriteLine($"{item.CodePayeh}, {item.AddressPayeh}");
//            }
//        }
//    }

//    public static IQueryable<dynamic> ExecuteDynamicQuery(YourDbContext context, string entityName, int codeOmoor)
//    {
//        // Get the entity type by name
//        Type entityType = context.GetType().GetProperties()
//            .Where(p => p.PropertyType.IsGenericType &&
//                        p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
//            .Select(p => p.PropertyType.GetGenericArguments()[0])
//            .FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

//        if (entityType == null)
//            throw new ArgumentException($"Entity type {entityName} not found in context.");

//        // Use reflection to execute the SQL query dynamically
//        string sqlQuery = @$"
//            SELECT Code_Payeh, Address_Payeh
//            FROM { entityName}
//        WHERE Code_FFM IN(
//            SELECT Code_QFFM

//            FROM tbl_QFFM

//            WHERE Fider IN(
//                SELECT Code_FFM

//                FROM tbl_FFM

//                WHERE Pft IN(
//                    SELECT Code_pft

//                    FROM tbl_PFT

//                    WHERE Omoor IN(
//                        SELECT Code_Omoor

//                        FROM tbl_Omoor

//                        WHERE Code_Omoor = { codeOmoor}
//                        )
//                    )
//                )
//            )";

//        // Use Dynamic LINQ to create a dynamic DbSet and execute the query
//        var dbSet = context.Set(entityType);
//        var results = dbSet.FromSqlRaw(sqlQuery).ToList();

//        return (IQueryable<dynamic>)results;
//    }
//}

