using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    {
        Playing,
        GameOver
    }

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;


    //double timer;
   // const double trigger = 1000;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();

       
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
       /* if (inputHelper.KeyPressed(Keys.D))
            currentBlock.RotateR();
        if (inputHelper.KeyPressed(Keys.A))
            currentBlock.RotateL();
        if (inputHelper.KeyPressed(Keys.Left))
            currentBlock.MoveL();
        if (inputHelper.KeyPressed(Keys.Right))
            currentBlock.MoveR();
       */
    }

    public void Update(GameTime gameTime, InputHelper inputHelper)
    {
      
        grid.Update(gameTime, inputHelper);
        grid.HandleInput(gameTime, inputHelper);
        grid.Random();

        
        
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
