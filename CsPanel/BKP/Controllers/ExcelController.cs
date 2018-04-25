using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsPanel.Models;
using LinqToExcel;

namespace CsPanel.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult ExcelUpload()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadExcel()
        {
            return View("ExcelUpload");
        }


        [HttpPost]
        public ActionResult UploadExcel(ExcelData excelData, HttpPostedFileBase FileUpload)
        {
            string data = "";
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    if (filename.EndsWith(".xlsx"))
                    {
                        string targetpath = Server.MapPath("~/DetailFormatInExcel/");
                        FileUpload.SaveAs(targetpath + filename);
                        string pathToExcelFile = targetpath + filename;

                        string sheetName = "CS Summary Report_Maksuda";

                        var excelFile = new ExcelQueryFactory(pathToExcelFile);
                        var worksheetNames = excelFile.GetWorksheetNames();

                        foreach (var VARIABLE in worksheetNames)
                        {
                          //  var empDetails = from a in excelFile.Worksheet<ExcelData>(sheetName) select a;
                            var indianaCompanies = from c in excelFile.Worksheet(VARIABLE.ToString())

                                select c;
                            foreach (var a in indianaCompanies)
                            {

                                if (a.ColumnNames != null)
                                {
                                    if (a[1] != "" || a[1] != "Service Name")
                                    {
                                        new CDA().ExecuteNonQuery("exec sp_InserIntoCsLogTable '" + VARIABLE.Replace("CS Summary Report_","") + "','" + a[1] + "','"
                                                                  + a[2] + "','" + a[3] + "','" + a[4] + "','" + a[5] + "','" + a[5] + "','" + a[7]
                                                                  + "'", "WAPDB");
                                    }

                                }

                                else
                                {
                                    // data = a.ServiceName + "Some fields are null, Please check your excel sheet";
                                    ViewBag.Message = data;
                                    //return View("ExcelUpload");
                                }

                            }
                        }
                       
                    }

                    else
                    {
                        data = "This file is not valid format";
                        ViewBag.Message = data;
                    }
                    return View("ExcelUpload");

                }

            }
            return View("ExcelUpload");
        }

        public int PostExcelData(int employeeNo, string firstName, string lastName, DateTime? dateOfBirth, string address, string mobileNo, string postelCode, string emailId)
        {

            var InsertExcelData = "";
            return 1;
        }
    }
}