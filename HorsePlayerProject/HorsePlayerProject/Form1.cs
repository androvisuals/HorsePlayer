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
        public Form1()
        {
            InitializeComponent();
        }

        int stream;
        //a sound has 4 song parts, needs a start loop and end loop point
        //we need 8 streams for one sound eg kick 01
        // then we need to interpolate between 8 volumes with one float


        private void Form1_Load(object sender, EventArgs e)
        {
            // init BASS using the default output device
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                // create a stream channel from a file
                stream = Bass.BASS_StreamCreateFile("KickLoop-01.wav", 0, 0, BASSFlag.BASS_DEFAULT);
                if (stream != 0)
                {
                    // play the stream channel
                    Bass.BASS_ChannelPlay(stream, false);
                }
                else
                {
                    // error creating the stream
                    Console.WriteLine("Stream error: {0}", Bass.BASS_ErrorGetCode());
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            // free the stream
            Bass.BASS_StreamFree(stream);
            // free BASS
            Bass.BASS_Free();

        }
    }
}
