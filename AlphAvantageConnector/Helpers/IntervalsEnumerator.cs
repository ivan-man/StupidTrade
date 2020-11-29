using AlphaVantageConnector.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlphaVantageConnector.Helpers
{
    /// <summary>
    /// Enumerator for Intervals enumeration 
    /// </summary>
    internal class IntervalsEnumerator : IEnumerator<string>
    {
        private static readonly Enums.Intervals[] _enumValues = Enum.GetValues(typeof(Enums.Intervals)) as Enums.Intervals[];

        /// <summary>
        /// initial value is 1, because 0 - is invalid, not initiated value
        /// </summary>
        private Enums.Intervals _currentEnumValue = (Enums.Intervals) 1;

        private readonly Intervals _intervalsEntity;

        public IntervalsEnumerator(Intervals intervalsEntity)
        {
            _intervalsEntity = intervalsEntity;
        }

        public string Current => _intervalsEntity[_currentEnumValue];

        object IEnumerator.Current => _intervalsEntity[_currentEnumValue];

        public void Dispose()
        {
            _currentEnumValue = (Enums.Intervals) 1;
        }

        public bool MoveNext()
        {
            _currentEnumValue++;

            return (byte) _currentEnumValue < _enumValues.Length;
        }

        public void Reset()
        {
            _currentEnumValue = (Enums.Intervals) 1;
        }
    }
}
