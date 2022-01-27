using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class InputManager
    {
        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;
        MouseState currentMouseState;
        MouseState priorMouseState;
        GamePadState currentGamePadState;
        GamePadState priorGamePadState;

        /// <summary>
        /// The current direction
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// Current mouse position
        /// </summary>
        public Vector2 MousePosition { get; private set; }

        /// <summary>
        /// If Warp functionality has been requested
        /// </summary>
        public bool Warp { get; private set; } = false;

        /// <summary>
        /// If the player has won
        /// </summary>
        public bool Win { get; private set; } = false;

        /// <summary>
        /// If the user has requested the game end
        /// </summary>
        public bool Exit { get; private set; } = false;

        public void Update(GameTime gameTime, Vector2 pos)
        {
            #region State Updating
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            priorGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(0);

            #endregion

            #region Direction Updating

            // Get position from GamePad
            Direction = currentGamePadState.ThumbSticks.Left * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds * new Vector2(1, -1);

            // Get position from keyboard
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A)) Direction += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D)) Direction += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W)) Direction += new Vector2(0, -100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S)) Direction += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //Get position of mouse
            MousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            #endregion

            #region Warp Input

            Warp = false;

            if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space)) Warp = true;

            if (currentGamePadState.IsButtonDown(Buttons.A) && priorGamePadState.IsButtonUp(Buttons.A)) Warp = true;

            #endregion

            #region Exit Input

            if (currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
                Exit = true;

            #endregion

            #region On Click

            if (currentMouseState.LeftButton == ButtonState.Pressed && priorMouseState.LeftButton == ButtonState.Released)
            {
                Warp = true;
            }

            #endregion
        }
    }
}
