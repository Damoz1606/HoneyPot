using UnityEngine;

public class Column
{
    public Transform[] row;

    public Column(int rowCount)
    {
        this.row = new Transform[rowCount];
    }
}