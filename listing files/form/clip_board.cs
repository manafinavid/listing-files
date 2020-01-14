using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace listing_files.form
{
    public partial class clip_board : Form
    {
        Form1 M;
        public clip_board(ref Form1 main)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.images;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            M = main;
        }
        string temp = "";
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (temp != Clipboard.GetText())
            {
                temp = Clipboard.GetText();
                if(Form1.Search_on_clipboard(ref temp, ref M))
                {
                    T.Image = listing_files.Properties.Resources.T;
                }
                else
                {
                    T.Image = listing_files.Properties.Resources.F;
                }
            }
        }

        bool S = false;
        private void Start_Click(object sender, EventArgs e)
        {
            if (S)
            {
                S = false;
                timer.Stop();
                start.Image = listing_files.Properties.Resources.start;
            }
            else
            {
                S = true;
                timer.Start();
                start.Image = listing_files.Properties.Resources.pause;
            }
        }
        Timer timer = new Timer();
    }
}
