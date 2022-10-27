using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButtonUIController : MonoBehaviour
{
    [SerializeField] private string _worldID;
    [SerializeField] private int _levelID;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<Star> _stars = new List<Star>();
    [SerializeField] private int _starCount;

    public int StarCount { set => this._starCount = value; }

    private void Start()
    {
        _text.text = $"{_levelID}";
        GameStats game = Storage.Instance.Read<GameStats>($"{StorageConstants.GAME_STATS}")[0];
        if (game.completedLevels == null) return;
        List<LevelStore> levels = new List<LevelStore>(game.completedLevels);
        LevelStore level = levels.Find(obj => obj.levelId == this._levelID);
        this._starCount = level != null ? level.starCount : 0;
        if (this._stars.Count < 0) return;
        for (int i = 0; i < this._starCount; i++)
        {
            _stars[i].UpdateReference();
        }
    }
}