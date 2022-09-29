using UnityEngine;

public interface ITile : IGameObject
{
    TileTypes type { get; }

    void OnEffect(IBlock block = default);
}