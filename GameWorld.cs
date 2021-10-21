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
        GameOver,
        Begin
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

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Begin;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();

       
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Enter))
            if (gameState == GameState.Begin)
                GameStart();
            else if (gameState == GameState.GameOver)
            {
                grid.Reset();
                GameStart();
            }
    }

    public void Update(GameTime gameTime, InputHelper inputHelper)
    {
        if (gameState == GameState.Playing)
        {
            grid.Update(gameTime, inputHelper);
            grid.HandleInput(gameTime, inputHelper);
            grid.Check();
        }
        if (grid.gameOver)
            gameState = GameState.GameOver;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        if (gameState == GameState.Playing)
            grid.Draw(gameTime, spriteBatch);
        if (gameState == GameState.Begin)
            spriteBatch.DrawString(font, "Press enter to begin", new Vector2(200, 200), Color.Black);
        if (gameState == GameState.GameOver)
            spriteBatch.DrawString(font, "Game Over. Press enter to restart", new Vector2(200, 200), Color.Black);
        spriteBatch.End();
    }

    public void GameStart()
    {
        gameState = GameState.Playing;
        grid.NewBlock();
        grid.NewBlock();
    }
}
