using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace ImageUploadInCore.Model
{
    public class Persons
    {
        public string PID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayName("Image Name")]
        public string Photo { get; set; }

        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public int IncomePerMonth { get; set; }

        [DisplayName("Upload File")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
