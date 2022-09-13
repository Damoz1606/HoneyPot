using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    private int deltaMovement = 1;
    public void Move(Vector2 direction)
    {
        float movement = (direction.Equals(Vector2.right)) ? this.deltaMovement : -this.deltaMovement;
        this.transform.position += new Vector3(movement, 0, 0);
        if (GameplayManagers.GridManager.GridTetromino.IsValidGridPosition(this.transform))
        {
            GameplayManagers.GridManager.GridTetromino.UpdateGrid(this.transform);
        } else {
            this.transform.position -= new Vector3(movement, 0, 0);
        }

    }
}
