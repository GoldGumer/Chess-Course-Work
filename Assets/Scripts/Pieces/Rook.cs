using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rook : Pieces
{
    public Rook(Tilemap ChessPiecesTilemap, string GivenColour, int[] StartingPosition, TileBase GivenTile) : base(ChessPiecesTilemap, "Rook", GivenColour, StartingPosition, GivenTile) { }
    public override void Move(Tilemap ChessPiecesTilemap, Tilemap ShowMovesTilemap, TileBase ShowMove)
    {
        // Logic of how a Rook can move
        (bool, bool) Checker;
        for (int j = 0; j <= 3; j++)
        {
            bool notBlocked = true;
            string[] directions = { "Right", "Left", "Up", "Down" };
            int[] posMultipliers;
            switch (j)
            {
                case 0:
                    posMultipliers = new int[2] { 1, 0 };
                    break;
                case 1:
                    posMultipliers = new int[2] { -1, 0 };
                    break;
                case 2:
                    posMultipliers = new int[2] { 0, 1 };
                    break;
                case 3:
                    posMultipliers = new int[2] { 0, -1 };
                    break;
                default:
                    posMultipliers = new int[2] { 0, 0 };
                    break;
            }
            for (int i = 1; i < 8; i++)
            {
                int[] position = { i * posMultipliers[0], i * posMultipliers[1] };
                Checker = CheckIfBlocked(ChessPiecesTilemap, position[0], position[1]);
                if (notBlocked && !CheckCellPos(directions[j], i))
                {
                    notBlocked = false;
                }
                else if (notBlocked && Checker.Item1)
                {
                    PlaceShowMoves(ShowMovesTilemap, ShowMove, position[0], position[1]);
                }
                else if (notBlocked && Checker.Item2)
                {
                    PlaceShowMoves(ShowMovesTilemap, ShowMove, position[0], position[1]);
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
