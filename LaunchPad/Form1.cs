using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Randomization.Core;
using Randomization.Framework;


namespace Launchpad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SingleRoll Getmon = new SingleRoll();
            for (int i = 0; i < 20; i++)
                listBox2.Items.Add(Getmon.Roll(1, DieType.D100, 0).ToString());
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
