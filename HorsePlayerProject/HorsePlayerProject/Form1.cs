using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace HorsePlayerProject
{
    public partial class Form1 : Form
    {
        float bpm = 90.0f;
        List<Channel> channels;
        List<Effect> effects;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // init BASS using the default output device
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                // init soundboard
                effects = new List<Effect>();
                effects.Add(new Effect("samples/airhorn.wav"));
                effects.Add(new Effect("samples/smokeweedeveryday.wav"));
                effects.Add(new Effect("samples/horse.wav"));
                //effects.Add(new Effect("samples/horse.wav"));

                // init channels with bpm
                channels = new List<Channel>();
                channels.Add(new Channel(1, "tracks/test.wav", bpm, 5, this));
                channels.Add(new Channel(2, "tracks/kick.wav", bpm, 5, this));
                channels.Add(new Channel(3, "tracks/snare.wav", bpm, 5, this));
                channels.Add(new Channel(4, "tracks/hihat.wav", bpm, 5, this));
                //channels.Add(new Channel("tracks/bass.wav", bpm, 4, trackBar1));

                foreach(Channel c in channels)
                {
                    // maybe put these all in a mixer later if there are sync problems
                    c.Play();
                }              
            }
            else
            {
                throw new System.SystemException("Could not load bass");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Channel c in channels)
            {
                c.Free();
            }

            foreach(Effect f in effects)
            {
                f.Free();
            }

            Bass.BASS_Free();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            effects[0].Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            effects[1].Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            effects[2].Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            effects[3].Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            effects[4].Play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            effects[5].Play();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            effects[6].Play();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            effects[7].Play();
        }

        delegate void InitTrackbarCallback(int channel, int length);

        public void InitTrackbar(int channel, int length)
        {
            TrackBar ctrl;

            switch (channel)
            {
                case 1:
                    ctrl = trackBar1;
                    break;
                case 2:
                    ctrl = trackBar2;
                    break;
                case 3:
                    ctrl = trackBar3;
                    break;
                case 4:
                    ctrl = trackBar4;
                    break;
                default:
                    ctrl = null;
                    Console.WriteLine("SetTrackbar: Channel does not exist: " + channel);
                    break;
            }

            if (ctrl.InvokeRequired)
            {
                InitTrackbarCallback d = new InitTrackbarCallback(SetTrackbar);
                this.Invoke(d, new object[] { channel, length });
            }
            else
            {
                ctrl.Minimum = 0;
                ctrl.Maximum = length;
            }

        }

        delegate void SetTrackbarCallback(int channel, int measure);

        public void SetTrackbar(int channel, int measure)
        {
            TrackBar ctrl;

            switch (channel)
            {
                case 1:
                    ctrl = trackBar1;
                    break;
                case 2:
                    ctrl = trackBar2;
                    break;
                case 3:
                    ctrl = trackBar3;
                    break;
                case 4:
                    ctrl = trackBar4;
                    break;
                default:
                    ctrl = null;
                    Console.WriteLine("SetTrackbar: Channel does not exist: " + channel);
                    break;
            }

            if (ctrl.InvokeRequired)
            {
                SetTrackbarCallback d = new SetTrackbarCallback(SetTrackbar);
                this.Invoke(d, new object[] { channel, measure });
            }
            else
            {
                ctrl.Value = measure;
            }
        }

        public int GetTrackbar(int channel)
        {
            switch (channel)
            {
                case 1:
                    return trackBar1.Value;
                case 2:
                    return trackBar2.Value;
                case 3:
                    return trackBar3.Value;
                case 4:
                    return trackBar4.Value;
                default:
                    Console.WriteLine("GetTrackbar: Channel does not exist: " + channel);
                    return 0;
            }
        }

        public bool GetHold(int channel)
        {
            switch (channel)
            {
                case 1:
                    return checkBox9.Checked;
                case 2:
                    return checkBox10.Checked;
                case 3:
                    return checkBox11.Checked;
                case 4:
                    return checkBox12.Checked;
                default:
                    Console.WriteLine("GetHold: Channel does not exist: " + channel);
                    return false;
            }
        }

    }
}
