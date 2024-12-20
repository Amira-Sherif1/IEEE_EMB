
﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace IEEE_EMB.Models
{
    public class DB
    {
        private string connectionstring = "Data Source= Eng-Omars-Lap; Initial Catalog= IEEE_EMB; Integrated Security=True; Trust Server Certificate=True;";
        public SqlConnection con = new();
        public DB()
        {
            con.ConnectionString = connectionstring;

        }

        public DataTable GetSession(int activityid)
        {
            DataTable dt = new DataTable();
            string querey = $"select* from SESSION where ACTIVITY_ID={activityid}";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception ex) 
            {

            }
            finally
            {
                con.Close();
            }
            return dt;

           }

        public DataTable GetAnnouncements()
        {
            DataTable dt = new DataTable();
            string query = "SELECT TITLE,START_DATE,TYPE,STATUS,DESCRIPTION FROM ACTIVITY";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }


        public void AddSession(Session session)
        {
            string getMaxIdQuery = "SELECT MAX(ID) FROM SESSION"; 
            int newId = 0;

            try
            {
                con.Open();
                SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, con);
                newId = (int)getMaxIdCommand.ExecuteScalar() + 1;

                string query = "INSERT INTO SESSION (ID, DATE, TITLE, Document, Video, ACTIVITY_ID) " +
                               "VALUES (@ID, @Date, @Title, @Document, @Video, @ActivityId)";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@ID", newId); 
                com.Parameters.AddWithValue("@Date", session.Date);
                com.Parameters.AddWithValue("@Title", session.Title);
                com.Parameters.AddWithValue("@Document", session.Document ?? (object)DBNull.Value);
                com.Parameters.AddWithValue("@Video", session.Video ?? (object)DBNull.Value);
                com.Parameters.AddWithValue("@ActivityId", session.ActivityId);

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void EditSeesion(Session session)
        {
            string querey = $"update SESSION set TITLE='{session.Title}'where ID={session.Id}";
            string querey2 = $"update SESSION set Date='{session.Date}'where ID={session.Id}";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
                SqlCommand com2 = new SqlCommand(querey2, con);

                com.ExecuteNonQuery();
                com2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
                
            }

        }
        public void DeleteSeesion(int SessionId)
        {
            string querey = $"Delete from SESSION where ID ={SessionId}";
            string querey2 = $"Delete from SESSIONS_DOCS where ID={SessionId}";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
                SqlCommand com2 = new SqlCommand(querey2, con);

                com.ExecuteNonQuery();
                com2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();

            }

        
        }
    }
}
