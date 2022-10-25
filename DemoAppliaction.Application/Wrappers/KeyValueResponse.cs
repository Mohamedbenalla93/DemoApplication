using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAppliaction.Core.Application.Wrappers;

public class KeyValueResponse<T> : Response<T>
{
    public int Count { get; set; }

    public KeyValueResponse()
    {
            
    }
    public KeyValueResponse(T data, string message = null) : base(data, message)
    {
            
    }

    public KeyValueResponse(string message) : base(message)
    {
            
    }
}