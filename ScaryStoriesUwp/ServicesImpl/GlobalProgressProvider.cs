using ScaryStoriesUwp.Shared.Services;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class GlobalProgressProvider : IGlobalProgressProvider
    {
        public event OnProgressChangedHandler OnProgressChanged;

        public void ChangeStatus(bool newStatus,string text)
        {
            if (OnProgressChanged != null)
            {
                OnProgressChanged(newStatus,text);
            }
        }

    }
}
