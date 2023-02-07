using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adviser : ChessPiece
{
    private void Start()
    {
        if ((SideType)type == 0)
        {
            code = 2;
        }
        else
        {
            code = -2;
        }
    }
    

    public override void moveTo(TargetPlace target)
    {
        base.moveTo(target);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10);
    }

    public override void InitMoveableTiles()
    {
        MoveableTiles.Clear();
        int x = (int)BoardPosition.x;
        int y = (int)BoardPosition.y;

        if (code < 0)
        {
            if (x > 3)
            {
                if (y > 0)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x - 1, y - 1]);
                }
                if (y < 2)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x - 1, y + 1]);
                }
            }
            if (x < 5)
            {
                if (y > 0)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x + 1, y - 1]);
                }
                if (y < 2)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x + 1, y + 1]);
                }
            }
        }
        else
        {
            if (x > 3)
            {
                if (y > 7)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x - 1, y - 1]);
                }
                if (y < 9)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x - 1, y + 1]);
                }
            }
            if (x < 5)
            {
                if (y > 7)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x + 1, y - 1]);
                }
                if (y < 9)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x + 1, y + 1]);
                }
            }
        }

        foreach (var tile in MoveableTiles.ToArray())
        {
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, tile.GetComponent<Tile>().BoardPosition);
            if (piece != null && piece.code * this.code > 0)
            {
                MoveableTiles.Remove(tile);
            }
        }

    }
}
