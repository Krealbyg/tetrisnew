using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Block
{
    protected bool[,] bGrid;
    protected Color color;
    public Vector2 position = new Vector2(2, 0);
    protected Texture2D cell;
    TetrisGrid grid = new TetrisGrid();

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

    public bool BCol()
    {
        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                if (bGrid[x, y] == true && position.Y + y >= grid.Height)
                    return true;
            }
        }
        return false;
    }

    public bool WCol(int z)
    {
        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                if (bGrid[x, y] == true && position.X + x + 1 > grid.Width && z == 1 || bGrid[x, y] == true && position.X + x < 0 && z == 0)
                    return true;
            }
        }
        return false;
    }

    public void MoveL()
    {
        position.X--;
    }

    public void MoveR()
    {
        position.X++;
    }

    public void Fall()
    {
        position.Y++;
    }
}
