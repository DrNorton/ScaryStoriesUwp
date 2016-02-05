using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Services.Models;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface ISpeechSyntizerService
    {
        Task Synt(string text);
        SynzSpeechStates State { get; }
        void Play();
        void Pause();
        void Stop();
        void SetMediaElement(object media);
        void PositionNull();
    }
}
