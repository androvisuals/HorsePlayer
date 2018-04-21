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
        private int track;
        private SYNCPROC sync;
        private Form1 form;
        private int length;
        private bool hold;
        private int channel;
        private int measure;

        private int synchandle;

        public Channel(int channel, string file, float bpm, int length, Form1 form)
        {
            this.form = form;
            this.bpm = bpm;
            this.channel = channel;
            this.length = length;
            this.hold = form.GetHold(channel);
            measure = 0;

            // GUI stuff
            form.InitTrackbar(channel, length);
            form.SetTrackbar(channel, 0);

            // load track
            track = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);

            long l = Bass.BASS_ChannelGetLength(track);
            Console.WriteLine(l);
            Console.WriteLine(BarsToByte(0));
            Console.WriteLine(BarsToByte(1));
            Console.WriteLine(BarsToByte(2));
            Console.WriteLine(BarsToByte(3));
            Console.WriteLine(BarsToByte(4));

            if (track == 0)
            {
                throw new System.ArgumentException("Error: " + track + " Can not load sample", file);
            }

            // init sync point for callback
            sync = new SYNCPROC(LoopEnd);
            synchandle = Bass.BASS_ChannelSetSync(track, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, BarsToByte(1), sync, IntPtr.Zero);
        }

        public void Play()
        {
            Bass.BASS_ChannelPlay(track, false);
        }

        private void LoopEnd(int handle, int c, int data, IntPtr user)
        {
            // switch to random measure if not on hold
            if (!form.GetHold(channel))
            {
                Random r = new Random(Guid.NewGuid().GetHashCode()); // add some more randomness
                measure = r.Next(0, length + 1);
                form.SetTrackbar(channel, measure);
            }

            // set start point
            Bass.BASS_ChannelSetPosition(track, BarsToByte(measure), BASSMode.BASS_POS_BYTE);

            // set end pos callback
            Bass.BASS_ChannelRemoveSync(track, synchandle);
            synchandle = Bass.BASS_ChannelSetSync(track, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, BarsToByte(measure + 1), sync, IntPtr.Zero);

            Console.WriteLine("Channel: " + channel + " Measure: " + measure);
        }

        public float BarsToSeconds(int bar)
        {
            return (float)bar * 4.0f / bpm * 60.0f;
        }

        public long BarsToByte(int bar)
        {
            return Bass.BASS_ChannelSeconds2Bytes(track, BarsToSeconds(bar));
        }

        public void Free()
        {
            Bass.BASS_StreamFree(track);
        }
    }
}
