using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject0.Collisions;

namespace GameProject0
{
    public class Key
    {
        private short keyAnimationFrame = 0;
        private double keyAnimationTimer;        
        /// <summary>
        /// The game this ball is a part of
        /// </summary>
        Game game;

        /// <summary>
        /// A color to help distinguish one ball from another
        /// </summary>
        public GameTime GameTime { get; set; }

        /// <summary>
        /// The texture to apply to a ball
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The position of the ball in the game world
        /// </summary>
        public Vector2 Position { get; set; }

        public bool Win { get; set; } = false;

        /// <summary>
        /// Bounding volume of sprite
        /// </summary>
        public BoundingRectangle Bounds { get; set; }

        /// <summary>
        /// Constructs a new ball instance
        /// </summary>
        /// <param name="game">The game this ball belongs in</param>
        /// <param name="color">A color to distinguish this ball</param>
        public Key(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Loads the ball's texture
        /// </summary>
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("AnimatedKey");
        }

        public void Update(GameTime gameTime, Keyhole hole, InputManager inputManager)
        {
            inputManager.Update(gameTime);
            GameTime = gameTime;
            Position = inputManager.MousePosition;
            Bounds = new BoundingRectangle(new Vector2(inputManager.MousePosition.X, inputManager.MousePosition.Y), 50, 50);
            if (hole.Bounds.CollidesWith(Bounds) && inputManager.Warp)
            {
                Win = true;
            }
        }

        /// <summary>
        /// Draws the ball at its current position and with 
        /// its assigned color
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to render with</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            //Update animation timer
            keyAnimationTimer += GameTime.ElapsedGameTime.TotalSeconds;

            if (keyAnimationTimer > 0.3)
            {
                //Update animation frame
                keyAnimationFrame++;
                if (keyAnimationFrame > 3) keyAnimationFrame = 0;
                keyAnimationTimer -= 0.3;
            }

            var source = new Rectangle(keyAnimationFrame * 64, 0, 64, 64);
            _spriteBatch.Draw(texture, Position, source, Color.White);
        }


    }
}
