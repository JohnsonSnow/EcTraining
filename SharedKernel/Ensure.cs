using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public static class Ensure
    {
        public static void NotNullOrEmpty([NotNull] string? value, [CallerArgumentExpression("value")] string? paramName = default) 
        { 
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(paramName);
        }
    }
}
