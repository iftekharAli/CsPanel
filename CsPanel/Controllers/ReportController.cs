using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsPanel.Models;

namespace CsPanel.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            if (Session["Uid"] == null)
            {
               // return RedirectToAction("Index", "Login");
            }
            DataSet ds = new DataSet();

            var empList = new List<ExcelData>();
            // ViewData["AgentName"]= new CDA().
            var dxs = new  CDA().GetDataSet("SELECT DISTINCT agentname as AgentName FROM[Partner_Basket].[dbo].[tbl_CSRaw] where cast([TimeStamp] as DATE) >='2018-04-06'", "WAPDB");
            var AgentName = dxs.Tables[0].AsEnumerable().Select(dataRow => new AgentDropdown
            {
               
                AgentName = dataRow.Field<string>("AgentName")
               
            }).ToList<AgentDropdown>();
            ViewData["AgentName"] = AgentName;
            ds = new CDA().GetDataSet("exec sp_GetIndexCsReport '" + DateTime.Now + "','" + DateTime.Now + "','" + "All" + "'", "WAPDB");
            if (ds != null)
            {
                empList = ds.Tables[0].AsEnumerable().Select(dataRow => new ExcelData
                {
                    ServiceName = dataRow.Field<string>("ServiceName"),
                    GrameenPhone = dataRow.Field<string>("GrameenPhone"),
                    Airtel = dataRow.Field<string>("Airtel"),
                    Banglalink = dataRow.Field<string>("Banglalink"),
                    Robi = dataRow.Field<string>("Robi"),
                    Teletalk = dataRow.Field<string>("Teletalk"),
                    Total = dataRow.Field<string>("Total"),
                }).ToList<ExcelData>();
            }

            string isMd = String.Empty;
            try
            {
                isMd = Request.QueryString["D"].ToString();
            }
            catch
            {
                isMd = "";
            }

            var rr = new RawInfo();
            if (isMd == "1")
            {
                rr = new RawInfo()
                {
                    ExcelDatas = empList,
                    ReportData = new ReportData()
                };
            }
            else
            {
                if (Session["Uid"] == null)
                {
                     return RedirectToAction("Index", "Login");
                }
                rr = new RawInfo()
                {
                    ExcelDatas = new List<ExcelData>(),
                    ReportData = new ReportData()
                };
            }
           
          // rr.ReportData.FromDate= Convert.ToDateTime(DateTime.Now.ToString("YYYY-MM-DD"));
         //  rr.ReportData.EndDate=Convert.ToDateTime(DateTime.Now.ToString("YYYY-MM-DD"));
            return View(rr);
        }
        [HttpPost]
        public ActionResult Index(RawInfo info)
        {
            var empList = new List<ExcelData>();
            var dxs = new CDA().GetDataSet("SELECT DISTINCT agentname as AgentName FROM[Partner_Basket].[dbo].[tbl_CSRaw] where cast([TimeStamp] as DATE) >='2018-04-06'", "WAPDB");

            var agentName = dxs.Tables[0].AsEnumerable().Select(dataRow => new AgentDropdown
            {

                AgentName = dataRow.Field<string>("AgentName")

            }).ToList<AgentDropdown>();
            ViewData["AgentName"] = agentName;
            if (Session["Uid"] == null)
            {
              //  return RedirectToAction("Index", "Login");
            }
            DataSet ds = new DataSet();
            ds = new CDA().GetDataSet("exec sp_GetIndexCsReport '" + info.ReportData.FromDate+"','"+info.ReportData.EndDate+"','"+info.ReportData.AgentName+"'", "WAPDB");
            if (ds != null)
            {
                empList = ds.Tables[0].AsEnumerable().Select(dataRow => new ExcelData
                {
                    ServiceName = dataRow.Field<string>("ServiceName"),
                    GrameenPhone = dataRow.Field<string>("GrameenPhone"),
                    Airtel = dataRow.Field<string>("Airtel"),
                    Banglalink = dataRow.Field<string>("Banglalink"),
                    Robi = dataRow.Field<string>("Robi"),
                    Teletalk = dataRow.Field<string>("Teletalk"),
                    Total = dataRow.Field<string>("Total"),
                }).ToList<ExcelData>();
            }
            else
            {
                ModelState.AddModelError("", @"Invalid Date");
                return View(info);
            }
            
            var rr = new RawInfo()
            {
                ExcelDatas = empList,
                ReportData = new ReportData()
            };
           
            return View(rr);
        }

        public ActionResult SummaryInfo()
        {
            if (Session["Uid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var rr = new SummaryInfo()
            {
                SummaryDatas = new List<SummaryData>(),
                ReportData = new ReportData()
            };
            return View(rr);
        }
        
        [HttpPost]
        public ActionResult SummaryInfo(SummaryInfo summaryInfo)
        {
            if (Session["Uid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataSet ds = new DataSet();
            ds = new CDA().GetDataSet("exec sp_GetAgentWiseSummart '" + summaryInfo.ReportData.FromDate + "','" + summaryInfo.ReportData.EndDate + "'", "WAPDB");
            var summaryList = ds.Tables[0].AsEnumerable().Select(dataRow => new SummaryData()
            {
                ServiceName = dataRow.Field<string>("ServiceName"),
                rawshon = dataRow.Field<string>("rawshon"),
                Romana = dataRow.Field<string>("Romana"),
                Total = dataRow.Field<string>("Total"),
               
            }).ToList<SummaryData>();
            var summaryView = new SummaryInfo()
            {
                SummaryDatas = summaryList,
                ReportData = new ReportData()
            };
            return View(summaryView);
        }
    }
}