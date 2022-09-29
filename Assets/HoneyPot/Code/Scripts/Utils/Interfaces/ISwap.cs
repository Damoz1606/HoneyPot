using UnityEngine;

public interface ISwap<B>
where B : IBlock
{
    void Swap(B currentBlock, B nextBlock);
    void ComboSwap(B block);
}