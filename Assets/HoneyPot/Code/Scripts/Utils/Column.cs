using UnityEngine;

public class Column<T>
{
    public T[] row;

    public Column(int rowCount)
    {
        this.row = new T[rowCount];
    }
}