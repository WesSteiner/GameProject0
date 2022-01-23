using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class KeyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Keyhole[] keyholes;
        private InputManager inputManager;
        private Texture2D key;

        public KeyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            keyholes = new Keyhole[]
            {
                new Keyhole(this, Color.White) {Position = new Vector2(150, 200), Correct = false},
                new Keyhole(this, Color.White) {Position = new Vector2(250, 200), Correct = false},
                new Keyhole(this, Color.White) {Position = new Vector2(350, 200), Correct = true},
                new Keyhole(this, Color.White) {Position = new Vector2(450, 200), Correct = false},
                new Keyhole(this, Color.White) {Position = new Vector2(550, 200), Correct = false}
            };
            inputManager = new InputManager();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (Keyhole k in keyholes) k.LoadContent();
            key = Content.Load<Texture2D>("KeySprite");

        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime, keyholes[2].Position);

            if (inputManager.Exit) Exit();

            // TODO: Add your update logic here
            if (inputManager.Warp) foreach (Keyhole k in keyholes) k.Warp();
            if (inputManager.Win) Win();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (Keyhole k in keyholes) k.Draw(_spriteBatch);
            _spriteBatch.Draw(key, inputManager.MousePosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Win()
        {
            MessageBox.Show("You did it!", "You may leave.", new[] { "Exit" });
            Exit();
        }
    }
}
