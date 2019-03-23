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
        private static readonly IntervalsEnum[] _enumValues = Enum.GetValues(typeof(IntervalsEnum)) as IntervalsEnum[];

        /// <summary>
        /// initial value is 1, because 0 - is invalid, not initiated value
        /// </summary>
        private IntervalsEnum _currentEnumValue = (IntervalsEnum) 1;

        private readonly Intervals _intervalsEntity;

        public IntervalsEnumerator(Intervals intervalsEntity)
        {
            _intervalsEntity = intervalsEntity;
        }

        public string Current => _intervalsEntity[_currentEnumValue];

        object IEnumerator.Current => _intervalsEntity[_currentEnumValue];

        public void Dispose()
        {
            _currentEnumValue = (IntervalsEnum) 1;
        }

        public bool MoveNext()
        {
            _currentEnumValue++;

            return (byte) _currentEnumValue < _enumValues.Length;
        }

        public void Reset()
        {
            _currentEnumValue = (IntervalsEnum) 1;
        }
    }
}
