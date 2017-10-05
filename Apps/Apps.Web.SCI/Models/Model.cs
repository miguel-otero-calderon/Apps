using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Web.SCI.Models
{
    public class Model
    {
        public string date { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public HttpPostedFileBase file2 { get; set; }
    }
}