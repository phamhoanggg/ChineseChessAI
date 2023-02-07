using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : ChessPiece
{
    private void Start()
    {
        if ((SideType)type == 0)
        {
            code = 1;
        }
        else
        {
            code = -1;
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
                MoveableTiles.Add(GameController.instance.tiles[x - 1, y]);
                if (y > 0)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y - 1]);
                }
                if (y < 2)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y + 1]);
                }
            }
            if (x < 5)
            {
                MoveableTiles.Add(GameController.instance.tiles[x + 1, y]);
                if (y > 0)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y - 1]);
                }
                if (y < 2)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y + 1]);
                }
            }
        }
        else
        {
            if (x > 3)
            {
                MoveableTiles.Add(GameController.instance.tiles[x - 1, y]);
                if (y > 7)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y - 1]);
                }
                if (y < 9)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y + 1]);
                }
            }
            if (x < 5)
            {
                MoveableTiles.Add(GameController.instance.tiles[x + 1, y]);
                if (y > 7)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y - 1]);
                }
                if (y < 9)
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, y + 1]);
                }
            }
        }

        foreach (GameObject tile in MoveableTiles.ToArray())
        {
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, tile.GetComponent<Tile>().BoardPosition);
            if (piece != null && piece.code * this.code > 0)
            {
                MoveableTiles.Remove(tile);
            }
        }

    }

    

}
