using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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

    //background image
    public Texture2D backgrnd;
    public Texture2D gameovr;
    public Texture2D playingbgrnd;

    //music stuff
    public Song theme;
    public bool songplaying;
    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Begin;

        backgrnd = TetrisGame.ContentManager.Load<Texture2D>("bgrnd");
        gameovr = TetrisGame.ContentManager.Load<Texture2D>("gmover");
        playingbgrnd = TetrisGame.ContentManager.Load<Texture2D>("mainbackground");

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        theme = TetrisGame.ContentManager.Load<Song>("theme");

        grid = new TetrisGrid();    
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Enter))
            if (gameState == GameState.Begin)//if playing for first time
                GameStart();
            else if (gameState == GameState.GameOver)//if replaying
            {
                grid.Reset();
                GameStart();
            }
    }

    public void Update(GameTime gameTime, InputHelper inputHelper)
    {
        if (gameState == GameState.Playing)//grid only updates while game is active
        {
            grid.Update(gameTime, inputHelper);
            grid.HandleInput(gameTime, inputHelper);
            grid.CheckRows();
        }
        if (grid.gameOver)
            gameState = GameState.GameOver;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        if (gameState == GameState.Playing)
        {
            spriteBatch.Draw(playingbgrnd, Vector2.Zero, Color.White);
            grid.Draw(gameTime, spriteBatch);
        }
        if (gameState == GameState.Begin)
        {
          if (songplaying == false)//makes it so song doesn't keep starting over and over while in GameState.Begin
            {
                MediaPlayer.Play(theme);
                MediaPlayer.Volume = 0.075f;
                MediaPlayer.IsRepeating = true;
                songplaying = true;
            }
            spriteBatch.Draw(backgrnd, Vector2.Zero, Color.White);

        }
        if (gameState == GameState.GameOver)
            spriteBatch.Draw(gameovr, Vector2.Zero, Color.White);
        spriteBatch.End();
    }

    public void GameStart()//starts game. NewBlock is called twice to first create a currentBlock and then the preview block
    {
        gameState = GameState.Playing;
        grid.NewBlock();
        grid.NewBlock();
    }
}
