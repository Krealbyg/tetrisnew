using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

abstract class Block
{
    public bool[,] bGrid;
    public bool[,] prevGrid;
    

    public Color color;
    public Vector2 position = new Vector2(2, 0);
    public Vector2 prevPos;
    protected Texture2D cell;
    
    public Block()
    {
        cell = TetrisGame.ContentManager.Load<Texture2D>("block");
        prevPos = position;
  
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

    public bool [,] RotateR(bool[,] rotR)
    {
        bool[,] temp = new bool[rotR.GetLength(0), rotR.GetLength(1)];

        for (int x = 0; x < rotR.GetLength(0); x++)
        {
            for (int y = 0; y < rotR.GetLength(1); y++)
            {
                temp[x, y] = rotR[y, rotR.GetLength(1) - 1 - x];
            }
        }
        for (int x = 0; x < temp.GetLength(0); x++)
        {
            for (int y = 0; y < temp.GetLength(1); y++)
            {
                while (temp[x, y] == true && position.X + x + 1 > (10))
                    position.X--;
                while (temp[x, y] == true && position.X + x - 0 < 0)
                    position.X++;
            }
        }
        return temp;
    }
    public bool[,] RotateL(bool[,] rotL)
    {
        bool[,] temp = new bool[rotL.GetLength(0), rotL.GetLength(1)];

        for (int x = 0; x < rotL.GetLength(0); x++)
        {
            for (int y = 0; y < rotL.GetLength(1); y++)
            {
                temp[x, y] = rotL[rotL.GetLength(0) - 1 - y, x];
            }
        }
        for (int x = 0; x < temp.GetLength(0); x++)
        {
            for (int y = 0; y < temp.GetLength(1); y++)
            {
                while (temp[x, y] == true && position.X + x + 1 > (10))
                    position.X--;
                while (temp[x, y] == true && position.X + x - 0 < 0)
                    position.X++;
            }
        }
        return temp;
    }

    public void PrevRot(bool[,] rot)
    {
        prevGrid = new bool[rot.GetLength(0), rot.GetLength(1)];
        for (int x = 0; x < rot.GetLength(0); x++)
        {
            for (int y = 0; y < rot.GetLength(1); y++)
            {
                if (rot[x, y] == true)
                    prevGrid[x, y] = true;
                else
                    prevGrid[x, y] = false;
            }
        }
    }

    public bool BCol()
    {
        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                if (bGrid[x, y] == true && position.Y + y == 19)
                {
                    return true;
                }
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
                if (bGrid[x, y] == true && position.X + x + 2 > (10) && z == 1 || bGrid[x, y] == true && position.X + x - 1 < 0 && z == 0)
                    return true;
            }
        }
        return false;
    }

    public void MoveL()
    {
        prevPos = position;
        if (!WCol(0))
            position.X--;
    }

    public void MoveR()
    {
        prevPos = position;
        if (!WCol(1))
            position.X++;
    }

    public void Fall()
    {
        prevPos = position;
        if (BCol() == false)
            position.Y++;
    }
    public void Update(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.Ticks % 60 == 1 && BCol() == false)
        {
            prevPos = position;
            position.Y++;
        }  
    }
}
