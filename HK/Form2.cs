using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HK
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();

        }
        private string Msg;
        private void button1_Click(object sender, EventArgs e)
        {
         
         
                Msg = textBox1.Text;
                textBox1.Text = null;
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public string GetMsg()
        {
            return Msg;
           
        }

        internal DialogResult ShowDiaolog()
        {
            throw new NotImplementedException();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
    }
}
