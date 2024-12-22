
﻿using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.X86;

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

        public DataTable GetProfileInfo()
        {
            DataTable dt = new DataTable();
            string query = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public void AddAssignment(Assign assign)
        {
            DataTable dt = new DataTable();
            string query = "INSERT INTO ASSIGN (ADMIN_SSN, MENTOR_SSN, ACTIVITY_ID)\r\nVALUES (@ADMIN_SSN, @MENTOR_SSN, @ACTIVITY_ID)";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ADMIN_SSN", assign.AddminSSN);
                cmd.Parameters.AddWithValue("@MENTOR_SSN", assign.MentorSSN);
                cmd.Parameters.AddWithValue("@ACTIVITY_ID", assign.ActivityId);
                cmd.ExecuteNonQuery();
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

        public string getUserType(string email, string password)
        {

            DataTable dt = new DataTable();
            string MemberQuery = "SELECT COUNT(*)\r\nFROM MEMBER M\r\nWHERE M.EMAIL = @EMAIL AND M.PASSWORD = @PASSWORD";
            using (SqlCommand cmd = new SqlCommand(MemberQuery, con))
            {
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                if (count > 0) return "Member";
            }

            string AdminQuery = "SELECT COUNT(*)\r\nFROM ADMIN A\r\nWHERE A.EMAIL = @EMAIL AND A.PASSWORD = @PASSWORD";
            using (SqlCommand cmd = new SqlCommand(AdminQuery, con))
            {
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                if (count > 0) return "Admin";
            }

            string MentorQuery = "SELECT COUNT(*)\r\nFROM MENTOR M\r\nWHERE M.EMAIL = @EMAIL AND M.PASSWORD = @PASSWORD";
            using (SqlCommand cmd = new SqlCommand(MentorQuery, con))
            {
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                if (count > 0) return "Mentor";
            }

            return null;


        }



        public string GetMentor(string mentorName) // get mentor ssn using his name
        {
            string ssn = null;
            string query = "SELECT M.SSN\r\nFROM MENTOR M\r\nWHERE M.NAME = @NAME";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@NAME", mentorName);
                ssn = cmd.ExecuteScalar()?.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return ssn;
        }

        public int GetActivityID(string Title)
        {
            int ID = -1;
            string query = "SELECT A.ID\r\nFROM ACTIVITY A\r\nWHERE A.TITLE = @TITLE";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TITLE", Title); // Parameterized query
                Console.WriteLine($"Email parameter: ");
                ID = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return ID;
        }
        public string GetUserSSN(string email)
        {
            string ssn = null;
            string query = @"
        SELECT SSN
        FROM
        (
            SELECT A.SSN, A.EMAIL FROM ADMIN A
            UNION ALL
            SELECT MEM.SSN, MEM.EMAIL FROM MEMBER MEM
            UNION ALL
            SELECT MEN.SSN, MEN.EMAIL FROM MENTOR MEN
        ) AllUsers
        WHERE EMAIL = @Email";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email); // Parameterized query
                Console.WriteLine($"Email parameter: ");
                ssn = cmd.ExecuteScalar()?.ToString(); // Get the single value directly
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return ssn; // Returns null if no match is found
        }
        public DataTable GetActvities()
        {
            DataTable dt = new DataTable();
            string query = "SELECT A.ID, A.TITLE, A.START_DATE, A.END_DATE, A.Capacity, A.TYPE, A.STATUS, M.NAME\r\nFROM ACTIVITY A JOIN ASSIGN AG ON A.ID = AG.ACTIVITY_ID\r\nJOIN MENTOR M ON AG.MENTOR_SSN = M.SSN WHERE A.ID > 3";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }


        public DataTable GetMentorsNames()
        {
            DataTable dt = new DataTable();
            string query = "SELECT M.Name\r\nFROM MENTOR M";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return dt;
        }

        public DataTable GetParticipantCount()
        {
            DataTable dt = new DataTable();
            string query = "SELECT A.ACTIVITY_ID, COUNT(*) ParticipantsCount\r\nFROM ACTIVITY_PARTICIPANTS A\r\nGROUP BY A.ACTIVITY_ID";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                con.Close();
            }

            return dt;
        }

        public DataTable GetSeminar()
        {
            DataTable dt = new DataTable();
            string query = "SELECT AC.ID, M.NAME, AC.TITLE , AC.CAPACITY, AC.START_DATE, AC.CAPACITY, AC.DESCRIPTION\r\nFROM ASSIGN A JOIN MENTOR M ON A.MENTOR_SSN = M.SSN\r\nJOIN ACTIVITY AC ON AC.ID = A.ACTIVITY_ID\r\nWHERE AC.TYPE = 'Seminar'";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                dt.Load(com.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }



            return dt;
        }

        public DataTable GetJournalClubs()
        {
            DataTable dt = new DataTable();
            string query = "SELECT AC.ID, M.NAME, AC.TITLE , AC.CAPACITY, AC.START_DATE, AC.CAPACITY, AC.DESCRIPTION\r\nFROM ASSIGN A JOIN MENTOR M ON A.MENTOR_SSN = M.SSN\r\nJOIN ACTIVITY AC ON AC.ID = A.ACTIVITY_ID\r\nWHERE AC.TYPE = 'JournalClub'";
            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                dt.Load(com.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }



            return dt;
        }



        public DataTable GetWorkshops()
        {
            DataTable dt = new DataTable();
            string query = "SELECT AC.ID, M.NAME, AC.TITLE , AC.CAPACITY, AC.START_DATE, AC.CAPACITY, AC.DESCRIPTION\r\nFROM ASSIGN A JOIN MENTOR M ON A.MENTOR_SSN = M.SSN\r\nJOIN ACTIVITY AC ON AC.ID = A.ACTIVITY_ID\r\nWHERE AC.TYPE = 'Workshop'";
            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                dt.Load(com.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }



            return dt;
        }

        public void AddActivity(Activity activity)
        {
            string getMaxIdQuery = "SELECT MAX(ID) FROM ACTIVITY";
            int newId = 0;

            try
            {
                con.Open();
                SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, con);
                newId = (int)getMaxIdCommand.ExecuteScalar() + 1;
                string query = "INSERT INTO ACTIVITY (ID, TITLE, START_DATE, END_DATE, CAPACITY, TYPE, STATUS, DESCRIPTION, MEMBER_ID)" +
                               "VALUES (@ID, @TITLE, @START_DATE, @END_DATE, @CAPACITY, @TYPE, @STATUS, @DESCRIPTION, @MEMBER_ID)";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@ID", newId);
                com.Parameters.AddWithValue("@TITLE", activity.Title);
                com.Parameters.AddWithValue("@START_DATE", activity.startdate);
                com.Parameters.AddWithValue("@END_DATE", activity.Enddate);
                com.Parameters.AddWithValue("@CAPACITY", activity.Capacity);
                com.Parameters.AddWithValue("@TYPE", activity.Type);
                com.Parameters.AddWithValue("@STATUS", activity.status);
                com.Parameters.AddWithValue("@DESCRIPTION", activity.Description);
                com.Parameters.AddWithValue("@MEMBER_ID", DBNull.Value);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }



        }

        public void AddParticipant(Participants participant)
        {
            try
            {
                con.Open();
            
                
                string query = "INSERT INTO PARTICIPANTS(SSN, NAME, EMAIL, PHONE, UNIVERSITY, PASSWORD)" +
                               "VALUES (@SSN, @NAME, @EMAIL, @PHONE, @UNIVERSITY, @PASSWORD)";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@SSN", participant.SSN);
                com.Parameters.AddWithValue("@NAME", participant.Name);
                com.Parameters.AddWithValue("@EMAIL", participant.Email);
                com.Parameters.AddWithValue("@PHONE", participant.Phone);
                com.Parameters.AddWithValue("@UNIVERSITY", participant.University);
                com.Parameters.AddWithValue("@PASSWORD", participant.Password);
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
