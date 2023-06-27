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
using System.Timers;
using System.Runtime.InteropServices;

namespace MoveMouseApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        public static void DoMouseClick()
        {
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void Form1_Resize()
        {
            
                Hide();
                notifyIcon1.Visible = true;
           
        }
        private void MoveCursor()
        {
            Random random = new Random();
            int a=random.Next(-50,50);
            int b=random.Next(-50,50);
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - a, Cursor.Position.Y - b);
            //Cursor.Clip = new Rectangle(this.Location, this.Size);
            Thread.Sleep(100);
            Cursor.Position = new Point(Cursor.Position.X + a, Cursor.Position.Y + b);


        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Start")
            {
                int count = Convert.ToInt32(numericUpDown1.Value);
                timer1.Interval = (count * 60 * 1000); // up to 20 mins
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Start();buttonStart.Text = "Stop";

                //Thread.Sleep(count * 60 * 1000); //Go to sleep for the next five minutes
                MoveCursor();Form1_Resize();
            }
            else
            {
                timer1.Stop();buttonStart.Text = "Start";
            }
          
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void buttonTray_Click(object sender, EventArgs e)
        {
            Form1_Resize();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveCursor();
            if (radioButtonYes.Checked==true) DoMouseClick();
           
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            radioButtonYes.Checked = true;
        }
    }
}
