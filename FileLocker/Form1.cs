/* **********************************
AUTHOR: ahmetmb

This tool simply locks a file by specifying FileShare.None when opening with System.IO
Any request to open the file(by this process or another process) will fail until the file is closed.

The purpose of this tool: this tool can be used for troubleshooting purposes.
For example an IIS configuration file can be opened with the tool to simulate it is locked
    by antivirus or backup applications.

You need to run this program as an administrator to be able to lock some files.
************************************ */

using System;
using System.Windows.Forms;
using System.IO;

namespace FileLocker
{
    public partial class Form1 : Form
    {
        FileStream fs = null;
        bool isFileLocked = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the file is already locked
            if (isFileLocked == false)
            {
                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Open the file
                    string fileName = openFileDialog1.FileName;
                    try
                    {
                        // FileShare.None ==> Declines sharing of the current file.
                        // Any request to open the file(by this process or another process) will fail
                        // until the file is closed.

                        fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                        button1.Text = "Unlock";
                        isFileLocked = true;
                        textBox1.Text = fileName;
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show(ex.Message, "File could be locked by another process");
                    }
                }
            }
            else
            {
                if (fs != null)
                {
                    fs.Close();
                    button1.Text = "Lock";
                    isFileLocked = false;
                    textBox1.Text = "Click Lock to choose file...";
                }
            }
        }
    }
}
