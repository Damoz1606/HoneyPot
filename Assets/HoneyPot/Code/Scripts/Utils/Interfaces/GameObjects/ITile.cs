using UnityEngine;

public interface ITile : IGameObject
{
    TileNormalType type { get; }

    void OnEffect(IBlock block = default);
}