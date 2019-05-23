using System.Collections.Generic;

namespace greenstar_api {
    public class SchoolNamesList {
        public string id {get; set;}
        public string schoolName {get; set;}
        public string address {get; set;}
        public string city {get; set;}
        public string country {get; set;}
    }

    public class Standard {
        public string schoolId {get; set;}
        public List<string> standardList = new List<string>();
    }

    public class SectionListForSchoolAndStandard {
        public string schoolId {get; set;}
        public string standard {get; set;}
        public List<string> sectionList = new List<string>();
    }

    public class GroupListForSchoolAndStandardAndGroup {
        public string schoolId {get; set;}
        public string standard {get; set;}
        public string section {get; set;}
        public List<string> IGroupList = new List<string>();
    }

    public class StudentListForSchoolAndStandardAndSectionAndGroup {
        public string schoolId {get; set;}
        public string standard {get; set;}
        public string section {get; set;}
        public string group {get; set;}
        public List<string> studentId = new List<string>();
        public List<string> studentList = new List<string>();
    }

    public class ParameterNameParameterValue {
        public string studentId {get; set;}
        public List<string> parameterName = new List<string>();
        public List<string> parameterValue = new List<string>();
    }
}