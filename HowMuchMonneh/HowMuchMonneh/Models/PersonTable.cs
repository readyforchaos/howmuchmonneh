using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowMuchMonneh.Models
{
    public class PersonTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
}