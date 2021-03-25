using UnityEngine;
using UnityEngine.Tilemaps;

public class King : Pieces
{
    static public bool[] CastlingOptions = { false, false, false, false };
    public King(string GivenColour, int[] StartingPosition, TileBase GivenTile) : base("King", GivenColour, StartingPosition, GivenTile) { }
    public override void Move()
    {
        // King Movements
        (bool, bool) Checker;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Checker = CheckIfBlocked(i, j);
                if (!(i == 0 && j == 0) && (this.Position[0] + i <= Right && this.Position[0] + i >= Left && this.Position[1] + j <= Top && this.Position[1] + j >= Bottom) && Checker.Item1 || Checker.Item2)
                {
                    PlaceShowMoves(i, j);
                }
            }
        }
        int Side;
        switch(this.Colour)
        {
            case "White":
                Side = 0;
                break;
            case "Black":
                Side = 2;
                break;
            default:
                Side = 10000;
                break;
        }
        if (CastlingOptions[Side])
        {
            if(ChessPiecesTilemap.GetTile(new Vector3Int(this.Position[0] + 1, this.Position[1], 0)) && ChessPiecesTilemap.GetTile(new Vector3Int(this.Position[0] + 2, this.Position[1], 0)))
            {

            }
        }
        if (CastlingOptions[Side + 1])
        {

        }
    }
}