using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Apps.Web.Models
{
    public class CompanyModel
    {
        [Required]
        [Display(Name ="Código de Empresa ")]
        [StringLength(maximumLength:2)]
        public string CodeCompany { get; set; }

        [Required]
        [Display(Name = "Código de Corporación ")]
        [StringLength(maximumLength: 2)]
        public string CodeCorporation { get; set; }

        [Required]
        [Display(Name = "Razón Social ")]
        [StringLength(maximumLength: 100)]
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Ruc { get; set; }
        public string Address { get; set; }
        public string PageWeb { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public short State { get; set; }       
    }
}