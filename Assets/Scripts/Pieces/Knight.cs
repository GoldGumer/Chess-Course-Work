using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Knight : Pieces
{
    public Knight(string GivenColour, int[] StartingPosition, TileBase GivenTile) : base("Knight", GivenColour, StartingPosition, GivenTile) { }
    public override void Move()
    {
        // Logic for the Knight movements
        (bool, bool) Checker;
        string[] allDirection = { "Right", "Left", "Up", "Down" };
        for (int i = 0; i <= 3; i++)
        {
            int[] posOne;
            int[] posTwo;
            string[] specificDirection;
            switch (i)
            {
                case 0:
                    posOne = new int[2] { 2, 1 };
                    posTwo = new int[2] { 2, -1 };
                    specificDirection = new string[2] { allDirection[2], allDirection[3] };
                    break;
                case 1:
                    posOne = new int[2] { -2, 1 };
                    posTwo = new int[2] { -2, -1 };
                    specificDirection = new string[2] { allDirection[2], allDirection[3] };
                    break;
                case 2:
                    posOne = new int[2] { 1, 2 };
                    posTwo = new int[2] { -1, 2 };
                    specificDirection = new string[2] { allDirection[0], allDirection[1] };
                    break;
                case 3:
                    posOne = new int[2] { 1, -2 };
                    posTwo = new int[2] { -1, -2 };
                    specificDirection = new string[2] { allDirection[0], allDirection[1] };
                    break;
                default:
                    posOne = new int[2] { 0, 0 };
                    posTwo = new int[2] { 0, 0 };
                    specificDirection = new string[2] { "default", "default" };
                    break;
            }
            Checker = CheckIfBlocked(posOne[0], posOne[1]);
            if (CheckCellPos(allDirection[i], 2) && CheckCellPos(specificDirection[0], 1) && (Checker.Item1 || Checker.Item2))
            {
                PlaceShowMoves(posOne[0], posOne[1]);
            }
            Checker = CheckIfBlocked(posTwo[0], posTwo[1]);
            if (CheckCellPos(allDirection[i], 2) && CheckCellPos(specificDirection[1], 1) && (Checker.Item1 || Checker.Item2))
            {
                PlaceShowMoves(posTwo[0], posTwo[1]);
            }
        }
    }
}
