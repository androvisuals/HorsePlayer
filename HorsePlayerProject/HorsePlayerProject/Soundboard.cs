using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace HorsePlayerProject
{
    enum SoundboardSample
    {
        SmokeWeedEveryday,
        Airhorn
    }

    class Soundboard
    {
        private int[] samples;

        public Soundboard()
        {
            samples = new int[32];
            AddSample(SoundboardSample.Airhorn, "samples/airhorn.wav");
            AddSample(SoundboardSample.SmokeWeedEveryday, "samples/smokeweedeveryday.wav");
        }

        public void Play(SoundboardSample sample)
        {
            Bass.BASS_ChannelPlay(samples[(int)sample], true);
        }

        private void AddSample(SoundboardSample sample, string file)
        {
            samples[(int)sample] = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);

            if (samples[(int)sample] == 0)
            {
                throw new System.ArgumentException("Error: " + samples[(int)sample] + " Can not load sample", file);
            }
        }

        public void Free()
        {
            // free all soundboard samples
            foreach (SoundboardSample sample in Enum.GetValues(typeof(SoundboardSample)))
            {
                Bass.BASS_StreamFree(samples[(int) sample]);
            }
        }
    }
}
