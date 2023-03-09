using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IOService.Model
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public string ShortName { get; set; }
        
        public string Inn { get; set; }
    }
}
