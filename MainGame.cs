﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DungeonPrototype
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MapLayer layer1;
        private MapLayer layer2;

        public const int W = 32;

        private SpriteFont _font;
        private Texture2D _sheet;


        private float cameraX = 10;
        private float cameraY = 10;

        private List<Dude> dudes = new List<Dude>();

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            layer1 = new MapLayer("Content/map.txt");
            layer2 = new MapLayer("Content/map2.txt");

            dudes = new List<Dude>() {
                new Dude(new Animator(),new PlayerInput(), 100, 100),
                new Dude(new Animator(), new MonsterInput(), 180, 100){body="red",hat="",armor="",weapon="grey-dagger"}
                /*new Dude(new Animator(){
                    IdleUpClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = W,
                                SourceLeft = 3*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 2*W,
                                SourceLeft = 3*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 3*W,
                                SourceLeft = 3*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    IdleDownClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = W,
                                SourceLeft = 1*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 2*W,
                                SourceLeft = 1*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 3*W,
                                SourceLeft = 1*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    IdleLeftClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = W,
                                SourceLeft = 2*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 2*W,
                                SourceLeft = 2*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 3*W,
                                SourceLeft = 2*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    IdleRightClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 2*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 3*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    AttackUpClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                FlipVertically = true,
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = -0.7f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                FlipVertically = true,
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = 0.5f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                FlipVertically = true,
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = 0.7f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            }
                        },{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 4*W,
                                SourceLeft = 3*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 5*W,
                                SourceLeft = 3*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 6*W,
                                SourceLeft = 3*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    AttackDownClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 4*W,
                                SourceLeft = 1*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 5*W,
                                SourceLeft = 1*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 6*W,
                                SourceLeft = 1*W,
                                SourceW = W,
                                SourceH = W
                            }
                        },{
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = -0.7f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = 0.5f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = 0.7f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    AttackLeftClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                FlipVertically = true,
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = -0.7f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                FlipVertically = true,
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = 0.5f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = (int)(W * .5f),
                                RelativeY = (int)(W * .5f),
                                FlipVertically = true,
                                AbsoluteRotation = false,
                                OriginOffsetY = (int)(W * .5f),
                                Rotation = 0.7f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            }
                        },{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 4*W,
                                SourceLeft = 2*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 5*W,
                                SourceLeft = 2*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 6*W,
                                SourceLeft = 2*W,
                                SourceW = W,
                                SourceH = W
                            }
                        }}
                    },
                    AttackRightClip = new AnimationClip(){
                        Frames = new AnimationFrame[,] {{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 1*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 1*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 2*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 1*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            }
                        },{
                            new AnimationFrame() {
                                RelativeX = 18,
                                RelativeY = 22,
                                AbsoluteRotation = false,
                                OriginOffsetY = 16,
                                Rotation = -0.78f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 19,
                                RelativeY = 21,
                                AbsoluteRotation = false,
                                OriginOffsetY = 16,
                                Rotation = -0.9f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 19,
                                RelativeY = 22,
                                AbsoluteRotation = false,
                                OriginOffsetY = 16,
                                Rotation = 0.12f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            },
                            new AnimationFrame() {
                                RelativeX = 18,
                                RelativeY = 22,
                                AbsoluteRotation = false,
                                OriginOffsetY = 16,
                                Rotation = 0.28f,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 7*W,
                                SourceLeft = 6*W,
                                SourceW = W,
                                SourceH = W
                            }
                        },{
                            new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = 1,
                                DestinationH = 1,
                                SourceTop = 8*W,
                                SourceLeft = 0*W,
                                SourceW = 1,
                                SourceH = 1
                            },new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = 1,
                                DestinationH = 1,
                                SourceTop = 8*W,
                                SourceLeft = 0*W,
                                SourceW = 1,
                                SourceH = 1
                            },new AnimationFrame() {
                                RelativeX = 16,
                                RelativeY = 0,
                                DestinationW = W,
                                DestinationH = W,
                                SourceTop = 8*W,
                                SourceLeft = 0*W,
                                SourceW = W,
                                SourceH = W
                            },new AnimationFrame() {
                                RelativeX = 0,
                                RelativeY = 0,
                                DestinationW = 1,
                                DestinationH = 1,
                                SourceTop = 8*W,
                                SourceLeft = 0*W,
                                SourceW = 1,
                                SourceH = 1
                            }
                        }}
                    }
                }, new MonsterInput(), 180, 100)*/
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("tehfont");
            _sheet = Content.Load<Texture2D>("sheit");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var dude in dudes)
            {
                var layer = dude.Layer == 0 ? layer1 : layer2;
                dude.Update(gameTime, layer, dudes.Except(new[] { dude }).ToArray());
            }

            cameraX = Math.Clamp(dudes[0].X + (W / 2) - (_graphics.PreferredBackBufferWidth / 4), 0, (dudes[0].Layer == 0 ? layer1.getWidth() : layer2.getWidth()) * W - _graphics.PreferredBackBufferWidth / 2f);
            cameraY = Math.Clamp(dudes[0].Y + (W / 2) - (_graphics.PreferredBackBufferHeight / 4), 0, (dudes[0].Layer == 0 ? layer1.getHeight() : layer2.getHeight()) * W - _graphics.PreferredBackBufferHeight / 2f);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.Identity * Matrix.CreateTranslation(new Vector3(-(int)cameraX, -(int)cameraY, 0)) * Matrix.CreateScale(2f));
            _spriteBatch.DrawString(_font, "some text", Vector2.One * 80, Color.Black);


            if (dudes[0].Layer == 0)
            {
                layer1.Draw(_spriteBatch, _sheet);
            }
            else
            {
                layer2.Draw(_spriteBatch, _sheet);
            }

            foreach (var dude in dudes.OrderBy(d => d.Y))
            {
                if (dudes[0].Layer == dude.Layer)
                    dude.Draw(_spriteBatch, _sheet, _font);
            }

            _spriteBatch.End();


            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, $"Layer: {dudes[0].Layer}\r\nX: {dudes[0].X}\r\nY: {dudes[0].Y}\r\n", Vector2.Zero, Color.Black);
            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
