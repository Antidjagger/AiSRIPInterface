using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSRIPInterface
{
    class StatusTypes
    {
        [Key]
        public int StatusID { get; set; }
        public string Status { get; set; }
        public ICollection<Companies> Companies { get; set; }
        public StatusTypes()
        {
            Companies = new List<Companies>();
        }
    }
}
