using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using A4A.Models;


namespace A4A.DataAccess
{
    public class DBController
    {
        DBManager dbMan;
        public DBController()
        {
            dbMan = new DBManager();
        }


        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }

        public int InsertUser(AccountModel AM)
        {
            string StoredProcedureName = StoredProcedures.Insert_User;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", AM.ID);
            Parameters.Add("@Fname", AM.Fname);
            Parameters.Add("@Lname", AM.Lname);
            Parameters.Add("@Email", AM.Email);
            Parameters.Add("@Rating", AM.Rating);
            Parameters.Add("@Password", AM.Password);
            Parameters.Add("@Type", AM.Type);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertGroup(GroupModel GM)
        {
            string StoredProcedureName = StoredProcedures.InsertGroup;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@GroupID", GM.GroupID);
            Parameters.Add("@GroupName", GM.GroupName);
            Parameters.Add("@AdminID", GM.AdminID);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }

        public int InsertContest(ContestModel CM)
        {
            string StoredProcedureName = StoredProcedures.InsertContest;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();            
            CM.ContestID = Count_Contests() + 1; ;
            Parameters.Add("@ContestID", CM.ContestID);
            Parameters.Add("@ContestName", CM.ContestName);
            Parameters.Add("@ContestDate", DateTime.Now.Date);
            Parameters.Add("@ContestLength", CM.ContestLength);
            Parameters.Add("@ContestWriterID", CM.ContestWriterID);

            int contest = dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
            int problem = InsertProblem(CM);
            return problem;
        }

        public int InsertProblem(ContestModel CM)
        {
            string StoredProcedureName = StoredProcedures.InsertProblem;
            string StoredProcedureName2 = StoredProcedures.InsertContestProblem;

            List<string> Topics = new List<string>();
            Topics.Add("Math");
            Topics.Add("Graphs");
            Topics.Add("Adhok");
            Topics.Add("Implementation");
            Topics.Add("Greedy");
            int done = 1;
            for (int i = 1; i <= 5; ++i)
            {
                Dictionary<string, object> Parameters = new Dictionary<string, object>();
                Dictionary<string, object> Parameters2 = new Dictionary<string, object>();

                //int ContestID = int.Parse(Guid.NewGuid().ToString());
                Parameters.Add("@ProblemWriter", CM.ContestWriterID);
                Parameters.Add("@ProblemName", CM.ContestName + " P" + i.ToString());
                Parameters.Add("@ProblemTopic", Topics[i-1]);
                string problem = "";
                switch (i)
                {
                    case 1:
                        problem = CM.Problem1;
                        break;
                    case 2:
                        problem = CM.Problem2;
                        break;
                    case 3:
                        problem = CM.Problem3;
                        break;
                    case 4:
                        problem = CM.Problem4;
                        break;
                    case 5:
                        problem = CM.Problem5;
                        break;
                    default:
                        break;

                }
                Parameters.Add("@ProblemLink", "https://codeforces.s3.amazonaws.com/" + problem + ".pdf");
                Parameters.Add("@ProblemDifficulty", 800+(i-1)*200);
                Parameters.Add("@ProblemContest", CM.ContestID);
                Parameters.Add("@ProblemID", problem);

                Parameters2.Add("@ContestID", CM.ContestID);
                Parameters2.Add("@ProblemID", problem);

                done = dbMan.ExecuteNonQuery(StoredProcedureName2, Parameters2);
                done = dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
            }


            return done;
        }

        public int Count_Users()
        {
            string StoredProcedureName = StoredProcedures.Count_Users;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }

        public int CountGroups()
        {
            string StoredProcedureName = StoredProcedures.Count_Groups;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }

        public int Select_User_ID(string Email, string Password)
        {
            string StoredProcedureName = StoredProcedures.Check_Email_And_Password;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email);
            Parameters.Add("@Password", Password);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        public DataTable SelectProblems()
        {
            string StoredProcedureName = StoredProcedures.LoadProblems;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public DataTable SelectContests()
        {
            string StoredProcedureName = StoredProcedures.LoadContests;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public int InsertSubmission(SubmissionModel SM)
        {
            string StoredProcedureName = StoredProcedures.InsertSubmission;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@SubmissionID", SM.SubmissionID);
            Parameters.Add("@ContestantID", SM.ContestantID);
            Parameters.Add("@SubmissionVerdict", SM.SubmissionVerdict);
            Parameters.Add("@SubmissionMemory", SM.SubmissionMemory);
            Parameters.Add("@SubmissionTime", SM.SubmissionTime);
            Parameters.Add("@SubmissionDate", SM.SubmissionDate);
            Parameters.Add("@SubmissionLang", SM.SubmissionLang);
            Parameters.Add("@ProblemID", SM.ProblemID);


            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }


      

        public DataTable SelectMyContests(int UserID)
        {
            string StoredProcedureName = StoredProcedures.LoadMyContests;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", UserID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public DataTable SelectContestProblems(int ContestID)
        {
            string StoredProcedureName = StoredProcedures.SelectContestProblems;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ContestID", ContestID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }

        public DataTable GetSubmissionByID(int SubmissionID)
        {
            string StoredProcedureName = StoredProcedures.GetSubmissionByID;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@SubmissionID", SubmissionID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public DataTable SelectUserNameByID(int id)
        {
            string StoredProcedureName = StoredProcedures.SelectUserNameByID;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", id);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }

        public string GetProblemNameByID(string ProblemID)
        {
            string StoredProcedureName = StoredProcedures.GetProblemNameByID;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ProblemID", ProblemID);

            return Convert.ToString(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }

        public DataTable SelectUsers()
        {
            string StoredProcedureName = StoredProcedures.ViewAllUsers;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public DataTable SelectFriends(int UserID)
        {
            string StoredProcedureName = StoredProcedures.Friends;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ID", UserID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public DataTable SelectUser(int id)
        {
            string StoredProcedureName = StoredProcedures.Select_User;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", id);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);

        }
        public DataTable SelectAllGroups()
        {
            string StoredProcedureName = StoredProcedures.LoadGroups;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public DataTable SelectMyGroups(int ID)
        {
            string StoredProcedureName = StoredProcedures.LoadGroupsOfUser;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", ID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }

        public int Select_UserID_By_Email(string Email)
        {

            string StoredProcedureName = StoredProcedures.Select_UserID_By_Email;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", Email );

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        
        public int InsertOrg(OrgModel OM)
        {
            string StoredProcedureName = StoredProcedures.InsertOrg;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OrgID", OM.OrgID);
            Parameters.Add("@OrgName", OM.OrgName);
            Parameters.Add("@AdminID", OM.AdminID);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertFriend(int ID , int FriendID)
        {
            string StoredProcedureName = StoredProcedures.InsertFriends;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", ID);
            Parameters.Add("@FriendID", FriendID);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public DataTable SelectOrgs()
        {
            string StoredProcedureName = StoredProcedures.Select_All_Orgs;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public DataTable Select_Orgs_of_Group(int GroupID)
        {
            string StoredProcedureName = StoredProcedures.Select_Orgs_of_Group;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@GroupID", GroupID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public int InsertOrgGroups(int OrgID, int GroupID)
        {
            string StoredProcedureName = StoredProcedures.InsertOrgGroups;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OrgID", OrgID);
            Parameters.Add("@GroupID", GroupID);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }

        public int InsertTeam(TeamModel TM)
        {
            string StoredProcedureName = StoredProcedures.InsertTeam;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@TeamID", TM.TeamID);
            Parameters.Add("@TeamName", TM.TeamName);
            Parameters.Add("@LeaderID", TM.LeaderID);
            Parameters.Add("@Rating", 0);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public DataTable SelectTeams()
        {
            string StoredProcedureName = StoredProcedures.Select_All_Teams;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public DataTable Select_Teams_of_Member(int MemberID)
        {
            string StoredProcedureName = StoredProcedures.Select_Teams_of_Member;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@MemberID", MemberID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public int InsertTeamMembers(int TeamID, int MemberID)
        {
            string StoredProcedureName = StoredProcedures.InsertTeamMembers;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@TeamID", TeamID);
            Parameters.Add("@MemberID", MemberID);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }

        public int Count_Contests()
        {
            string StoredProcedureName = StoredProcedures.Count_Contests;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }
        public int Count_Orgs()
        {
            string StoredProcedureName = StoredProcedures.Count_Orgs;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }
        public int Count_Blogs()
        {
            string StoredProcedureName = StoredProcedures.Count_Blogs;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }
        public int Count_Teams()
        {
            string StoredProcedureName = StoredProcedures.Count_Teams;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }
        public int Count_Submissions()
        {
            string StoredProcedureName = StoredProcedures.Count_Submissions;
            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, null));
        }

        public int Select_Id_by_Email(string email)
        {
            string StoredProcedureName = StoredProcedures.Select_Id_by_Email;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Email", email);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }

        public DataTable Select_Team_By_ID(int TeamID)
        {
            string StoredProcedureName = StoredProcedures.Select_Team_By_ID;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@TeamID", TeamID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public DataTable Select_Team_members_Names(int TeamID)
        {
            string StoredProcedureName = StoredProcedures.Select_Team_members_Names;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@TeamID", TeamID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }

        public int SelectOrgGroups(int OrgID)
        {
            string StoredProcedureName = StoredProcedures.SelectOrgGroups;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OrgID", OrgID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }

        public DataTable SelectGroupMembers(int GroupID)
        {
            string StoredProcedureName = StoredProcedures.SelectGroupMembers;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@GroupID", GroupID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }

        public string SelectTypeById(int UserID)
        {
            if (UserID == null || UserID == 0)
            {
                return "";
            }

            string StoredProcedureName = StoredProcedures.SelectTypeById;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", UserID);

            return Convert.ToString(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }

        public DataTable SelectGroupContests(int GroupID)
        {
            string StoredProcedureName = StoredProcedures.SelectGroupContests;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@GroupID", GroupID);

            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public DataTable GetAvailableProblems()
        {
            string StoredProcedureName = StoredProcedures.GetvailableProblems;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }

        public int InsertBlog(string BlogTitle, int BlogWriter, int GroupID, string BlogContent)
        {
            string StoredProcedureName = StoredProcedures.InsertBlog;
            int BlogID = Count_Blogs() + 1;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@BlogID", BlogID);
            Parameters.Add("@BlogTitle", BlogTitle);
            Parameters.Add("@BlogWriter", BlogWriter);
            Parameters.Add("@GroupID", GroupID);
            Parameters.Add("@BlogContent", BlogContent);

            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public DataTable SelectBlogs()
        {
            string StoredProcedureName = StoredProcedures.GetAllBlogs;
            return dbMan.ExecuteReader(StoredProcedureName, null);
        }
        public DataTable SelectMyBlogs(int UserID)
        {
            string StoredProcedureName = StoredProcedures.GetMyBlogs;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", UserID);
            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }
        public DataTable SelectABlogs(int BlogID)
        {
            string StoredProcedureName = StoredProcedures.GetABlog;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@BlogID", BlogID);
            return dbMan.ExecuteReader(StoredProcedureName, Parameters);
        }

        public int Binding(int UserID)
        {
            string StoredProcedureName = StoredProcedures.Binding;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", UserID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }

        public int Solved(int UserID)
        {
            string StoredProcedureName = StoredProcedures.Solved;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserID", UserID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }

        public int DeleteBlog(int BlogID)
        {
            string StoredProcedureName = StoredProcedures.DeleteBlog;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@BlogID", BlogID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        public int DeleteContest(int ContestID)
        {
            string StoredProcedureName = StoredProcedures.DeleteContest;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ContestID", ContestID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        public int DeleteTeam(int TeamID)
        {
            string StoredProcedureName = StoredProcedures.DeleteTeam;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@TeamID", TeamID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        public int DeleteGroup(int GroupID)
        {
            string StoredProcedureName = StoredProcedures.DeleteGroup;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@GroupID", GroupID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        public int DeleteOrg(int OrgID)
        {
            string StoredProcedureName = StoredProcedures.DeleteOrg;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OrgID", OrgID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
        public int DeleteProblem(string ProblemID)
        {
            string StoredProcedureName = StoredProcedures.DeleteProblem;

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ProblemID", ProblemID);

            return Convert.ToInt32(dbMan.ExecuteScalar(StoredProcedureName, Parameters));
        }
    }
}
