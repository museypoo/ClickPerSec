using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickPerSec
{
    public partial class Form1 : Form
    {
        private int amountofclicks = 0;
        bool stop = true;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Task<int> longwait = LongAsyncwaithing();
            if (amountofclicks > 0) {
                if (stop)
                {
                    amountofclicks = amountofclicks + 1;
                    button1.Text = amountofclicks.ToString() + " Clicks every " + textBox1.Text + " second(s)";
                }
            } else
            {
                if (stop)
                {
                    int x;
                    if(!int.TryParse(textBox1.Text, out x))
                    {
                        MessageBox.Show("You may only use numbers in that textbox");
                        return;
                    }

                    amountofclicks = amountofclicks + 1;
                    button1.Text = amountofclicks.ToString() + " Clicks every " + textBox1.Text  + " second(s)";
                    int result = await longwait;
                    amountofclicks = 0;
                    stop = false;
                }
                
            }
        }

        public async Task<int> LongAsyncwaithing() 
        {
            await Task.Delay(int.Parse(textBox1.Text) * 1000); 
            return 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop = true;
            button1.Text = " Start clicking";
        }
    }
}
