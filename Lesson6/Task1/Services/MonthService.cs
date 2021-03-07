using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Task1.Services
{
    public class MonthService : IReadOnlyCollection<string>
    {
        private string[] _months;

        public MonthService()
        {
            _months = DateTimeFormatInfo.CurrentInfo.MonthNames;
        }

        public int Count => _months.Count();

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var m in _months)
            {
                yield return m;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _months.GetEnumerator();
        }
    }
}
