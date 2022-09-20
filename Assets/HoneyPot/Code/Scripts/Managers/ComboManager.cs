using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private GameObject _beePollenPrefab;
    [SerializeField] private GameObject _honeyPot;

    public GameObject BeePollen { get { return this._beePollenPrefab; } }
    public GameObject HoneyPot { get { return this._honeyPot; } }

    public void InstanceCombo(ComboTypes combo, Block block)
    {
        Destroy(block.Child.gameObject);
        block.Child = null;
        switch (combo)
        {
            case ComboTypes.BOMB:
                GameObject bomb = Instantiate(BeePollen, block.transform.position, Quaternion.identity);
                bomb.transform.SetParent(block.transform);
                block.Child = bomb.GetComponent<ComboTile>();
                break;
            case ComboTypes.STAR:
                break;
            case ComboTypes.HONEYPOT:
                GameObject honeyPot = Instantiate(BeePollen, block.transform.position, Quaternion.identity);
                honeyPot.transform.SetParent(this.transform);
                block.Child = honeyPot.GetComponent<ComboTile>();
                break;
            default:
                break;
        }
    }
}
