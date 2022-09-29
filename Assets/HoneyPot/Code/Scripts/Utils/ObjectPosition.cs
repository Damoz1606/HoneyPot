using UnityEngine;

public static class ObjectPosition
{
    public static Vector3Int ObjectVector3IntPosition(GameObject gameObject) =>
        new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);

    public static void ObjectResetPosition(GameObject gameObject) => gameObject.transform.position = Vector3.zero;
    public static void ObjectResetLocalPosition(GameObject gameObject) => gameObject.transform.localPosition = Vector3.zero;
    public static void ObjectResetRotation(GameObject gameObject) => gameObject.transform.rotation = Quaternion.identity;
}