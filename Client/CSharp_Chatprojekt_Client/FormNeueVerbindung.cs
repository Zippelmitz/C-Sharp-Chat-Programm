﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CSharp_Chatprojekt_Client
{
    public partial class FormNeueVerbindung : Form
    {
        private string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        private Form1 hauptform;

        public FormNeueVerbindung(Form1 hauptform)
        {
            InitializeComponent();
            this.hauptform = hauptform;
        }

        private void btnAbbrechen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVerbinden_Click(object sender, EventArgs e)
        {


            
            if (hauptform.UserVerbinden(tbxIP.Text, Convert.ToInt32(tbxPort.Text), tbxServerPW.Text, tbxBenutzername.Text, tbxbenutzerPW.Text))
            {
                ServerSpeichern();
                Close();
            }
            else
            {
                MessageBox.Show("Die Verbindung is gescheitert!","Verbindungsfehler",MessageBoxButtons.AbortRetryIgnore,MessageBoxIcon.Error);
            }

           

        }

        private bool FormularVollständig()
        {
            if (tbxBenutzername.Text.Length > 0)
            {
                if (tbxbenutzerPW.Text.Length > 0)
                {
                    if (tbxIP.Text.Length > 0)
                    {
                        if (tbxPort.Text.Length > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void tbxIP_TextChanged(object sender, EventArgs e)
        {
            btnVerbinden.Enabled = FormularVollständig();
        }

        private void tbxPort_TextChanged(object sender, EventArgs e)
        {
            btnVerbinden.Enabled = FormularVollständig();
        }

        private void tbxBenutzername_TextChanged(object sender, EventArgs e)
        {
            btnVerbinden.Enabled = FormularVollständig();
        }

        private void tbxbenutzerPW_TextChanged(object sender, EventArgs e)
        {
            btnVerbinden.Enabled = FormularVollständig();
        }

        private void ServerSpeichern()
        {
            string dateipfad = dir+"\\" + @"serverliste.csv";
            int zaehler=0;
            if (!File.Exists(dateipfad))
            {
                File.Create(dateipfad).Close();
            }
   
            ServerDatenSchreiben(dateipfad);
                

            
        }

        private void ServerDatenSchreiben(string dateipfad)
        {
            string data = hauptform.ServerStatusAbfragen(tbxIP.Text, Convert.ToInt32(tbxPort.Text));
            string[] daten = data.Split(';');
            StreamWriter sw = new StreamWriter(dateipfad);
            sw.WriteLine(daten[4] + ";"+tbxIP.Text+";" + tbxPort.Text);
            
            sw.Close();
        }

        private bool PortIstPort()
        {
            bool portIsPort = true;
            char[] chartest = tbxPort.Text.ToCharArray();
            for (int i = 0; i < chartest.Length-1 || portIsPort == true; i++)
            {
                if (Char.IsDigit(chartest[i]))
                {
                    portIsPort = true;
                }
                else
                {
                    portIsPort = false;
                }
            }
            return portIsPort;
        }


    }
}
