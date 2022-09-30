using System.Threading.Tasks;
using UnityEngine;

public interface ISwap<B>
where B : IBlock
{
    Task Swap(B currentBlock, B nextBlock);
    void ComboSwap(B block);
}