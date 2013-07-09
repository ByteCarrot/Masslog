using System;
using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.Rules
{
    public static class NumberOperationTypeExtensions
    {
        private static readonly Dictionary<string, NumberOperationType> _map =
            new Dictionary<string, NumberOperationType>
                {
                    {"=", NumberOperationType.Equals},
                    {"!=", NumberOperationType.NotEquals},
                    {">", NumberOperationType.GreaterThan},
                    {">=", NumberOperationType.GreaterThanOrEqual},
                    {"<", NumberOperationType.LessThan},
                    {"<=", NumberOperationType.LessThanOrEqual},
                };

        public static NumberOperationType ToNumberOperationType(this string s)
        {
            if (_map.ContainsKey(s))
            {
                return _map[s];
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}