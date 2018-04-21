using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace HorsePlayerProject
{
    class Effect
    {
        private int channel;

        public Effect(string file)
        { 
            channel = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);
            if (channel == 0)
            {
                throw new System.ArgumentException("Error: " + channel + " Can not load sample", file);
            }
        }

        public void Play()
        {
            Bass.BASS_ChannelPlay(channel, true);
        }

        public void Free()
        {
            Bass.BASS_StreamFree(channel);
        }

    }
}
