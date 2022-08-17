using DungeonPrototype.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

    class AnimatorData
    {
        public string State { get; set; } = "idle";
        public string Direction { get; set; } = "east";
        public float currentFrameTime { get; set; } = 0f;
        public float perFrameTime { get; set; } = 100f; // ~10fps
        public Dictionary<string, int> currentFrameIndexes { get; set; } = new Dictionary<string, int>();

        public string hat = "";
        public string body = "";
        public string armor = "";
        public string weapon = "";
        public string weapon_effect = "";
        public string mask = "";


        public void Update(Dude owner, float deltaTime)
        {
            State = owner.state is IdleState ? "idle" : "attack";
            // east, south, west, north == 0, 1, 2, 3
            switch (owner.Direction)
            {
                case 0:
                    Direction = "east";
                    break;
                case 1:
                    Direction = "south";
                    break;
                case 2:
                    Direction = "west";
                    break;
                case 3:
                    Direction = "north";
                    break;
            }

            hat = owner.hat;
            body = owner.body;
            armor = owner.armor;
            weapon = owner.weapon;
            weapon_effect = owner.weapon_effect;
            mask = owner.mask;
            currentFrameTime += deltaTime;
        }

        public string getHatKey(IDictionary<string, Clip> allAnimations)
        {
            return buildAnimationKey("hat", hat, allAnimations);
        }
        public string getBodyKey(IDictionary<string, Clip> allAnimations)
        {
            return buildAnimationKey("body", body, allAnimations);
        }
        public string getArmorKey(IDictionary<string, Clip> allAnimations)
        {
            return buildAnimationKey("armor", armor, allAnimations);
        }
        public string getWeaponKey(IDictionary<string, Clip> allAnimations)
        {
            return buildAnimationKey("weapon", weapon, allAnimations);
        }
        public string getWeaponEffectKey(IDictionary<string, Clip> allAnimations)
        {
            return buildAnimationKey("weapon-effect", weapon_effect, allAnimations);
        }
        public string getMaskKey(IDictionary<string, Clip> allAnimations)
        {
            return buildAnimationKey("mask", mask, allAnimations);
        }

        private string buildAnimationKey(string root, string value, IDictionary<string, Clip> allAnimations)
        {
            var key = $"{root}_{value}_{State}_{Direction}";
            if (!allAnimations.ContainsKey(key) && value != "")
                key = $"{root}_generic_{State}_{Direction}";

            return key;
        }

        public int GetFrameIndex(string key)
        {
            return currentFrameIndexes.GetValueOrDefault(key, 0);
        }

        public void UpdateFrameIndex(string key, int value)
        {
            currentFrameIndexes[key] = value;
        }

        public void ResetFrameIndexes()
        {
            currentFrameIndexes.Clear();
        }
    }

    class Animator
    {
        public const int W = 32;
        public static Dictionary<string, Clip> genericClipMap = new Dictionary<string, Clip>()
        {
            {"body_white_idle_east", new Clip(
                new FrameTransform(){
                    RelativeX = 0,
                    RelativeY = 0,
                    DestinationW = W,
                    DestinationH = W
                },
                new FrameSource(){
                    SourceTop = 1*W,
                    SourceLeft = 4*W,
                    SourceW = W,
                    SourceH = W
                }, 1, 3)
            },
            {"body_white_idle_south", new Clip(
                new FrameTransform(){
                    RelativeX = 0,
                    RelativeY = 0,
                    DestinationW = W,
                    DestinationH = W
                },
                new FrameSource(){
                    SourceTop = 1*W,
                    SourceLeft = 5*W,
                    SourceW = W,
                    SourceH = W
                } , 1, 3)
            },
            {"body_white_idle_west", new Clip(
                new FrameTransform(){
                    RelativeX = 0,
                    RelativeY = 0,
                    DestinationW = W,
                    DestinationH = W
                },
                new FrameSource(){
                    SourceTop = 1*W,
                    SourceLeft = 6*W,
                    SourceW = W,
                    SourceH = W
                } , 1, 3)
            },
            {"body_white_idle_north", new Clip(
                new FrameTransform(){
                    RelativeX = 0,
                    RelativeY = 0,
                    DestinationW = W,
                    DestinationH = W
                },
                new FrameSource(){
                    SourceTop = 1*W,
                    SourceLeft = 7*W,
                    SourceW = W,
                    SourceH = W
                }, 1, 3)
            },
            {"body_red_idle_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
            }},
            {"body_red_idle_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
            }},
            {"body_red_idle_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
            }},
            {"body_red_idle_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 3*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
            }},
            {"armor_generic_idle_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"armor_generic_idle_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"armor_generic_idle_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"armor_generic_idle_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"hat_generic_idle_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
            }},
            {"hat_generic_idle_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
            }},
            {"hat_generic_idle_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
            }},
            {"hat_generic_idle_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -9,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
            }},
            {"mask_generic_idle_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"mask_generic_idle_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"mask_generic_idle_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"mask_generic_idle_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -1,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"armor_generic_attack_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"armor_generic_attack_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"armor_generic_attack_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"armor_generic_attack_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = ArmorSourcer.GetInstance()
                    }
                }
            }},
            {"hat_generic_attack_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
}},
            {"hat_generic_attack_south", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
    }},
            {"hat_generic_attack_west", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
    }},
            {"hat_generic_attack_north", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -11,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = -12,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = HatSourcer.GetInstance(),
                    },
                }
    }},
            {"mask_generic_attack_east", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"mask_generic_attack_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"mask_generic_attack_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"mask_generic_attack_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -3,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
                        RelativeX = 0,
                        RelativeY = -4,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = MaskSourcer.GetInstance()
                    },
                }
            }},
            {"weapon_generic_attack_east", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = -0.78f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = -0.9f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = 0.12f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = 0.28f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    }
                }
    }},
            {"weapon_generic_attack_south", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = -1f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = -1.2f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = 0.62f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = 0.66f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    }
                }
    }},
            {"weapon_generic_attack_west", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.78f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        RelativeRotation = true,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = 0.9f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        RelativeRotation = true,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -0.12f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        FlipVertically = true,
                        OriginOffsetY = 16,
                        Rotation = -0.28f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    }
                }
    }},
            {"weapon_generic_attack_north", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = -1f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 21,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = -1.2f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 19,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = 0.62f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 18,
                        RelativeY = 22,
                        RelativeRotation = true,
                        OriginOffsetY = 16,
                        Rotation = 0.66f,
                        DestinationW = W,
                        DestinationH = W
                        },
                        Sourcer = WeaponSourcer.GetInstance()
                    }
                }
    }},
            {"body_white_attack_east", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 4*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_white_attack_south", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 5*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_white_attack_west", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 6*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_white_attack_north", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 7*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_red_attack_east", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_red_attack_south", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_red_attack_west", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"body_red_attack_north", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 2*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    },
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 1*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    }
                }
    }},
            {"weapon-effect_basic-slash_attack_east", new Clip()
    {
        Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 16,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = W,
                        SourceH = W}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 0*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_south", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 16,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = W,
                        SourceH = W}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 1*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_west", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = 0,
                        RelativeY = 0,
                        DestinationW = 1,
                        DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform(){
RelativeX = -16,
                        RelativeY = 0,
                        DestinationW = W,
                        DestinationH = W
},
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = W,
                        SourceH = W}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 2*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    }
                }
            }},
            {"weapon-effect_basic-slash_attack_north", new Clip() {
                Frames = new AnimationFrame[]
                {
                    new AnimationFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = -16,
                            DestinationW = W,
                            DestinationH = W
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = W,
                        SourceH = W}
}
                    },new AnimationFrame() {
                        Transform = new FrameTransform()
                        {
                            RelativeX = 0,
                            RelativeY = 0,
                            DestinationW = 1,
                            DestinationH = 1,
                        },
                        Sourcer = new DirectSourcer() { Source = new FrameSource(){
SourceTop = 5*W,
                        SourceLeft = 3*W,
                        SourceW = 1,
                        SourceH = 1}
}
                    }
                }
            }},
        };
        public static float defaultPerFrameTime = 100f; // ~10fps

        public AnimatorData data = new AnimatorData() { perFrameTime = defaultPerFrameTime };

        public bool IsOnLastFrame(Dude owner)
        {
            var animationBodyKey = $"body_{owner.body}_{data.State}_{data.Direction}";
            if (genericClipMap.ContainsKey(animationBodyKey))
            {
                return data.GetFrameIndex(animationBodyKey) == genericClipMap[animationBodyKey].GetLastFrame();
            }

            return false;
        }

        public void HandleStateChange(Dude owner)
        {
            data.ResetFrameIndexes();
            data.Update(owner, 0f);
        }

        public void Update(Dude owner, GameTime gameTime)
        {
            data.Update(owner, (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            if (data.currentFrameTime >= data.perFrameTime)
            {
                data.currentFrameTime -= data.perFrameTime;

                var animationBodyKey = data.getBodyKey(genericClipMap);
                if (genericClipMap.ContainsKey(animationBodyKey))
                {
                    data.UpdateFrameIndex(animationBodyKey, genericClipMap[animationBodyKey].Tick(data.GetFrameIndex(animationBodyKey)));
                }

                var animationArmorKey = data.getArmorKey(genericClipMap);
                if (genericClipMap.ContainsKey(animationArmorKey))
                {
                    data.UpdateFrameIndex(animationArmorKey, genericClipMap[animationArmorKey].Tick(data.GetFrameIndex(animationArmorKey)));
                }

                var animationMaskKey = data.getMaskKey(genericClipMap);
                if (genericClipMap.ContainsKey(animationMaskKey))
                {
                    data.UpdateFrameIndex(animationMaskKey, genericClipMap[animationMaskKey].Tick(data.GetFrameIndex(animationMaskKey)));
                }

                var animationHatKey = data.getHatKey(genericClipMap);
                if (genericClipMap.ContainsKey(animationHatKey))
                {
                    data.UpdateFrameIndex(animationHatKey, genericClipMap[animationHatKey].Tick(data.GetFrameIndex(animationHatKey)));
                }

                var animationWeaponKey = data.getWeaponKey(genericClipMap);
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    data.UpdateFrameIndex(animationWeaponKey, genericClipMap[animationWeaponKey].Tick(data.GetFrameIndex(animationWeaponKey)));
                }

                var animationWeaponEffectKey = data.getWeaponEffectKey(genericClipMap);
                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    data.UpdateFrameIndex(animationWeaponEffectKey, genericClipMap[animationWeaponEffectKey].Tick(data.GetFrameIndex(animationWeaponEffectKey)));
                }
            }
        }

        public void Draw(Dude owner, SpriteBatch sb, Texture2D sheet)
        {
            var animationWeaponKey = data.getWeaponKey(genericClipMap);
            var animationWeaponEffectKey = data.getWeaponEffectKey(genericClipMap);
            if (data.Direction == "west" || data.Direction == "north")
            {
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponKey], data.GetFrameIndex(animationWeaponKey), owner, sb, sheet);
                }

                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponEffectKey], data.GetFrameIndex(animationWeaponEffectKey), owner, sb, sheet);
                }
            }


            var animationBodyKey = data.getBodyKey(genericClipMap);
            if (genericClipMap.ContainsKey(animationBodyKey))
            {
                DrawCurrentFrame(genericClipMap[animationBodyKey], data.GetFrameIndex(animationBodyKey), owner, sb, sheet);
            }

            var animationArmorKey = data.getArmorKey(genericClipMap);
            if (genericClipMap.ContainsKey(animationArmorKey))
            {
                DrawCurrentFrame(genericClipMap[animationArmorKey], data.GetFrameIndex(animationArmorKey), owner, sb, sheet);
            }

            var animationMaskKey = data.getMaskKey(genericClipMap);
            if (genericClipMap.ContainsKey(animationMaskKey))
            {
                DrawCurrentFrame(genericClipMap[animationMaskKey], data.GetFrameIndex(animationMaskKey), owner, sb, sheet);
            }

            var animationHatKey = data.getHatKey(genericClipMap);
            if (genericClipMap.ContainsKey(animationHatKey))
            {
                DrawCurrentFrame(genericClipMap[animationHatKey], data.GetFrameIndex(animationHatKey), owner, sb, sheet);
            }


            if (data.Direction == "east" || data.Direction == "south")
            {
                if (genericClipMap.ContainsKey(animationWeaponKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponKey], data.GetFrameIndex(animationWeaponKey), owner, sb, sheet);
                }

                if (genericClipMap.ContainsKey(animationWeaponEffectKey))
                {
                    DrawCurrentFrame(genericClipMap[animationWeaponEffectKey], data.GetFrameIndex(animationWeaponEffectKey), owner, sb, sheet);
                }
            }
        }


        public virtual void DrawCurrentFrame(Clip clip, int frameIndex, Dude owner, SpriteBatch sb, Texture2D sheet)
        {
            var frame = clip.GetFrame(frameIndex);

            var transform = frame.Transform;
            var finalRotation = (transform.RelativeRotation ? (MathHelper.PiOver2 * owner.Direction) : 0f) + transform.Rotation;
            var spriteEffectFlags = (transform.FlipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (transform.FlipVertically ? SpriteEffects.FlipVertically : SpriteEffects.None);

            var source = frame.Sourcer.GetSource(owner);
            sb.Draw(sheet, new Rectangle(owner.X + transform.RelativeX, owner.Y + transform.RelativeY, transform.DestinationW, transform.DestinationW), new Rectangle(source.SourceLeft, source.SourceTop, source.SourceW, source.SourceH), Color.White, finalRotation, new Vector2(transform.OriginOffsetX, transform.OriginOffsetY), spriteEffectFlags, 0);
        }

    }

    class Dude
    {

        public const int W = 32;

        public Input input;
        public Animator animator;
        public State state;

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

        public Dude(Animator animator, Input input, int x, int y)
        {
            this.input = input;
            this.animator = animator;
            this.X = x;
            this.Y = y;
            this.PreviousX = x;
            this.PreviousY = y;
            this.state = new IdleState { Context = this };
        }

        public void Update(GameTime gameTime, MapLayer layer, Dude[] dudes)
        {
            PreviousX = X;
            PreviousY = Y;

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

            state.Tick((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            animator.Update(this, gameTime);
        }

        public bool WillHit(int bottom, int top, int left, int right)
        {
            if (!(state is AttackState))
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
            animator.Draw(this, sb, sheet);
        }

        public void ChangeState(State state)
        {
            this.state = state;
            animator.HandleStateChange(this);
        }
    }
}
