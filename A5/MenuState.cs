using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A5
{
    public class MenuState : A5.State
    {
        static MenuState instance;
        bool isLoaded = false;
        Texture2D back;
        Sprite star = new Sprite();
        SpriteFont arial;
        public Vector2 menuSpacing = new Vector2(0, 35);
        Vector2 middle = Vector2.Zero;
        Vector2 title = Vector2.Zero;
        Vector2 titleMeasure = Vector2.Zero;
        Vector2 solo = Vector2.Zero;
        Vector2 soloMeasure = Vector2.Zero;
        Vector2 versus = Vector2.Zero;
        Vector2 versusMeasure = Vector2.Zero;
        Vector2 quit = Vector2.Zero;
        Vector2 quitMeasure = Vector2.Zero;
        Vector2 starPos = Vector2.Zero;
        KeyboardState currentState;
        KeyboardState previousState;
        Audio menuMusic = new Audio();
        int menuCursor = 1;
        float titleAlpha = 0f;
        float alpha = 0f;



        public static MenuState Instance
        {
            get
            {
                return instance;
            }
        }


        public MenuState() : base()
        {
            instance = this;
        }



        public override void Update(ContentManager content, GameTime gameTime)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                middle = new Vector2(Game1.Instance.ScreenWidth / 2, Game1.Instance.ScreenHeight / 2);
                back = content.Load<Texture2D>("back");
                star.Load(content, "starSelector");
                arial = content.Load<SpriteFont>("Arial");
                titleMeasure = (arial.MeasureString("A5"));
                title = new Vector2(middle.X - titleMeasure.X / 2, middle.Y - titleMeasure.Y / 2);
                soloMeasure = (arial.MeasureString("Solo"));
                solo = new Vector2(middle.X - soloMeasure.X / 2, middle.Y - soloMeasure.Y / 2) - menuSpacing;
                versusMeasure = (arial.MeasureString("Versus"));
                versus = new Vector2(middle.X - versusMeasure.X / 2, middle.Y - versusMeasure.Y / 2);
                quitMeasure = (arial.MeasureString("Quit"));
                quit = new Vector2(middle.X - quitMeasure.X / 2, middle.Y - quitMeasure.Y / 2) + menuSpacing;
                menuMusic.Load(content, "A5_Menu_Music");
                menuMusic.soundInstance.IsLooped = true;
                menuMusic.soundInstance.Play();
                previousState = Keyboard.GetState();
                menuCursor = 1;
                titleAlpha = 0f;
                alpha = 0f;
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            titleAlpha = MathHelper.Clamp(titleAlpha, 0f, 1f);
            alpha = MathHelper.Clamp(alpha, 0f, 1f);
            title.Y = MathHelper.Clamp(title.Y, Game1.Instance.ScreenHeight * 0.2f, Game1.Instance.ScreenHeight);
            title.Y -= 50 * deltaTime;
            titleAlpha += 0.25f * deltaTime;

            if (title.Y <= Game1.Instance.ScreenHeight * 0.2f)
                alpha += 0.2f * deltaTime;

            currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up) || currentState.IsKeyDown(Keys.W) && previousState.IsKeyUp(Keys.W))
                menuCursor -= 1;
            if (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down) || currentState.IsKeyDown(Keys.S) && previousState.IsKeyUp(Keys.S))
                menuCursor += 1;
            if (alpha >= 0.25f)
            {
                switch (menuCursor)
                {
                case 1:
                    starPos = solo - star.offset + new Vector2(-star.texture.Width, soloMeasure.Y / 2);
                        if (currentState.IsKeyDown(Keys.Enter) && previousState.IsKeyDown(Keys.Enter))
                        {
                            isLoaded = false;
                            StateManager.ChangeState("Solo Game");
                            menuMusic.soundInstance.Stop();
                        }
                    break;
                case 2:
                    starPos = versus - star.offset + new Vector2(-star.texture.Width, versusMeasure.Y / 2);
                    break;
                default:
                    starPos = quit - star.offset + new Vector2(-star.texture.Width, quitMeasure.Y / 2);
                        if (currentState.IsKeyDown(Keys.Enter))
                            Game1.Instance.Exit();
                    break;
                }
            }
            previousState = currentState;
            if (menuCursor > 3)
                menuCursor = 1;
            if (menuCursor < 1)
                menuCursor = 3;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(back, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            spriteBatch.DrawString(arial, "A5", title, Color.White * titleAlpha);
            spriteBatch.DrawString(arial, "Solo", solo, Color.White * alpha);
            spriteBatch.DrawString(arial, "Versus", versus, Color.White * alpha);
            spriteBatch.DrawString(arial, "Quit", quit, Color.White * alpha);
            if (alpha >= 0.25f)
            spriteBatch.Draw(star.texture, starPos , Color.White);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}
