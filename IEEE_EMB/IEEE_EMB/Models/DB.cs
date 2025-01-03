using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace IEEE_EMB.Models
{
    public class DB
    {
        private string connectionstring =  "Server=db11993.public.databaseasp.net; Database=db11993; User Id=db11993; Password=eW@9-4Pk2r%B; Encrypt=True; TrustServerCertificate=True;";

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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();


            }
            return dt;


        }

        public DataTable GetParticipants()
        {
            DataTable dt = new DataTable();
            string querey = "select * from PARTICIPANTS";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
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


        public DataTable GetProfileInfo(string email, string ssn)
        {
            DataTable dt = new DataTable();
            string query = "SELECT NAME, EMAIL, BRIEF, PHOTO, UNIVERSITY, PHONE\r\nFROM (SELECT MEM.SSN SSN, MEM.NAME NAME, MEM.EMAIL EMAIL, MEM.BRIEF BRIEF, MEM.PERSONAL_PHOTO PHOTO , MEM.UNIVERSITY UNIVERSITY, Mem.PHONE PHONE\r\nFROM MEMBER MEM\r\nUNION ALL\r\nSELECT MEM.SSN, MEM.NAME NAME, MEM.EMAIL EMAIL, MEM.BRIEF BRIEF, MEM.PERSONAL_PHOTO PHOTO , MEM.UNIVERSITY UNIVERSITY, Mem.PHONE PHONE\r\nFROM ADMIN MEM\r\nUNION ALL\r\nSELECT MEM.SSN, MEM.NAME NAME, MEM.EMAIL EMAIL, MEM.BRIEF BRIEF, MEM.PERSONAL_PHOTO PHOTO , MEM.GRADUATED_UNIVERSITY UNIVERSITY, Mem.PHONE PHONE\r\nFROM MENTOR MEM\r\nUNION ALL\r\nSELECT MEM.SSN, MEM.NAME NAME, MEM.EMAIL EMAIL, MEM.BRIEF BRIEF, MEM.PERSONAL_PHOTO PHOTO , MEM.UNIVERSITY UNIVERSITY, MEM.PHONE PHONE\r\nFROM PARTICIPANTS MEM\r\n) USER_PERSONAL_DATA \r\nWHERE EMAIL = @Email AND SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@SSN", ssn);


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

            string ParticipantQuery = "SELECT COUNT(*)\r\nFROM PARTICIPANTS M\r\nWHERE M.EMAIL = @EMAIL AND M.PASSWORD = @PASSWORD";
            using (SqlCommand cmd = new SqlCommand(ParticipantQuery, con))
            {
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                if (count > 0) return "Participant";
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
            UNION ALL
            SELECT P.SSN SSN, P.EMAIL FROM PARTICIPANTS P
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
            string query = "SELECT A.ID, A.TITLE, A.START_DATE, A.END_DATE, A.Capacity, A.TYPE, A.STATUS, M.NAME\r\n" +
                "FROM ACTIVITY A JOIN ASSIGN AG ON A.ID = AG.ACTIVITY_ID\r\n" +
                "JOIN MENTOR M ON AG.MENTOR_SSN = M.SSN";

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
            string query = "SELECT TITLE,START_DATE,TYPE,STATUS,DESCRIPTION FROM ACTIVITY \r\nwhere STATUS='Upcoming'\r\norder by START_DATE desc";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
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

        public DataTable GetSeminar()
        {
            DataTable dt = new DataTable();
            string query = "SELECT AC.ID, M.NAME, AC.TITLE , AC.CAPACITY, AC.START_DATE, AC.CAPACITY, AC.DESCRIPTION, AC.STATUS FROM ASSIGN A JOIN MENTOR M ON A.MENTOR_SSN = M.SSN JOIN ACTIVITY AC ON AC.ID = A.ACTIVITY_ID WHERE AC.TYPE = 'Seminar' order by AC.START_DATE desc";
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
            string query = "SELECT AC.ID, M.NAME, AC.TITLE , AC.CAPACITY, AC.START_DATE, AC.CAPACITY, AC.DESCRIPTION, AC.STATUS\r\nFROM ASSIGN A JOIN MENTOR M ON A.MENTOR_SSN = M.SSN\r\nJOIN ACTIVITY AC ON AC.ID = A.ACTIVITY_ID\r\nWHERE AC.TYPE = 'JournalClub' order by AC.START_DATE desc\r\n";
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
            string query = "SELECT AC.ID, M.NAME, AC.TITLE , AC.CAPACITY, AC.START_DATE, AC.CAPACITY, AC.DESCRIPTION, AC.STATUS\r\nFROM ASSIGN A JOIN MENTOR M ON A.MENTOR_SSN = M.SSN\r\nJOIN ACTIVITY AC ON AC.ID = A.ACTIVITY_ID\r\nWHERE AC.TYPE = 'Workshop' order by AC.START_DATE desc\r\n";
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
                object result = getMaxIdCommand.ExecuteScalar();

                // Handle null by setting newId to 1 if the table is empty
                newId = result == DBNull.Value || result == null ? 1 : Convert.ToInt32(result) + 1;

                string query = "INSERT INTO ACTIVITY (ID, TITLE, START_DATE, END_DATE, CAPACITY, TYPE, STATUS, DESCRIPTION)" +
                               "VALUES (@ID, @TITLE, @START_DATE, @END_DATE, @CAPACITY, @TYPE, @STATUS, @DESCRIPTION)";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@ID", newId);
                com.Parameters.AddWithValue("@TITLE", activity.Title);
                com.Parameters.AddWithValue("@START_DATE", activity.startdate);
                com.Parameters.AddWithValue("@END_DATE", activity.Enddate);
                com.Parameters.AddWithValue("@CAPACITY", activity.Capacity);
                com.Parameters.AddWithValue("@TYPE", activity.Type);
                com.Parameters.AddWithValue("@STATUS", activity.status);
                com.Parameters.AddWithValue("@DESCRIPTION", activity.Description ?? (object)DBNull.Value);
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

        public void EditActivity(Activity activity)
        {
            string editTitleQuery = $"UPDATE ACTIVITY\r\nSET TITLE = {activity.Title}\r\nWHERE ID = {activity.Id}";
            string editDescriptionQuery = $"UPDATE ACTIVITY\r\nSET DESCRIPTION = {activity.Description}\r\nWHERE ID = {activity.Id}";
            string editCapacityQuery = $"UPDATE ACTIVITY\r\nSET CAPACITY = {activity.Capacity}\r\nWHERE ID = {activity.Id}";
            try
            {
                con.Open();
                SqlCommand editTitleCommand = new SqlCommand(editTitleQuery, con);
                SqlCommand editDescriptionCommand = new SqlCommand(editTitleQuery, con);
                SqlCommand editCapacityCommand = new SqlCommand(editTitleQuery, con);
                editTitleCommand.ExecuteNonQuery();
                editDescriptionCommand.ExecuteNonQuery();
                editCapacityCommand.ExecuteNonQuery();
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
                object result = getMaxIdCommand.ExecuteScalar();

                // Handle null by setting newId to 1 if the table is empty
                newId = result == DBNull.Value || result == null ? 1 : Convert.ToInt32(result) + 1;

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
            }
        }

        public DataTable GetActivitiesForMentor(string MentorId)
        {
            DataTable dt = new DataTable();
            string query = $"SELECT A.ID, A.TITLE, A.START_DATE, A.END_DATE, A.Capacity, A.TYPE, A.STATUS, M.NAME\r\n" +
                $"FROM ACTIVITY A JOIN ASSIGN AG ON A.ID = AG.ACTIVITY_ID\r\n" +
                $"JOIN MENTOR M ON AG.MENTOR_SSN = M.SSN WHERE M.SSN ='{MentorId}'";
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
       


        
        public void AddSessionTaskAnswer(int SessionId , string TaskAnswer)
        {
            string querey = $"update SESSION set TASK_ANSWER='{TaskAnswer}'where ID={SessionId}";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
              

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

        public void DeleteActivity(int activityID)
        {

            string deleteAssignmentQuery = $"DELETE FROM ASSIGN\r\nWHERE ACTIVITY_ID = {activityID}";
            string deleteSessionQuery = $"DELETE FROM SESSION\r\nWHERE ACTIVITY_ID = {activityID}";
            string deleteActivityQuery = $"DELETE FROM ACTIVITY\r\nWHERE ID = {activityID}";
            try
            {
                con.Open();
                SqlCommand comAssign = new SqlCommand(deleteAssignmentQuery, con);
                SqlCommand comSession = new SqlCommand(deleteSessionQuery, con);
                SqlCommand comActivity = new SqlCommand(deleteActivityQuery, con);
                comAssign.ExecuteNonQuery();
                comSession.ExecuteNonQuery();
                comActivity.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }


        }
        public void DeleteSeesion(int SessionId)
        {
            string querey = $"Delete from SESSION where ID ={SessionId}";
            //string querey2 = $"Delete from SESSIONS_DOCS where ID={SessionId}";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
                // SqlCommand com2 = new SqlCommand(querey2, con);

                com.ExecuteNonQuery();
                // com2.ExecuteNonQuery();
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

        // Not Complete yet
        public void AddAdmin(Admin admin)
        {
            string query = "INSERT INTO ADMIN (SSN, NAME, PASSWORD, ROLE, EMAIL, CV, PHONE, PERSONAL_PHOTO, UNIVERSITY, BRIEF)" +
                "VALUES (@SSN, @NAME, @PASSWORD, @ROLE, @EMAIL, @CV, @PHONE, @PERSONAL_PHOTO, @UNIVERSITY, @BRIEF)";


            // Not Complete yet
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", admin.SSN);
                cmd.Parameters.AddWithValue("@NAME", admin.Name);
                cmd.Parameters.AddWithValue("@PASSWORD", admin.password);
                cmd.Parameters.AddWithValue("@ROLE", admin.Role ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EMAIL", admin.Email);
                cmd.Parameters.AddWithValue("@CV", admin.CV ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PHONE", admin.Phone);
                cmd.Parameters.AddWithValue("@PERSONAL_PHOTO", admin.PersonalPhoto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UNIVERSITY", admin.University ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BRIEF", admin.Brief ?? (object)DBNull.Value);
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
        

        public void DeleteAdmin(Admin admin)
        {

            string deleteAdminQuery = "DELETE FROM ADMIN WHERE SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand deleteAdminCommand = new SqlCommand(deleteAdminQuery, con);
                deleteAdminCommand.Parameters.AddWithValue("@SSN", admin.SSN);
                deleteAdminCommand.ExecuteNonQuery();

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

        public DataTable GetAdmins()
        {
            DataTable dt = new DataTable();
            string getAdminsQuery = "SELECT A.NAME, A.EMAIL, A.UNIVERSITY, A.CV" +
                                    "\r\n FROM ADMIN A";
            try
            {
                con.Open();
                SqlCommand getAdminsCommand = new SqlCommand(getAdminsQuery, con);
                dt.Load(getAdminsCommand.ExecuteReader());
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

        public DataTable GetParticipantsInActivity(int activityID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT P.SSN, P.NAME, P.EMAIL, P.PHONE, P.UNIVERSITY\r\n" +
                "FROM ACTIVITY A \r\n" +
                "JOIN ACTIVITY_PARTICIPANTS AP ON AP.ACTIVITY_ID = A.ID\r\n" +
                "JOIN PARTICIPANTS P ON P.SSN = AP.PARTICIPANT_SSN\r\n" +
                "WHERE A.ID = @ID";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", activityID);
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



        /////////////////////////////////////
        public DataTable GetSpecificActivity(int ActivityId)
        {
            DataTable dt = new DataTable();
            string query = $"select a.TITLE as 'ActivityTitle', a.DESCRIPTION , m.NAME , m.EDUCATION , s.TITLE as 'SessionTitle' , s.DATE , s.ID as 'SessionId'\r\n" +
                $"from ACTIVITY a join ASSIGN ass on a.ID = ass.ACTIVITY_ID join MENTOR m on ass.MENTOR_SSN = m.SSN left join SESSION s on s.ACTIVITY_ID= a.ID\r\nwhere a.ID={ActivityId}";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                dt.Load(command.ExecuteReader());
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


        ///////////////////////
        public DataTable getspecificsession(int SessionId)
        {
            DataTable dt = new DataTable();
            string query = $"select * from SESSION where ID={SessionId};";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                dt.Load(command.ExecuteReader());
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

        public int NumAdmins()
        {
            int count = 0;
            string query = "select count(*) from ADMIN ;";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return count;
        }
        public int NumMentors()
        {
            int count = 0;
            string query = "select count(*) from MENTOR  ;";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return count;
        }

        public int NumMembers()
        {
            int count = 0;
            string query = "select count(*) from MEMBER;";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return count;
        }
        public int NumParticipant()
        {
            int count = 0;
            string query = "select count(*) from PARTICIPANTS ;";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return count;
        }

        public DataTable ParticipantsPerActivity()
        {
            DataTable participants = new DataTable();
            string query = "select a.TYPE,count(AP.PARTICIPANT_SSN) as 'count'\r\nfrom ACTIVITY_PARTICIPANTS AP left join PARTICIPANTS p on AP.PARTICIPANT_SSN= p.SSN right join ACTIVITY a on a.ID = AP.ACTIVITY_ID\r\ngroup by a.TYPE\r\n ;";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                participants.Load(cmd.ExecuteReader());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return participants;
        }

        public int NumOfActivityPerMonth(int month, string ActivityType)
        {
            int count = 0;
            string query = $"select count(*) \r\nfrom ACTIVITY \r\nwhere  MONTH(START_DATE) = {month} and TYPE ='{ActivityType}'\r\n;";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return count;
        }
        public DataTable TopFiveParticipants()
        {
            DataTable participants = new DataTable();
            string query = "select top 5 P.NAME,count(*)\r\nfrom ACTIVITY_PARTICIPANTS AP join PARTICIPANTS P on Ap.PARTICIPANT_SSN= P.SSN \r\ngroup by P.NAME\r\norder by count(*) desc ;";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                participants.Load(cmd.ExecuteReader());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return participants;
        }


        public DataTable TopFiveActivities()
        {
            DataTable Activities = new DataTable();
            string query = " select top 5 a.TITLE , count(ap.PARTICIPANT_SSN)\r\nfrom ACTIVITY_PARTICIPANTS ap  join ACTIVITY a on ap.ACTIVITY_ID = a.ID\r\ngroup by a.ID,  a.TITLE \r\norder by  count(ap.PARTICIPANT_SSN) desc ;";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                Activities.Load(cmd.ExecuteReader());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return Activities;
        }

        ////////////////////////////////////////////////////////////
        public int GetNumberOfActivitiesForMentor(string ssn)
        {
            int count = 0;
            string query = "SELECT COUNT(*)\r\n" +
                "FROM ACTIVITY A\r\n" +
                "JOIN ASSIGN AG ON A.ID = AG.ACTIVITY_ID\r\n" +
                "JOIN MENTOR M ON M.SSN = AG.MENTOR_SSN\r\n" +
                "WHERE M.SSN = @SSN \r\n" +
                "GROUP BY M.NAME, M.SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", ssn);
                count = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                con.Close();
            }
            return count;

        }

        public DataTable GetParticipantsWithMentor(string ssn)
        {
            DataTable participants = new DataTable();
            string query = "SELECT A.ID, M.NAME, A.TITLE, COUNT(*) ParticipantsNumber\r\n" +
                "FROM ACTIVITY A\r\n" +
                "JOIN ASSIGN AG ON A.ID = AG.ACTIVITY_ID\r\n" +
                "JOIN MENTOR M ON M.SSN = AG.MENTOR_SSN\r\n" +
                "JOIN ACTIVITY_PARTICIPANTS AP ON AP.ACTIVITY_ID = A.ID\r\n" +
                "WHERE M.SSN = @SSN \r\n" +
                "GROUP BY A.ID, M.NAME, A.TITLE\r\n";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", ssn);
                participants.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }

            return participants;
        }



        public int GetTotalParticipantsNumberWithMentor(string ssn)
        {
            int count = 0;
            string query = "SELECT COUNT(*) ParticipantsNumber\r\n" +
                "FROM ACTIVITY A\r\n" +
                "JOIN ASSIGN AG ON A.ID = AG.ACTIVITY_ID\r\n" +
                "JOIN MENTOR M ON M.SSN = AG.MENTOR_SSN\r\n" +
                "JOIN ACTIVITY_PARTICIPANTS AP ON AP.ACTIVITY_ID = A.ID\r\n" +
                "WHERE M.SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", ssn);
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally
            {
                con.Close();
            }
            return count;
        }


       
         public void MemberJoin(Member member)
        {
            string query = "INSERT INTO MEMBER(SSN, YEAR, PHONE, PASSWORD, EMAIL, PREVIOUS_EXPERIENCE, NAME, STATUS, MAJOR, BRIEF, UNIVERSITY, PERSONAL_PHOTO)\r\n" +
                "VALUES(@SSN, @YEAR, @PHONE, @PASSWORD, @EMAIL, @PREVIOUS_EXPERIENCE, @NAME, 'Pending', @MAJOR, @BRIEF, @UNIVERSITY, @PERSONAL_PHOTO)\r\n";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", member.SSN);
                cmd.Parameters.AddWithValue("@NAME", member.Name);
                cmd.Parameters.AddWithValue("@PASSWORD", member.password);
                cmd.Parameters.AddWithValue("@EMAIL", member.Email);
                cmd.Parameters.AddWithValue("@PHONE", member.Phone);
                cmd.Parameters.AddWithValue("@UNIVERSITY", member.University);
                cmd.Parameters.AddWithValue("@PERSONAL_PHOTO", member.PersonalPhoto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BRIEF", member.Brief ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PREVIOUS_EXPERIENCE", member.PreviousExperience ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MAJOR", member.Major);
                cmd.Parameters.AddWithValue("@YEAR", member.Currentyear);
               
                
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
         public void MentorJoin(Mentor mentor)
        {
            string query = "INSERT INTO MENTOR(SSN, NAME, PASSWORD, EMAIL, CV, PHONE, PERSONAL_PHOTO, BRIEF, UNIVERSITY, EDUCATION, BIO, STATUS)\r\n" +
                "VALUES(@SSN, @NAME, @PASSWORD, @EMAIL, @CV, @PHONE, @PERSONAL_PHOTO, @BRIEF, @UNIVERSITY, @EDUCATION, @BIO, 'Pending' )\r\n";


            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", mentor.SSN);
                cmd.Parameters.AddWithValue("@NAME", mentor.Name);
                cmd.Parameters.AddWithValue("@PASSWORD", mentor.Password);
                cmd.Parameters.AddWithValue("@EMAIL", mentor.Email);
                cmd.Parameters.AddWithValue("@PHONE", mentor.Phone);
                cmd.Parameters.AddWithValue("@UNIVERSITY", mentor.University);
                cmd.Parameters.AddWithValue("@PERSONAL_PHOTO", mentor.PersonalPhoto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BRIEF", mentor.Brief ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BIO", mentor.BIO);
                cmd.Parameters.AddWithValue("@STATUS", mentor.status);
                cmd.Parameters.AddWithValue("@EDUCATION", mentor.Education);
                cmd.Parameters.AddWithValue("@CV", mentor.CV ?? (object)DBNull.Value);
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
        public DataTable GetMember()
        {
            DataTable dt = new DataTable();
            string querey = "select * from MEMBER";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
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
        public DataTable GetMentor()
        {
            DataTable dt = new DataTable();
            string querey = "select * from MENTOR";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
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
        public void AddMember(Member member)
        {
            try
            {
                con.Open();


                string query = "INSERT INTO MEMBER(SSN, NAME, EMAIL, PHONE, UNIVERSITY, PASSWORD, YEAR, PREVIOUS_EXPERIENCE, STATUS, MAJOR, BRIEF)" +
                               "VALUES(@SSN, @NAME, @EMAIL, @PHONE, @UNIVERSITY, @PASSWORD, @YEAR, @PREVIOUS_EXPERIENCE, @STATUS, @MAJOR, @BRIEF)";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@SSN", member.SSN);
                com.Parameters.AddWithValue("@NAME", member.Name);
                com.Parameters.AddWithValue("@EMAIL", member.Email);
                com.Parameters.AddWithValue("@PHONE", member.Phone);
                com.Parameters.AddWithValue("@UNIVERSITY", member.University);
                com.Parameters.AddWithValue("@PASSWORD", member.password);
                com.Parameters.AddWithValue("@YEAR", member.Currentyear);
                com.Parameters.AddWithValue("@PREVIOUS_EXPERIENCE", member.PreviousExperience);
                com.Parameters.AddWithValue("@STATUS", member.status);
                com.Parameters.AddWithValue("@MAJOR", member.Major);
                com.Parameters.AddWithValue("@BRIEF", member.Brief);

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
        public void AddMentor(Mentor mentor)
        {
            string query = "INSERT INTO MENTOR(SSN, NAME, EMAIL, PHONE, EDUCATION, PASSWORD, CV, BIO)" +
                            "VALUES(@SSN, @NAME, @EMAIL, @PHONE, @EDUCATION, @PASSWORD, @CV, @BIO)";
            try
            {
                con.Open();


               
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@SSN", mentor.SSN);
                com.Parameters.AddWithValue("@NAME", mentor.Name);
                com.Parameters.AddWithValue("@EMAIL", mentor.Email);
                com.Parameters.AddWithValue("@PHONE", mentor.Phone);
                com.Parameters.AddWithValue("@EDUCATION", mentor.Education);
                com.Parameters.AddWithValue("@PASSWORD", mentor.Password);
                com.Parameters.AddWithValue("@CV", mentor.CV);
                com.Parameters.AddWithValue("@BIO", mentor.BIO);
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
        public int ACTIVITYID(int SessionID)
        {

                int ActivityId=0;
                string query = "select s.ACTIVITY_ID\r\nfrom SESSION s where ID= 2";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    ActivityId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return ActivityId;
            

        }
        public void DeleteParticipant(string ParticipantSSN)
        {

            string deleteACT_PARQuery = $"DELETE FROM ACTIVITY_PARTICIPANTS\r\nWHERE PARTICIPANT_SSN = {ParticipantSSN}";
            string deleteFeedBackQuery = $"DELETE FROM FEEDBACK\r\nWHERE PARTICIPANT_SSN = {ParticipantSSN}";
            string deletePARTICIPANTQuery = $"DELETE FROM PARTICIPANTS\r\nWHERE SSN = {ParticipantSSN}";
            
            try
            {
                con.Open();
                SqlCommand comACT_Par = new SqlCommand(deleteACT_PARQuery, con);
                SqlCommand comFeed_Par = new SqlCommand(deleteFeedBackQuery, con);
                SqlCommand comPar = new SqlCommand(deletePARTICIPANTQuery, con);
                comACT_Par.ExecuteNonQuery();
                comFeed_Par.ExecuteNonQuery();  
                comPar.ExecuteNonQuery();
         
          

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }


        }

        public string GetActivityType(int ActivityId)
        {
            string query = $"select TYPE from ACTIVITY where ID={ActivityId}";
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                object result = command.ExecuteScalar();
                return result?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            finally
            {
                con.Close();
            }
        }
        public int GetSessionCount(int activityid)
        {
            int count = 0;
            string querey = $"select COUNT(*) from SESSION where ACTIVITY_ID={activityid}";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(querey, con);
                count = (int)com.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();


            }
            return count;


        }

        public void ChangeMemberStatus(string ssn, string status)
        {
            string query = "UPDATE MEMBER\r\n" +
                "SET STATUS = @STATUS\r\n" +
                "WHERE SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@SSN", ssn);
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

        public void ChangeMentorStatus(string ssn, string status)
        {
            string query = "UPDATE MENTOR\r\n" +
                "SET STATUS = @STATUS\r\n" +
                "WHERE SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@SSN", ssn);
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

        public void DeleteMember(string ssn) {
            string query = "DELETE FROM MEMBER\r\nWHERE SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", ssn);
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
        
        

        public DataTable UserWithActivity(string UserID)
        {
            DataTable dt = new DataTable();
            string Query = $"select ap.ACTIVITY_ID from ACTIVITY_PARTICIPANTS ap where PARTICIPANT_SSN ='{UserID}'";
            SqlCommand cmd= new SqlCommand(Query, con);
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public void DeleteMentor(string ssn) {
            string query = "DELETE FROM MENTOR\r\nWHERE SSN = @SSN";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SSN", ssn);
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
        public void Enroll(int activityId , string ssn)
        {
            string query = $"insert into ACTIVITY_PARTICIPANTS(ACTIVITY_ID, PARTICIPANT_SSN)\r\nvalues({activityId},'{ssn}')";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
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

       
        
        // public
    }
}
