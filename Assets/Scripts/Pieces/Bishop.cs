using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bishop : Pieces
{
    public Bishop(string GivenColour, int[] StartingPosition, TileBase GivenTile) : base("Bishop", GivenColour, StartingPosition, GivenTile) { }
    public override void Move()
    {
        // Logic of how a Bishop can move
        for (int j = 0; j <= 3; j++)
        {
            bool notBlocked = true;
            (bool, bool) Checker;
            string[] allDirections = { "Right", "Left", "Up", "Down" };
            string[] specificDirection;
            int[] positionMultipliers;
            switch (j)
            {
                case 0:
                    positionMultipliers = new int[2] { 1, 1 };
                    specificDirection = new string[2] { allDirections[0], allDirections[2] };
                    break;
                case 1:
                    positionMultipliers = new int[2] { 1, -1 };
                    specificDirection = new string[2] { allDirections[0], allDirections[3] };
                    break;
                case 2:
                    positionMultipliers = new int[2] { -1, 1 };
                    specificDirection = new string[2] { allDirections[1], allDirections[2] };
                    break;
                case 3:
                    positionMultipliers = new int[2] { -1, -1 };
                    specificDirection = new string[2] { allDirections[1], allDirections[3] };
                    break;
                default:
                    positionMultipliers = new int[2] { 0, 0 };
                    specificDirection = new string[2] { "default", "default" };
                    break;
            }
            for (int i = 1; i < 8; i++)
            {
                int[] position = { i * positionMultipliers[0], i * positionMultipliers[1] };
                Checker = CheckIfBlocked(position[0], position[1]);
                if (notBlocked && !(CheckCellPos(specificDirection[0], i) && CheckCellPos(specificDirection[1], i)))
                {
                    notBlocked = false;
                }
                else if (notBlocked && Checker.Item1)
                {
                    PlaceShowMoves(position[0], position[1]);
                }
                else if (notBlocked && Checker.Item2)
                {
                    PlaceShowMoves(position[0], position[1]);
                    notBlocked = false;
                }
                else
                {
                    notBlocked = false;
                }
            }
        }
    }
}
