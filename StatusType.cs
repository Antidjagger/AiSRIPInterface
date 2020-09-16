using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSRIPInterface
{
    class StatusType
    {
        [Key]
        public int ID { get; set; }
        public string Status { get; set; }
        //public ICollection<Company> Companies { get; set; }
        //public StatusType()
        //{
        //    Companies = new List<Company>();
        //}
    }
}
