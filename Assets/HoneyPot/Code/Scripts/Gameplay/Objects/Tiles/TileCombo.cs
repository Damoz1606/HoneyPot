using UnityEngine;

public class TileCombo : MonoBehaviour, ITile, IPoolObject
{
    [SerializeField] private TileComboModel data;
    public TileNormalType type => data.tileType;
    public TileComboType comboType => data.comboType;

    public void OnActivate()
    {
        this.transform.localScale = Vector3.one;
        this.data.hasEffectBeenActive = false;
    }

    public void OnDeactivate()
    {
        this.transform.localScale = Vector3.one;
        this.data.hasEffectBeenActive = false;
    }

    public void OnEffect(IBlock block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        GameplayManagers.GameManager.IsGameActive = false;
        if (block == null || this.data.hasEffectBeenActive) return;
        EventManager.TriggerEvent(EventEnum.SCORE.ToString(), this.data.score);
        switch (this.comboType)
        {
            case TileComboType.BOMB:
                EventManager.TriggerEvent(EventEnum.POP_AROUND.ToString(), block);
                break;
            case TileComboType.HONEYPOT:
                EventManager.TriggerEvent(EventEnum.POP_ALL.ToString(), null);
                break;
            default: break;
        }
        GameplayManagers.GameManager.IsGameActive = true;
        this.data.hasEffectBeenActive = true;
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}