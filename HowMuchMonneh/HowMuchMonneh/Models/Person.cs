using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HowMuchMonneh.Models
{
    public class Person
    {
        public string Gender { get; set; }
        public int Age { get; set; }
        public float Income { get; set; }

        public override string ToString()
        {
            return Gender + "," + Age + "," + Income;
        }
    }
}