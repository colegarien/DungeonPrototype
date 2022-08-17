using System;

namespace DungeonPrototype.Animation
{
    struct Clip
    {
        public bool RunOnce;
        public AnimationFrame[] Frames;

        public Clip(FrameTransform rootTransform, FrameSource rootSource, int horizontalFrames, int verticalFrames, bool loop=true)
        {
            var frames = new AnimationFrame[horizontalFrames * verticalFrames];
            for (var index = 0; index < frames.Length; index++)
            {
                var newSource = rootSource;
                newSource.SourceLeft += (index % horizontalFrames) * newSource.SourceW;
                newSource.SourceTop += (index % verticalFrames) * newSource.SourceH;

                frames[index] = new AnimationFrame
                {
                    Transform = rootTransform,
                    Sourcer = new DirectSourcer() { Source = newSource }
                };
            }

            RunOnce = !loop;
            Frames = frames;
        }

        public int Tick(int index)
        {
            var lastFrame = GetLastFrame();
            if (!RunOnce && index == lastFrame)
                return 0;
            else if (RunOnce && index == lastFrame)
                return index;

            return NextFrameIndex(index);
        }

        public AnimationFrame GetFrame(int index)
        {
            return Frames[index % Frames.Length];
        }

        public int GetLastFrame()
        {
            return Frames.Length - 1;
        }

        public int NextFrameIndex(int index)
        {
            return (index + 1) % Frames.Length;
        }

    }

}
