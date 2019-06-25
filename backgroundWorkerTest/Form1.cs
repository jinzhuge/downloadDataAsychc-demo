using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace backgroundWorkerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks!");
        }

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            System.Threading.AutoResetEvent waiter = new System.Threading.AutoResetEvent(false);
            WebClient myweb = new WebClient();
            string url = "https://patentimages.storage.googleapis.com/cb/37/09/ecf488aa9a4576/AU717061B2.pdf";
            Uri uri = new Uri(url);

            myweb.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);

            myweb.DownloadDataAsync(uri, waiter);

            //waiter.WaitOne();
        }

        private void DownloadDataCallback(Object sender, DownloadDataCompletedEventArgs e)
        {
            System.Threading.AutoResetEvent waiter = (System.Threading.AutoResetEvent)e.UserState;

            try
            {
                // If the request was not canceled and did not throw
                // an exception, display the resource.
                if (!e.Cancelled && e.Error == null)
                {
                    byte[] data = (byte[])e.Result;
                    string save_path = "E:";
                    string default_str = @"";
                    string Para = default_str + save_path + "/aaa.pdf";
                    System.IO.File.WriteAllBytes(Para, data);
                    MessageBox.Show("Thanks!");
                }
                else 
                {
                    resultLabel.Text = "Error: " + e.Error.Message;
                }
            }
            finally
            {
                // Let the main application thread resume.
                waiter.Set();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {

        }
    }
}
