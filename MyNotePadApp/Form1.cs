using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotePadApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ToolStrip toolStrip = new ToolStrip();
            toolStrip.BackColor = Color.DarkGreen;
            toolStrip.Dock = DockStyle.Bottom;
            ToolStrip toolStripTop = new ToolStrip
            {
                Dock = DockStyle.Bottom,
                BackColor = Color.DarkGreen
            };
           
            Control[] controlsArray = new Control[] { toolStrip, toolStripTop };
            this.Controls.AddRange(controlsArray);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created with C#", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = cd.Color;
            }
        }
       
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.Text += Clipboard.GetText(TextDataFormat.Text).ToString();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            printDialog.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "open new file",
                Filter = ".txt(NotePad Files)|*.txt"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Form1 openFrom = new Form1();
                StreamReader reader = new StreamReader(openFile.FileName);

                openFrom.richTextBox1.Text = reader.ReadToEnd();
                openFrom.Show();
            }
            
           
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "Save your file"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter streamWriter = new StreamWriter(sfd.FileName + ".txt");
                    streamWriter.WriteLine(richTextBox1.Text);
                    streamWriter.Close();
                    MessageBox.Show("Saved", "This Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Files is Empty");
                }

            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            LettersToolStrip.Text = "Letters:" + richTextBox1.Text.Length.ToString();
            string[] words = richTextBox1.Text.Trim().Split(' ');
            WordsToolStrip.Text = "words:" + words.Length.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string str = richTextBox1.Text;

           
            if (str.IndexOf(textBox2.Text) != -1)
            {
                
                richTextBox1.SelectionStart = str.IndexOf(textBox2.Text);
                richTextBox1.SelectionLength = textBox2.Text.Length;
                richTextBox1.SelectionBackColor = Color.Red;
              
            }
            else
            {
                richTextBox1.SelectionBackColor = Color.White;
            }
            if (textBox2.Text.Length < richTextBox1.SelectionLength)
            { MessageBox.Show("LOH"); }

        }
    }
}
