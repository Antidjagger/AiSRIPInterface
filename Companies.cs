using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AiSRIPInterface
{
    class Companies
    {
        [Key]
        public int CompaniesID { get; set; }
        public string Name { get; set; }
        public int? StatusID { get; set; }
        public StatusTypes StatusTypes { get; set; }
        public ICollection<Users> Users { get; set; }
        public Companies()
        {
            Users = new List<Users>();
            //UsersCount = Users.Count;

        }
    }
}
