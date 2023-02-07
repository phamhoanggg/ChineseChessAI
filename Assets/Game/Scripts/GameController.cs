using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Hard
}

public enum GameState
{
    MainMenu,
    GamePlay,
}
public class GameController : FastSingleton<GameController>
{
    [SerializeField] private LayerMask chessLayer, targetLayer;
    [SerializeField] private Camera MainCam;

    public GameObject[,] tiles = new GameObject[9, 10];
    
    public ChessPiece ChosenChess;

    public int Turn;
    public bool IsCheckMate;
    public BotAI bot;

    public List<ChessPiece> PlayerChessPieceList = new List<ChessPiece>();

    public int DEPTH;
    public int POSITION_EVALUATED;
    public Difficulty difficulty;


    public GameState gameState;

    public ChessPiece[,] PieceAtPosition = new ChessPiece[9, 10];

    // Start is called before the first frame update
    void Start()
    {
        IsCheckMate = false;
        gameState = GameState.MainMenu;
        InitTile();

        ChessPiece[] pieceList = FindObjectsOfType<ChessPiece>();
        foreach (var piece in pieceList)
        {
            if (piece.type == SideType.BotAI)
                bot.AIChessPieceList.Add(piece);
            else
            {
                PlayerChessPieceList.Add(piece);
            }

            PieceAtPosition[(int)piece.BoardPosition.x, (int)piece.BoardPosition.y] = piece;
        }

    }

    public void StartGame()
    {
        Turn = 0;
        ChangeTurn();
        if (difficulty == Difficulty.Easy)
        {
            DEPTH = 10;
            POSITION_EVALUATED = 1000;
        }
        else if (difficulty == Difficulty.Hard)
        {
            DEPTH = 20;
            POSITION_EVALUATED = 10000;
        }
    }

    public void EndGame()
    {
        if (Turn % 2 == 0)
        {
            // Red win
            UI_Manager.instance.EndGame.SetActive(true);
            UI_Manager.instance.EndGameText.text = "Red  Win ! ! !";
        }
        else
        {
            // Black win
            UI_Manager.instance.EndGame.SetActive(true);
            UI_Manager.instance.EndGameText.text = "Black  Win ! ! !";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Turn % 2 == 1)
        {
            Ray myray = MainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myray, out RaycastHit hit, 10, targetLayer))
            {
                if (hit.collider.gameObject != null)
                {
                    TargetPlace target = hit.collider.GetComponent<TargetPlace>();


                    ChessPiece piece = pieceAt(PieceAtPosition, target.BoardPosition);
                    if (piece != null)
                    {
                        bot.AIChessPieceList.Remove(piece);
                        Destroy(piece.gameObject);
                    }
                    UI_Manager.instance.PrePos.gameObject.SetActive(true);
                    UI_Manager.instance.PrePos.position = ChosenChess.transform.position;
                    ChosenChess.moveTo(target);
                    ChosenChess.MoveableTiles.Clear();
                    if (ChosenChess.CheckCheckMate())
                    {
                        Turn++;
                        UI_Manager.instance.TextAppear("Check Mate ! ! !");
                        if (legalMoves(PieceAtPosition, Turn).Count == 0)
                        {
                            EndGame();
                        }
                    }
                    else
                    {
                        ChangeTurn();
                    }

                    if (Turn % 2 == 0)
                    {
                        bot.Invoke(nameof(bot.BotMove), 1);
                    }
                }              
            }

            GameObject[] DisplayTile = GameObject.FindGameObjectsWithTag("DisplayTile");
            foreach(var tile in DisplayTile)
            {
                Destroy(tile);
            }
          
            if (Physics.Raycast(myray, out hit, 10, chessLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    ChosenChess = hit.collider.GetComponent<ChessPiece>();
                    ChosenChess.MoveableTiles.Clear();
                    ChosenChess.InitMoveableTiles();
                    ChosenChess.DisplayMoveableTiles();
                }
                
            }
        }

    }

    public void ChangeTurn()
    {
        Turn++;
        if (Turn % 2 == 0)
        {
            UI_Manager.instance.TextAppear("Bot AI   Moving...");
        }
        else
        {
            UI_Manager.instance.TextAppear("Human   Moving...");
        }
    }
    private void InitTile()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                float idx = (x + y * 9);
                tiles[x, y] = GameObject.Find("Tile (" + idx + ")");
                tiles[x, y].GetComponent<Tile>().BoardPosition = new Vector2(x, y);

                if (Physics.Raycast(tiles[x,y].transform.position + Vector3.down, Vector3.up, out RaycastHit hit, 100, chessLayer))
                {
                    hit.collider.GetComponent<ChessPiece>().BoardPosition = new Vector2(x, y); 
                }
                PieceAtPosition[x, y] = null;
            }
        }
    }

    public bool containsPoint(Vector2 p)
    {
        return (p.x >= 0 && p.y >= 0
                && p.x <= 9 && p.y <= 10);
    }

    public ChessPiece pieceAt(ChessPiece[,] PieceAtPosition, Vector2 p)
    {
        if (containsPoint(p))
            return PieceAtPosition[(int)p.x,(int)p.y];
        else
            return null;
    }

    public ChessPiece pieceAt(ChessPiece[,] PieceAtPosition, int r, int c)
    {
        if (containsPoint(new Vector2(r, c)))
            return PieceAtPosition[r,c];
        else
            return null;
    }

    public void setPieceAt(ChessPiece[,] PieceAtPosition, Vector2 p, ChessPiece piece)
    {
        PieceAtPosition[(int)p.x, (int)p.y] = piece;
    }

    // List of available moves
    public List<Move> candidateMoves(ChessPiece[,] PieceAtPosition, int turn)
    {
        List<Move> moves = new List<Move>();
        bool hasKing = false;
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                ChessPiece piece = pieceAt(PieceAtPosition, new Vector2(i, j));
                if (piece == null)
                {
                    continue;
                }
                else
                {
                    if (turn % 2 == (int)piece.type)
                    {
                        if (Mathf.Abs(piece.code) == 1)
                            hasKing = true;
                        piece.InitMoveableTiles();
                        List<GameObject> ends = piece.MoveableTiles;
                        foreach (var tile in ends)
                        {
                            Vector2 p = tile.GetComponent<Tile>().BoardPosition;
                            moves.Add(new Move(piece, pieceAt(this.PieceAtPosition, p), piece.BoardPosition, p));
                        }
                    }
                }
            }
        }

       
        if (hasKing)
            return moves;
        else
            return new List<Move>();
    }

    // List of legal moves
    public List<Move> legalMoves(ChessPiece[,] pieceAtPos, int turn)
    {
        List<Move> candidates = candidateMoves(pieceAtPos, turn);

        List<Move> legalMoves = new List<Move>();
        ChessPiece[,] copy = pieceAtPos;
        int tmpTurn = turn;
        foreach (Move m in candidates)
        {
            setPieceAt(copy, m.end, m.piece);
            setPieceAt(copy, m.start, null);
            tmpTurn++;

            bool legal = true;
            List<Move> enemyMoves = candidateMoves(copy, tmpTurn);
            foreach (Move em in enemyMoves)
            {
                if (tmpTurn % 2 == 0)
                {
                    if (pieceAt(copy, em.end) != null
                            && pieceAt(copy, em.end).code == -1)
                    {
                        legal = false;
                        break;
                    }
                }
                else
                {
                    if (pieceAt(copy, em.end) != null
                            && pieceAt(copy, em.end).code == 1)
                    {
                        legal = false;
                        break;
                    }
                }
            }

            if (legal)
            {
                legalMoves.Add(m);
            }

            tmpTurn--;
            setPieceAt(copy, m.start, m.piece);
            setPieceAt(copy, m.end, m.target);
        }
        return legalMoves;

    }


    public bool legal(Move move,ChessPiece[,] pieceAtPos, int turn)
    {
        bool legal = false;
        foreach (Move lm in legalMoves(pieceAtPos, turn))
        {
            if (lm.equalTo(move))
            {
                legal = true;
                break;
            }
        }
        return legal;
    }


}
