using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class King : Pieces
{
    static public bool[] CastlingOptions = { false, false, false, false };
    public King(Tilemap ChessPiecesTilemap, string GivenColour, int[] StartingPosition, TileBase GivenTile) : base(ChessPiecesTilemap, "King", GivenColour, StartingPosition, GivenTile) { }
    public override void Move(Tilemap ChessPiecesTilemap, Tilemap ShowMovesTilemap, TileBase ShowMove)
    {
        // King Movements
        (bool, bool) Checker;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Checker = CheckIfBlocked(ChessPiecesTilemap, i, j);
                if (!(i == 0 && j == 0) && (this.Position[0] + i <= Right && this.Position[0] + i >= Left && this.Position[1] + j <= Top && this.Position[1] + j >= Bottom) && Checker.Item1 || Checker.Item2)
                {
                    PlaceShowMoves(ShowMovesTilemap, ShowMove, i, j);
                }
            }
        }
    }
}
