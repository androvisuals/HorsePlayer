using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace HorsePlayerProject
{
    class Channel
    {
        private float bpm;
        private int track;
        private bool hold;

        public Channel(float bpm, string file)
        {
            hold = false;
            this.bpm = bpm;

            // load track
            track = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);

            if (track == 0)
            {
                throw new System.ArgumentException("Error: " + track + " Can not load sample", file);
            }
        }

        public void Play()
        {
            // TOOD calculate random bar to start
            // BASS_ChannelSeconds2Bytes

            // TODO if hold, reuse start point

            // TODO play sample
            Bass.BASS_ChannelPlay(track, false);
        }

        public float BarsToSeconds(int bar)
        {
            return (float)bar * 4.0f / bpm * 60.0f;
        }

        public void Free()
        {
            Bass.BASS_StreamFree(track);
        }
    }
}
