using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;
    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }
    private Color[,] grid;
    private bool[,] big;
    public Random r = new Random();
    public int score;
    SpriteFont font;
    public Block currentBlock;
    Block previewBlock;
    public int level;
    SoundEffect boom;
    SoundEffect levelup;
    SoundEffect place;
    public bool gameOver;
   
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        place = TetrisGame.ContentManager.Load<SoundEffect>("GrassFinal");
        levelup = TetrisGame.ContentManager.Load<SoundEffect>("level");
        boom = TetrisGame.ContentManager.Load<SoundEffect>("boomfinal");
      

        currentBlock = new Long();
        position = Vector2.Zero;
        grid = new Color[Width, Height];
        big = new bool[Width, Height];
        level = 1;
        gameOver = false;

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

        for (int x = 0; x < previewBlock.bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < previewBlock.bGrid.GetLength(1); y++)
            {
                if (previewBlock.bGrid[x, y] == true)
                    spriteBatch.Draw(emptyCell, new Vector2((x + 11) * emptyCell.Width, (y + 5) * emptyCell.Height), previewBlock.color);
            }
        }

        currentBlock.Draw(gameTime, spriteBatch);

        spriteBatch.DrawString(font, "Score:" + score, new Vector2(330, 6), Color.Black);
        spriteBatch.DrawString(font, "Level:" + level, new Vector2(330, 36), Color.Black);
        spriteBatch.DrawString(font, "Next Block:", new Vector2(330, 118), Color.Black);
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Save()
    {
        place.Play(volume: 0.5f, pitch: 0.0f, pan: 1.0f);
        
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
        NewBlock();
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
            Save();
        GCol();

        if (score == 1500 * level)
        {
            levelup.Play(volume: 0.2f, pitch: 0.0f, pan: 0.0f);
            level++;
        }
        currentBlock.speedMod = level;

        currentBlock.Update(gameTime);
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.D))
        {
            currentBlock.PrevRot(currentBlock.bGrid);
            currentBlock.bGrid = currentBlock.RotateR(currentBlock.bGrid);
            RCol();
        }
        if (inputHelper.KeyPressed(Keys.A))
        {
            currentBlock.PrevRot(currentBlock.bGrid);
            currentBlock.bGrid = currentBlock.RotateL(currentBlock.bGrid);
            RCol();
        }
        if (inputHelper.KeyPressed(Keys.Left))
             currentBlock.MoveL();
        if (inputHelper.KeyPressed(Keys.Right))
             currentBlock.MoveR();
        if (inputHelper.KeyPressed(Keys.Down))
            currentBlock.Fall();
        if (inputHelper.KeyPressed(Keys.Space))
            currentBlock.dropping = true;
    }

    public void GCol()
    {
        for (int x = 0; x < currentBlock.bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < currentBlock.bGrid.GetLength(1); y++)
            {
                if (currentBlock.bGrid[x, y] && big[(int)currentBlock.position.X + x, (int)currentBlock.position.Y + y] == true)
                    if (currentBlock.position == currentBlock.prevPos)
                        gameOver = true;
                    else
                        currentBlock.position = currentBlock.prevPos;
                if (currentBlock.bGrid[x, y] && big[(int)currentBlock.position.X + x, (int)currentBlock.position.Y + y + 1] == true)
                {
                    currentBlock.dropping = false;
                    Save();
                    Check();
                }         
            }
        }
    }

    public void RCol()
    {
        for (int x = 0; x < currentBlock.bGrid.GetLength(0); x++)
        {
            for (int y = 0; y < currentBlock.bGrid.GetLength(1); y++)
            {
                if (currentBlock.bGrid[x, y] && big[(int)currentBlock.position.X + x, (int)currentBlock.position.Y + y] == true)
                    currentBlock.bGrid = currentBlock.prevGrid;
            }
        }
    }

    public void Check()
    {
        int count = 0;
        for (int i = Height - 1; i >= 0; i--)
        {
            bool full = true;
            for (int j = 0; j < Width; j++)
            {
                if (!big[j, i])
                    full = false;
            }
            if (full)
            {
                
                Clear(i);
                i++;
                count++;
            }
        }
        ScoreIncrease(count);
    }

    public void Clear(int x)
    {
        boom.Play(volume: 1.0f, pitch: 0.0f, pan: 0.0f);
        for (int i = x; i >= 0; i--)
        {
            for (int j = 0; j < Width; j++)
            {
                if (i == 0)
                {
                    grid[j, i] = Color.White;
                    big[j, i] = false;
                }
                else
                {
                    grid[j, i] = grid[j, i - 1];
                    big[j, i] = big[j, i - 1];
                }
            }
        }
        
    }

    public void ScoreIncrease(int x)
    {
        switch(x)
        {
            case 1:
                score += 100;
                break;
            case 2:
                score += 250;
                break;
            case 3:
                score += 500;
                break;
            case 4:
                score += 1000;
                break;
        }
    }

    public void NewBlock()
    {
        currentBlock = previewBlock;
        int rBlock = r.Next(0, 7);
        switch (rBlock)
        {
            case 0:
                previewBlock = new SnakeR();
                break;
            case 1:
                previewBlock = new Long();
                break;
            case 2:
                previewBlock = new Square();
                break;
            case 3:
                previewBlock = new SnakeL();
                break;
            case 4:
                previewBlock = new LL();
                break;
            case 5:
                previewBlock = new LR();
                break;
            case 6:
                previewBlock = new T();
                break;
        }
    }
    
    public void Reset()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                grid[i, j] = Color.White;
                big[i, j] = false;
            }
        }
        score = 0;
        level = 1;
        gameOver = false;
    }
}

