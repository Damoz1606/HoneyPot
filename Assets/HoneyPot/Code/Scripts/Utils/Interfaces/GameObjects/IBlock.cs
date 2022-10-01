using System.Collections.Generic;
using UnityEngine;

public interface IBlock : IGameObject, IMovement
{
    bool CanPop { get; set; }
    bool CanDecrease { get; set; }
    bool CanSwap { get; set; }
    bool IsSwapping { get; set; }
    bool IsDecreasing { get; set; }
    ITile tile { get; set; }
    Vector3Int Position { get; }
    IBlock Top { get; }
    IBlock Bottom { get; }
    IBlock Left { get; }
    IBlock Right { get; }
    List<IBlock> Neighbourhood { get; }
    List<IBlock> NeighboursVertical { get; }
    List<IBlock> NeighboursHorizontal { get; }

    void AttachTile(ITile tile);
    void DeattachTile();
    void OnEffect();
    List<IBlock> GetConnections(List<IBlock> exclude = null);
    List<IBlock> GetConnections(AxisTypes axis, List<IBlock> neighbours = null, List<IBlock> exclude = null);

}