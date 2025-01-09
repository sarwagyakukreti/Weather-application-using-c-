using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace Weather_application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string city;
        private void button1_Click(object sender, EventArgs e)
        {
            string city;
            city = txtcity.Text;

            string uri = string.Format("\r\nhttp://api.weatherapi.com/v1/forecast.xml?key=4b52ac1594624655a1f25503242910&q={0}&days=1", city);

            XDocument doc = XDocument.Load(uri);

            string iconUri = (string)doc.Descendants("icon").FirstOrDefault();

            WebClient client = new WebClient();
            byte[] image = client.DownloadData("http:" + iconUri);
            MemoryStream stream = new MemoryStream(image);

            Bitmap newBitMap = new Bitmap(stream);
            string maxtemp = (string)doc.Descendants("maxtemp_c").FirstOrDefault();
            string mintemp = (string)doc.Descendants("mintemp_c").FirstOrDefault();

            string maxwindm = (string)doc.Descendants("maxwind_mph").FirstOrDefault();
            string maxwindk = (string)doc.Descendants("maxwind_kph").FirstOrDefault();
            string humidity = (string)doc.Descendants("avghumidity").FirstOrDefault();

            string country = (string)doc.Descendants("country").FirstOrDefault();
            string cloud = (string)doc.Descendants("text").FirstOrDefault();

            Bitmap icon = newBitMap;

            txtmaxtemp.Text= maxtemp;
            txtmintemp.Text= mintemp;

            txtwindm.Text = maxwindm;
            txtwindk.Text = maxwindk;

            txthumidity.Text = humidity;
            label7.Text = cloud;

            txtcountry.Text= country;
            pictureBox1.Image = icon;


        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Max Temp", typeof(string));
            dt.Columns.Add("Min Temp", typeof(string));
            dt.Columns.Add("MaxWindmph", typeof(string));
            dt.Columns.Add("MaxWindkph", typeof(string));
            dt.Columns.Add("Humidity", typeof(string));
            dt.Columns.Add("Cloud", typeof(string));
            dt.Columns.Add("Icon", typeof(Bitmap));

            city = txtcity.Text;

            string uri = string.Format("http://api.weatherapi.com/v1/forecast.xml?key=4b52ac1594624655a1f25503242910&q={0}&days=5", city);

            XDocument doc = XDocument.Load(uri);


            foreach(var npc in doc.Descendants("forecastday"))
            {
                string iconUri = (string)npc.Descendants("icon").FirstOrDefault();

                WebClient client = new WebClient();
                byte[] image = client.DownloadData("http:" + iconUri);
                MemoryStream stream = new MemoryStream(image);


                Bitmap newBitmap = new Bitmap(stream);

                dt.Rows.Add(new object[]
                {
                    (string)doc.Descendants("country").FirstOrDefault(),
                    (string)npc.Descendants("date").FirstOrDefault(),
                    (string)npc.Descendants("maxtemp_c").FirstOrDefault(),
                (string)npc.Descendants("mintemp_c").FirstOrDefault(),

                (string)npc.Descendants("maxwind_mph").FirstOrDefault(),
                (string)npc.Descendants("maxwind_kph").FirstOrDefault(),
                (string)npc.Descendants("avghumidity").FirstOrDefault(),


                (string)npc.Descendants("text").FirstOrDefault(),
                
                newBitmap

            });
            }
            dataGridView1.DataSource = dt;

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
