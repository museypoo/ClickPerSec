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
using MySql.Data.MySqlClient;

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
                    stop = false;

                    await Task.Factory.StartNew(() => InsterDBStuff());
                    amountofclicks = 0;
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

        public void InsterDBStuff()
        {
            var connectionString = "Database=kkcookiecliker;Data Source=defiancedb.cztab1l9swup.us-west-2.rds.amazonaws.com;User Id=root;Password=LifeDBpass1!;port=3306";
            string numofclicks = amountofclicks.ToString();
            string doneforsec = textBox1.Text;
            string ip = new System.Net.WebClient().DownloadString("http://icanhazip.com");
            string time = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
            string query = "INSERT INTO record (Date, Clicks, IP, Doneforsecs) Values('" + time + "', '" + numofclicks + "', '" + ip + "', '"+ doneforsec +"')";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader =  commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
