using UnityEngine.Tilemaps;

public class Queen : Pieces
{
    public Queen(string GivenColour, int[] StartingPosition, TileBase GivenTile) : base("Queen", GivenColour, StartingPosition, GivenTile) { }
    public override void Move()
    {
        for (int j = 0; j <= 3; j++)
        {
            bool notBlockedStraight = true;
            bool notBlockedDiagonal = true;
            (bool, bool) Checker;
            string[] allDirections = { "Right", "Left", "Up", "Down" };
            string[] specificDirection;
            int[] positionMultipliers;
            switch (j)
            {
                case 0:
                    specificDirection = new string[2] { allDirections[0], allDirections[2] };
                    positionMultipliers = new int[4] { 1, 0, 1, 1 };
                    break;
                case 1:
                    specificDirection = new string[2] { allDirections[1], allDirections[3] };
                    positionMultipliers = new int[4] { -1, 0, -1, -1 };
                    break;
                case 2:
                    specificDirection = new string[2] { allDirections[2], allDirections[1] };
                    positionMultipliers = new int[4] { 0, 1, -1, 1 };
                    break;
                case 3:
                    specificDirection = new string[2] { allDirections[3], allDirections[0] };
                    positionMultipliers = new int[4] { 0, -1, 1, -1 };
                    break;
                default:
                    specificDirection = new string[2] { "default", "default" };
                    positionMultipliers = new int[4] { 0, 0, 0, 0 };
                    break;
            }
            for (int i = 1; i < 8; i++)
            {
                int[] position = { i * positionMultipliers[0], i * positionMultipliers[1], i * positionMultipliers[2], i * positionMultipliers[3] };
                //Straights
                Checker = CheckIfBlocked(position[0], position[1]);
                if (notBlockedStraight && !CheckCellPos(specificDirection[0], i))
                {
                    notBlockedStraight = false;
                }
                else if (notBlockedStraight && Checker.Item1)
                {
                    PlaceShowMoves(position[0], position[1]);
                }
                else if (notBlockedStraight && Checker.Item2)
                {
                    PlaceShowMoves(position[0], position[1]);
                    notBlockedStraight = false;
                }
                else
                {
                    notBlockedStraight = false;
                }
                //Diagonals
                Checker = CheckIfBlocked(position[2], position[3]);
                if (notBlockedDiagonal && !(CheckCellPos(specificDirection[0], i) && CheckCellPos(specificDirection[1], i)))
                {
                    notBlockedDiagonal = false;
                }
                else if (notBlockedDiagonal && Checker.Item1)
                {
                    PlaceShowMoves(position[2], position[3]);
                }
                else if (notBlockedDiagonal && Checker.Item2)
                {
                    PlaceShowMoves(position[2], position[3]);
                    notBlockedDiagonal = false;
                }
                else
                {
                    notBlockedDiagonal = false;
                }
            }
        }
    }
}
