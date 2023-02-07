using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideType
{   
    BotAI = 0,
    Player,
}

public class ChessPiece : MonoBehaviour
{
    public LayerMask TileLayer, chessLayer;
    public GameObject TileLocated;
    public GameObject Target;
    public List<GameObject> MoveableTiles = new List<GameObject>();
    public SideType type;
    public Vector2 BoardPosition;
    public int code;
    public int value;

    public void Awake()
    {
        if (CompareTag("Player"))
        {
            type = SideType.Player;
        }
        else
        {
            type = SideType.BotAI;
        }

        value = FlexValueMatrix.CONST_VALUE[Mathf.Abs(code)];   //Set default value for each piece
    }

    public int GetFlexValue(Vector2 pos)
    {
        value = FlexValueMatrix.CONST_VALUE[Mathf.Abs(code)];
        if (Mathf.Abs(code) == 1)
        {
            value = FlexValueMatrix.GENERAL_FLEX_VAL[(int)pos.x, (int)pos.y];
        }

        if (Mathf.Abs(code) == 7)
        {
            value = FlexValueMatrix.SOLDIER_FLEX_VAL[(int)pos.x, (int)pos.y];
        }

        if (Mathf.Abs(code) == 4 || Mathf.Abs(code) == 5 || Mathf.Abs(code) == 6)
        {
            if (code > 0)
            {
                value += FlexValueMatrix.BLACK_PLUS_FLEX_VAL[(int)pos.x, (int)pos.y];
            }
            else
            {
                value += FlexValueMatrix.RED_PLUS_FLEX_VAL[(int)pos.x, (int)pos.y];
            }
        }

        return value;
    }
    public virtual void moveTo(TargetPlace target)
    {
        
        GameController.instance.setPieceAt(GameController.instance.PieceAtPosition, target.BoardPosition, this);
        GameController.instance.setPieceAt(GameController.instance.PieceAtPosition, this.BoardPosition, null);
        this.BoardPosition = target.BoardPosition;
    }

    public bool CheckCheckMate()
    {
        if (type == SideType.Player)
        {
            foreach (var piece in GameController.instance.PlayerChessPieceList.ToArray())
            {
                piece.InitMoveableTiles();

                foreach (var tile in piece.MoveableTiles.ToArray())
                {
                    ChessPiece oppositePiece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, tile.GetComponent<Tile>().BoardPosition);
                    if (oppositePiece != null && oppositePiece.type != this.type && Mathf.Abs(oppositePiece.code) == 1)
                    {
                        GameController.instance.IsCheckMate = true;
                        return true;
                    }
                }
            }
        }
        else
        {
            foreach (var piece in GameController.instance.bot.AIChessPieceList.ToArray())
            {
                piece.InitMoveableTiles();

                foreach (var tile in piece.MoveableTiles.ToArray())
                {
                    ChessPiece oppositePiece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, tile.GetComponent<Tile>().BoardPosition);
                    if (oppositePiece != null && oppositePiece.type != this.type && Mathf.Abs(oppositePiece.code) == 1)
                    {
                        GameController.instance.IsCheckMate = true;
                        return true;
                    }
                }
            }
        }

        GameController.instance.IsCheckMate = false;
        return false;
        
    }

    public virtual void InitMoveableTiles()
    {
        
    }

    public virtual void DisplayMoveableTiles()
    {
        foreach (var tile in MoveableTiles)
        {
            if (Physics.Raycast(tile.transform.position + Vector3.up * 2, Vector3.down, out RaycastHit hit, 10, chessLayer))
            {
                if (hit.collider.tag == "Player")
                {
                    continue;
                }
            }


            Instantiate(Target, new Vector3(tile.transform.position.x, 0.006f, tile.transform.position.z), Quaternion.identity);

        }
    }

    public bool CheckPieceAt(Vector2 boardPos)
    {
        ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, boardPos);
        if (piece != null && piece.code * this.code > 0)
        {
            return true;
        }
        return false;
    }

    public bool equalTo(ChessPiece piece)
    {
        if (piece.BoardPosition == this.BoardPosition && piece.type == this.type && piece.code == this.code)
        {
            return true;
        }
        return false;
    }


}