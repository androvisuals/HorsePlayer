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
        Soundboard soundboard;
        List<Channel> channels;

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
                soundboard = new Soundboard();

                // init channels with bpm
                channels = new List<Channel>();
                channels.Add(new Channel(bpm, "samples/kick.wav"));
                channels.Add(new Channel(bpm, "samples/snare.wav"));
                channels.Add(new Channel(bpm, "samples/hihat.wav"));
                channels.Add(new Channel(bpm, "samples/bass.wav"));

                foreach(Channel c in channels)
                {
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
            // free soundboard samples
            soundboard.Free();

            // TODO: free channel samples

            // free bass
            Bass.BASS_Free();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            soundboard.Play(SoundboardSample.Airhorn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            soundboard.Play(SoundboardSample.SmokeWeedEveryday);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
