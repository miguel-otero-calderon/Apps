using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Apps.Web.Models
{
    public class UserModel
    {        
        [Required]
        [Display(Name ="Usuario ")]
        [StringLength(maximumLength:10,MinimumLength =6)]
        public string CodeUser { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        [StringLength(maximumLength: 10, MinimumLength = 6)]

        public string Password { get; set; }

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
                else
                    return "Inactivo";
            }
        }
    }
}