using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace ZIKurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPStatus status = IPStatus.Unknown;
            try
            {
                status = new Ping().Send("www.google.ru").Status;
            }
            catch { }

            if (status == IPStatus.Success)
            {
                richTextBox1.Text = "Данный компьютер подключен к интернету";

            }
            else
            {
                richTextBox1.Text = "Данный компьютер не подключен к интернету";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(File.Exists((@"c:\windows\regedit.exe"))))
            {
                richTextBox2.Text = "Фаервол WF.msc установлен!";
            }
            else
            {
                richTextBox2.Text = "Фаервол WF.msc не установлен!";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            WebClient Client = new WebClient();
            String Response;
            try
            {
                Response = Client.DownloadString("https://www.google.com");
            }
            catch
            {
                richTextBox3.Text = "Межсетевой экран функционирует правильно!";
            }
            if (richTextBox3.Text == "")
            {
                richTextBox3.Text = "Межсетевой экран функционирует неверно, или не функционирует вовсе!";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.Contains("AvastSvc")) > 0)
            {
                richTextBox5.Text = "Антивирус Avast Free работает!";
            }
            else
            {
                richTextBox5.Text = "Антивирус Avast Free не работает!";
            }
        }

            private void GetInstalledSoftware()
        {
            List<string> items = new List<string>();
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))  // или 32
            using (var rk = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            if (sk.GetValue("DisplayName") != null)
                            {
                                items.Add(sk.GetValue("DisplayName").ToString());
                                listBox1.Items.Add(new ListViewItem(items.ToArray()));
                                listBox1.SetSelected(listBox1.Items.Count - 1, true);
                                if (listBox1.SelectedItem.ToString() == "ListViewItem: {Avast Free Antivirus}")
                                {
                                    richTextBox6.Text = "Антивирус Avast Free установлен в системе";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    items.Clear();
                }
            }
        }
        private string CheckValue(object input)
        {
            if (input != null)
                return input.ToString();
            else
                return string.Empty;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            GetInstalledSoftware();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox4.Text = "Ожидаем..";
            string file_name = "eicar.com";
            File.WriteAllText("eicar.com", "X5O!P%@AP[4\\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H" + "+H*");
            Thread.Sleep(35000);
            if (!File.Exists(file_name))
            {
                richTextBox4.Text = "Антивирус Avast Free прошел проверку";
            } else
            {
                richTextBox4.Text = "Антивирус Avast Free НЕ прошел проверку";
            }

        }

        private void button7_Click_2(object sender, EventArgs e)
        {

            richTextBox7.Text = "Ожидаем..";

            button1_Click(button1, null);
            button2_Click(button2, null);
            button3_Click(button3, null);
            button4_Click(button4, null);
            button5_Click(button5, null);
            button6_Click(button6, null);

            richTextBox7.Clear();

            richTextBox7.Text = richTextBox1.Text + System.Environment.NewLine + richTextBox2.Text + System.Environment.NewLine + richTextBox3.Text + System.Environment.NewLine + richTextBox4.Text + System.Environment.NewLine + richTextBox5.Text + System.Environment.NewLine + richTextBox6.Text;

            richTextBox1.Clear(); richTextBox2.Clear();
            richTextBox3.Clear(); richTextBox4.Clear();
            richTextBox5.Clear(); richTextBox6.Clear();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                // сохраняем текст в файл
                File.WriteAllText(filename, richTextBox7.Text);


            }
        }
            private void button9_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
