using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSender.Models
{
    public class Camtar
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Boolean IsArrived { get; set; }  

        public Boolean  IsLeft { get; set; }
    }
}
