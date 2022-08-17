using System.Collections.Generic;

namespace DungeonPrototype.Animation
{
    struct FrameTransform
    {
        public int RelativeX;
        public int RelativeY;
        public int DestinationW;
        public int DestinationH;

        public bool RelativeRotation;
        public float Rotation;
        public float OriginOffsetX;
        public float OriginOffsetY;

        public bool FlipHorizontally;
        public bool FlipVertically;
    }

    struct FrameSource
    {
        public int SourceTop;
        public int SourceLeft;
        public int SourceW;
        public int SourceH;
    }

    interface FrameSourcer
    {
        public FrameSource GetSource(Dude owner);
    }

    struct AnimationFrame
    {
        public FrameTransform Transform;
        public FrameSourcer Sourcer;
    }

    struct DirectSourcer : FrameSourcer
    {
        public FrameSource Source;

        public FrameSource GetSource(Dude owner)
        {
            return Source;
        }
    }

    struct HatSourcer : FrameSourcer
    {
        private static Dictionary<string, FrameSource> sources = new Dictionary<string, FrameSource>()
        {
            { "green", new FrameSource()
                {
                    SourceLeft = 0,
                    SourceTop = 4 * 32,
                    SourceW = 32,
                    SourceH = 32
            }},
            { "cowboy", new FrameSource()
                {
                    SourceLeft = 0,
                    SourceTop = 7 * 32,
                    SourceW = 32,
                    SourceH = 32
            }},
        };
        private static HatSourcer _instance = new HatSourcer();
        public static HatSourcer GetInstance()
        {
            return _instance;
        }

        public FrameSource GetSource(Dude owner)
        {
            if (sources.ContainsKey(owner.hat))
            {
                var source = sources[owner.hat];
                source.SourceLeft += owner.Direction * 32;

                return source;
            }

            return new FrameSource();
        }
    }

    struct MaskSourcer : FrameSourcer
    {
        private static MaskSourcer _instance = new MaskSourcer();
        public static MaskSourcer GetInstance()
        {
            return _instance;
        }

        public FrameSource GetSource(Dude owner)
        {
            switch (owner.mask)
            {
                case "sunglasses":
                    return new FrameSource()
                    {
                        SourceLeft = owner.Direction * 32,
                        SourceTop = 9 * 32,
                        SourceW = 32,
                        SourceH = 32
                    };
                default:
                    return new FrameSource();
            }
        }
    }

    struct WeaponSourcer : FrameSourcer
    {
        private static WeaponSourcer _instance = new WeaponSourcer();
        public static WeaponSourcer GetInstance()
        {
            return _instance;
        }

        public FrameSource GetSource(Dude owner)
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

    struct ArmorSourcer : FrameSourcer
    {
        private static ArmorSourcer _instance = new ArmorSourcer();
        public static ArmorSourcer GetInstance()
        {
            return _instance;
        }

        public FrameSource GetSource(Dude owner)
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
                case "biker":
                    return new FrameSource()
                    {
                        SourceLeft = owner.Direction * 32,
                        SourceTop = 8 * 32,
                        SourceW = 32,
                        SourceH = 32
                    };
                default:
                    return new FrameSource();
            }
        }
    }

}
