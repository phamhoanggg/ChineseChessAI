using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chariot : ChessPiece
{
    private void Start()
    {
        if ((SideType)type == 0)
        {
            code = 5;
        }
        else
        {
            code = -5;
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

        for (int i = x + 1; i < 9; i++)
        {           
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(i, y));
            if (piece != null)
            {
                if (piece.code * this.code > 0)
                {
                    break;
                }
                else
                {
                    MoveableTiles.Add(GameController.instance.tiles[i, y]);
                    break;
                }
            }
            MoveableTiles.Add(GameController.instance.tiles[i, y]);
        }

        for (int i = x - 1; i >= 0; i--)
        {
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(i, y));
            if (piece != null)
            {
                if (piece.code * this.code > 0)
                {
                    break;
                }
                else
                {
                    MoveableTiles.Add(GameController.instance.tiles[i, y]);
                    break;
                }
            }
            MoveableTiles.Add(GameController.instance.tiles[i, y]);
        }

        for (int j = y + 1; j < 10; j++)
        {
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, j));
            if (piece != null)
            {
                if (piece.code * this.code > 0)
                {
                    break;
                }
                else
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, j]);
                    break;
                }
            }
            MoveableTiles.Add(GameController.instance.tiles[x, j]);
        }

        for (int j = y - 1; j >= 0; j--)
        {
            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, j));
            if (piece != null)
            {
                if (piece.code * this.code > 0)
                {
                    break;
                }
                else
                {
                    MoveableTiles.Add(GameController.instance.tiles[x, j]);
                    break;
                }
            }
            MoveableTiles.Add(GameController.instance.tiles[x, j]);
        }


    }

}

