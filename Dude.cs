using DungeonPrototype.Animation;
using Microsoft.Xna.Framework;
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

    class Animator
    {
        public const int W = 32;
        public static Dictionary<string, Clip> genericClipMap = new Dictionary<string, Clip>()
        {
            {"body_white_idle_east", new LinearClip(
                new ManualFrame() {
                    Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                    },
                    Source = new FrameSource(){
                        SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
                    }
                }, 1, 3)
            },
            {"body_white_idle_south", new LinearClip(
                new ManualFrame() {
                    Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                    },
                    Source = new FrameSource(){
                        SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
                    }
                }, 1, 3)
            },
            {"body_white_idle_west", new LinearClip(
                new ManualFrame() {
                    Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                    },
                    Source = new FrameSource(){
                        SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
                    }
                }, 1, 3)
            },
            {"body_white_idle_north", new LinearClip(
                new ManualFrame() {
                    Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                    },
                    Source = new FrameSource(){
                        SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
                    }
                }, 1, 3)
            },
            {"body_red_idle_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
            }},
            {"body_red_idle_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
            }},
            {"body_red_idle_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
            }},
            {"body_red_idle_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
            }},
            {"armor_generic_idle_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"armor_generic_idle_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"armor_generic_idle_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"armor_generic_idle_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"hat_generic_idle_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"hat_generic_idle_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"hat_generic_idle_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"hat_generic_idle_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_idle_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_idle_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_idle_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_idle_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"armor_generic_attack_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"armor_generic_attack_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"armor_generic_attack_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"armor_generic_attack_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ArmorFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
            }},
            {"hat_generic_attack_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
},
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
},
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
},
                    },
                }
}},
            {"hat_generic_attack_south", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
    }},
            {"hat_generic_attack_west", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
    }},
            {"hat_generic_attack_north", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new HatFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
    }},
            {"mask_generic_attack_east", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_attack_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_attack_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"mask_generic_attack_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new MaskFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                }
            }},
            {"weapon_generic_attack_east", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -0.78f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -0.9f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.12f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.28f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
    }},
            {"weapon_generic_attack_south", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -1f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -1.2f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.62f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.66f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
    }},
            {"weapon_generic_attack_west", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.78f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.9f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -0.12f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -0.28f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
    }},
            {"weapon_generic_attack_north", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -1f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = -1.2f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.62f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    },
                    new WeaponFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        AbsoluteRotation = false,
                        OriginOffsetY = 16,
                        Rotation = 0.66f,
                        DestinationW = W,
                        DestinationH = W
                        },
                    }
                }
    }},
            {"body_white_attack_east", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_white_attack_south", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_white_attack_west", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_white_attack_north", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_red_attack_east", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_red_attack_south", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_red_attack_west", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"body_red_attack_north", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    },
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    }
                }
    }},
            {"weapon-effect_basic-slash_attack_east", new FramesClip()
    {
        Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 16,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1
}
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_south", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 16,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1
}
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_west", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform(){
RelativeX = -16,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W
}
                    },new ManualFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1
}
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_north", new FramesClip() {
                Frames = new AnimationFrame[]
                {
                    new ManualFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1
}
                    },new ManualFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = -16,
                            DestinationW = W,
                            DestinationH = W
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W
}
                    },new ManualFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1
}
                    }
                }
            }},
        };
        public static float perFrameTime = 100f; // ~10fps

        public float curFrameTime = 0;
        public string state = "idle";
        public Dictionary<string, int> frameIndexes = new Dictionary<string, int>();


        public void Update(Dude owner, GameTime gameTime)
        {
            if (owner.IsAttacking && state != "attack")
            {
                state = "attack";
                // reset all frame counters
                frameIndexes.Clear();
            }
            else if (!owner.IsAttacking && state != "idle")
            {
                state = "idle";
                // reset all frame counters
                frameIndexes.Clear();
            }

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
                    frameIndexes[animationBodyKey] = genericClipMap[animationBodyKey].Tick(frameIndexes.GetValueOrDefault(animationBodyKey, 0));
                }

                var animationArmorKey = $"armor_{owner.armor}_{state}_{direction}";
                if (!genericClipMap.ContainsKey(animationArmorKey) && owner.armor != "")
                    animationArmorKey = $"armor_generic_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationArmorKey))
                {
                    frameIndexes[animationArmorKey] = genericClipMap[animationArmorKey].Tick(frameIndexes.GetValueOrDefault(animationArmorKey, 0));
                }

                var animationMaskKey = $"mask_{owner.mask}_{state}_{direction}";
                if (!genericClipMap.ContainsKey(animationMaskKey) && owner.mask != "")
                    animationMaskKey = $"mask_generic_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationMaskKey))
                {
                    frameIndexes[animationMaskKey] = genericClipMap[animationMaskKey].Tick(frameIndexes.GetValueOrDefault(animationMaskKey, 0));
                }

                var animationHatKey = $"hat_{owner.hat}_{state}_{direction}";
                if (!genericClipMap.ContainsKey(animationHatKey) && owner.hat != "")
                    animationHatKey = $"hat_generic_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationHatKey))
                {
                    frameIndexes[animationHatKey] = genericClipMap[animationHatKey].Tick(frameIndexes.GetValueOrDefault(animationHatKey, 0));
                }

                var animationWeaponKey = $"weapon_{owner.weapon}_{state}_{direction}";
                if (!genericClipMap.ContainsKey(animationWeaponKey) && owner.weapon != "")
                    animationWeaponKey = $"weapon_generic_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    frameIndexes[animationWeaponKey] = genericClipMap[animationWeaponKey].Tick(frameIndexes.GetValueOrDefault(animationWeaponKey, 0));
                }

                var animationWeaponEffectKey = $"weapon-effect_{owner.weapon_effect}_{state}_{direction}";
                if (!genericClipMap.ContainsKey(animationWeaponEffectKey) && owner.weapon_effect != "")
                    animationWeaponEffectKey = $"weapon-effect_generic_{state}_{direction}";
                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    frameIndexes[animationWeaponEffectKey] = genericClipMap[animationWeaponEffectKey].Tick(frameIndexes.GetValueOrDefault(animationWeaponEffectKey, 0));
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
            if (!genericClipMap.ContainsKey(animationWeaponKey) && owner.weapon != "")
                animationWeaponKey = $"weapon_generic_{state}_{direction}";
            var animationWeaponEffectKey = $"weapon-effect_{owner.weapon_effect}_{state}_{direction}";
            if (!genericClipMap.ContainsKey(animationWeaponEffectKey) && owner.weapon_effect != "")
                animationWeaponEffectKey = $"weapon-effect_generic_{state}_{direction}";
            if (direction == "west" || direction == "north")
            {
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponKey], frameIndexes.GetValueOrDefault(animationWeaponKey, 0), owner, sb, sheet, x, y, rotation);
                }

                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponEffectKey], frameIndexes.GetValueOrDefault(animationWeaponEffectKey, 0), owner, sb, sheet, x, y, rotation);
                }
            }


            var animationBodyKey = $"body_{owner.body}_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationBodyKey))
            {
                DrawCurrentFrame(genericClipMap[animationBodyKey], frameIndexes.GetValueOrDefault(animationBodyKey, 0), owner, sb, sheet, x, y, rotation);
            }

            var animationArmorKey = $"armor_{owner.armor}_{state}_{direction}";
            if (!genericClipMap.ContainsKey(animationArmorKey) && owner.armor != "")
                animationArmorKey = $"armor_generic_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationArmorKey))
            {
                DrawCurrentFrame(genericClipMap[animationArmorKey], frameIndexes.GetValueOrDefault(animationArmorKey, 0), owner, sb, sheet, x, y, rotation);
            }

            var animationMaskKey = $"mask_{owner.mask}_{state}_{direction}";
            if (!genericClipMap.ContainsKey(animationMaskKey) && owner.mask != "")
                animationMaskKey = $"mask_generic_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationMaskKey))
            {
                DrawCurrentFrame(genericClipMap[animationMaskKey], frameIndexes.GetValueOrDefault(animationMaskKey, 0), owner, sb, sheet, x, y, rotation);
            }

            var animationHatKey = $"hat_{owner.hat}_{state}_{direction}";
            if (!genericClipMap.ContainsKey(animationHatKey) && owner.hat != "")
                animationHatKey = $"hat_generic_{state}_{direction}";
            if (genericClipMap.ContainsKey(animationHatKey))
            {
                DrawCurrentFrame(genericClipMap[animationHatKey], frameIndexes.GetValueOrDefault(animationHatKey, 0), owner, sb, sheet, x, y, rotation);
            }


            if (direction == "east" || direction == "south")
            {
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponKey], frameIndexes.GetValueOrDefault(animationWeaponKey, 0), owner, sb, sheet, x, y, rotation);
                }

                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponEffectKey], frameIndexes.GetValueOrDefault(animationWeaponEffectKey, 0), owner, sb, sheet, x, y, rotation);
                }
            }
        }


        public virtual void DrawCurrentFrame(Clip clip, int frameIndex, Dude owner, SpriteBatch sb, Texture2D sheet, int x, int y, float rotation)
        {
            var frame = clip.GetFrame(frameIndex);
            if (frame == null)
                return;

            var transform = frame.Transform;
            var finalRotation = (transform.AbsoluteRotation ? 0f : rotation) + transform.Rotation;
            var spriteEffectFlags = (transform.FlipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (transform.FlipVertically ? SpriteEffects.FlipVertically : SpriteEffects.None);

            var source = frame.GetSource(owner);
            sb.Draw(sheet, new Rectangle(x + transform.RelativeX, y + transform.RelativeY, transform.DestinationW, transform.DestinationW), new Rectangle(source.SourceLeft, source.SourceTop, source.SourceW, source.SourceH), Color.White, finalRotation, new Vector2(transform.OriginOffsetX, transform.OriginOffsetY), spriteEffectFlags, 0);
        }

    }

    class Dude
    {

        public const int W = 32;

        public Input input;
        public Animator animator;

        /** TODO this!?
         * 1. Head - Always in front of body
         * 1* Mask - Always in front of body, behind head-wear
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
        public string mask = "";

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
