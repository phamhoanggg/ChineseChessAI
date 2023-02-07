using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public ChessPiece piece;
    public ChessPiece target;

    public Vector2 start;
    public Vector2 end;

    public Move(ChessPiece p, ChessPiece t, Vector2 s, Vector2 e)
    {
        this.piece = p;
        this.target = t;
        this.start = s;
        this.end = e;
    }

    public bool equalTo(Move m)
    {
        if (!m.piece.equalTo(this.piece))
        {
            return false;
        }
        
        if (m.target == null && this.target != null)
        {
            return false;
        }else if (m.target != null && this.target == null)
        {
            return false;
        }else if (m.target != null && this.target != null && !m.target.equalTo(this.target))
        {
            return false;
        }

        return true;

        
    }
}