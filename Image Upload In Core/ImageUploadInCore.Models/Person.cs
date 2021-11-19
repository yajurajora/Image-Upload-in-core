using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploadInCore.Models
{
    public class Person
    {
        public string PID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Photo { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }



        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public int IncomePerMonth { get; set; }

    }
}
