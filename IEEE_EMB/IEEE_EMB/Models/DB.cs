using Microsoft.Data.SqlClient;
using System.Data;

namespace IEEE_EMB.Models
{
    public class DB
    {
        public string ConnectionString = "Data Source=Eng-Omars-Lap;Initial Catalog=IEEE_EMB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        
        public SqlConnection Connection {  get; set; }
        public DB() { 
        Connection = new SqlConnection(ConnectionString);
        }

        public DataTable GetAnnouncements()
        {
            DataTable dt = new DataTable();
            string query = "SELECT TITLE,START_DATE,TYPE,STATUS,DESCRIPTION FROM ACTIVITY";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }


            return dt;
        }
    }
}
