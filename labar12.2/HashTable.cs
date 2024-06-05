using library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace labar12._2
{
    public class Item<TKey, TValue> where TKey : ICloneable where TValue : ICloneable
    {
        public TKey? Key { get; set; }
        public TValue? Value { get; set; }
        public Item(TKey key, TValue value)
        {
            this.Key = (TKey)key.Clone();
            this.Value = (TValue)value.Clone();
        }
        public override int GetHashCode()
        {
            return Key == null ? 0 : Key.GetHashCode();
        }
    }
}