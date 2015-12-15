namespace ScaryStoriesUwp.Shared.Services
{
    public delegate void OnProgressChangedHandler(bool status,string text);
    public interface IGlobalProgressProvider
    {
        event OnProgressChangedHandler OnProgressChanged;
        void ChangeStatus(bool newStatus,string text);
    }
}