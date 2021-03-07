using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Task1.Services
{
    public class WeekService : IReadOnlyCollection<string>
    {
        private string[] _weeks;

        public WeekService()
        {
            _weeks = DateTimeFormatInfo.CurrentInfo.DayNames;
        }

        public int Count => _weeks.Count();

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var w in _weeks)
            {
                yield return w;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _weeks.GetEnumerator();
        }
    }
}
