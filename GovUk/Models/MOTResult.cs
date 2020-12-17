using System;
namespace MOTDetails.GovUk
{
    public class MOTResult
    {
        public DateTime completedDate { get; set; }
        public DateTime? expiryDate { get; set; }
        public string testResult { get; set; }
        public int odometerValue { get; set; }
        public string odometerUnit { get; set; }

        public MOTResult()
        {
        }
    }
}
