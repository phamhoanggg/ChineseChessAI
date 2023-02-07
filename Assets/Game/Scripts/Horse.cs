using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : ChessPiece
{
    private void Start()
    {
        if ((SideType)type == 0)
        {
            code = 4;
        }
        else
        {
            code = -4;
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

        if (y < 8)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, y+1)) == null)
            {
                if (x > 0)
                    MoveableTiles.Add(GameController.instance.tiles[x - 1, y + 2]);
                if (x < 8)
                    MoveableTiles.Add(GameController.instance.tiles[x + 1, y + 2]);
            }
        }
        if (y > 1)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, y - 1)) == null)
            {
                if (x > 0)
                    MoveableTiles.Add(GameController.instance.tiles[x - 1, y - 2]);
                if (x < 8)
                    MoveableTiles.Add(GameController.instance.tiles[x + 1, y - 2]);
            }
        }
        if (x < 7)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x+1, y)) == null)
            {
                if (y > 0)
                    MoveableTiles.Add(GameController.instance.tiles[x + 2, y - 1]);
                if (y < 9)
                    MoveableTiles.Add(GameController.instance.tiles[x + 2, y + 1]);
            }
        }
        if (x > 1)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x-1, y)) == null)
            {
                if (y > 0)
                    MoveableTiles.Add(GameController.instance.tiles[x - 2, y - 1]);
                if (y < 9)
                    MoveableTiles.Add(GameController.instance.tiles[x - 2, y + 1]);
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
