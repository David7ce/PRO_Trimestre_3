using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaMercancias;

public class MercanciasException : Exception
{
    public MercanciasException() { }
    public MercanciasException(string message) : base(message) { }
}
