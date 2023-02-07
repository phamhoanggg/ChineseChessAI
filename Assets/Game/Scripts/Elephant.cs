using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : ChessPiece
{
    private void Start()
    {
        if ((SideType)type == 0)
        {
            code = 3;
        }
        else
        {
            code = -3;
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
            if (x + 2 <= 8)
            {
                if (y + 2 <= 4)
                    MoveableTiles.Add(GameController.instance.tiles[x + 2, y + 2]);
                if (y - 2 >= 0)
                    MoveableTiles.Add(GameController.instance.tiles[x + 2, y - 2]);
            }
            if (x - 2 >= 0 && y + 2 <= 4)
            {
                if (y + 2 <= 4)
                    MoveableTiles.Add(GameController.instance.tiles[x - 2, y + 2]);
                if (y - 2 >= 0)
                    MoveableTiles.Add(GameController.instance.tiles[x - 2, y - 2]);
            }
        }
        else
        {
            if (x + 2 <= 8)
            {
                if (y + 2 <= 9)
                    MoveableTiles.Add(GameController.instance.tiles[x + 2, y + 2]);
                if (y - 2 >= 5)
                    MoveableTiles.Add(GameController.instance.tiles[x + 2, y - 2]);
            }
            if (x - 2 >= 0 && y + 2 <= 4)
            {
                if (y + 2 <= 9)
                    MoveableTiles.Add(GameController.instance.tiles[x - 2, y + 2]);
                if (y - 2 >= 5)
                    MoveableTiles.Add(GameController.instance.tiles[x - 2, y - 2]);
            }
        }

        foreach (var tile in MoveableTiles.ToArray())
        {
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, tile.GetComponent<Tile>().BoardPosition);
            if (piece != null && piece.code * this.code > 0)
            {
                MoveableTiles.Remove(tile);
            }

            Vector2 midPoint = (tile.GetComponent<Tile>().BoardPosition + this.BoardPosition) / 2;
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, midPoint) != null)
            {
                MoveableTiles.Remove(tile);
            }
        }
    }

}

