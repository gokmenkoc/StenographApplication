using stenoapp.biz;
using stenoapp.core;
using stenoapp.core.Loggers;

using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace stenoapp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IoCHelper.Instance.Resolve<LoggerContext, FileLogger>();
            IResponse returnValue = InvocationContext.Instance.Invoke<string>("SayHello", new ImageManager(), "Gökmen");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Response<Image> returnValue = (Response<Image>)InvocationContext.Instance.Invoke<Image>(nameof(StenoHelper.EncryptTextAndEmbedInImage), new StenoHelper(), textBox1.Text, pictureBox1.Image, ConfigurationManager.AppSettings["secret"]);
            pictureBox2.Image = returnValue.Data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Response<string> returnValue = (Response<string>)InvocationContext.Instance.Invoke<string>(nameof(StenoHelper.ExtractTextFromImageAndDecypt), new StenoHelper(), new Bitmap(pictureBox2.Image), ConfigurationManager.AppSettings["secret"]);
            textBox2.Text = returnValue.Data;
        }
    }
}
