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

    Block currentBlock;

    double timer;
    const double trigger = 1000;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();

        currentBlock = new LL();
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.D))
            currentBlock.RotateR();
        if (inputHelper.KeyPressed(Keys.A))
            currentBlock.RotateL();
        if (inputHelper.KeyPressed(Keys.Left))
            currentBlock.MoveL();
        if (inputHelper.KeyPressed(Keys.Right))
            currentBlock.MoveR();
    }

    public void Update(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (timer > trigger)
        {
            currentBlock.Fall();
            timer -= trigger;
        }
        if (currentBlock.BCol())
            currentBlock.position.Y--;
        if (currentBlock.WCol(0))
            currentBlock.position.X++;
        if (currentBlock.WCol(1))
            currentBlock.position.X--;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        currentBlock.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
