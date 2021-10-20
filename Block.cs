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
    
    

    public Color color;
    public Vector2 position = new Vector2(2, 0);
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
        return temp;
    }
    public bool[,] RotateL(bool[,] RotL)
    {
        bool[,] temp = new bool[RotL.GetLength(0), RotL.GetLength(1)];

        for (int x = 0; x < RotL.GetLength(0); x++)
        {
            for (int y = 0; y < RotL.GetLength(1); y++)
            {
                temp[x, y] = RotL[RotL.GetLength(0) - 1 - y, x];
            }
        }
        return temp;
    }

    public bool BCol()
    {
        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                if (bGrid[x, y] == true && position.Y + y >= 20)
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
                if (bGrid[x, y] == true && position.X + x + 1 > (10) && z == 1 || bGrid[x, y] == true && position.X + x < 0 && z == 0)
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
    public void Update(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.Ticks % 60 == 1)
        {
            position.Y++;
        }  
      
        if (WCol(0))
            position.X++;
        if (WCol(1))
            position.X--;
    }
}
