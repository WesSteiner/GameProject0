using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject0.Collisions;

namespace GameProject0
{
    public class KeyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Keyhole[] keyholes;
        private InputManager inputManager;
        private SpriteFont pixel;
        private Key key;
        private int clickCount = 0;

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
                new Keyhole(this, Color.White) {Position = new Vector2(150, 200), Bounds = new BoundingRectangle(new Vector2(150, 200), 64, 64), Correct = false},
                new Keyhole(this, Color.White) {Position = new Vector2(250, 200), Bounds = new BoundingRectangle(new Vector2(250, 200), 64, 64), Correct = false},
                new Keyhole(this, Color.White) {Position = new Vector2(350, 200), Bounds = new BoundingRectangle(new Vector2(350, 200), 64, 64), Correct = true},
                new Keyhole(this, Color.White) {Position = new Vector2(450, 200), Bounds = new BoundingRectangle(new Vector2(450, 200), 64, 64), Correct = false},
                new Keyhole(this, Color.White) {Position = new Vector2(550, 200), Bounds = new BoundingRectangle(new Vector2(550, 200), 64, 64), Correct = false}
            };
            inputManager = new InputManager();
            key = new Key(this);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (Keyhole k in keyholes) k.LoadContent();
            key.LoadContent();
            pixel = Content.Load<SpriteFont>("Pixel");
        }

        protected override void Update(GameTime gameTime)
        {
            key.Update(gameTime, keyholes[2], inputManager);

            if (inputManager.Exit) Exit();

            // TODO: Add your update logic here
            if (inputManager.Warp && key.Win)
            {
                clickCount++;
                Win();
            }

            if (inputManager.Warp)
            {
                clickCount++;
                foreach (Keyhole k in keyholes) 
                {                    
                    k.Warp();
                    k.Bounds = new BoundingRectangle(new Vector2(k.Position.X, k.Position.Y), 64, 64);
                }                                
            }                           
            
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (Keyhole k in keyholes) k.Draw(_spriteBatch);
            //_spriteBatch.Draw(key, inputManager.MousePosition, Color.White);
            _spriteBatch.DrawString(pixel, "Scatters: " + clickCount, new Vector2(2, 2), Color.Gold);
            _spriteBatch.DrawString(pixel, "Click the correct slot!", new Vector2(150, 100), Color.Gold);
            key.Draw(_spriteBatch);
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
