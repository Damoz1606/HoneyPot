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
        switch (combo)
        {
            case ComboTypes.BOMB:
                GameObject bomb = Instantiate(_beePollenPrefab, block.transform.position, Quaternion.identity);
                block.Child = bomb.GetComponent<ComboTile>();
                bomb.transform.SetParent(block.transform);
                break;
            case ComboTypes.HONEYPOT:
                GameObject honeyPot = Instantiate(_honeyPotPrefab, block.transform.position, Quaternion.identity);
                block.Child = honeyPot.GetComponent<ComboTile>();
                honeyPot.transform.SetParent(this.transform);
                break;
            case ComboTypes.STAR:
                GameObject star = Instantiate(_starPrefab, block.transform.position, Quaternion.identity);
                block.Child = star.GetComponent<ComboTile>();
                star.transform.SetParent(this.transform);
                break;
            default:
                break;
        }
    }
}
