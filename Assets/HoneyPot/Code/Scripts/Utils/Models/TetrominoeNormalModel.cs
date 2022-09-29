using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TetrominoeNormalModel : ATetrominoeModel
{
    [SerializeField] public List<Vector3> _pivots;
}