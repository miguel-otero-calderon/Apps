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
        protected EUser eUser;
        public UserModel()
        {
            eUser = new EUser();
        }

        [Required]
        [Display(Name ="Usuario ")]
        [StringLength(maximumLength:10,MinimumLength =6)]
        public string CodeUser { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        [StringLength(maximumLength: 10, MinimumLength = 6)]

        public string Password { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public short State { get; set; }

        public EUser GetUser()
        {
            eUser = new EUser();
            eUser.CodeUser = this.CodeUser;
            eUser.Name = this.Name;
            eUser.Password = this.Password;
            eUser.Email = this.Email;
            eUser.State = this.State;
            return eUser;
        }

        public void SetUser(EUser eUser)
        {
            if (this.eUser == null)
                this.eUser = new EUser();
            this.eUser = eUser;
        }
    }
}