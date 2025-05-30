using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.Shared
{
    public class DeviceReadDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Make { get; set; }
        public string? SerialNo { get; set; }
    }
}
