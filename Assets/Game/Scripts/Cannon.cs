using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : ChessPiece
{
    private void Start()
    {
        if ((SideType)type == 0)
        {
            code = 6;
        }
        else
        {
            code = -6;
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
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(i, y)) != null)
            {
                for (int k = i + 1; k < 9; k++)
                {
                    ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(k, y));
                    if (piece != null && piece.code * this.code < 0)
                    {
                        MoveableTiles.Add(GameController.instance.tiles[k, y]);
                        break;
                    }
                    else if (piece != null && piece.code * this.code > 0)
                    {
                        break;
                    }

                }
                break;
            }
            MoveableTiles.Add(GameController.instance.tiles[i, y]);
        }

        for (int i = x - 1; i >= 0; i--)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(i, y)) != null)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(k, y));
                    if (piece != null && piece.code * this.code < 0)
                    {
                        MoveableTiles.Add(GameController.instance.tiles[k, y]);
                        break;
                    }
                    else if (piece != null && piece.code * this.code > 0)
                    {
                        break;
                    }

                }
                break;
            }
            MoveableTiles.Add(GameController.instance.tiles[i, y]);
        }

        for (int j = y + 1; j < 10; j++)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, j)) != null)
            {
                for (int k = j + 1; k <= 9; k++)
                {
                    ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, k));
                    if (piece != null && piece.code * this.code < 0)
                    {
                        MoveableTiles.Add(GameController.instance.tiles[x, k]);
                        break;
                    }
                    else if (piece != null && piece.code * this.code > 0)
                    {
                        break;
                    }

                }
                break;
            }
            MoveableTiles.Add(GameController.instance.tiles[x, j]);
        }

        for (int j = y - 1; j >= 0; j--)
        {
            if (GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, j)) != null)
            {
                for (int k = j - 1; k >=0; k--)
                {
                    ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, new Vector2(x, k));
                    if (piece != null && piece.code * this.code < 0)
                    {
                        MoveableTiles.Add(GameController.instance.tiles[x, k]);
                        break;
                    }
                    else if (piece != null && piece.code * this.code > 0)
                    {
                        break;
                    }

                }
                break;
            }
            MoveableTiles.Add(GameController.instance.tiles[x, j]);
        }


    }
}