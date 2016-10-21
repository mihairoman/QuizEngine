using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizEngine.Model.Data
{
    [Serializable]
    public class KeyValuePairInfo<TKey, TValue>
    {
        private TKey _key;
        private TValue _value;

        public KeyValuePairInfo()
        {

        }

        public KeyValuePairInfo(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public TKey Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public TValue Value
        {
            get { return _value; }
            set { _value = value; }

        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            KeyValuePairInfo<TKey, TValue> toCompare = obj as KeyValuePairInfo<TKey, TValue>;

            if (toCompare == null)
                return false;

            return this.Key.Equals(toCompare.Key) && this.Value.Equals(toCompare.Value);
        }

        public override int GetHashCode()
        {
            return this._key.GetHashCode() ^ this._key.GetHashCode();
        }
    }
}
