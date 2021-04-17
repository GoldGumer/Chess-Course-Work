using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainCode : MonoBehaviour
{
    private Pieces movingPiece;
    private Vector3Int cellPosNew;
    private Vector3Int cellPosLast;
    private string BoardNotation = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private Pieces[] AllPieces = new Pieces[64];
    private int Top = 8;
    private int Bottom = 1;
    private int Right = 8;
    private int Left = 1;
    private Dictionary<char, int> BoardToInt = new Dictionary<char, int>()
    {
        {'a',0},{'b',1},{'c',2},{'d',3},{'e',4},{'f',5},{'g',6},{'h',7}
    };
    private string PlayerToMove;
    private double MoveCount = 0;
    private int DrawMoveCount = 0;
    public Tilemap ChessPiecesTilemap;
    public Tilemap ShowMovesTilemap;
    public Tilemap ChessBoardTilemap;
    public TileBase ShowMove;
    public TileBase W_Pawn;
    public TileBase W_Rook;
    public TileBase W_Bishop;
    public TileBase W_Knight;
    public TileBase W_Queen;
    public TileBase W_King;
    public TileBase B_Pawn;
    public TileBase B_Rook;
    public TileBase B_Bishop;
    public TileBase B_Knight;
    public TileBase B_Queen;
    public TileBase B_King;
    public TileBase Black_Light;
    public TileBase Black_Dark;
    public TileBase Green_Dark;
    public TileBase Green_Light;
    public TileBase Brown_Dark;
    public TileBase Brown_Light;

    // Code For changing a piece into another
    private void Promotion(Pieces PieceToPromote, int PositionInArr, string PieceToPromoteTo)
    {
        switch (PieceToPromote.Colour)
        {
            case "White":
                switch (PieceToPromoteTo)
                {
                    case "Rook":
                        AllPieces[PositionInArr] = new Rook("White", PieceToPromote.Position, W_Rook);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), W_Rook);
                        break;
                    case "Bishop":
                        AllPieces[PositionInArr] = new Bishop("White", PieceToPromote.Position, W_Bishop);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), W_Bishop);
                        break;
                    case "Knight":
                        AllPieces[PositionInArr] = new Knight("White", PieceToPromote.Position, W_Knight);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), W_Knight);
                        break;
                    case "Queen":
                        AllPieces[PositionInArr] = new Queen("White", PieceToPromote.Position, W_Queen);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), W_Queen);
                        break;
                    default:
                        break;
                }
                break;
            case "Black":
                switch (PieceToPromoteTo)
                {
                    case "Rook":
                        AllPieces[PositionInArr] = new Rook("Black", PieceToPromote.Position, B_Rook);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), B_Rook);
                        break;
                    case "Bishop":
                        AllPieces[PositionInArr] = new Bishop("Black", PieceToPromote.Position, B_Bishop);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), B_Bishop);
                        break;
                    case "Knight":
                        AllPieces[PositionInArr] = new Knight("Black", PieceToPromote.Position, B_Knight);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), B_Knight);
                        break;
                    case "Queen":
                        AllPieces[PositionInArr] = new Queen("Black", PieceToPromote.Position, B_Queen);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.Position[0], PieceToPromote.Position[1], 0), B_Queen);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    // Code that finds the piece position in the array that the player clicks on
    private int FindPosInArr(int[] PositionsToFind)
    {
        for (int i = 0; i < 64; i++)
        {
            if (AllPieces[i] != null && AllPieces[i].Position[0] == PositionsToFind[0] && AllPieces[i].Position[1] == PositionsToFind[1])
            {
                return i;
            }
        }
        return -1;
    }

    // Code for removing all 'ShowMove' tiles
    private void RemoveShowMoves()
    {
        // Traversing the tilmap graph starting from the bottom left corner and going up then left when the top is reached
        for (int i = Left; i <= Right; i++)
        {
            for (int j = Bottom; j <= Top; j++)
            {
                Vector3Int nullPos = new Vector3Int(i, j, 0);
                ShowMovesTilemap.SetTile(nullPos, null);
            }
        }
    }

    // Code for saving a piece
    private void NewPieceSelected()
    {
        RemoveShowMoves();
        movingPiece = AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })];
        cellPosLast = cellPosNew;
        movingPiece.Move();
    }

    // Code for setting castling options false
    private void CheckCastling()
    {
        switch (movingPiece.Type)
        {
            case "King":
                if (movingPiece.Colour == "White")
                {
                    King.CastlingOptions[0] = false;
                    King.CastlingOptions[1] = false;
                }
                else if (movingPiece.Colour == "Black")
                {
                    King.CastlingOptions[2] = false;
                    King.CastlingOptions[3] = false;
                }
                break;
            case "Rook":
                if (movingPiece.Colour == "White")
                {
                    if (movingPiece.Position[0] == Right)
                    {
                        King.CastlingOptions[0] = false;
                    }
                    else if (movingPiece.Position[0] == Left)
                    {
                        King.CastlingOptions[1] = false;
                    }
                }
                else if (movingPiece.Colour == "Black")
                {
                    if (movingPiece.Position[0] == Right)
                    {
                        King.CastlingOptions[2] = false;
                    }
                    else if (movingPiece.Position[0] == Left)
                    {
                        King.CastlingOptions[3] = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    // Code that changes the turn order
    private void PlayerTurn()
    {
        switch (PlayerToMove)
        {
            case "White":
                PlayerToMove = "Black";
                break;
            case "Black":
                PlayerToMove = "White";
                break;
            default:
                break;
        }
        MoveCount = MoveCount + 0.5;
    }

    // Code for taking the piece at the new cell position and replacing it with the last piece saved
    private void MoveAPiece()
    {
        RemoveShowMoves();
        if (ChessPiecesTilemap.GetTile(cellPosNew) != null)
        {
            AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })] = null;
            MoveCount = 0;
        }
        CheckCastling();
        ChessPiecesTilemap.SetTile(cellPosNew, ChessPiecesTilemap.GetTile(cellPosLast));
        AllPieces[FindPosInArr(new int[2] { cellPosLast.x, cellPosLast.y })].Position = new int[2] { cellPosNew.x, cellPosNew.y };
        if (movingPiece.Type == "Pawn")
        {
            if (Pawn.EnPassantSquare[0] == cellPosNew.x && Pawn.EnPassantSquare[1] == cellPosNew.y && movingPiece.Colour == "White")
            {
                AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y - 1 })] = null;
                ChessPiecesTilemap.SetTile(new Vector3Int(cellPosNew.x, cellPosNew.y - 1, cellPosNew.z), null);
            }
            else if (Pawn.EnPassantSquare[0] == cellPosNew.x && Pawn.EnPassantSquare[1] == cellPosNew.y && movingPiece.Colour == "Black")
            {
                AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y + 1 })] = null;
                ChessPiecesTilemap.SetTile(new Vector3Int(cellPosNew.x, cellPosNew.y + 1, cellPosNew.z), null);
            }
            else if (movingPiece.Position[1] == Top || movingPiece.Position[1] == Bottom)
            {
                Promotion(movingPiece, FindPosInArr(new int[2] { movingPiece.Position[1], movingPiece.Position[1] }), "Queen");
            }
            for (int i = -1; i <= 1; i += 2)
            {
                if (cellPosLast.y == cellPosNew.y + i * 2)
                {
                    Pawn.EnPassantSquare = new int[2] { cellPosNew.x, cellPosNew.y + i };
                    break;
                }
                else
                {
                    Pawn.EnPassantSquare = new int[2] { 0, 0 };
                }
            }
            MoveCount = 0;
        }
        else if (movingPiece.Type == "King")
        {
            for (int i = -1; i <= 1; i += 2)
            {
                if (cellPosLast.x == cellPosNew.x + i * 2)
                {
                    int PositionInArray = -1;
                    switch (i)
                    {
                        case -1:
                            PositionInArray = FindPosInArr(new int[2] { 8, cellPosNew.y });
                            ChessPiecesTilemap.SetTile(new Vector3Int(8, cellPosNew.y, cellPosNew.z), null);
                            break;
                        case 1:
                            PositionInArray = FindPosInArr(new int[2] { 1, cellPosNew.y });
                            ChessPiecesTilemap.SetTile(new Vector3Int(1, cellPosNew.y, cellPosNew.z), null);
                            break;
                        default:
                            break;
                    }
                    AllPieces[PositionInArray].Position = new int[2] { cellPosNew.x + i, cellPosNew.y };
                    if (movingPiece.Colour == "White")
                    {
                        ChessPiecesTilemap.SetTile(new Vector3Int(cellPosNew.x + i, cellPosNew.y, cellPosNew.z), W_Rook);
                    }
                    else
                    {
                        ChessPiecesTilemap.SetTile(new Vector3Int(cellPosNew.x + i, cellPosNew.y, cellPosNew.z), B_Rook);
                    }
                    break;
                }
            }
        }
        movingPiece = null;
        ChessPiecesTilemap.SetTile(cellPosLast, null);
        PlayerTurn();
    }

    //Sets up the Chessboard Tilemap with the appropriate theme
    private void ThemeSetup(int Theme)
    {
        TileBase Light;
        TileBase Dark;
        switch (Theme)
        {
            case 1:
                Light = Green_Light;
                Dark = Green_Dark;
                break;
            case 2:
                Light = Brown_Light;
                Dark = Brown_Dark;
                break;
            case 3:
                Light = Black_Light;
                Dark = Black_Dark;
                break;
            default:
                Light = null;
                Dark = null;
                break;
        }
        for (int i = Top; i >= Bottom; i--)
        {
            for (int j = Left; j <= Right; j++)
            {
                if ((j + i) % 2 == 1 || (j + i) % 2 == -1)
                {
                    Vector3Int Position = new Vector3Int(j, i, 0);
                    ChessBoardTilemap.SetTile(Position, Dark);
                }
                else
                {
                    Vector3Int Position = new Vector3Int(j, i, 0);
                    ChessBoardTilemap.SetTile(Position, Light);
                }
            }
        }
    }

    //Counts for ReadFEN
    private int[] Counter(int[] IntArray)
    {
        IntArray[0]++;
        IntArray[1]++;
        IntArray[3]++;
        return IntArray;
    }

    //Reads FEN and sets up all pieces
    private void ReadFEN(string FEN)
    {
        bool IsBoardSetup = false;
        int[] Values = { 0, 0, Top, Left };
        int[] Position;
        while (!IsBoardSetup)
        {
            if (Values[2] >= Bottom && Values[3] <= Right + 1)
            {
                switch (BoardNotation[Values[0]])
                {
                    case 'P':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Pawn("White", Position, W_Pawn);
                        Values = Counter(Values);
                        break;
                    case 'R':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Rook("White", Position, W_Rook);
                        Values = Counter(Values);
                        break;
                    case 'B':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Bishop("White", Position, W_Bishop);
                        Values = Counter(Values);
                        break;
                    case 'N':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Knight("White", Position, W_Knight);
                        Values = Counter(Values);
                        break;
                    case 'K':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new King("White", Position, W_King);
                        Values = Counter(Values);
                        break;
                    case 'Q':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Queen("White", Position, W_Queen);
                        Values = Counter(Values);
                        break;
                    case 'p':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Pawn("Black", Position, B_Pawn);
                        Values = Counter(Values);
                        break;
                    case 'r':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Rook("Black", Position, B_Rook);
                        Values = Counter(Values);
                        break;
                    case 'b':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Bishop("Black", Position, B_Bishop);
                        Values = Counter(Values);
                        break;
                    case 'n':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Knight("Black", Position, B_Knight);
                        Values = Counter(Values);
                        break;
                    case 'k':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new King("Black", Position, B_King);
                        Values = Counter(Values);
                        break;
                    case 'q':
                        Position = new int[2] { Values[3], Values[2] };
                        AllPieces[Values[1]] = new Queen("Black", Position, B_Queen);
                        Values = Counter(Values);
                        break;
                    case '/':
                        Values[0]++;
                        Values[2]--;
                        Values[3] = Left;
                        break;
                    default:
                        if (char.IsNumber(BoardNotation, Values[0]))
                        {
                            Values[3] = Values[3] + int.Parse(BoardNotation[Values[0]].ToString());
                            Values[0]++;
                        }
                        else
                        {
                            Values[1]++;
                            Values[0]++;
                            Values[3]++;
                        }
                        break;
                }
            }
            else
            {
                switch (BoardNotation[Values[0]])
                {
                    case 'w':
                        PlayerToMove = "White";
                        break;
                    case 'b':
                        PlayerToMove = "Black";
                        MoveCount += 0.5;
                        break;
                    default:
                        break;
                }
                for (int i = 2; i <= 5; i++)
                {
                    char Letter = ' ';
                    switch (i)
                    {
                        case 2:
                            Letter = 'K';
                            break;
                        case 3:
                            Letter = 'Q';
                            break;
                        case 4:
                            Letter = 'k';
                            break;
                        case 5:
                            Letter = 'q';
                            break;
                        default:
                            break;
                    }
                    if (BoardNotation[Values[0] + i] == Letter)
                    {
                        King.CastlingOptions[i - 2] = true;
                    }
                }
                if (BoardNotation[Values[0] + 7] != '-')
                {
                    Pawn.EnPassantSquare = new int[2] { Left + BoardToInt[BoardNotation[Values[0] + 7]], int.Parse(BoardNotation[Values[0] + 8].ToString()) };
                    Debug.Log(Pawn.EnPassantSquare[0]);
                    Debug.Log(Pawn.EnPassantSquare[1]);
                    DrawMoveCount += BoardNotation[Values[0] + 10];
                    MoveCount += BoardNotation[Values[0] + 12];
                }
                else
                {
                    DrawMoveCount += BoardNotation[Values[0] + 9];
                    MoveCount += BoardNotation[Values[0] + 11];
                }
                IsBoardSetup = true;
            }
        }
    }

    // Code that sets up pieces
    private void SetParameters()
    {
        Pieces.Top = Top;
        Pieces.Bottom = Bottom;
        Pieces.Right = Right;
        Pieces.Left = Left;
        Pieces.ChessPiecesTilemap = ChessPiecesTilemap;
        Pieces.ShowMovesTilemap = ShowMovesTilemap;
        Pieces.ShowMove = ShowMove;
    }

    // Code is ran once when the object is created
    private void Start()
    {
        SetParameters();
        for (int i = 0; i < 64; i++)
        {
            AllPieces[i] = null;
        }
        ThemeSetup(2);
        ReadFEN(BoardNotation);
    }

    // Code that runs every frame
    private void Update()
    {
        // Converting the 3d vector position to the tilemap 3d vector which is in integers
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPosNew = ChessPiecesTilemap.WorldToCell(worldPos);
        if (Input.GetMouseButtonDown(0) && cellPosNew.x >= Left && cellPosNew.x <= Right && cellPosNew.y >= Bottom && cellPosNew.y <= Top)
        {
            // When no piece has been selected and a piece is clicked
            if (ChessPiecesTilemap.GetTile(cellPosNew) != null && movingPiece == null && PlayerToMove == AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })].Colour)
            {
                NewPieceSelected();
            }
            // When the piece is moving to a legal place and there is a piece being saved
            else if (movingPiece != null && ShowMovesTilemap.GetTile(cellPosNew) == ShowMove)
            {
                MoveAPiece();
            }
            // Checking if the piece is the same and saving that piece as the new piece
            else if (ChessPiecesTilemap.GetTile(cellPosNew) != null && PlayerToMove == AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })].Colour)
            {
                NewPieceSelected();
            }
        }
    }
}
