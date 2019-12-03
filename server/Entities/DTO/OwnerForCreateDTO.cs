using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Entities.DTO
{
    public class OwnerForCreateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60,ErrorMessage ="Name can't be longer than 60 characters")]
        public String Name { get; set; }

        [Required(ErrorMessage ="Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage ="Addres is required")]
        [StringLength(100,ErrorMessage ="Address cannot be longer than 100 characters")]
        public String Address { get; set; }
    }
}
