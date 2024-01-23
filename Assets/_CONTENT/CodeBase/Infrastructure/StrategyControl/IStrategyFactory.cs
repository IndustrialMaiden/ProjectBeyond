namespace _CONTENT.CodeBase.Infrastructure.StrategyControl
{
    public interface IStrategyFactory
    {
        TActionType Get<TActionType>(params object[] args) where TActionType : IStrategy;
    }
}