using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Block
{
    protected bool[,] bGrid;
    protected Color color;
    protected Vector2 position = new Vector2(2, 0);
    protected Texture2D cell;

    public Block()
    {
        cell = TetrisGame.ContentManager.Load<Texture2D>("block");
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                if (bGrid[x, y] == true)
                    spriteBatch.Draw(cell, new Vector2((x + position.X) * cell.Width, (y + position.Y) * cell.Height), color);
            }
        }
    }

    public void RotateR()
    {
        bool[,] temp = new bool[bGrid.GetLength(0), bGrid.GetLength(1)];

        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                temp[x, y] = bGrid[y, bGrid.GetLength(1) - 1 - x];
            }
        }
        bGrid = temp;
    }
    public void RotateL()
    {
        bool[,] temp = new bool[bGrid.GetLength(0), bGrid.GetLength(1)];

        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                temp[x, y] = bGrid[bGrid.GetLength(1) - 1 - y, x];
            }
        }
        bGrid = temp;
    }

    public void MoveL()
    {
        if (position.X > 0)
            position.X--;
    }

    public void MoveR()
    {
        if (position.X < 10 - bGrid.GetLength(0))
            position.X++;
    }

    public void Fall()
    {
        position.Y++;
    }
}
