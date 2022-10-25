using UnityEngine;

[System.Serializable]
public class GameStats
{
    public bool hasCompleteTutorial = false;
    public int audioVolume = 50;
    public int sfxVolume = 50;
    public int uiVolume = 50;
    public LevelStore[] completedLevels;
}