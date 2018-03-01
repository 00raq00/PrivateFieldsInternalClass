using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalClasses
{
  class InternalCallReadonlyVsConst
  {
    private readonly int privateReadonlyInt = 100;
    private readonly string privateReadonlyString = "100";

    private static readonly int privateStaticReadonlyInt = 100;
    private static readonly string privateStaticReadonlyString = "100";

    private const int privateConstInt = 100;
    private const string privateConstString = "100";
  }
}
