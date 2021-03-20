using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pawn : Pieces
{
    static public int[] EnPassantSquare = new int[2];
    public Pawn(string GivenColour, int[] StartingPosition, TileBase GivenTile) : base("Pawn", GivenColour, StartingPosition, GivenTile) { }
    public override void Move()
    {
        // Logic for a pawn movement
        int Side;
        bool[] BoolCellPos;
        switch (this.Colour)
        {
            case "White":
                BoolCellPos = new bool[2] { CheckCellPos("Up", 1), CheckCellPos("Up", 2) };
                Side = 1;
                break;
            case "Black":
                BoolCellPos = new bool[2] { CheckCellPos("Down", 1), CheckCellPos("Down", 2) };
                Side = -1;
                break;
            default:
                BoolCellPos = new bool[2] { false, false };
                Side = 0;
                break;
        }
        (bool, bool) Checker = CheckIfBlocked(0, Side);
        if(BoolCellPos[0] && Checker.Item1)
        {
            PlaceShowMoves(0, Side);
            Checker = CheckIfBlocked(0, Side*2);
            if (BoolCellPos[1] && Checker.Item1 && this.Position[1] == (2.5*Side*-1-0.5))
            {
                PlaceShowMoves(0, Side * 2);
            }
        }
        Checker = CheckIfBlocked(1, Side);
        if(BoolCellPos[0] && Checker.Item2)
        {
            PlaceShowMoves(1, Side);
        }
        Checker = CheckIfBlocked(-1, Side);
        if(BoolCellPos[0] && Checker.Item2)
        {
            PlaceShowMoves(-1, Side);
        }
    }
}
