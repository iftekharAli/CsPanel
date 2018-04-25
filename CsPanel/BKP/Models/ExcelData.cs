using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CsPanel.Models
{
    public class ExcelData
    {
        public string AgentName { get; set; }

        public string ServiceName { get; set; }
        public string GrameenPhone { get; set; }
        public string Banglalink { get; set; }
        public string Robi { get; set; }
        public string Airtel { get; set; }
        public string Teletalk { get; set; }
        public string Total { get; set; }

        public DateTime TimeStamp { get; set; }
        //public string Operator { get; set; }
        //public string ClubZ { get; set; }
        //public string Buddy { get; set; }
        //public string bdtube { get; set; }
        //public string DarunShow { get; set; }
        //public string QuizPlay { get; set; }
        //public string FitnessClub { get; set; }
    }

    public class SummaryData
    {
      
        public string ServiceName { get; set; }
        public string rawshon { get; set; }
        public string Romana { get; set; }
        public string Total { get; set; }

    }

    public class ReportData
    {

        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AgentName { get; set; }
    }

    public class AgentDropdown
    {
        public string AgentName { get; set; }
    }

    public class RawInfo
    {
        public List<ExcelData> ExcelDatas { get; set; }
        public ReportData ReportData { get; set; }
        
    }

    public class SummaryInfo
    {
        public List<SummaryData> SummaryDatas { get; set; }
        public ReportData ReportData { get; set; }

    }
    public class Login
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}