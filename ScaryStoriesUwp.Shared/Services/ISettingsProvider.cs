using System.ComponentModel;
using ScaryStoriesUwp.Shared.Services.Models;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface ISettingsProvider
    {
        TextInfoSettings TextSettings { get; set; }
        bool IsOnline { get; set; }

        long DatabaseVersion { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
