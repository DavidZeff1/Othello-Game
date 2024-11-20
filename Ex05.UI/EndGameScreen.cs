using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UI
{
    public partial class EndGameScreen : Form
    {
        public bool m_PlayAgain;

        public EndGameScreen(string i_EndGameMessage)
        {
            InitializeComponent();
            label1.Text = i_EndGameMessage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_PlayAgain = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_PlayAgain = false;
            this.Close();
        }

        private void EndGameScreen_Load(object sender, EventArgs e)
        {

        }
    }

}
