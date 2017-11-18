using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Apps.Entity;

namespace Apps.Web.Models
{
    public class UserModel
    {
        public UserModel()
        {
            CompaniesModel = new List<CompanyModel>();
        }
        [Required]
        [Display(Name ="Usuario ")]
        [StringLength(maximumLength:10,MinimumLength =5)]
        public string CodeUser { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        [StringLength(maximumLength: 10, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Nombre ")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Email { get; set; }

        [Display(Name = "Estado ")]
        public short State { get; set; }
        public string StateActive
        {
            get
            {
                if (State == 1)
                    return "Activo";

                if (State == 0)
                    return "Inactivo";

                return "!Estado desconocido!";
            }
        }
        public bool StateCheck
        {
            get
            {
                return (State == 1);
            }
            set
            {
                if (value)
                    State = 1;
                else
                    State = 0;
            }
        }
        public List<CompanyModel> CompaniesModel { get; set; }
        public string CompaniesSplit { get; set; }
    }
}