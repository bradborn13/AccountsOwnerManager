using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Owner Id is required")]

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
