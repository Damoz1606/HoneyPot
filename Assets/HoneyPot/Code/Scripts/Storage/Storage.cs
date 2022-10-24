using System.Collections.Generic;
using UnityEngine;

public class Storage
{

    public static string ROOT = Application.dataPath + System.IO.Path.DirectorySeparatorChar + "Resources" + System.IO.Path.DirectorySeparatorChar;

    private static Storage _instance;
    // public static Storage Instance => _instance;
    public static Storage Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Storage();
            }
            return _instance;
        }
    }
    /* private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    } */

    public void Store<T>(T forStore, string path)
    {
        List<T> data = new List<T>();
        data.Add(forStore);
        string json = JsonHelper.ToJSON(data.ToArray(), true);
        System.IO.File.WriteAllText(path, json);
    }

    public List<T> Read<T>(string path)
    {
        string json = string.Empty;
        try
        {
            json = System.IO.File.ReadAllText(path);
        }
        catch (System.Exception)
        {
            T sample = default;
            this.Store<T>(sample, path);
            json = System.IO.File.ReadAllText(path);
        }
        T[] data = JsonHelper.FromJSON<T>(json);
        return new List<T>(data);
    }
}