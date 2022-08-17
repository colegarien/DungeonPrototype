using Microsoft.Xna.Framework.Graphics;

namespace DungeonPrototype.Animation
{

    abstract class State
    {
        public Dude Context { get; set; }

        public abstract void Tick(float deltaTime);

        protected void TransitionTo(State state)
        {
            state.Context = Context;
            Context.ChangeState(state);
        }
    }

    class IdleState : State
    {
        public override void Tick(float deltaTime)
        {
            if (Context.input.DoAttack())
            {
                TransitionTo(new AttackState());
            }
        }
    }

    class AttackState : State
    {
        public override void Tick(float deltaTime)
        {
            if (!Context.input.DoAttack() && Context.animator.IsOnLastFrame(Context))
            {
                TransitionTo(new IdleState());
            }
        }
    }
}
