using System.Collections.Generic;
using UnityEngine;

public static class VectorRound
{
    public static Vector2Int Vector2Round(Vector2 vector) => new Vector2Int((int)Mathf.Round(vector.x), (int)Mathf.Round(vector.y));
    public static Vector2Int[] Vectors2Round(Vector2[] vectors)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        foreach (Vector2 vector in vectors)
        {
            result.Add(Vector2Round(vector));
        }
        return result.ToArray();
    }

    public static Vector3Int Vector3Round(Vector3 vector) => new Vector3Int((int)Mathf.Round(vector.x), (int)Mathf.Round(vector.y), (int)Mathf.Round(vector.z));
}