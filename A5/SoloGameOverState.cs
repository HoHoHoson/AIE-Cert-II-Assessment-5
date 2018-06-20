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
    public class SoloGameOverState : A5.State
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
        Vector2 score = Vector2.Zero;
        Vector2 scoreMeasure = Vector2.Zero;
        Vector2 starPos = Vector2.Zero;
        Audio music = new Audio();
        KeyboardState oldState;
        int menuCursor = 1;



        public SoloGameOverState() : base()
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
                titleMeasure = arial.MeasureString("GAME OVER");
                title = new Vector2(middle.X - titleMeasure.X / 2, middle.Y - titleMeasure.Y / 2) - (MenuState.Instance.menuSpacing * 3);
                retryMeasure = arial.MeasureString("Retry");
                retry = new Vector2(middle.X - retryMeasure.X / 2, middle.Y - retryMeasure.Y / 2);
                menuMeasure = arial.MeasureString("Menu");
                menu = new Vector2(middle.X - menuMeasure.X / 2, middle.Y - menuMeasure.Y / 2) + MenuState.Instance.menuSpacing;
                scoreMeasure = arial.MeasureString("You Survived " + SoloGameState.Instance.gameTimer.ToString("##0") + " Seconds");
                score = new Vector2(middle.X - scoreMeasure.X / 2, middle.Y - scoreMeasure.Y / 2) - (MenuState.Instance.menuSpacing * 2);
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
                        StateManager.ChangeState("Solo Game");
                        music.soundInstance.Stop();
                        menuCursor = 1;
                        SoloGameState.Instance.gameTimer = 0;
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
                        SoloGameState.Instance.gameTimer = 0;
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
            spriteBatch.Draw(background, new Rectangle (0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            spriteBatch.DrawString(arial, "GAME OVER", title, Color.White);
            spriteBatch.DrawString(arial, "Retry", retry, Color.White);
            spriteBatch.DrawString(arial, "Menu", menu, Color.White);
            spriteBatch.DrawString(arial, ("You Survived " + SoloGameState.Instance.gameTimer.ToString("##0") + " Seconds"), score, Color.White);
            spriteBatch.Draw(star.texture, starPos, Color.White);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}
