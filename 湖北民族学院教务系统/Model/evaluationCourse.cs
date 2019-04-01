using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 湖北民族学院教务系统.Model
{
    public class evaluationCourse
    {
        public string id { get; set; }
        public string teacherId { get; set; }
        public string studentId { get; set; }
        public string tcId { get; set; }
        public string evaluationCourseType { get; set; }
        public string evaluatorType { get; set; }
    }
}
