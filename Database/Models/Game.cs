using Database.Models.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Game : Model
    {
        [Display(Name = "Nome do Jogo")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedStrings))]
        [StringLength(64, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedStrings))]
        public string Name { get; set; }

        [Display(Name = "Disponível?")]
        public bool Available { get; set; }

        [Display(Name = "Id do Amigo")]
        public int? FriendId { get; set; }

        [Display(Name = "Amigo")]
        public virtual Friend Friend { get; set; }
        
        [Display(Name = "Data do Empréstimo")]
        public DateTime? LendingDate { get; set; }

        [Display(Name = "Prazo de Entrega")]
        public DateTime? Deadline { get; set; }
    }
}
