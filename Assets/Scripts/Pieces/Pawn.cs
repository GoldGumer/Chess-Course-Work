using UnityEngine.Tilemaps;

public class Pawn : Pieces
{
    static public int[] EnPassantSquare = new int[2];
    public Pawn(string GivenColour, int[] StartingPosition, TileBase GivenTile) : base("Pawn", GivenColour, StartingPosition, GivenTile) { }
    public override void Move()
    {
        // Logic for a pawn movement
        int Side;
        bool BoolCellPos;
        switch (this.Colour)
        {
            case "White":
                BoolCellPos = CheckCellPos("Up", 2);
                Side = 1;
                break;
            case "Black":
                BoolCellPos = CheckCellPos("Down", 2);
                Side = -1;
                break;
            default:
                BoolCellPos = false;
                Side = 0;
                break;
        }
        (bool, bool) Checker = CheckIfBlocked(0, Side);
        if (Checker.Item1)
        {
            Checker = CheckIfBlocked(0, 2 * Side);
            if (Checker.Item1 && (this.Position[1] == Top - 1 || this.Position[1] == Bottom + 1) && BoolCellPos)
            {
                PlaceShowMoves(0, 2 * Side);
            }
            PlaceShowMoves(0, Side);
        }
        Checker = CheckIfBlocked(Side, Side);
        if (Checker.Item2)
        {
            PlaceShowMoves(Side, Side);
        }
        Checker = CheckIfBlocked(-Side, Side);
        if (Checker.Item2)
        {
            PlaceShowMoves(-Side, Side);
        }
    }
}
