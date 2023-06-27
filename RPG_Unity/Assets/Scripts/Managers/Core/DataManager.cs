using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public interface ILoader<Key, Item>
{
    Dictionary<Key, Item> MakeDic();
    bool Validate();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public Dictionary<int, TextData> Texts { get; private set; }


    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDic();
        Texts = LoadXml<TextDataLoader, int, TextData>("TextData").MakeDic();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    private Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Loader)xs.Deserialize(stream);
    }
}

