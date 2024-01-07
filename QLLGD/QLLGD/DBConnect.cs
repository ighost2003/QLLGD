using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLLGD
{
    class DBConnect : IDisposable
    {
        private SqlConnection connect;
        private bool disposed = false;

        public SqlConnection Connection { get; set; }
        private string strConnect = "Data Source=DESKTOP-LSFEJMC\\SQLEXPRESS; Initial Catalog=QLGD; User Id=sa; Password=123";

        public DBConnect()
        {
            connect = new SqlConnection(strConnect);
        }

        public SqlConnection GetConnection()
        {
            return connect;
        }

        public void Open()
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
        }

        public void Close()
        {
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }

        public int GetNonQuery(string sql)
        {
            Open();
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public object GetScalar(string sql)
        {
            Open();
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                object result = cmd.ExecuteScalar();
                return result;
            }
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, connect))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public int UpdateDatabase(string sql, DataTable dt)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sql, connect))
            {
                using (SqlCommandBuilder cmb = new SqlCommandBuilder(da))
                {
                    int result = da.Update(dt);
                    return result;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    connect.Dispose();
                }

                // Dispose unmanaged resources

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void open()
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
        }
        public void close()
        {
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }
    }
}
