using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KM_Management_Api.Models
{
    public class GetTokenRequest
    {
        public string EmpId { get; set; }
        public string Password { get; set; }
    }
}
