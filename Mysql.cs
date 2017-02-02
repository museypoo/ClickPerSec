using System;

public class Class1
{
	public Class1()
	{
        methands.InsterDBStuff();

	}

    public class methands
    {
        public static void InsterDBStuff()
        {
            var connectionString = "Database=kkcookiecliker;Data Source=defiancedb.cztab1l9swup.us-west-2.rds.amazonaws.com;User Id=root;Password=LifeDBpass1!;port=3306";
            string numofclicks = amountofclicks.ToString();
            string doneforsec = textBox1.Text;
            string ip = new System.Net.WebClient().DownloadString("http://icanhazip.com");
            string time = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
            string query = "INSERT INTO record (Date, Clicks, IP, Doneforsecs) Values('" + time + "', '" + numofclicks + "', '" + ip + "', '" + doneforsec + "')";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
