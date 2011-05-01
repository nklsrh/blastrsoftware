using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace blastrs
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public Player[] Player;
        public int NumberOfPlayers;

        Texture2D[] ScoreBar = new Texture2D[2];
        Texture2D PausedScreen;

        public Stadium Stadium;
        Input Input;

        Blast[] Blast = new Blast[10];

        public Panel[] Panels = new Panel[20];
        public Box[] Boxes = new Box[3];
        public Bot[] Bots;

        public SpriteFont Font, BoldFont;
        public Menu Menu;
        public TimeSpan CountDownTime;

        public int[] myint;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //if (!graphics.IsFullScreen)
            //{
            //    //graphics.ToggleFullScreen();
            //}
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            for (int r = 0; r < 10; r++)
            {
                Blast[r] = new Blast(this);
            }

            Stadium = new Stadium(this);
            Input = new Input(this);         

            Stadium.LevelNumber = 6;
            Stadium.Initialize(graphics, this);

            NumberOfPlayers = 2;
            Player = new Player[NumberOfPlayers];

            for (int r = 0; r < NumberOfPlayers; r++)
            {
                Player[r] = new Player(this);
                Player[r].StartPosition = Stadium.StartPosition[r];
            }

            NewGame();

            base.Initialize();
        }
        public void GameInitialize()
        {
            Initialize();
        }
        public void NewGame()
        {
            for (int r = 0; r < 10; r++)
            {
                Blast[r].Initialize();
            }

            try
            {
                Stadium.Sprite = Content.Load<Texture2D>("Levels\\Level" + Stadium.LevelNumber);
            }
            catch
            {
                Stadium.LevelNumber = 1;
                NewGame();
            }
            Stadium.CollisionMap = Stadium.Sprite;
            Stadium.CameraPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); //STILL CAMERA FOR NOW         
            Stadium.Initialize(graphics, this);
            

            for (int r = 0; r < NumberOfPlayers; r++)
            {
                Player[r].StartPosition = Stadium.StartPosition[r];
                Player[r].Initialize();
            }

            Boxes = new Box[Stadium.NumberOfBoxes];

            Input.Initialize(this, Player);

            DistributePanels();
            DistributeBoxes();
            DistributeBots();

            CountDownTime = new TimeSpan(0, 1, 0);
        }

        public void DistributePanels()
        {
            switch (Stadium.LevelNumber)
            {
                case 1:
                    Stadium.InitiatePanel(0, 460, 368, true, this);
                    Stadium.InitiatePanel(1, 901, 227, false, this);
                    Stadium.InitiatePanel(2, 540, 388, false, this);
                    Stadium.InitiatePanel(3, 921, 307, false, this);
                    Stadium.InitiatePanel(4, 620, 408, false, this);
                    Stadium.InitiatePanel(5, 901, 387, false, this);
                    break;
                case 2:
                    Stadium.InitiatePanel(0, 310, 100, false, this);
                    Stadium.InitiatePanel(3, 910, 100, false, this);
                    Stadium.InitiatePanel(1, 390, 100, false, this);
                    Stadium.InitiatePanel(4, 830, 100, false, this);
                    Stadium.InitiatePanel(2, 470, 100, false, this);
                    Stadium.InitiatePanel(5, 750, 100, false, this);
                    break;
                case 3:
                    Stadium.InitiatePanel(0, 607, 221, true, this);
                    Stadium.InitiatePanel(1, 687, 221, false, this);
                    Stadium.InitiatePanel(2, 767, 221, false, this);
                    Stadium.InitiatePanel(3, 607, 270, false, this);
                    Stadium.InitiatePanel(4, 687, 320, false, this);
                    Stadium.InitiatePanel(5, 767, 320, false, this);
                    break;
                case 4:
                    Stadium.InitiatePanel(0, 600, 160, false, this);
                    Stadium.InitiatePanel(1, 680, 220, false, this);
                    Stadium.InitiatePanel(2, 760, 280, false, this);
                    Stadium.InitiatePanel(3, 840, 340, false, this);
                    Stadium.InitiatePanel(4, 920, 410, false, this);
                    Stadium.InitiatePanel(5, 1000, 480, false, this);
                    break;
                case 5:
                    Stadium.InitiatePanel(0, 610, 140, false, this);
                    Stadium.InitiatePanel(1, 690, 140, false, this);
                    Stadium.InitiatePanel(2, 770, 140, false, this);
                    Stadium.InitiatePanel(3, 850, 140, false, this);
                    Stadium.InitiatePanel(4, 1044, 313, false, this);
                    Stadium.InitiatePanel(5, 1044, 393, false, this);
                    break;
                case 6:
                    Stadium.InitiatePanel(0, 580, 122, false, this);
                    Stadium.InitiatePanel(1, 660, 122, false, this);
                    Stadium.InitiatePanel(2, 740, 122, false, this);
                    Stadium.InitiatePanel(3, 820, 122, false, this);
                    Stadium.InitiatePanel(4, 1000, 272, false, this);
                    Stadium.InitiatePanel(5, 1000, 352, false, this);
                    Stadium.InitiatePanel(6, 619, 502, false, this);
                    Stadium.InitiatePanel(7, 699, 502, false, this);
                    Stadium.InitiatePanel(8, 779, 502, false, this);
                    break;
            }
        }
        public void DistributeBoxes()
        {
            switch (Stadium.LevelNumber)
            {
                case 1:
                    break;
                case 2:
                    Stadium.InitiateBox(0, 183, 427, false, Color.Orange, this);
                    Stadium.InitiateBox(1, 1140, 427, false, Color.Blue, this);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    Stadium.InitiateBox(0, 434, 266, false, Color.Orange, this);
                    Stadium.InitiateBox(1, 1077, 246, false, Color.Blue, this);
                    break;
                case 6:
                    Stadium.InitiateBox(0, 322, 230, false, Color.Blue, this);
                    Stadium.InitiateBox(1, 947, 184, false, Color.Orange, this);
                    Stadium.InitiateBox(2, 993, 540, false, Color.Blue, this);
                    Stadium.InitiateBox(3, 915, 481, false, Color.Orange, this);
                    break;
            }
        }
        public void DistributeBots()
        {
            Bots = new Bot[Stadium.NumberOfBots];
            switch (Stadium.LevelNumber)
            {

                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    Bots[0] = new Bot(this);
                    Bots[0].Initialize(this);
                    Bots[0].isDead = false;
                    Bots[0].StartPosition = new Vector2(380, 410);
                    Bots[0].Position = Bots[0].StartPosition;
                    break;
                case 5:
                    break;
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            try
            {
                Player[0].Sprite = Content.Load<Texture2D>("Characters\\redPlayer");
                Player[0].ShadowSprite = Content.Load<Texture2D>("Characters\\shadow");
            }
            catch { }

            try
            {
                Player[1].Sprite = Content.Load<Texture2D>("Characters\\bluePlayer");
                Player[1].ShadowSprite = Content.Load<Texture2D>("Characters\\shadow");
            }
            catch {}

            ScoreBar[0] = Content.Load<Texture2D>("redBar");
            ScoreBar[1] = Content.Load<Texture2D>("blueBar");
            

            
            for (int r = 0; r < 10; r++)
            {
                Blast[r].Initialize();
                Blast[r].Sprite = Content.Load<Texture2D>("PlayerBlast");
            }

            Font = Content.Load<SpriteFont>("font");
            BoldFont = Content.Load<SpriteFont>("BoldFont");

            PausedScreen = Content.Load<Texture2D>("Paused");
        //--------------------------------------------------------------------------------MENU SELECTLOLOLOL
            Menu = new Menu(this);
            Menu.CurrentScreen = Menu.Card.InGame;
            Menu.Initialize(this, spriteBatch, Content);

        }
        protected override void Update(GameTime gameTime)
        {
            Input.Update(gameTime, Blast, spriteBatch, Menu, this, Content, Player);

            if (Menu.CurrentScreen == Menu.Card.InGame)
            {
                for (int r = 0; r < NumberOfPlayers; r++)
                {
                    Player[r].Update(this, gameTime);
                    Stadium.CheckCollisionWithPlayer(Player[r], gameTime, r, this);

                    for (int b = 0; b < 10; b++)
                    {
                        Blast[b].Update(gameTime, Player[r]);
                        Blast[b].PlayerImpact(gameTime, Player[r], r);
                        for (int g = 0; g < Stadium.NumberOfBoxes; g++)
                        {
                            Blast[b].BoxImpact(gameTime, Boxes[g]);
                        }
                        for (int t = 0; t < Stadium.NumberOfBots; t++)
                            {
                                try
                                {
                                    Bots[t].Update(gameTime, Stadium.CollisionMap, Player);
                                    Blast[b].BotImpact(gameTime, Bots[t]);
                                }
                                catch
                                {
                                    Bots[t].isDead = true;
                                    Bots[t].Position = Bots[t].StartPosition;
                                }
                            }
                    }
                }
                
                switch (Stadium.LevelNumber)
                {
                    case 1:
                        Stadium.Level1(NumberOfPlayers, Panels, Player);
                        break;
                    case 2:
                        Stadium.Level2(this, NumberOfPlayers, Panels, Player, Boxes);
                        break;
                    case 3:
                        Stadium.Level3(NumberOfPlayers, Panels, Player);
                        break;
                    case 4:
                        Stadium.Level4(NumberOfPlayers, Panels, Player, Bots);
                        break;
                    case 5:
                        Stadium.Level5(this, NumberOfPlayers, Panels, Player, Boxes);
                        break;
                    case 6:
                        Stadium.Level6(this, NumberOfPlayers, Panels, Player, Boxes);
                        break;
                }

                Timer(gameTime);
            }


            base.Update(gameTime);
        }

        public void Timer(GameTime gameTime)
        {
            CountDownTime -= gameTime.ElapsedGameTime;
            if (CountDownTime <= TimeSpan.Zero)
            {
                //Menu.CurrentScreen = Menu.Card.Scoreboard;
                //Menu.Initialize(this, spriteBatch, Content);
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Menu.Draw(this, gameTime, spriteBatch);

            if (Menu.CurrentScreen == Menu.Card.InGame || Menu.CurrentScreen == Menu.Card.Paused)
            {
                Stadium.Draw(gameTime, spriteBatch);

                for (int r = 0; r < Stadium.NumberOfPanels; r++)
                {
                    if (Panels[r].isVisible)
                    {
                        Panels[r].Draw(gameTime, spriteBatch);
                    }
                }
                for (int r = 0; r < Stadium.NumberOfBoxes; r++)
                {
                    Boxes[r].Draw(gameTime, spriteBatch);
                }
                for (int r = 0; r < NumberOfPlayers; r++)
                {
                    Player[r].Draw(gameTime, spriteBatch);
                }
                for (int r = 0; r < Stadium.NumberOfBots; r++)
                {
                    if(!Bots[r].isDead)
                    Bots[r].Draw(gameTime, spriteBatch);
                } 
                for (int r = 0; r < 10; r++)
                {
                    Blast[r].Draw(spriteBatch);
                } 

                //DrawScore();

                if (Menu.CurrentScreen == Menu.Card.Paused)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(PausedScreen, Vector2.Zero, Color.White);
                    spriteBatch.End();
                }
            }


            base.Draw(gameTime);
        }
        public void DrawScore()
        {
                spriteBatch.Begin();
                try
                {
                    spriteBatch.Draw(ScoreBar[0], new Rectangle(42, (int)(622.5f - ((float)(Player[0].Score / 1000f) * 379f)), 85, (int)(((float)(Player[0].Score / 1000f) * 379f))), Color.White);
                    //spriteBatch.DrawString(Font, Player[0].Score.ToString(), new Vector2(160, 460), new Color(232, 156, 54));
                }
                catch (Exception e) { }
                try
                {
                    spriteBatch.Draw(ScoreBar[1], new Rectangle(165, (int)(622.5f - ((float)(Player[1].Score / 1000f) * 379f)), 85, (int)(((float)(Player[1].Score / 1000f) * 379f))), Color.White);
                    //spriteBatch.DrawString(Font, Player[1].Score.ToString(), new Vector2(1130, 460), new Color(179, 194, 219));
                } 
                catch (Exception e) { }
                try
                {
                    spriteBatch.Draw(ScoreBar[2], new Rectangle(1087, (int)(622.5f - ((float)(Player[2].Score / 1000f) * 379f)), 85, (int)(((float)(Player[2].Score / 1000f) * 379f))), Color.White);
                    //spriteBatch.DrawString(Font, Player[2].Score.ToString(), new Vector2(80, 560), new Color(179, 219, 189));
                }
                catch (Exception e) { }
                try
                {
                    spriteBatch.Draw(ScoreBar[3], new Rectangle(1223, (int)(622.5f - ((float)(Player[3].Score / 1000f) * 379f)), 85, (int)(((float)(Player[3].Score / 1000f) * 379f))), Color.White);
                    //spriteBatch.DrawString(Font, Player[3].Score.ToString(), new Vector2(1200, 560), new Color(243, 237, 217));
                }
                catch (Exception e) { }

                spriteBatch.DrawString(Font, "seconds", new Vector2(40, 140), new Color(150, 150, 150));
                spriteBatch.DrawString(BoldFont, CountDownTime.Seconds.ToString(), new Vector2(40, 0), new Color(222, 222, 222));
                spriteBatch.End();
        }
        public void DrawScoreboard()
        {
            for (int r = 0; r < NumberOfPlayers; r++)
            {
                if (Player[r].Score > 1000)
                {
                    Player[r].Score = 1000;
                }
            }
            spriteBatch.Begin();
            spriteBatch.Draw(Menu.Screen, Vector2.Zero, Color.White);
            try
            {
                spriteBatch.Draw(ScoreBar[0], new Rectangle(442, (int)(554f - ((float)(Player[0].Score / 1000f) * 324f)), 85, (int)(((float)(Player[0].Score / 1000f) * 324f))), Color.White);
            }
            catch (Exception e) { }
            try
            {
                spriteBatch.Draw(ScoreBar[1], new Rectangle(570, (int)(554f - ((float)(Player[1].Score / 1000f) * 324f)), 85, (int)(((float)(Player[1].Score / 1000f) * 324f))), Color.White);   
            }
            catch (Exception e) { }
            try
            {
                spriteBatch.Draw(ScoreBar[2], new Rectangle(724, (int)(554f - ((float)(Player[2].Score / 1000f) * 324f)), 85, (int)(((float)(Player[2].Score / 1000f) * 324f))), Color.White);   
            }
            catch (Exception e) { }
            try
            {
                spriteBatch.Draw(ScoreBar[3], new Rectangle(860, (int)(554f - ((float)(Player[3].Score / 1000f) * 324f)), 85, (int)(((float)(Player[3].Score / 1000f) * 324f))), Color.White);
            }
            catch (Exception e) { }
            spriteBatch.End();
        }
    }
}
