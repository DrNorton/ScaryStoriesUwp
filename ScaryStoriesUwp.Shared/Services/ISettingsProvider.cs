using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Services.Models;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface ISettingsProvider
    {
        TextInfoSettings TextSettings { get; set; }
        bool IsOffline { get; set; }

        long DatabaseVersion { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
