// CODE is taken from dalaklib

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationWFA.DataManagement
{
    [Serializable]
    public class SerializedDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
    {
        List<K> keys = new List<K>();
        List<V> values = new List<V>();

        public void OnAfterDeserialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in this)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnBeforeSerialize()
        {
            for (int i = 0; i < keys.Count; i++)
                Add(keys[i], values[i]);

            keys.Clear();
            values.Clear();
        }
    }


    [Serializable]
    public class SerializedList<T> : List<T>, ISerializationCallbackReceiver
    {
        List<T> values = new List<T>();

        public void OnBeforeSerialize()
        {
            values.Clear();

            foreach (var lv in this)
            {
                values.Add(lv);
            }
        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < values.Count; i++)
                Add(values[i]);

            values.Clear();
        }
    }


    [Serializable] public class StringStringSerializedDic : SerializedDictionary<string, string> { }
    [Serializable] public class StringIntSerializedDic : SerializedDictionary<string, int> { }
    [Serializable] public class StringFloatSerializedDic : SerializedDictionary<string, float> { }
    [Serializable] public class StringLongSerializedDic : SerializedDictionary<string, long> { }
    [Serializable] public class IntFloatSerializedDic : SerializedDictionary<int, float> { }
    [Serializable] public class StringBoolSerializedDic : SerializedDictionary<string, bool> { }
    [Serializable] public class IntBoolSerializedDic : SerializedDictionary<int, bool> { }

    [Serializable] public class StringStringListSerializedDic : SerializedDictionary<string, SerializedList<string>> { }
    [Serializable] public class StringIntListSerializedDic : SerializedDictionary<string, SerializedList<int>> { }
    [Serializable] public class StringFloatListSerializedDic : SerializedDictionary<string, SerializedList<float>> { }
    [Serializable] public class StringLongListSerializedDic : SerializedDictionary<string, SerializedList<long>> { }
    [Serializable] public class StringBoolListSerializedDic : SerializedDictionary<string, SerializedList<bool>> { }

    [Serializable] public class StringToStringStringSerializedDic : SerializedDictionary<string, StringStringSerializedDic> { }
    [Serializable] public class StringToStringIntSerializedDic : SerializedDictionary<string, StringIntSerializedDic> { }
    [Serializable] public class StringToStringFloatSerializedDic : SerializedDictionary<string, StringFloatSerializedDic> { }
    [Serializable] public class StringToIntFloatSerializedDic : SerializedDictionary<string, IntFloatSerializedDic> { }
    [Serializable] public class StringToStringBoolSerializedDic : SerializedDictionary<string, StringBoolSerializedDic> { }
    [Serializable] public class StringToIntBoolSerializedDic : SerializedDictionary<string, IntBoolSerializedDic> { }

}
