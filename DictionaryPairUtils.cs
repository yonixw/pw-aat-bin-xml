using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTextData_bin_cho_mh
{
    public static class DictionaryPairUtils
    {
        public static List<Pair<K,V>> ToPairs<K,V>(Dictionary<K,V> dict)
        {
            List<Pair<K, V>> result = new List<Pair<K, V>>();
            foreach(KeyValuePair<K,V> kv in dict)
            {
                result.Add(new Pair<K, V>() { key = kv.Key, value = kv.Value });
            }
            return result;
        }

        public static Dictionary<K, V> FromPairs<K, V>(List<Pair<K, V>> list)
        {
            Dictionary<K, V> result = new Dictionary<K, V>();
            
            if (list == null || list.Count == 0) return result;

            foreach (Pair<K, V> kv in list)
            {
                if (!result.ContainsKey(kv.key))
                    result.Add(kv.key, kv.value);
            }

            return result;
        }

        public static Dictionary<V,K> ReverseDict<K, V>(Dictionary<K, V> dict)
        {
            Dictionary<V, K> result = new Dictionary<V, K>();

            foreach (KeyValuePair<K, V> kv in dict)
            {
                if (!result.ContainsKey(kv.Value))
                {
                    result.Add(kv.Value, kv.Key);
                }
            }

            return result;
        }

    }

    public class Pair<K,V>
    {
        public K key;
        public V value;
    }
}



