﻿using Microsoft.Xna.Framework;

namespace DungeonPrototype.Animation
{
    class FrameTransform
    {
        public int RelativeX { get; set; } = 0;
        public int RelativeY { get; set; } = 0;
        public int DestinationW { get; set; } = 0;
        public int DestinationH { get; set; } = 0;

        public bool AbsoluteRotation = true;
        public float Rotation { get; set; } = 0;
        public float OriginOffsetX { get; set; } = 0;
        public float OriginOffsetY { get; set; } = 0;

        public bool FlipHorizontally = false;
        public bool FlipVertically = false;
    }

    class FrameSource
    {
        public int SourceTop { get; set; } = 0;
        public int SourceLeft { get; set; } = 0;
        public int SourceW { get; set; } = 0;
        public int SourceH { get; set; } = 0;
    }

    class AnimationFrame
    {
        public FrameTransform Transform { get; set; }

        public virtual FrameSource GetSource(Dude owner)
        {
            return new FrameSource();
        }

        public AnimationFrame ShallowCopy()
        {
            return (AnimationFrame)this.MemberwiseClone();
        }
    }

    class ManualFrame : AnimationFrame
    {
        public FrameSource Source { get; set; }

        public override FrameSource GetSource(Dude owner)
        {
            return Source;
        }
    }

    class HatFrame : AnimationFrame
    {
        public override FrameSource GetSource(Dude owner)
        {
            switch (owner.hat)
            {
                case "green":
                    return new FrameSource()
                    {
                        SourceLeft = owner.Direction * 32,
                        SourceTop = 4 * 32,
                        SourceW = 32,
                        SourceH = 32
                    };
                default:
                    return new FrameSource();
            }
        }
    }

    class WeaponFrame : AnimationFrame
    {
        public override FrameSource GetSource(Dude owner)
        {
            switch (owner.weapon)
            {
                case "pink-sword":
                    return new FrameSource()
                    {
                        SourceLeft = 6 * 32,
                        SourceTop = 4 * 32,
                        SourceW = 32,
                        SourceH = 32
                    };
                case "grey-dagger":
                    return new FrameSource()
                    {
                        SourceLeft = 4 * 32,
                        SourceTop = 4 * 32,
                        SourceW = 32,
                        SourceH = 32
                    };
                default:
                    return new FrameSource();
            }
        }
    }

    class ArmorFrame : AnimationFrame
    {
        public override FrameSource GetSource(Dude owner)
        {
            switch (owner.armor)
            {
                case "cyan":
                    return new FrameSource()
                    {
                        SourceLeft = owner.Direction * 32,
                        SourceTop = 6 * 32,
                        SourceW = 32,
                        SourceH = 32
                    };
                default:
                    return new FrameSource();
            }
        }
    }

}
