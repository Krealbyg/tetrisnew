using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;
    public Block currentBlock;
    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }
    private Color[,] grid;
    private bool[,] big;
    public Random r = new Random();
    public int rBlock;
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        currentBlock = new Long();
        position = Vector2.Zero;
        grid = new Color[Width, Height];
        big = new bool[Width, Height];

        Clear();

    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                spriteBatch.Draw(emptyCell, new Rectangle(i * emptyCell.Width, j * emptyCell.Height, emptyCell.Width, emptyCell.Height), grid[i, j]);
            }
        }
        
        currentBlock.Draw(gameTime, spriteBatch);
        
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public int Random()
    {
        rBlock = r.Next(0, 6);
        return rBlock;
        
    }
    public void Save()
    {
        int x = (int)currentBlock.position.X, y = (int)currentBlock.position.Y;
      
        for (int i = 0; i < currentBlock.bGrid.GetLength(0); i++)
        {
            for (int j = 0; j < currentBlock.bGrid.GetLength(1); j++)
            {
                if (currentBlock.bGrid[i, j])
                {
                    big[x + i, j + y] = true;
                    grid[x + i, j + y] = currentBlock.color;
                }
            }
        }
        
        switch (rBlock)
        {
            case 0: currentBlock = new SnakeR();
                 break;
            case 1:currentBlock = new Long();
                break;
            case 2: currentBlock = new Square();
                break;
            case 3: currentBlock = new SnakeL();
                break;
            case 4: currentBlock = new LL();
                break;
            case 5: currentBlock = new LR();
                break;
            case 6: currentBlock = new T();
                break;
        
        }
    }
    public void Clear()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                grid[i, j] = Color.White;
            }
        }

    }
    public void Update(GameTime gameTime, InputHelper inputHelper)
    {
        if (currentBlock.BCol())
        {
            currentBlock.position.Y--;
            Save();
        }
        if (GCol())
        {
            while (GCol())
                currentBlock.position.Y--;
            Save();
        }
     
        currentBlock.Update(gameTime);
    }
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.D))
            currentBlock.bGrid = currentBlock.RotateR(currentBlock.bGrid);
        if (inputHelper.KeyPressed(Keys.A))
            currentBlock.bGrid = currentBlock.RotateL(currentBlock.bGrid);
        if (inputHelper.KeyPressed(Keys.Left))
             currentBlock.MoveL();
        if (inputHelper.KeyPressed(Keys.Right))
             currentBlock.MoveR();
        if (inputHelper.KeyPressed(Keys.Down))
            currentBlock.Fall();
    }

    public bool GCol()
    {
        for (int x = 0; x < currentBlock.bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < currentBlock.bGrid.GetLength(1); y++)
            {
                if (currentBlock.bGrid[x, y] && big[(int)currentBlock.position.X + x, (int)currentBlock.position.Y + y] == true)
                    return true;
            }
        }
        return false;
    }
    public List<int> Check()
    {
        List<int> row = new List<int>();
        int x = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (grid[j, i] != Color.White)
                {
                    x++;
                }
            }
            if (x == 10)
            {
                row.Add(i);
            }
            x = 0;
        }
        return row;
    }
    public void Clear(List<int> x)
    {
        foreach (int i in x)
        {
            for (int j = 0; j < Width; j++)
            {
                grid[j, i] = Color.White;
            }
        }

       /* switch (x.Count)
        {
            case 1:
                score += 40;
                break;
            case 2:
                score += 100;
                break;
            case 3:
                score += 300;
                break;
            case 4:
                score += 1200;
                break;
            default:
                break;
        }*/
    }
}

