using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace greenstar_api
{
    public static class school_param
    {
        [FunctionName("SchoolsList")]
        public static async Task<IActionResult> SchoolsList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "schools")] HttpRequest req,
            ILogger log) {
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                
                DataSet ds = new DataSet();
                List<SchoolNamesList> schoolNamesList = new List<SchoolNamesList>();

                using(SqlConnection conn = new SqlConnection(dbConnect)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select school_id, school_name, address, city, country from dbo.greenstar_school", conn);
                    using(SqlDataReader sdr = await cmd.ExecuteReaderAsync()) {
                        ds.Tables.Add().Load(sdr);
                    }
                }

                foreach(DataRow row in ds.Tables[0].Rows) {
                    schoolNamesList.Add(new SchoolNamesList() { 
                        id = row[0].ToString(), 
                        schoolName = row[1].ToString(),
                        address = row[2].ToString(),
                        city = row[3].ToString(),
                        country = row[4].ToString()
                    });
                }

                return (ActionResult)new OkObjectResult(schoolNamesList);
        }

        [FunctionName("StandardsListForAGivenSchool")]
        public static async Task<IActionResult> StandardsListForAGivenSchool(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "standards/{id}")] HttpRequest req,
            ILogger log) {
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                return (ActionResult)new OkObjectResult("hello func");
        }

        [FunctionName("SectionsListForAGivenSchoolAndStandard")]
        public static async Task<IActionResult> SectionsListForAGivenSchoolAndStandard(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sections/{id}")] HttpRequest req,
            ILogger log) {
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                return (ActionResult)new OkObjectResult("hello func");
        }

        [FunctionName("GroupsListForAGivenSchoolAndStandardAndSection")]
        public static async Task<IActionResult> GroupsListForAGivenSchoolAndStandardAndSection(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{id}")] HttpRequest req,
            ILogger log) {
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                return (ActionResult)new OkObjectResult("hello func");
        }

        [FunctionName("StudentListForAGivenSchoolAndStandardAndSection")]
        public static async Task<IActionResult> StudentListForAGivenSchoolAndStandardAndSection(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "students")] HttpRequest req,
            ILogger log) {
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                return (ActionResult)new OkObjectResult("hello func");
        }

        [FunctionName("ParameterListForAGivenSchoolAndStandardAndSectionAndStudentName")]
        public static async Task<IActionResult> ParameterListForAGivenSchoolAndStandardAndSectionAndStudentName(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "parameters")] HttpRequest req,
            ILogger log) {
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                return (ActionResult)new OkObjectResult("hello func");
        }
    }
}
