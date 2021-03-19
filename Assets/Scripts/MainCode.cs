﻿using System.Collections;
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
    private TileBase Tile;
    private int Top = 3;
    private int Bottom = -4;
    private int Right = 3;
    private int Left = -4;
    private string PlayerToMove;
    private double MoveCount = 0;
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
        switch (PieceToPromote.GetColour())
        {
            case "White":
                switch (PieceToPromoteTo)
                {
                    case "Rook":
                        AllPieces[PositionInArr] = new Rook(ChessPiecesTilemap, "White", PieceToPromote.GetPosition(), W_Rook);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), W_Rook);
                        break;
                    case "Bishop":
                        AllPieces[PositionInArr] = new Bishop(ChessPiecesTilemap, "White", PieceToPromote.GetPosition(), W_Bishop);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), W_Bishop);
                        break;
                    case "Knight":
                        AllPieces[PositionInArr] = new Knight(ChessPiecesTilemap, "White", PieceToPromote.GetPosition(), W_Knight);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), W_Knight);
                        break;
                    case "Queen":
                        AllPieces[PositionInArr] = new Queen(ChessPiecesTilemap, "White", PieceToPromote.GetPosition(), W_Queen);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), W_Queen);
                        break;
                    default:
                        break;
                }
                break;
            case "Black":
                switch (PieceToPromoteTo)
                {
                    case "Rook":
                        AllPieces[PositionInArr] = new Rook(ChessPiecesTilemap, "Black", PieceToPromote.GetPosition(), B_Rook);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), B_Rook);
                        break;
                    case "Bishop":
                        AllPieces[PositionInArr] = new Bishop(ChessPiecesTilemap, "Black", PieceToPromote.GetPosition(), B_Bishop);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), B_Bishop);
                        break;
                    case "Knight":
                        AllPieces[PositionInArr] = new Knight(ChessPiecesTilemap, "Black", PieceToPromote.GetPosition(), B_Knight);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), B_Knight);
                        break;
                    case "Queen":
                        AllPieces[PositionInArr] = new Queen(ChessPiecesTilemap, "Black", PieceToPromote.GetPosition(), B_Queen);
                        ChessPiecesTilemap.SetTile(new Vector3Int(PieceToPromote.GetPosition()[0], PieceToPromote.GetPosition()[1], 0), B_Queen);
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
        for(int i = 0; i < 64; i++)
        {
            if (AllPieces[i] != null && AllPieces[i].GetPosition()[0] == PositionsToFind[0] && AllPieces[i].GetPosition()[1] == PositionsToFind[1])
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
        movingPiece.Move(ChessPiecesTilemap, ShowMovesTilemap, ShowMove);
    }

    // Code for taking the piece at the new cell position and replacing it with the last piece saved
    private void MoveAPiece(bool Taking)
    {
        RemoveShowMoves();
        ChessPiecesTilemap.SetTile(cellPosNew, ChessPiecesTilemap.GetTile(cellPosLast));
        if (Taking)
        {
            AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })] = null;
            MoveCount = 0;
        }
        if (movingPiece.GetPieceType() == "Pawn")
        {
            if (cellPosNew.y == Top || cellPosNew.y == Bottom)
            {
                Promotion(movingPiece, FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y }), "Queen");
            }
            MoveCount = 0;
        }
        AllPieces[FindPosInArr(new int[2] { cellPosLast.x, cellPosLast.y })].SetPosition(new int[2] { cellPosNew.x, cellPosNew.y });
        movingPiece = null;
        ChessPiecesTilemap.SetTile(cellPosLast, null);
        switch(PlayerToMove)
        {
            case 'White':
                PlayerToMove = "Black";
                break;
            case 'Black':
                PlayerToMove = "White";
                break;
            default:
                break;
        }
        MoveCount = MoveCount + 0.5;
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
                if ((j+i)%2 == 1 || (j+i)%2 == -1)
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
            if (Values[0] < BoardNotation.Length)
            {
                if (Values[2] >= Bottom && Values[3] <= Right+1)
                {
                    switch (BoardNotation[Values[0]])
                    {
                        case 'P':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Pawn(ChessPiecesTilemap, "White", Position, W_Pawn);
                            Values = Counter(Values);
                            break;
                        case 'R':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Rook(ChessPiecesTilemap, "White", Position, W_Rook);
                            Values = Counter(Values);
                            break;
                        case 'B':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Bishop(ChessPiecesTilemap, "White", Position, W_Bishop);
                            Values = Counter(Values);
                            break;
                        case 'N':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Knight(ChessPiecesTilemap, "White", Position, W_Knight);
                            Values = Counter(Values);
                            break;
                        case 'K':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new King(ChessPiecesTilemap, "White", Position, W_King);
                            Values = Counter(Values);
                            break;
                        case 'Q':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Queen(ChessPiecesTilemap, "White", Position, W_Queen);
                            Values = Counter(Values);
                            break;
                        case 'p':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Pawn(ChessPiecesTilemap, "Black", Position, B_Pawn);
                            Values = Counter(Values);
                            break;
                        case 'r':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Rook(ChessPiecesTilemap, "Black", Position, B_Rook);
                            Values = Counter(Values);
                            break;
                        case 'b':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Bishop(ChessPiecesTilemap, "Black", Position, B_Bishop);
                            Values = Counter(Values);
                            break;
                        case 'n':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Knight(ChessPiecesTilemap, "Black", Position, B_Knight);
                            Values = Counter(Values);
                            break;
                        case 'k':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new King(ChessPiecesTilemap, "Black", Position, B_King);
                            Values = Counter(Values);
                            break;
                        case 'q':
                            Position = new int[2] { Values[3], Values[2] };
                            AllPieces[Values[1]] = new Queen(ChessPiecesTilemap, "Black", Position, B_Queen);
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
                    switch(BoardNotation[Values[0]])
                    {
                        case 'w':
                            PlayerToMove = "White";
                            break;
                        case 'b':
                            PlayerToMove = "Black";
                            break;
                        default:
                            break;
                    }
                    Values[0]++;
                }
            }
            else
            {
                IsBoardSetup = true;
            }
        }
    }

    // Code is ran once when the object is created
    private void Start()
    {
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
            if (ChessPiecesTilemap.GetTile(cellPosNew) != null && movingPiece == null && PlayerToMove == AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })].GetColour())
            {
                NewPieceSelected();
            }
            // When the piece is moving to a legal place and there is a piece being saved
            else if (ChessPiecesTilemap.GetTile(cellPosNew) == null && movingPiece != null && ShowMovesTilemap.GetTile(cellPosNew) == ShowMove)
            {
                MoveAPiece(false);
            }
            // Checking if the piece is the same and saving that piece as the new piece
            else if (ChessPiecesTilemap.GetTile(cellPosNew) != null  && PlayerToMove == AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })].GetColour())
            {
                NewPieceSelected();
            }
            // Checking if the piece is the opponents piece and taking it
            else if (ChessPiecesTilemap.GetTile(cellPosNew) != null && PlayerToMove != AllPieces[FindPosInArr(new int[2] { cellPosNew.x, cellPosNew.y })].GetColour() && ShowMovesTilemap.GetTile(cellPosNew) == ShowMove)
            {
                MoveAPiece(true);
            }
        }
    }
}
