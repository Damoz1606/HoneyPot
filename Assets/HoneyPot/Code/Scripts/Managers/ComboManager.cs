using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private GameObject _beePollenPrefab;
    [SerializeField] private GameObject _honeyPotPrefab;
    [SerializeField] private GameObject _starPrefab;

    public void InstanceCombo(ComboTypes combo, Block block)
    {
        Destroy(block.Child.gameObject);
        block.Child = null;
        GameObject child = null;
        switch (combo)
        {
            case ComboTypes.BOMB:
                child = Instantiate(_beePollenPrefab, block.transform.position, Quaternion.identity);
                break;
            case ComboTypes.HONEYPOT:
                child = Instantiate(_honeyPotPrefab, block.transform.position, Quaternion.identity);
                break;
            case ComboTypes.STAR:
                child = Instantiate(_starPrefab, block.transform.position, Quaternion.identity);
                break;
        }
        if (child == null) return;
        block.Child = child.GetComponent<ComboTile>();
        child.transform.SetParent(block.transform);
    }
}
