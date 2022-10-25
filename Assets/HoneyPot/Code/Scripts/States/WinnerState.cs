using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinnerState : _StatesBase
{
    public override void OnActivate()
    {
        EventManager.TriggerEvent(Channels.POP_CHANNEL, PopEvent.POP_ALL_WITHOUT_DISTINGUITION, null);
        // GameplayManagers.AudioManager.PlayPopupOpen();
        GameplayManagers.UIManager.WinnerPopup.OnActivatePopup();
        GameStats stats = Storage.Instance.Read<GameStats>($"{Storage.ROOT}{StorageConstants.GAME_STATS}")[0];
        List<LevelStore> levels = new List<LevelStore>();
        int starCount = 0;
        if (GameplayManagers.ScoreManager.ScoreReferences[0] <= GameplayManagers.ScoreManager.CurrentScore)
            starCount = 1;
        if (GameplayManagers.ScoreManager.ScoreReferences[1] <= GameplayManagers.ScoreManager.CurrentScore)
            starCount = 2;
        if (GameplayManagers.ScoreManager.ScoreReferences[2] <= GameplayManagers.ScoreManager.CurrentScore)
            starCount = 3;
        if (stats.completedLevels != null)
        {
            levels = new List<LevelStore>(stats.completedLevels);
            int level = levels.FindIndex(o => o.levelId == ConfigurationManager.Instance.LevelID);
            if (levels[level] != null)
            {
                levels[level] = new LevelStore(ConfigurationManager.Instance.LevelID, ConfigurationManager.Instance.WorldID, starCount);
            }
            else
            {
                levels.Add(new LevelStore(ConfigurationManager.Instance.LevelID, ConfigurationManager.Instance.WorldID, starCount));
            }
        }
        else
        {
            levels.Add(new LevelStore(ConfigurationManager.Instance.LevelID, ConfigurationManager.Instance.WorldID, starCount));
        }
        stats.completedLevels = levels.ToArray();
        Storage.Instance.Store<GameStats>(stats, $"{Storage.ROOT}{StorageConstants.GAME_STATS}");
        Debug.Log("<color=green>Winner State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        // GameplayManagers.UIManager.GameOverPopup.OnDeactivatePopup();
        Debug.Log("<color=red>Winner State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Winner State</color> OnUpdate");
    }
}