using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    private Dictionary<string, int> _tilesDict = new Dictionary<string, int>();

    public void UpdateTiles(string key)
    {
        if (_tilesDict.ContainsKey(key))
            _tilesDict[key] += 1;
        else
            _tilesDict.Add(key, 1);
    }

    public int GetTiles(string key)
    {
        return _tilesDict.ContainsKey(key) ? this._tilesDict[key] : 0;
    }
}