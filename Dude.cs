﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DungeonPrototype
{

    interface Input
    {
        public bool MoveUp();
        public bool MoveDown();
        public bool MoveLeft();
        public bool MoveRight();
        public bool DoAttack();
    }
    class PlayerInput : Input
    {
        public bool MoveUp()
        {
            return Keyboard.GetState().IsKeyDown(Keys.W);
        }
        public bool MoveDown()
        {
            return Keyboard.GetState().IsKeyDown(Keys.S);
        }
        public bool MoveLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.A);
        }
        public bool MoveRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.D);
        }
        public bool DoAttack()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }
    }
    class MonsterInput : Input
    {
        public bool MoveUp()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }
        public bool MoveDown()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Down);
        }
        public bool MoveLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Left);
        }
        public bool MoveRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Right);
        }
        public bool DoAttack()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Enter);
        }
    }


    class AnimationFrame
    {
        public int RelativeX { get; set; } = 0;
        public int RelativeY { get; set; } = 0;

        public bool AbsoluteRotation = true;
        public float Rotation { get; set; } = 0;
        public float OriginOffsetX { get; set; } = 0;
        public float OriginOffsetY { get; set; } = 0;

        public bool FlipHorizontally = false;
        public bool FlipVertically = false;

        public int DestinationW { get; set; } = 0;
        public int DestinationH { get; set; } = 0;

        public virtual Rectangle GetSource(Dude owner)
        {
            return new Rectangle(0, 0, 0, 0);
        }
    }

    class ManualFrame : AnimationFrame
    {
        public int SourceTop { get; set; } = 0;
        public int SourceLeft { get; set; } = 0;
        public int SourceW { get; set; } = 0;
        public int SourceH { get; set; } = 0;

        public override Rectangle GetSource(Dude owner)
        {
            return new Rectangle(SourceLeft, SourceTop, SourceW, SourceH);
        }
    }

    class AnimationClip
    {
        public AnimationFrame[] Frames { get; set; }
        public int currentFrame = 0;

        public virtual void Tick()
        {
            if (Frames == null || Frames.Length == 0)
                return;

            currentFrame++;
            if (currentFrame >= Frames.Length)
                currentFrame = 0;
        }

        public virtual void Draw(Dude owner, SpriteBatch sb, Texture2D sheet, int x, int y, float rotation)
        {
            if (Frames == null || Frames.Length == 0)
                return;

            var frame = Frames[currentFrame];
            var finalRotation = (frame.AbsoluteRotation ? 0f : rotation) + frame.Rotation;
            var spriteEffectFlags = (frame.FlipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (frame.FlipVertically ? SpriteEffects.FlipVertically : SpriteEffects.None);

            var source = frame.GetSource(owner);
            sb.Draw(sheet, new Rectangle(x + frame.RelativeX, y + frame.RelativeY, frame.DestinationW, frame.DestinationW), source, Color.White, finalRotation, new Vector2(frame.OriginOffsetX, frame.OriginOffsetY), spriteEffectFlags, 0);
        }
    }

    class ItertativeAnimationClip : AnimationClip
    {
        public int VerticalFrames { get; set; } = 0;
        public int HorizontalFrames { get; set; } = 0;
        public int TotalFrames { get; set; } = 0;

        public AnimationFrame BasicFrame { get; set; }

        public override void Tick()
        {
            if (BasicFrame == null || TotalFrames == 0 || VerticalFrames == 0 || HorizontalFrames == 0)
                return;

            currentFrame++;
            if (currentFrame >= TotalFrames)
                currentFrame = 0;
        }

        public override void Draw(Dude owner, SpriteBatch sb, Texture2D sheet, int x, int y, float rotation)
        {
            if (BasicFrame == null || TotalFrames == 0 || VerticalFrames == 0 || HorizontalFrames == 0)
                return;

            var frame = BasicFrame;
            var finalRotation = (frame.AbsoluteRotation ? 0f : rotation) + frame.Rotation;
            var spriteEffectFlags = (frame.FlipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (frame.FlipVertically ? SpriteEffects.FlipVertically : SpriteEffects.None);

            var source = frame.GetSource(owner);
            source.X += (currentFrame % HorizontalFrames) * source.Width;
            source.Y += (currentFrame % VerticalFrames) * source.Height;

            sb.Draw(sheet, new Rectangle(x + frame.RelativeX, y + frame.RelativeY, frame.DestinationW, frame.DestinationW), source, Color.White, finalRotation, new Vector2(frame.OriginOffsetX, frame.OriginOffsetY), spriteEffectFlags, 0);
        }
    }

    class AnimationState
    {
        public string Name { get; set; } = "";

        public string BodyKey { get; set; } = "generic";
        public string HatKey { get; set; } = "";
        public string ArmorKey { get; set; } = "";
        public string WeaponKey { get; set; } = "";
        public string WeaponEffectKey { get; set; } = "basic-slash";
    }


    class Animator
    {
        public const int W = 32;

        public static Dictionary<string, AnimationClip> genericClipMap = new Dictionary<string, AnimationClip>()
        {
            {"body_white_idle_east", new ItertativeAnimationClip() {
                HorizontalFrames = 1,
                VerticalFrames = 3,
                TotalFrames = 3,
                BasicFrame = new ManualFrame() {
                    RelativeX = 0,
                    RelativeY = 0,
                    DestinationW = W,
                    DestinationH = W,
                    SourceTop = 1*W,
                    SourceLeft = 4*W,
                    SourceW = W,
                    SourceH = W
               }
            }},
            {"body_white_idle_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_white_idle_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_white_idle_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_red_idle_east", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_red_idle_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_red_idle_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_red_idle_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 3*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"armor_cyan_idle_east", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 6*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"armor_cyan_idle_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 6*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"armor_cyan_idle_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 6*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"armor_cyan_idle_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 6*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"hat_green_idle_east", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    },
                }
            }},
            {"hat_green_idle_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    },
                }
            }},
            {"hat_green_idle_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    },
                }
            }},
            {"hat_green_idle_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    },
                }
            }},
            {"weapon_pink-sword_attack_east", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -0.78f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -0.9f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.12f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.28f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"weapon_pink-sword_attack_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -1f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -1.2f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.62f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.66f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"weapon_pink-sword_attack_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.78f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.9f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -0.12f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -0.28f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"weapon_pink-sword_attack_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -1f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -1.2f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.62f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.66f,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 4*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_white_attack_east", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_white_attack_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_white_attack_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"body_white_attack_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 2*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    },
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_east", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 16,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_south", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 16,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_west", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = -16,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_north", new AnimationClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = -16,
                        DestinationW = W,
                        DestinationH = W,
                        SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
                    },new ManualFrame() {
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1
                    }
                }
            }},
        };

        public static float perFrameTime = 100f; // ~10fps
        public float curFrameTime = 0;

        public string state = "idle";



        public void Update(Dude owner, GameTime gameTime)
        {
            if (owner.IsAttacking)
                state = "attack";
            else
                state = "idle";

            curFrameTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (curFrameTime >= perFrameTime)
            {
                curFrameTime -= perFrameTime;

                // east, south, west, north == 0, 1, 2, 3
                var direction = "";
                switch (owner.Direction)
                {
                    case 0:
                        direction = "east";
                        break;
                    case 1:
                        direction = "south";
                        break;
                    case 2:
                        direction = "west";
                        break;
                    case 3:
                        direction = "north";
                        break;
                }
                var animationBodyKey = $"body_{owner.body}_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationBodyKey))
                {
                    genericClipMap[animationBodyKey].Tick();
                }

                var animationArmorKey = $"armor_{owner.armor}_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationArmorKey))
                {
                    genericClipMap[animationArmorKey].Tick();
                }

                var animationHatKey = $"hat_{owner.hat}_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationHatKey))
                {
                    genericClipMap[animationHatKey].Tick();
                }

                var animationWeaponKey = $"weapon_{owner.weapon}_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    genericClipMap[animationWeaponKey].Tick();
                }

                var animationWeaponEffectKey = $"weapon-effect_{owner.weapon_effect}_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    genericClipMap[animationWeaponEffectKey].Tick();
                }
            }
        }

        public void Draw(Dude owner, SpriteBatch sb, Texture2D sheet, int x, int y, float rotation)
        {
            // east, south, west, north == 0, 1, 2, 3
            var direction = "";
            switch (owner.Direction)
            {
                case 0:
                    direction = "east";
                    break;
                case 1:
                    direction = "south";
                    break;
                case 2:
                    direction = "west";
                    break;
                case 3:
                    direction = "north";
                    break;
            }


            var animationWeaponKey = $"weapon_{owner.weapon}_{state}_{direction}";
            var animationWeaponEffectKey = $"weapon-effect_{owner.weapon_effect}_{state}_{direction}";
            if (direction == "west" || direction == "north")
            {
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    genericClipMap[animationWeaponKey].Draw(owner, sb, sheet, x, y, rotation);
                }

                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    genericClipMap[animationWeaponEffectKey].Draw(owner, sb, sheet, x, y, rotation);
                }
            }


            var animationBodyKey = $"body_{owner.body}_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationBodyKey))
            {
                genericClipMap[animationBodyKey].Draw(owner, sb, sheet, x, y, rotation);
            }

            var animationArmorKey = $"armor_{owner.armor}_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationArmorKey))
            {
                genericClipMap[animationArmorKey].Draw(owner, sb, sheet, x, y, rotation);
            }

            var animationHatKey = $"hat_{owner.hat}_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationHatKey))
            {
                genericClipMap[animationHatKey].Draw(owner, sb, sheet, x, y, rotation);
            }


            if (direction == "east" || direction == "south")
            {
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    genericClipMap[animationWeaponKey].Draw(owner, sb, sheet, x, y, rotation);
                }

                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    genericClipMap[animationWeaponEffectKey].Draw(owner, sb, sheet, x, y, rotation);
                }
            }
        }

    }

    class Dude
    {

        public const int W = 32;

        public Input input;
        public Animator animator;

        /**
         * 1. Head - Always in front of body
         * 2. Body - core character 'skin'
         * 3. Armor - Always in front of body
         * 4. Weapon - On top when right or down, behind when left or up
         * 5. Weappon-Effect - On top when right or down, behind when left or up
         * 6. various spell effects or whatever
         */
        public string hat = "green";
        public string body = "white";
        public string armor = "cyan";
        public string weapon = "pink-sword";
        public string weapon_effect = "basic-slash";

        // right, down, left, up == 0, 1, 2, 3
        public int Direction { get; set; } = 0;
        public int X { get; set; } = 100;
        public int Y { get; set; } = 100;
        public int PreviousX { get; set; } = 100;
        public int PreviousY { get; set; } = 100;
        public int Layer { get; set; } = 0;

        public int Speed { get; set; } = 1;

        public int Health = 100;
        public int MaxHealth = 100;

        public float hitDelay = 60;
        public float hitDelayTimer = 0;

        public bool IsAttacking { get; set; } = false;
        public float AttackSwingTimer { get; set; } = 0;
        public float AttackSwingSpeed { get; set; } = 0.03f;
        public float AttackArcTop { get; set; } = -0.7f;
        public float AttackArcBot { get; set; } = 0.7f;
        public float P0 { get; set; } = -7f;
        public float P4 { get; set; } = 7f;

        public Dude(Animator animator, Input input, int x, int y)
        {
            this.input = input;
            this.animator = animator;
            this.X = x;
            this.Y = y;
            this.PreviousX = x;
            this.PreviousY = y;
        }

        public void Update(GameTime gameTime, MapLayer layer, Dude[] dudes)
        {
            PreviousX = X;
            PreviousY = Y;

            if (IsAttacking)
            {
                AttackSwingTimer += AttackSwingSpeed;
                if (AttackSwingTimer > 1f)
                    IsAttacking = false;
            }

            if (input.MoveRight() && !IsCollision(X + Speed, Y, layer, dudes))
            {
                Direction = 0;
                X += Speed;
            }
            if (input.MoveLeft() && !IsCollision(X - Speed, Y, layer, dudes))
            {
                Direction = 2;
                X -= Speed;
            }
            if (input.MoveDown() && !IsCollision(X, Y + Speed, layer, dudes))
            {
                Direction = 1;
                Y += Speed;
            }
            if (input.MoveUp() && !IsCollision(X, Y - Speed, layer, dudes))
            {
                Direction = 3;
                Y -= Speed;
            }
            if (input.DoAttack() && !IsAttacking)
            {
                IsAttacking = true;
                AttackSwingTimer = 0;
            }


            if (layer.IsStairUp(X + (int)(W / 2f), Y + (int)(W / 2f)))
            {
                Layer++;
            }
            else if (layer.IsStairDown(X + (int)(W / 2f), Y + W))
            {
                Layer--;
            }

            if (hitDelayTimer >= hitDelay && GotHit(dudes))
            {
                Health -= 1;
                if (Health < 0)
                    Health = 0;

                hitDelayTimer = 0;
            }
            else if (hitDelayTimer < hitDelay)
            {
                hitDelayTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            animator.Update(this, gameTime);
        }

        public bool WillHit(int bottom, int top, int left, int right)
        {
            if (!IsAttacking)
                return false;

            var bottom2 = Y + W;
            var top2 = bottom2 - 16;
            var left2 = X + 7;
            var right2 = X + W - 7;

            // right, down, left, up == 0, 1, 2, 3
            switch (Direction)
            {
                case 0:
                    right2 += 28;
                    break;
                case 1:
                    bottom2 += 28;
                    break;
                case 2:
                    left2 -= 28;
                    break;
                case 3:
                    top2 -= 28;
                    break;
                default:
                    break;
            }

            return ((bottom2 >= top && top >= top2) || (bottom2 >= bottom && bottom >= top2)) && ((left2 <= left && left <= right2) || (left2 <= right && right <= right2));
        }

        private bool GotHit(Dude[] dudes)
        {
            var bottom = Y + W;
            var top = bottom - 16;
            var left = X + 7;
            var right = X + W - 7;

            foreach (var dude in dudes)
            {
                if (dude.Layer == Layer && dude.WillHit(bottom, top, left, right))
                    return true;
            }

            return false;
        }

        private bool IsCollision(int x, int y, MapLayer layer, Dude[] dudes)
        {
            var bottom = y + W;
            var top = bottom - 16;
            var left = x + 7;
            var right = x + W - 7;

            foreach (var dude in dudes)
            {
                var bottom2 = dude.Y + W;
                var top2 = bottom2 - 16;
                var left2 = dude.X + 7;
                var right2 = dude.X + W - 7;

                if (dude.Layer == Layer && ((bottom2 >= top && top >= top2) || (bottom2 >= bottom && bottom >= top2)) && ((left2 <= left && left <= right2) || (left2 <= right && right <= right2)))
                    return true;
            }

            return layer.IsWall(left, bottom) || layer.IsWall(right, bottom) || layer.IsWall(left, top) || layer.IsWall(right, top);
        }

        public void Draw(SpriteBatch sb, Texture2D sheet, SpriteFont font)
        {
            sb.DrawString(font, $"{Health} / {MaxHealth}", new Vector2(X - 15, Y - 15), Color.Red);
            animator.Draw(this, sb, sheet, X, Y, (MathHelper.PiOver2 * Direction));
        }
    }
}
