using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AiSRIPInterface
{
    class Company
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        //public int UsersCount { get; set; }
        public string CStatus { get; set; }
        public ICollection<Users> Users { get; set; }
        public Company()
        {
            Users = new List<Users>();
            //UsersCount = Users.Count;

        }
    }
}
