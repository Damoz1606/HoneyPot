using System.Collections.Generic;
using UnityEngine;

public interface IComboSearch<T>
where T : IBlock
{
    void LookCombos(T block, List<T> blocks);
}