using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface IProgressDownloadReceiver
    {
        void DownloadStatusChange(string status);
        void DownloadProgressChange(int progress);
        void DownloadStart();
        void DownloadFinish();
        void DownloadErrorFinish();
    }
}
