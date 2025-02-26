using System.Collections.Generic;
using TMPro;

namespace AD.Services.Router
{
    public class OptionDataList<T>
    {
        private readonly List<T> values;
        private readonly List<TMP_Dropdown.OptionData> options;

        public T this[int index] => values[index];

        public List<TMP_Dropdown.OptionData> Options => options;

        public OptionDataList(IEnumerable<T> values, IEnumerable<TMP_Dropdown.OptionData> options)
        {
            this.values = new(values);
            this.options = new(options);
        }

        public int IndexOf(T value)
        {
            return values.IndexOf(value);
        }

        public string GetOptions(int index)
        {
            return options[index].text;
        }
    }
}