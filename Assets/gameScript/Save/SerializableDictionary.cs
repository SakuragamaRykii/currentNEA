using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SerializableDictionary <Key, Value> : Dictionary<Key, Value>, ISerializationCallbackReceiver
{

    [SerializeField] private List<Key> keys = new List<Key>();
    [SerializeField] private List <Value> values = new List<Value>();
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<Key, Value> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        Clear();

        if(keys.Count != values.Count)
        {
            Debug.LogError("Becuase the amount of keys = " + keys.Count + " and the amount of values = " 
                + values.Count + ", there is a problem deserializing");
        }
        for(int i = 0; i < keys.Count; i++) 
        {
            Add(keys[i], values[i]);
        }
    }


}
