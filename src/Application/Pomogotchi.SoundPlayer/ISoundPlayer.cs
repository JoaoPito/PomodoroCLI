using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomogotchi.Application.SoundPlayer
{
    public interface ISoundPlayer : IPlayer
    {
        public void TogglePause();
        public void Stop();
        public void SetVolume(int percentage);
        public void AttachMediaFile(string path);
    }
}