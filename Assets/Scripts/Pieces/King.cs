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
    }
}
