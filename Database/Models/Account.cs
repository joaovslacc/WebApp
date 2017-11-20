using Database.Models.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Account : Model
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedStrings))]
        [StringLength(256, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedStrings))]
        public string Username { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedStrings))]
        [StringLength(1024, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedStrings))]
        [DataType(DataType.Password, ErrorMessageResourceName = "DataType", ErrorMessageResourceType = typeof(SharedStrings))]
        public string Password { get; set; }
    }
}
