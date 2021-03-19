using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Pieces
{
    public string Type { get; }
    public string Colour { get; }
    public int[] Position { get; set; }
    public TileBase Tile;
    static public int Top;
    static public int Bottom;
    static public int Right;
    static public int Left;

    protected Pieces(Tilemap ChessPiecesTilemap, string GivenType, string GivenColour, int[] StartingPosition, TileBase GivenTile)
    {
        this.Type = GivenType;
        this.Colour = GivenColour;
        this.Position = StartingPosition;
        this.Tile = GivenTile;
        Vector3Int VectorPos = new Vector3Int(this.Position[0], this.Position[1], 0);
        ChessPiecesTilemap.SetTile(VectorPos, this.Tile);
    }

    //Code for Placing 'ShowMove' tiles
    protected void PlaceShowMoves(Tilemap ShowMovesTilemap, TileBase ShowMove, int xPosition, int yPosition)
    {
        ShowMovesTilemap.SetTile(new Vector3Int(this.Position[0] + xPosition, this.Position[1] + yPosition, 0), ShowMove);
    }

    //Code for Checking if the new position is on the board
    protected bool CheckCellPos(string Direction, int Displacement)
    {
        switch (Direction)
        {
            case "Right":
                break;
        }
        if (Direction == "Right")
        {
            return (this.Position[0] + Displacement <= Right);
        }
        else if (Direction == "Left")
        {
            return (this.Position[0] - Displacement >= Left);
        }
        else if (Direction == "Up")
        {
            return (this.Position[1] + Displacement <= Top);
        }
        else if (Direction == "Down")
        {
            return (this.Position[1] - Displacement >= Bottom);
        }
        else
        {
            return (false);
        }
    }

    //Code for checking if the piece is blocked
    protected (bool, bool) CheckIfBlocked(Tilemap ChessPiecesTilemap, int xDirection, int yDirection)
    {
        (bool, bool) Output;
        Vector3Int testPiecePos = new Vector3Int(this.Position[0] + xDirection, this.Position[1] + yDirection, 0);
        bool BoolOne = ChessPiecesTilemap.GetTile(testPiecePos) == null;
        if (!BoolOne)
        {
            bool BoolTwo = ChessPiecesTilemap.GetTile(testPiecePos).name.Substring(0,5) != this.Colour;
            Output = (BoolOne, BoolTwo);
        }
        else
        {
            Output = (BoolOne, false);
        }
        return Output;
    }

    ////Returns the tile
    //public TileBase GetTile()
    //{
    //    return this.Tile;
    //}

    ////Returns the positions of the piece
    //public int[] GetPosition()
    //{
    //    return this.Position;
    //}

    ////Changes the position
    //public void SetPosition(int[] NewPosition)
    //{
    //    this.Position = NewPosition;
    //}

    ////Returns piece type
    //public string GetPieceType()
    //{
    //    return this.Type;
    //}

    ////Returns Piece Colour
    //public string GetColour()
    //{
    //    return this.Colour;
    //}

    public abstract void Move(Tilemap ChessPiecesTilemap, Tilemap ShowMovesTilemap, TileBase ShowMove);
}