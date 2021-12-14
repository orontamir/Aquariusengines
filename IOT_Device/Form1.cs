
using DataModel;
using IOT_Device.RestFull;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOT_Device
{
    public partial class Form1 : Form
    {
        ManualResetEvent m_stopThreadsEvent = new ManualResetEvent(false);

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        private readonly int _timeToRun = int.Parse(ConfigurationManager.AppSettings["TimeToRun"]);
        private readonly int _textBoxLines = int.Parse(ConfigurationManager.AppSettings["textBoxLines"]);
        private readonly int _everyMsc = int.Parse(ConfigurationManager.AppSettings["everySec"]);
        private readonly string _Url = ConfigurationManager.AppSettings["Url"];
        public Form1()
        {
            InitializeComponent();
            //StartBtn.Enabled = false;
            //StopBtn.Enabled = true;
            //m_stopThreadsEvent = new ManualResetEvent(false);
            //Thread t1 = new Thread(new ThreadStart(Run));
            //t1.Start();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;
            m_stopThreadsEvent = new ManualResetEvent(false);
            Task t1 = Task.Factory.StartNew(() => Run());
            
        }


       
        private void Run()
        {
            AppendText("Start Running");
            int i = 0;
            int counter = _timeToRun * _everyMsc;
            Signal _signal = new Signal();
            DateTime TimeToBadSignal = DateTime.Now.AddSeconds(GetRandomNumber(2, 5));
            double sine = 0;
            double state = 0;
           
            while (i < counter)
            {
                try
                {
                    string datetime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.FFF");
                    if (DateTime.Now < TimeToBadSignal)
                    {
                        // Get Sin
                        sine = _signal.Sine.GetGoodSignal(i);
                        log.Debug($"sine : {sine}, i: {i}");
                        //Get State
                        state = _signal.State.GetGoodSignal(i);
                        
                    }
                    else
                    {
                        TimeToBadSignal = DateTime.Now.AddSeconds(GetRandomNumber(2, 5));
                        //Get bad sim
                        sine = _signal.Sine.GetBadSignal(i);
                        //Get Bad State
                       state = _signal.State.GetBadSignal(i);
                       
                    }
                    //Build JSON
                    JObject json = new JObject(
                    new JProperty("Time", datetime),
                    new JProperty("Sine", sine),
                    new JProperty("State", state)
                   
                    );
                    //Rest API POST
                    log.Debug(json.ToString());
                    string url = $@"{_Url}Home/Create";
                    var result = Task.Factory.StartNew(() => RestApi.Instance().POST(json, url));
                    Thread.Sleep(_everyMsc);    
                }
                catch (Exception ex)
                {
                    AppendText($" error message: {ex.Message}");
                }
                if (m_stopThreadsEvent.WaitOne(_everyMsc))
                {
                    break;
                }
                i += _everyMsc;
            }
            AppendText("Stop Running");
        }

        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        delegate void AppendTextDelegate(string text);

        private void AppendText(string text)
        {
            if (LogtextBox.InvokeRequired)
            {
                LogtextBox.Invoke(new AppendTextDelegate(this.AppendText), new object[] { text });
            }
            else
            {
                LogtextBox.Text += $"{DateTime.Now.ToString()}: {text}\r\n";
                log.Debug(text);
                if (LogtextBox.Lines.Count() > _textBoxLines)
                {
                    LogtextBox.Lines = LogtextBox.Lines.Skip(1).ToArray();
                }
                LogtextBox.SelectionStart = LogtextBox.Text.Length;
                LogtextBox.ScrollToCaret();
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = false;
            StartBtn.Enabled = true;
            m_stopThreadsEvent.Set();
            AppendText("Stop Running");
        }

        private void OpenLogBtn_Click(object sender, EventArgs e)
        {
            string FileName = "";
            try
            {
                FileName = @"C:\Log\IOT_Device\" + DateTime.Now.ToString("yyyy-MM-dd") + "_IOT_Device.txt";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "notepad++.exe";
                proc.StartInfo.Arguments = FileName;
                proc.Start();
            }
            catch (Exception ex)
            {

                log.Error($"Rrror when try to open log {FileName}, error message: {ex.Message}");

            }
        }

        private void OpenLogFolderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = @"C:\Log\SynchronizePhones\";
                proc.StartInfo.Arguments = FileName;
                proc.Start();
            }
            catch (Exception ex)
            {
                log.Error($"Rrror when try to open log folder c:\\Log\\, error message: {ex.Message}"); ;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_stopThreadsEvent.Set();
            Thread.Sleep(2000);
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            
          
        }

       
    }
}
