using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOService.Model
{
    internal class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ExternallId { get; set; }
        public string RegAdress { get; set; }
        public List<Card> Cards { get; set; }
    }
}
