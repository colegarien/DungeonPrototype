﻿namespace DungeonPrototype.Animation
{
    internal abstract class Clip
    {
        public bool Loop { get; set; } = true;
        
        protected abstract int GetLastFrame();
        protected abstract int NextFrameIndex(int index);

        public int Tick(int index)
        {
            var lastFrame = GetLastFrame();
            if (Loop && index == lastFrame)
                return 0;
            else if (!Loop && index == lastFrame)
                return index;

            return NextFrameIndex(index);
        }

        public abstract AnimationFrame GetFrame(int index);
    }

    internal class FramesClip : Clip
    {
        public AnimationFrame[] Frames { get; set; }

        public override AnimationFrame GetFrame(int index)
        {
            return Frames[index % Frames.Length];
        }

        protected override int GetLastFrame()
        {
            return Frames.Length - 1;
        }

        protected override int NextFrameIndex(int index)
        {
            return (index + 1) % Frames.Length;
        }
    }

    internal class LinearClip : FramesClip
    {
        public LinearClip(ManualFrame rootFrame, int horizontalFrames, int verticalFrames)
        {
            Frames = new AnimationFrame[horizontalFrames * verticalFrames];
            Frames[0] = rootFrame;
            for(var index = 1; index < Frames.Length; index++)
            {
                var newFrame = new ManualFrame
                {
                    Transform = rootFrame.Transform.ShallowCopy(),
                    Source = rootFrame.Source.ShallowCopy()
                };

                newFrame.Source.SourceLeft += (index % horizontalFrames) * newFrame.Source.SourceW;
                newFrame.Source.SourceTop += (index % verticalFrames) * newFrame.Source.SourceH;

                Frames[index] = newFrame;
            }
        }
    }
}
