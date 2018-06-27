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
    public class VersusGameOver : A5.State
    {
        bool isLoaded = false;
        SpriteFont arial;
        Texture2D background;
        Sprite star = new Sprite();
        Vector2 middle = Vector2.Zero;
        Vector2 titleMeasure = Vector2.Zero;
        Vector2 title = Vector2.Zero;
        Vector2 retryMeasure = Vector2.Zero;
        Vector2 retry = Vector2.Zero;
        Vector2 menuMeasure = Vector2.Zero;
        Vector2 menu = Vector2.Zero;
        Vector2 starPos = Vector2.Zero;
        Audio music = new Audio();
        KeyboardState oldState;
        int menuCursor = 1;



        public VersusGameOver() : base()
        {
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                arial = Content.Load<SpriteFont>("Arial");
                background = Content.Load<Texture2D>("back");
                star.Load(Content, "starSelector");
                music.Load(Content, "A5_End_Music");
                music.soundInstance.IsLooped = true;
                music.soundInstance.Play();
                oldState = Keyboard.GetState();
                middle = new Vector2(Game1.Instance.ScreenWidth / 2, Game1.Instance.ScreenHeight / 2);
                if (VersusGameState.p1Wins == true)
                {
                    titleMeasure = arial.MeasureString("Player 1 Wins!");
                    title = new Vector2(middle.X - titleMeasure.X / 2, middle.Y - titleMeasure.Y / 2) - (MenuState.Instance.menuSpacing * 2);
                }
                else
                {
                    titleMeasure = arial.MeasureString("Player 2 Wins!");
                    title = new Vector2(middle.X - titleMeasure.X / 2, middle.Y - titleMeasure.Y / 2) - (MenuState.Instance.menuSpacing * 2);
                }
                retryMeasure = arial.MeasureString("Rematch");
                retry = new Vector2(middle.X - retryMeasure.X / 2, middle.Y - retryMeasure.Y / 2);
                menuMeasure = arial.MeasureString("Menu");
                menu = new Vector2(middle.X - menuMeasure.X / 2, middle.Y - menuMeasure.Y / 2) + MenuState.Instance.menuSpacing;
            }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up) || newState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
                menuCursor -= 1;
            if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down) || newState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
                menuCursor += 1;
            switch (menuCursor)
            {
                case 1:
                    starPos = retry - star.offset + new Vector2(-star.texture.Width, retryMeasure.Y / 2);
                    if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        isLoaded = false;
                        StateManager.ChangeState("Versus Game");
                        music.soundInstance.Stop();
                        menuCursor = 1;
                        VersusGameState.p1Wins = false;
                    }
                    break;
                default:
                    starPos = menu - star.offset + new Vector2(-star.texture.Width, menuMeasure.Y / 2);
                    if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        isLoaded = false;
                        StateManager.ChangeState("Menu");
                        music.soundInstance.Stop();
                        menuCursor = 1;
                        VersusGameState.p1Wins = false;
                    }
                    break;
            }
            oldState = newState;
            if (menuCursor > 2)
                menuCursor = 1;
            if (menuCursor < 1)
                menuCursor = 2;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            if (VersusGameState.p1Wins == true)
                spriteBatch.DrawString(arial, "Player 1 Wins!", title, Color.White);
            else
                spriteBatch.DrawString(arial, "Player 2 Wins!", title, Color.White);
            spriteBatch.DrawString(arial, "Rematch", retry, Color.White);
            spriteBatch.DrawString(arial, "Menu", menu, Color.White);
            spriteBatch.Draw(star.texture, starPos, Color.White);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}