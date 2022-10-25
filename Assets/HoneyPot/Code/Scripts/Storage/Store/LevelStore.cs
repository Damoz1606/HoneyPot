using UnityEngine;

[System.Serializable]
public class LevelStore
{
    public int levelId;
    public int worldId;
    public int starCount;

    public LevelStore(int levelId, int worldId, int starCount)
    {
        this.levelId = levelId;
        this.worldId = worldId;
        this.starCount = starCount;
    }
}