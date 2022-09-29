using UnityEngine;

public interface IGameObject
{
    Transform transform { get; }
    GameObject gameObject { get; }
}