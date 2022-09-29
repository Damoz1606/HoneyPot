
using System.Collections.Generic;
using UnityEngine;

public interface ITetrominoe : IGameObject
{

    List<IBlock> Blocks { get; set; }
    TetrominoeTypes type { get; }

    FallComponent FallComponent { set; get; }
    HorizontalMovement HorizontalMovement { set; get; }
    RotationComponent RotationComponent { set; get; }

    void PlaceBlockOnChild();
}