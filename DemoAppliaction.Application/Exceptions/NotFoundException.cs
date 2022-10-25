using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAppliaction.Core.Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException() : base("Element not found")
        {

        }
    }
}
