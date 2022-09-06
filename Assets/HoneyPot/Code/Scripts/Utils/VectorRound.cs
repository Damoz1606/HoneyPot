using UnityEngine;

public static class VectorRound
{
    public static Vector2 Vector2Round(Vector2 vector) => new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
}