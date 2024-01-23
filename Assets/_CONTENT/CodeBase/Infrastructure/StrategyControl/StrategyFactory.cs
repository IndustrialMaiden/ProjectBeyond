using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StrategyControl.Strategies;

namespace _CONTENT.CodeBase.Infrastructure.StrategyControl
{
    public class StrategyFactory : IStrategyFactory
    {
        private readonly StateMachine _stateMachine;
        
        private readonly Dictionary<Type, Func<object[], IStrategy>> _strategies =
            new Dictionary<Type, Func<object[], IStrategy>>();

        public StrategyFactory(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;

            _strategies[typeof(ViewPlanetAction)] = args => new ViewPlanetAction((int) args[0], _stateMachine);
        }

        public TActionType Get<TActionType>(params object[] args) where TActionType : IStrategy
        {
            var strategyType = typeof(TActionType);
            if (_strategies.TryGetValue(strategyType, out var creator))
            {
                return (TActionType) creator(args);
            }

            throw new InvalidOperationException($"Strategy type of {strategyType.Name} was not registered.");
        }
    }
}