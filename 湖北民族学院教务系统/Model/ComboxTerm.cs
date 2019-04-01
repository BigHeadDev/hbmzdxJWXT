using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 湖北民族学院教务系统.Model
{
    public class ComboxTerm
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public ComboxTerm(string text,int value)
        {
            this.Text = text;
            this.Value = value;
        }
    }
}
