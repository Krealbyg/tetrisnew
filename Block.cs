using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tetrisnew
{
    public abstract class Block
    {
        protected bool[,] bGrid;
        protected Color color;
        protected Vector2 position = new Vector2(0,0);
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
                        spriteBatch.Draw(cell, new Vector2(x * cell.Width, y * cell.Height), color);
                }
            }
        }

        public void Rotate()
        {
            
        }
    }
}
