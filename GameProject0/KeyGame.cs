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
        private Texture2D key;
        private SpriteFont pixel;
        private int clickCount = 0;
        private short keyAnimationFrame = 0;
        private double keyAnimationTimer;
        private BoundingRectangle keyBounds;

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
            keyBounds = new BoundingRectangle(new Vector2(inputManager.MousePosition.X, inputManager.MousePosition.Y), 59, 64);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (Keyhole k in keyholes) k.LoadContent();
            key = Content.Load<Texture2D>("AnimatedKey");
            pixel = Content.Load<SpriteFont>("Pixel");
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime, keyholes[2].Position);

            if (inputManager.Exit) Exit();

            // TODO: Add your update logic here
            if (inputManager.Warp)
            {
                clickCount++;
                foreach (Keyhole k in keyholes) 
                {                    
                    k.Warp();
                    k.Bounds = new BoundingRectangle(new Vector2(k.Position.X, k.Position.Y), 64, 64);
                }                                
            }
            
            if (keyholes[2].Bounds.CollidesWith(keyBounds))
            {
                clickCount++;
                Win();
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

            //Update animation timer
            keyAnimationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (keyAnimationTimer > 0.3)
            {
                //Update animation frame
                keyAnimationFrame++;
                if (keyAnimationFrame > 3) keyAnimationFrame = 0;
                keyAnimationTimer -= 0.3;
            }

            var source = new Rectangle(keyAnimationFrame * 64, 0, 59, 64);
            _spriteBatch.Draw(key, inputManager.MousePosition, source, Color.White);
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
