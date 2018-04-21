using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using System.Windows.Forms;

namespace HorsePlayerProject
{
    class Channel
    {
        private float bpm;
        private int channel;
        private SYNCPROC sync;
        private Form1 form;
        private int length;

        Random random;

        private int nr;
        private int measure;

        private int synchandle;

        public Channel(int nr, string file, float bpm, int length, Form1 form)
        {
            this.form = form;
            this.bpm = bpm;
            this.nr = nr;
            this.length = length;

            measure = 0;
            random = new Random(Guid.NewGuid().GetHashCode());

            // GUI stuff
            form.InitTrackbar(nr, length);
            form.SetTrackbar(nr, 0);

            // load track
            channel = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);
            if (channel == 0)
            {
                throw new System.ArgumentException("Error: " + channel + " Can not load sample", file);
            }

            // init sync point for callback
            sync = new SYNCPROC(LoopEnd);
            synchandle = Bass.BASS_ChannelSetSync(channel, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, MeasuresToBytes(1), sync, IntPtr.Zero);
        }

        public void Play()
        {
            Bass.BASS_ChannelPlay(channel, false);
        }

        // callback that handles end of measure
        private void LoopEnd(int handle, int c, int data, IntPtr user)
        {
            // switch to random measure if not on hold
            if (!form.GetHold(nr))
            {
                measure = random.Next(0, length + 1);
                form.SetTrackbar(nr, measure);

                // set end pos callback
                Bass.BASS_ChannelRemoveSync(channel, synchandle);
                synchandle = Bass.BASS_ChannelSetSync(channel, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, MeasuresToBytes(measure + 1), sync, IntPtr.Zero);
            }

            // move to start point
            Bass.BASS_ChannelSetPosition(channel, MeasuresToBytes(measure), BASSMode.BASS_POS_BYTE);

            //Console.WriteLine("Channel: " + channel + " Measure: " + measure);
        }

        public float MeasuresToSeconds(int measure)
        {
            return measure * 4.0f / bpm * 60.0f;
        }

        public long MeasuresToBytes(int measure)
        {
            return Bass.BASS_ChannelSeconds2Bytes(channel, MeasuresToSeconds(measure));
        }

        public void Free()
        {
            Bass.BASS_StreamFree(channel);
        }
    }
}
