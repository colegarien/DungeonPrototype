using Microsoft.Xna.Framework;

namespace DungeonPrototype.Animation
{

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

        public AnimationFrame ShallowCopy()
        {
            return (AnimationFrame)this.MemberwiseClone();
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

    class HatFrame : AnimationFrame
    {
        public override Rectangle GetSource(Dude owner)
        {
            switch (owner.hat) {
                case "green":
                    return new Rectangle(owner.Direction * 32, 4 * 32, 32, 32);
                default:
                    return new Rectangle(0,0,0,0);
            }
        }
    }

    class WeaponFrame : AnimationFrame
    {
        public override Rectangle GetSource(Dude owner)
        {
            switch (owner.weapon)
            {
                case "pink-sword":
                    return new Rectangle(6 * 32, 4 * 32, 32, 32);
                case "grey-dagger":
                    return new Rectangle(4 * 32, 4 * 32, 32, 32);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }
    }

    class ArmorFrame : AnimationFrame
    {
        public override Rectangle GetSource(Dude owner)
        {
            switch (owner.armor)
            {
                case "cyan":
                    return new Rectangle(owner.Direction * 32, 6 * 32, 32, 32);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }
    }

}
