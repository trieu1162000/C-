using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using AForge.Video;

namespace demo2
{
    public partial class Form1 : Form
    {
        MJPEGStream stream;
        public Form1()
        {
            InitializeComponent();
            stream = new MJPEGStream("http://192.168.100.10/");
            stream.NewFrame += getNewFrame;
        }

        void getNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
        }

        static MqttClient client;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void lblUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
                try
                {
                    client = new MqttClient(txtBroker.Text, int.Parse(txtPort.Text), false, MqttSslProtocols.None, null, null);
                    client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
                    byte code = client.Connect(Guid.NewGuid().ToString(), txtUsername.Text, txtPassword.Text);
                    if (code == 0)
                    {
                        MessageBox.Show(this, "Connect Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);

                        txtStatus.Text = "Connected";
                        txtStatus.ForeColor = System.Drawing.Color.Green;

                        //Subcribe camera and direction status from ESP
                        //client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                        //client.Subscribe(new string[] {  "MQTT/camera" }, new byte[] { 0 });

                        //READ LED STATUS
                        client.Publish("MQTT/controlDirection", Encoding.UTF8.GetBytes(0.ToString()));
                    }

                    else MessageBox.Show(this, "Connect Fail", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

                catch (Exception)
                {
                    MessageBox.Show(this, "Wrong Format", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stream.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stream.Stop();
        }

        private void btnDir2_Click(object sender, EventArgs e)
        {

        }

        private void btnDir1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabManual_MouseClick(object sender, MouseEventArgs e)
        {
            lblMode.Text = "Manual";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tabAuto_Click(object sender, EventArgs e)
        {
            lblMode.Text = "Auto";
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDir1_MouseDown(object sender, MouseEventArgs e)
        {
            Button but = (Button)sender;
            char data = but.Name.ToCharArray()[6];
            client.Publish("MQTT/controlDirection", Encoding.UTF8.GetBytes(data.ToString()));
        }

        private void btnDir1_MouseUp(object sender, MouseEventArgs e)
        {
            client.Publish("MQTT/controlDirection", Encoding.UTF8.GetBytes("0"));
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
