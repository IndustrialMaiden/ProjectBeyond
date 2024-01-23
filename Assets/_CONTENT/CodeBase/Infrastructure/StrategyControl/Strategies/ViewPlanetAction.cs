using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StateControl.States;

namespace _CONTENT.CodeBase.Infrastructure.StrategyControl.Strategies
{
    public class ViewPlanetAction : IStrategy
    {
        private readonly int _planetIndex;
        private readonly StateMachine _stateMachine;

        public ViewPlanetAction(int planetIndex, StateMachine stateMachine)
        {
            _planetIndex = planetIndex;
            _stateMachine = stateMachine;
        }

        public void Execute() => _stateMachine.Enter<PlanetViewState, int>(_planetIndex);
    }
}