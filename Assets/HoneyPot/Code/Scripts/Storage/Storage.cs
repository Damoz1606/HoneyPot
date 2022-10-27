using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Storage
{
    private string folderName = "Resources";

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

    private string AssetFolder()
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_STANDALONE_WIN
        // return Path.Combine(Application.dataPath, this.folderName);
        return Application.dataPath;
#endif
#if UNITY_ANDROID || PLATFORM_ANDROID
        // return Path.Combine(Application.persistentDataPath, this.folderName);
        return Application.persistentDataPath;
#endif
    }

    private void CheckFolder()
    {
        if (!Directory.Exists(AssetFolder()))
            Directory.CreateDirectory(AssetFolder());
    }

    public void Store<T>(T forStore, string filename)
    {
        string path = Path.Combine(AssetFolder(), filename);
        List<T> data = new List<T>();
        this.CheckFolder();
        data.Add(forStore);
        string json = JsonHelper.ToJSON(data.ToArray(), true);
        System.IO.File.WriteAllText(path, json);
    }

    public async Task StoreAsync<T>(T forStore, string filename)
    {
        string path = Path.Combine(AssetFolder(), filename);
        Debug.Log(path);
        List<T> data = new List<T>();
        this.CheckFolder();
        data.Add(forStore);
        string json = JsonHelper.ToJSON(data.ToArray(), true);
        await System.IO.File.WriteAllTextAsync(path, json);
    }

    public List<T> Read<T>(string filename)
    {
        string path = Path.Combine(AssetFolder(), filename);
        if (!File.Exists(path))
        {
            T sample = default;
            this.Store<T>(sample, filename);
        }
        string json = string.Empty;
        try
        {
            json = System.IO.File.ReadAllText(path);
        }
        catch (System.Exception)
        {
            T sample = default;
            this.Store<T>(sample, filename);
            json = System.IO.File.ReadAllText(path);
        }
        T[] data = JsonHelper.FromJSON<T>(json);
        return new List<T>(data);
    }

    public async Task<List<T>> ReadAsync<T>(string filename)
    {
        string path = Path.Combine(AssetFolder(), filename);
        if (!File.Exists(path))
        {
            T sample = default;
            this.Store<T>(sample, filename);
        }
        Debug.Log(path);
        Task<string> json = default;
        try
        {
            json = System.IO.File.ReadAllTextAsync(path);
        }
        catch (System.Exception)
        {
            T sample = default;
            await this.StoreAsync<T>(sample, filename);
            json = System.IO.File.ReadAllTextAsync(path);
        }
        T[] data = JsonHelper.FromJSON<T>(json.Result);
        return new List<T>(data);
    }
}