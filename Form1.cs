using stenoapp.Loggers;
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
            IResponse returnValue = InvocationContext.Instance.Invoke<string>(nameof(sayHello), this, "Gökmen");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string encrypted  = StringCipher.Encrypt(textBox1.Text, ConfigurationManager.AppSettings["secret"]);
            pictureBox2.Image = SteganographyHelper.embedText(encrypted, new Bitmap(pictureBox1.Image));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = StringCipher.Decrypt(SteganographyHelper.extractText(new Bitmap(pictureBox2.Image)), ConfigurationManager.AppSettings["secret"]);
        }

        private string sayHello(string name)
        {
            return "Hello " + name;
        }
    }
}
