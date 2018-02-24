using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalClasses
{
    internal class InternalClassPublicConstructorPrivateField
  {
    public InternalClassPublicConstructorPrivateField()
    {
      privateInt = 666;
    }
    private int privateInt ;
  }
}
