using Database.Models.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Friend : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedStrings))]
        [StringLength(64, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedStrings))]
        public string Name { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedStrings))]
        [StringLength(11, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedStrings))]
        public string Phone { get; set; }

        [NotMapped]
        public string PhoneNumberStrings
        {
            get
            {
                try
                {
                    return string.Format("({0}) {1}-{2}", Phone.Substring(0, 2), Phone.Substring(2, 5), Phone.Substring(7, 4));
                }
                catch (Exception)
                {
                    return Phone;
                }
            }
        }
    }
}
