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
            ILogger log,
            string Id) {
                
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                
                DataSet ds = new DataSet();
                var standardList = new Standard();

                using(SqlConnection conn = new SqlConnection(dbConnect)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select distinct standard from dbo.greenstar_students where school_id ='" + Id.ToString() + "'", conn);
                    using(SqlDataReader sdr = await cmd.ExecuteReaderAsync()) {
                        ds.Tables.Add().Load(sdr);
                    }
                    standardList.schoolId = Id.ToString();
                    foreach(DataRow row in ds.Tables[0].Rows) {
                        standardList.standardList.Add(row[0].ToString());
                    }
                }
                
                return (ActionResult)new OkObjectResult(standardList);
        }

        [FunctionName("SectionsListForAGivenSchoolAndStandard")]
        public static async Task<IActionResult> SectionsListForAGivenSchoolAndStandard(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sections")] HttpRequest req,
            ILogger log) {

                var schoolId = req.Query["school_id"];
                var standardName = req.Query["standard"];

                DataSet ds = new DataSet();
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                var sectionListForSchoolAndStandard = new SectionListForSchoolAndStandard();

                using(SqlConnection conn = new SqlConnection(dbConnect)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "select distinct section from dbo.greenstar_students where school_id = '" + schoolId.ToString() + "' and standard = '" + standardName.ToString() + "'", 
                        conn);
                    using(SqlDataReader sdr = await cmd.ExecuteReaderAsync()) {
                        ds.Tables.Add().Load(sdr);
                    }
                }

                sectionListForSchoolAndStandard.schoolId = schoolId;
                sectionListForSchoolAndStandard.standard = standardName;
                foreach(DataRow row in ds.Tables[0].Rows) {
                    sectionListForSchoolAndStandard.sectionList.Add(row[0].ToString());
                }

                return (ActionResult)new OkObjectResult(sectionListForSchoolAndStandard);
        }

        [FunctionName("GroupsListForAGivenSchoolAndStandardAndSection")]
        public static async Task<IActionResult> GroupsListForAGivenSchoolAndStandardAndSection(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups")] HttpRequest req,
            ILogger log) {
                var school_id = req.Query["school_id"];
                var standardName = req.Query["standard"];
                var sectionName = req.Query["section"];

                DataSet ds = new DataSet();
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                var groupsListForAGivenSchoolAndStandardAndSection = new GroupListForSchoolAndStandardAndGroup();

                using(SqlConnection conn = new SqlConnection(dbConnect)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "select distinct group_name from dbo.greenstar_students where school_id = '" + school_id.ToString() + "' and standard = '" + standardName.ToString() + "' and section = '" + sectionName.ToString() + "'", 
                        conn);
                    using(SqlDataReader sdr = await cmd.ExecuteReaderAsync()) {
                        ds.Tables.Add().Load(sdr);
                    }
                }
                
                groupsListForAGivenSchoolAndStandardAndSection.schoolId = school_id.ToString();
                groupsListForAGivenSchoolAndStandardAndSection.standard = standardName.ToString();
                groupsListForAGivenSchoolAndStandardAndSection.section = sectionName.ToString();
                foreach(DataRow row in ds.Tables[0].Rows) {
                    groupsListForAGivenSchoolAndStandardAndSection.IGroupList.Add(row[0].ToString());
                }

                return (ActionResult)new OkObjectResult(groupsListForAGivenSchoolAndStandardAndSection);
        }

        [FunctionName("StudentListForAGivenSchoolAndStandardAndSection")]
        public static async Task<IActionResult> StudentListForAGivenSchoolAndStandardAndSection(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "students")] HttpRequest req,
            ILogger log) {
                var school_id = req.Query["school_id"];
                var standardName = req.Query["standard"];
                var sectionName = req.Query["section"];
                var groupName = req.Query["group"];

                DataSet ds = new DataSet();
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                var studentListForAGivenSchoolAndStandardAndSection = new StudentListForSchoolAndStandardAndSectionAndGroup();

                using(SqlConnection conn = new SqlConnection(dbConnect)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "select distinct student_id,student_name from dbo.greenstar_students where school_id = '" + 
                        school_id.ToString() + "' and standard = '" + standardName.ToString() + "' and section = '" + 
                        sectionName.ToString() + "' and group_name = '" + groupName.ToString() + "'", 
                        conn);
                    using(SqlDataReader sdr = await cmd.ExecuteReaderAsync()) {
                        ds.Tables.Add().Load(sdr);
                    }
                }

                studentListForAGivenSchoolAndStandardAndSection.schoolId = school_id.ToString();
                studentListForAGivenSchoolAndStandardAndSection.standard = standardName.ToString();
                studentListForAGivenSchoolAndStandardAndSection.section = sectionName.ToString();
                studentListForAGivenSchoolAndStandardAndSection.group = groupName.ToString();
                foreach(DataRow row in ds.Tables[0].Rows) {
                    studentListForAGivenSchoolAndStandardAndSection.studentId.Add(row[0].ToString());
                    studentListForAGivenSchoolAndStandardAndSection.studentList.Add(row[1].ToString());
                }

                return (ActionResult)new OkObjectResult(studentListForAGivenSchoolAndStandardAndSection);
        }

        [FunctionName("ParameterListForAGivenSchoolAndStandardAndSectionAndStudentName")]
        public static async Task<IActionResult> ParameterListForAGivenSchoolAndStandardAndSectionAndStudentName(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "parameters")] HttpRequest req,
            ILogger log) {
                var student_id = req.Query["student_id"];

                DataSet ds = new DataSet();
                var dbConnect = Environment.GetEnvironmentVariable("AZURE_SQLSERVER_GREENSTARDB_CONNECTION");
                var parameterNameParameterValue = new ParameterNameParameterValue();

                using(SqlConnection conn = new SqlConnection(dbConnect)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "select paramter_name,parameter_value,Date from dbo.greenstar_parameters where student_id = '" + student_id.ToString() + "'", 
                        conn);
                    using(SqlDataReader sdr = await cmd.ExecuteReaderAsync()) {
                        ds.Tables.Add().Load(sdr);
                    }
                }

                parameterNameParameterValue.studentId = student_id.ToString();
                foreach(DataRow row in ds.Tables[0].Rows) {
                    parameterNameParameterValue.parameterName.Add(row[0].ToString());
                    parameterNameParameterValue.parameterValue.Add(row[1].ToString());
                }

                return (ActionResult)new OkObjectResult(parameterNameParameterValue);
        }
    }
}
