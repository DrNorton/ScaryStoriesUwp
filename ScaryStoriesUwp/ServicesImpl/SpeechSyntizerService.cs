using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.Services.Models;

namespace ScaryStoriesUwp.ServicesImpl
{
    public  class SpeechSyntizerService : ISpeechSyntizerService
    {
        private SpeechSynthesizer _synth;
        private MediaElement _speechPlayer;

        public SpeechSyntizerService()
        {
           _synth = new SpeechSynthesizer();
        }

        public void SetMediaElement(object media)
        {
            _speechPlayer =(MediaElement)media;
        }

        public void PositionNull()
        {
            if(_speechPlayer!=null)
            _speechPlayer.Position=new TimeSpan();
        }

        public SynzSpeechStates State
        {
            get
            {
                switch (_speechPlayer.CurrentState)
                {
                    case MediaElementState.Playing:
                        return SynzSpeechStates.Play;

                    case MediaElementState.Paused:
                        return SynzSpeechStates.Pause;


                    case MediaElementState.Stopped:
                        return SynzSpeechStates.Stop;

                    default:
                        return SynzSpeechStates.Stop;
                }
                
            }
        }

        public void Play()
        {
            _speechPlayer.Play();
        }

        public void Pause()
        {
            _speechPlayer.Pause();
        }

        public void Stop()
        {
            _speechPlayer.Stop();
        }


        public async Task Synt(string text)
        {
            _synth = new SpeechSynthesizer();
            var test2=SpeechSynthesizer.AllVoices.Where(x=>x.Language.Contains("ru")).ToList();
            _synth.Voice=test2.FirstOrDefault();
            var stream = await _synth.SynthesizeTextToStreamAsync(text);
                _speechPlayer.SetSource(stream, stream.ContentType);
                _speechPlayer.Play();
        }
    }
}
