namespace _CONTENT.CodeBase.Infrastructure.Services.Progress
{
    public interface ISavedProgressReader
    {
        void LoadProgress<T>(T progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress<T>(T progress);
    }
}