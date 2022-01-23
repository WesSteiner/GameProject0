using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject0
{
    public class Keyhole
    {
        /// <summary>
        /// Random number gen
        /// </summary>
        Random random;
        /// <summary>
        /// The game this ball is a part of
        /// </summary>
        Game game;

        /// <summary>
        /// A color to help distinguish one ball from another
        /// </summary>
        Color color;

        /// <summary>
        /// The texture to apply to a ball
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The position of the ball in the game world
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Tells if keyhole is correct keyhole
        /// </summary>
        public bool Correct { get; set; }

        /// <summary>
        /// Constructs a new ball instance
        /// </summary>
        /// <param name="game">The game this ball belongs in</param>
        /// <param name="color">A color to distinguish this ball</param>
        public Keyhole(Game game, Color color)
        {
            this.game = game;
            this.color = color;
            this.random = new Random();
        }

        /// <summary>
        /// Loads the ball's texture
        /// </summary>
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("KeyholeSprite");
        }

        /// <summary>
        /// Draws the ball at its current position and with 
        /// its assigned color
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to render with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, Position, color);
        }

        /// <summary>
        /// Warps ball to a random location on screen 
        /// </summary>
        public void Warp()
        {
            Position = new Vector2(
                (float)random.NextDouble() * game.GraphicsDevice.Viewport.Width,
                (float)random.NextDouble() * game.GraphicsDevice.Viewport.Height
                );
        }
    }
}
