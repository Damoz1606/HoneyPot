using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tetrominos;

    public void Spawn()
    {
        int randomIndex = Random.Range(0, this._tetrominos.Count);
        GameObject temp = Instantiate(this._tetrominos[randomIndex], new Vector3(GameplayManagers.GridManager.GridWidth / 2, GameplayManagers.GridManager.GridHeight - Constants.GRID_GREACE_HEIGHT, 0), Quaternion.identity);
        GameplayManagers.GameManager.CurrentTetromino = temp.GetComponent<Tetromino>();
        temp.transform.SetParent(GameplayManagers.GameManager.BlockHolder);

        GameplayManagers.InputManager.IsInputActive = true;
    }
}
