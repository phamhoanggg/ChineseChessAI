using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    public List<ChessPiece> AIChessPieceList;

    /** arbitrary lower bound of the heuristic function */
    static double MIN_EVAL = -10000;

    /** arbitrary upper bound of the heuristic function */
    static double MAX_EVAL = 10000;

    /** number of heuristic functions evaluated since last reset */
    private int positionsEvaluated = 0;

    public void BotMove()
    {
        Move m = bestMove(GameController.instance.PieceAtPosition, GameController.instance.Turn, GameController.instance.DEPTH);
        if (m == null)
        {
            m = bestMove(GameController.instance.PieceAtPosition, GameController.instance.Turn, 1);
            if (m == null)
            {
                GameController.instance.EndGame();
            }
        }
        if (GameController.instance.legal(m,GameController.instance.PieceAtPosition, GameController.instance.Turn))
        {

            ChessPiece piece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, m.start);
            UI_Manager.instance.PrePos.position = piece.transform.position;
            ChessPiece playerPiece = GameController.instance.pieceAt(GameController.instance.PieceAtPosition, m.end);
            if (playerPiece != null)
            {
                GameController.instance.PlayerChessPieceList.Remove(playerPiece);
                Destroy(playerPiece.gameObject);   
            }
            GameController.instance.PieceAtPosition[(int)m.end.x, (int)m.end.y] = GameController.instance.PieceAtPosition[(int)m.start.x, (int)m.start.y];
            GameController.instance.PieceAtPosition[(int)m.start.x, (int)m.start.y] = null;
            piece.BoardPosition = m.end;
            Tile target = GameController.instance.tiles[(int)m.end.x, (int)m.end.y].GetComponent<Tile>();
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, target.transform.position, 10);
            GameController.instance.ChangeTurn();
            piece.CheckCheckMate();
        }
    }

    // Evaluate the value of current position
    public double heuristic(ChessPiece[,] pieceAtPos)
    {

        double turn = (GameController.instance.Turn % 2 == 0) ? 1.0 : -1.0;
        double eval = 0;

        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                ChessPiece piece = GameController.instance.pieceAt(pieceAtPos, new Vector2(i, j));
                if (piece == null)
                {
                    continue;
                }
                else
                {
                    eval += turn * piece.GetFlexValue(new Vector2(j, i));
                }
            }
        }

        positionsEvaluated++;
        return eval;
    }


    public KeyValuePair<double, Move> evaluate(ChessPiece[,] pieceAtPos, int depth, double a, double b, int turn)
    {
        int tmpTurn = turn;
        if (depth == 0)
        {
            KeyValuePair<double, Move> res = new(heuristic(GameController.instance.PieceAtPosition), null);

            return res;
        }

        List<Move> moves = GameController.instance.legalMoves(pieceAtPos, tmpTurn);
        double maxEval = MIN_EVAL - 1;
        Move maxMove = null;

        tmpTurn += 1;
        foreach (Move m in moves)
        {
            GameController.instance.setPieceAt(pieceAtPos, m.end, m.piece);
            GameController.instance.setPieceAt(pieceAtPos, m.start, null);

            KeyValuePair<double, Move> em = evaluate(pieceAtPos, depth - 1, -b, -a, tmpTurn);
            double eval = -1 * em.Key;
            if (eval > maxEval)
            {
                maxEval = eval;
                maxMove = m;
            }

            GameController.instance.setPieceAt(pieceAtPos, m.start, m.piece);
            GameController.instance.setPieceAt(pieceAtPos, m.end, m.target);

            a = a > eval ? a : eval;
            if (a >= b)
                break;
        }
        tmpTurn -= 1;

        return new KeyValuePair<double, Move>(maxEval, maxMove);
    }


    public Move bestMove(ChessPiece[,] pieces, int turn, int depth)
    {
        Move best = null;
        positionsEvaluated = 0;

        for (int i = 0; i <= depth; i++)
        {
            positionsEvaluated = 0;
            ChessPiece[,] newpieces = pieces;
            KeyValuePair<double, Move> em = evaluate(newpieces, i, MIN_EVAL, MAX_EVAL, turn);
            best = em.Value;
            
            if (positionsEvaluated > GameController.instance.POSITION_EVALUATED)
            {
                break;
            }
        }

        positionsEvaluated = 0;
        return best;
    }

}
