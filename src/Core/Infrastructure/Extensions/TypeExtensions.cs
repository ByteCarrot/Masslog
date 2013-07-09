using System;
using System.Linq;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static bool Implements<TInterface>(this Type t)
        {
            var @interface = typeof (TInterface);
            return t.GetInterfaces().Any(x => x == @interface);
        }
    }
}
