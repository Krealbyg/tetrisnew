using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Class to create the blocks
/// </summary>
abstract class Block
{
    //Grids
    public bool[,] bGrid;
    public bool[,] prevGrid;
    
    //Properties of the blocks
    public Color color;
    public Vector2 position = new Vector2(2, 0);
    public Vector2 prevPos;
    protected Texture2D cell;

    //miscellaneous variables
    public int speedMod;
    public bool dropping;
    
    public Block()
    {
        cell = TetrisGame.ContentManager.Load<Texture2D>("ass_without_the_G");
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

    #region Rotation
    //region where all methods having to do with rotation reside
    public bool [,] RotateR(bool[,] rotR)
    {
        bool[,] temp = new bool[rotR.GetLength(0), rotR.GetLength(1)];//creates temp grid equal in size to main block grid

        for (int x = 0; x < rotR.GetLength(0); x++)
        {
            for (int y = 0; y < rotR.GetLength(1); y++)
            {
                temp[x, y] = rotR[y, rotR.GetLength(1) - 1 - x];//turns temp grid into clockwise rotation of block grid
            }
        }
        for (int x = 0; x < temp.GetLength(0); x++)
        {
            for (int y = 0; y < temp.GetLength(1); y++)
            {
                //moves block away from wall if rotation would exit grid
                while (temp[x, y] == true && position.X + x + 1 > (10))
                    position.X--;
                while (temp[x, y] == true && position.X + x - 0 < 0)
                    position.X++;
            }
        }
        return temp;//turns current grid into this temp grid
    }
    public bool[,] RotateL(bool[,] rotL)//same as RotateR, except counter-clockwise
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

    public void PrevRot(bool[,] rot)//saves current rotation into another back-up grid. This is used in another class
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
    #endregion

    #region Collision
    //region where methods having to do with collision reside
    public bool BottomCol()//this checks if block has reached bottom of grid
    {
        for (int x = 0; x < bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < bGrid.GetLength(1); y++)
            {
                if (bGrid[x, y] == true && position.Y + y == 19)
                {
                    dropping = false;
                    return true;
                }
            }
        }
        return false;
    }

    public bool WallCol(int z)//checks if block has reached one of the walls
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
    #endregion

    #region Movement
    //region where methods having to do with movement reside

    //Movement sideways
    public void MoveL()
    {
        prevPos = position;//saves position in a back-up. Used in another class
        if (!WallCol(0))//don't move in at wall
            position.X--;
    }

    public void MoveR()//same as MoveL. except other side
    {
        prevPos = position;
        if (!WallCol(1))
            position.X++;
    }

    //Movement downwards
    public void Fall()//Moves down one
    {
        prevPos = position;
        if (BottomCol() == false)//don't if reached bottom
            position.Y++;
    }

    public void Drop()//Keeps moving down
    {
        if (dropping)//While dropping true, falls
            Fall();
    }
    #endregion

    public void Update(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.Ticks % (60 / speedMod) == 1 && BottomCol() == false)//speed of automatic falling
        {
            prevPos = position;
            position.Y++;
        }

        Drop();//keeps drop working
    }
}
