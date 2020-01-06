using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using A4A.Models;
using A4A.DataAccess;
using Helpers;

namespace A4A.Helpers
{
    public class ProblemObject
    {
        public int ContestId;
        public char Index;
        public string Name;
        public string Type;
        public double Points;
        public int Rating;
        public string[] Tags;
    }
    public class MembersHandle
    {
        [JsonProperty(PropertyName = "Handle")]
        public string Handle;
    }
    public class AuthorObject
    {
        public int ContestId;
        public List<MembersHandle> Members = new List<MembersHandle>();
        public string ParticipateType;
        public bool Ghost;
        public int Room;
        public int StartTimeSeconds;
    }
    public class SubmissionObject
    {
        public int Id;
        public int ContestId;
        public int creationTimeSeconds;
        public int relativeTimeSeconds;
        public ProblemObject Problem;
        public AuthorObject Author;
        public string ProgrammingLanguage;
        public string Verdict;
        public string TestSet;
        public int PassedTestCount;
        public int TimeConsumedMillis;
        public int MemoryConsumedBytes;
    }

    public class SubmissionJsonObject
    {
        [JsonProperty(PropertyName = "status")]
        public string Status;

        [JsonProperty(PropertyName = "result")]
        public List<SubmissionObject> SubmissionList;
    }

    public class SubmissionHelper
    {
        public int ParseSubmission(string SubmissionJson, int UserID, int ContestID)
        {            
            bool isLastSubmission = false;
            int LastSubmission = 0;
            SubmissionJsonObject SubmissionJsonDes =
                JsonConvert.DeserializeObject<SubmissionJsonObject>(SubmissionJson);
                
            if (SubmissionJsonDes.Status == "OK")
            {
                List<SubmissionModel> SubmissionArr = new List<SubmissionModel>();
                SubmissionJsonDes.SubmissionList.ForEach(s =>
                {
                    SubmissionModel sub = new SubmissionModel()
                    {
                        
                        SubmissionID = s.Id,
                        ContestantID = UserID,

                        SubmissionVerdict = s.Verdict,
                        SubmissionMemory = s.MemoryConsumedBytes,
                        SubmissionTime = s.TimeConsumedMillis,
                        SubmissionDate = UnixTime.ToDateTime(s.creationTimeSeconds),
                        SubmissionLang = s.ProgrammingLanguage,
                        ProblemID = string.Format("{0}{1}", s.Problem.ContestId ,s.Problem.Index)
                    };

                    //SubmissionArr.Add(sub);
                    DBController db = new DBController();
                    db.InsertSubmission(sub);
                    if (!isLastSubmission)
                    {
                        LastSubmission = sub.SubmissionID;
                        isLastSubmission = true;
                    }
                }
                );
            }
            else
            {
                Console.Write("Request to Submission Json Failed");
            }
            return LastSubmission;
        }
    }
}