using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object source)
        {
            return (source == null);
        }

        public static bool IsNotNull(this object source)
        {
            return (source != null);
        }
    }
}
