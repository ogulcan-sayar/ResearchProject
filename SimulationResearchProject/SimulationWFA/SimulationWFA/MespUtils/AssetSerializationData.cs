using System;
using System.Collections.Generic;
using System.Numerics;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using SimulationWFA.DataManagement;
using static RenderLibrary.Graphics.Rendering.Texture;
using static RenderLibrary.Shaders.ShaderPool;

namespace SimulationWFA.MespUtils
{
    [Serializable]
    public class AssetSerializationData
    {
        public Guid uniqueFileID = Guid.Empty;

        public StringStringSerializedDic stringStringSerializedDic;
        public StringIntSerializedDic stringIntSerializedDic;
        public StringFloatSerializedDic stringFloatSerializedDic;
        public StringBoolSerializedDic stringBoolSerializedDic;
        public StringLongSerializedDic stringLongSerializedDic;
        
        public StringStringListSerializedDic stringStringListSerializedDic;
        public StringIntListSerializedDic stringIntListSerializedDic;
        public StringFloatListSerializedDic stringFloatListSerializedDic;
        public StringLongListSerializedDic stringLongListSerializedDic;
        public StringBoolListSerializedDic stringBoolListSerializedDic;
        
        public StringToStringStringSerializedDic stringToStringStringSerializedDic;
        public StringToStringIntSerializedDic stringToStringIntSerializedDic;
        public StringToStringFloatSerializedDic stringToStringFloatSerializedDic;
        public StringToStringBoolSerializedDic stringToStringBoolSerializedDic;

        public AssetSerializationData()
        {
            stringStringSerializedDic = new StringStringSerializedDic();
            stringIntSerializedDic = new StringIntSerializedDic();
            stringFloatSerializedDic = new StringFloatSerializedDic();
            stringBoolSerializedDic = new StringBoolSerializedDic();
            stringLongSerializedDic = new StringLongSerializedDic();

            stringStringListSerializedDic = new StringStringListSerializedDic();
            stringIntListSerializedDic = new StringIntListSerializedDic();
            stringFloatListSerializedDic = new StringFloatListSerializedDic();
            stringLongListSerializedDic = new StringLongListSerializedDic();
            stringBoolListSerializedDic = new StringBoolListSerializedDic();

            stringToStringStringSerializedDic = new StringToStringStringSerializedDic();
            stringToStringIntSerializedDic = new StringToStringIntSerializedDic();
            stringToStringFloatSerializedDic = new StringToStringFloatSerializedDic();
            stringToStringBoolSerializedDic = new StringToStringBoolSerializedDic();

            uniqueFileID = Guid.NewGuid();

        }


        static V Get<V>(IDictionary<string, V> dic, string key, V def) where V : struct
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }

            dic[key] = def;
            return def;
        }

        static V Get<V>(IDictionary<string, V> dic, string key) where V : class, new()
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }

            var t = new V();
            dic[key] = t;
            return t;
        }


        public void SetInt(string key, int val) => stringIntSerializedDic[key] = val;
        public void SetBool(string key, bool val) => stringBoolSerializedDic[key] = val;
        public void SetLong(string key, long val) => stringLongSerializedDic[key] = val;
        public void SetString(string key, string val) => stringStringSerializedDic[key] = val;
        public void SetFloat(string key, float val) => stringFloatSerializedDic[key] = val;

        public int GetInt(string key, int val) => Get(stringIntSerializedDic, key, val);
        public bool GetBool(string key, bool val) => Get(stringBoolSerializedDic, key, val);
        public long GetLong(string key, long val) => Get(stringLongSerializedDic, key, val);
        public float GetFloat(string key, float val) => Get(stringFloatSerializedDic, key, val);
        public string GetString(string key, string val)
        {
            if (stringStringSerializedDic.ContainsKey(key))
            {
                return stringStringSerializedDic[key];
            }

            stringStringSerializedDic[key] = val;
            return val;
        }

        public List<string> GetStringList(string key) => Get(stringStringListSerializedDic, key);
        public List<int> GetIntList(string key) => Get(stringIntListSerializedDic, key);
        public List<float> GetFloatList(string key) => Get(stringFloatListSerializedDic, key);
        public List<long> GetLongList(string key) => Get(stringLongListSerializedDic, key);
        public List<bool> GetBoolList(string key) => Get(stringBoolListSerializedDic, key);


        public Dictionary<string, string> DicStringString(string key) => Get(stringToStringStringSerializedDic, key);
        public Dictionary<string, int> DicStringInt(string key) => Get(stringToStringIntSerializedDic, key);
        public Dictionary<string, float> DicStringFloat(string key) => Get(stringToStringFloatSerializedDic, key);
        public Dictionary<string, bool> DicStringBool(string key) => Get(stringToStringBoolSerializedDic, key);
    }

    public interface IAssetSerializator
    {
        object Serializate();
        object Deserializate(AssetSerializationData data);
    }
}
