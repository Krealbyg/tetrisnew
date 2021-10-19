using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;
    public Block currentblock;
    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }
    private Color[,] grid;
    private bool[,] big;
    public Random r = new Random();
    public int rblok;
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        currentblock = new Long();
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
        
        currentblock.Draw(gameTime, spriteBatch);
        
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public int Random()
    {
        rblok = r.Next(0, 6);
        return rblok;
        
    }
    public void opslaan()
    {
        int x = (int)currentblock.position.X, y = (int)currentblock.position.Y;
      
        for (int i = 0; i < currentblock.bGrid.GetLength(0); i++)
        {
            for (int j = 0; j < currentblock.bGrid.GetLength(1); j++)
            {
                if (currentblock.bGrid[i, j])
                {
                    big[x + i, j + y] = true;
                    grid[x + i, j + y] = currentblock.color;
                }
            }
        }
        
        switch (rblok)
        {
            case 0: currentblock = new SnakeR();
                 break;
            case 1:currentblock = new Long();
                break;
            case 2: currentblock = new Square();
                break;
            case 3: currentblock = new SnakeL();
                break;
            case 4: currentblock = new LL();
                break;
            case 5: currentblock = new LR();
                break;
            case 6: currentblock = new T();
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
        if (currentblock.BCol())
        {
            currentblock.position.Y--;
            opslaan();
        }
     
        currentblock.Update(gameTime);
    }
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.D)) 
            currentblock.bGrid = currentblock.RotateR(currentblock.bGrid);

        

    
        if (inputHelper.KeyPressed(Keys.A))
            currentblock.bGrid = currentblock.RotateL(currentblock.bGrid);
          
         if (inputHelper.KeyPressed(Keys.Left))
             currentblock.MoveL();
         if (inputHelper.KeyPressed(Keys.Right))
             currentblock.MoveR();
        if (inputHelper.KeyDown(Keys.Down))
            currentblock.Fall();
    }
}

