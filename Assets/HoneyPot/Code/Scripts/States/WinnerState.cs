using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class WinnerState : _StatesBase
{
    public override async void OnActivate()
    {
        EventManager.TriggerEvent(Channels.POP_CHANNEL, PopEvent.POP_ALL_WITHOUT_DISTINGUITION, null);
        // GameplayManagers.AudioManager.PlayPopupOpen();
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIWinner);
        GameplayManagers.UIManager.WinnerPopup.OnActivatePopup();
        GameStats stats = await ReadAsync();
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
            if (level >= 0 && levels[level] != null)
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
        await this.StoreAsync(stats);
    }

    public override void OnDeactivate()
    {
        // GameplayManagers.UIManager.GameOverPopup.OnDeactivatePopup();
    }

    public override void OnUpdate()
    {
    }

    public async Task StoreAsync(GameStats stats)
    {
        await Storage.Instance.StoreAsync<GameStats>(stats, $"{StorageConstants.GAME_STATS}");
    }

    public async Task<GameStats> ReadAsync()
    {
        return (await Storage.Instance.ReadAsync<GameStats>($"{StorageConstants.GAME_STATS}"))[0];
    }
}