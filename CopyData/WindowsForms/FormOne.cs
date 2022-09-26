using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsForms
{
    public partial class FormOne : Form
    {
        private Point point;
        private string searchFile;
        private int accuracy = 0;
        private bool isSearch;
        private bool fileFolder = true;

        private List<int> folderTree = new List<int>();
        private int pathStage;
        private int currentDir = 0;

        FormTwo formTwo = new FormTwo();
        List<Form> forms = new List<Form>();

        string saveFolder = "AutoComplete.txt";
        AutoCompleteStringCollection autoCompletePath = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteName = new AutoCompleteStringCollection();
        string[] collectionPath = new string[0];
        string[] collectionName = new string[0];

        DriveInfo[] drive = DriveInfo.GetDrives();
        Rectangle rect = Screen.GetBounds(new Point());

        private string[] currentDirectory = new string[1000];
        public FormOne()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + "Name.txt")) collectionName = Load("Name.txt");
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + "Path.txt")) collectionPath = Load("Path.txt");
            else
            {
                string[] arr = { "Disk_C ", "Disk_D " };
                collectionPath = arr;
            }

            
            


            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left += 100;
            this.Top += 590 - this.Height / 2;

            if (collectionName.Length != 0)
            {
                autoCompleteName.AddRange(collectionName);
                DocumentName.AutoCompleteCustomSource = autoCompleteName;
            }
            if (collectionPath.Length != 0)
            {
                autoCompletePath.AddRange(collectionPath);
                searchPath.AutoCompleteCustomSource = autoCompletePath;
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (isSearch) return;

            if (searchFile == null) searchFile = DocumentName.Text;

            try
            {
                accuracy = Int32.Parse(Accur.Text);
            }
            catch (FormatException)
            {
                accuracy = 100;
            }

            accuracy = (accuracy * searchFile.Length) / 100;

            for (int i = 0; i < searchFile.Length; i++)
            {
                if (i > accuracy)
                {
                    searchFile = searchFile.Remove(i);
                    if (i < searchFile.Length - 1) i--;
                }
            }

            currentDir = 0;

            currentDirectory = new string[1000];
            if (searchPath.Text != "") currentDirectory[0] = searchPath.Text;
            else
            {
                for (int i = 0; i < drive.Length; i++) currentDirectory[i] = drive[i].Name;
            }
            folderTree.Add(1);

            for (int i = 0; i < forms.Count; i++)
            {
                forms[i].Close();
            }

            Save(collectionPath, "Path.txt");
            Save(collectionName, "Name.txt");

            isSearch = true;
        }

        private void NewForm(Form form)
        {
            formTwo = (FormTwo)form;

            formTwo.StartPosition = FormStartPosition.Manual;
            formTwo.Location = new Point(rect.Width / 2 - formTwo.Width / 2 + 500, rect.Height / 2 - formTwo.Height / 2);
            formTwo.Show();
        }

        private void ResultForm(string text)
        {
            FormTwo form = new FormTwo();

            forms.Add(form);
            form.box.Text = text;

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(rect.Width / 2 - formTwo.Width / 2 + 500, rect.Height / 2 - formTwo.Height / 2 + formTwo.Height);
            form.Show();
        }

        private void Accur_TextChanged(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isSearch)
            {
                try 
                {
                    Console.Text = string.Format(currentDirectory[currentDir]);

                    if (!fileFolder)
                    {
                        string name = Path.GetFileName(currentDirectory[currentDir]);
                        if (name == null) return;

                        for (int i = 0; i < name.Length; i++)
                        {
                            if (i >= accuracy)
                            {
                                name = name.Remove(i);
                                if (i < name.Length - 1) i--;
                            }
                        }

                        if (name == searchFile)
                        {
                            ResultForm(
                                "Name: " + Path.GetFileName(currentDirectory[currentDir]) + Environment.NewLine +
                                "Path: " + currentDirectory[currentDir]
                                );
                        }
                    }
                    else
                    {
                        string[] file = new string[0];
                        if (currentDirectory[currentDir] != null)
                        {
                            if(pathStage != 0) file = Directory.GetFiles(Directory.GetParent(currentDirectory[0]).FullName);
                        }

                        if (file.Length != 0)
                        {
                            for (int i = 0; i < file.Length; i++)
                            {
                                string name = Path.GetFileNameWithoutExtension(file[i]);
                                if (name == null) return;

                                for (int j = 0; j < name.Length; j++)
                                {
                                    if (i >= accuracy)
                                    {
                                        name = name.Remove(j);
                                        if (j < name.Length - 1) j--;
                                    }
                                }

                                if (name == searchFile)
                                {
                                    ResultForm(
                                        "Name: " + Path.GetFileName(currentDirectory[currentDir]) + Environment.NewLine +
                                        "Path: " + currentDirectory[currentDir]
                                        );
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine(currentDirectory[0]);
                    currentDirectory = Directory.GetDirectories(Directory.GetParent(Directory.GetParent(currentDirectory[0]).FullName).FullName);
                    pathStage--;
                    if (pathStage == 0) Pause();
                    currentDir = folderTree[pathStage] ;

                    return;
                }

                try
                {
                    if(Directory.GetDirectories(currentDirectory[currentDir]).Length == 0)
                    {
                        currentDir++;
                        folderTree[pathStage] = currentDir;

                        return;
                    }

                    currentDirectory = Directory.GetDirectories(currentDirectory[currentDir]);

                    folderTree[pathStage] = currentDir + 1;
                    pathStage++;
                    if (currentDir == 0) currentDir = 1;

                    try { folderTree[pathStage] = currentDir; }
                    catch (Exception) { folderTree.Add(currentDir); }

                    currentDir = 0;
                }
                catch (Exception)
                {
                    currentDir++;
                    folderTree[pathStage] = currentDir;
                }
            }

        }


        private void Save(string[] arr, string name)
        {
            File.Delete(Directory.GetCurrentDirectory() + "\\" + name);
            FileStream file = File.Create(Directory.GetCurrentDirectory() + "\\" + name);
            Encoding encoder = Encoding.UTF8;
            for (int i = 0; i < arr.Length; i++)
            {
                file.Write(encoder.GetBytes((arr[i]) + " "), 0, encoder.GetByteCount(arr[i]));
            }
        }

        private string[] Load(string name)
        {
            string[] arr = new string[1000];

            string file = File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + name);
            int wordsCount = 0;
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = -1; j < wordsCount;)
                {
                    if (file[i] == ' ')
                    {
                        wordsCount++;
                    }
                    else { arr[wordsCount] += file[i]; }
                    j = wordsCount;
                }
            }

            return arr;
        }


        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Left += e.X - point.X;
                Top += e.Y - point.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            fileFolder = true;
            button2.BackColor = button1.BackColor;
            button1.BackColor = Color.Green;
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            fileFolder = false;
            button1.BackColor = button2.BackColor;
            button2.BackColor = Color.Green;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pause();
        }
        private void Pause()
        {
            if (isSearch)
            {
                isSearch = false;
                button4.Text = "Continue";
            }
            else
            {
                isSearch = true;
                button4.Text = "Stop";
            }
        }

        private void DocumentName_EnabledChanged(object sender, EventArgs e)
        { 
            switch (DocumentName.Text)
            {
                case "Disk_C ": searchFile = string.Format("C:\\");
                    break;
                case "Disk_D ":
                    searchFile = string.Format("D:\\");
                    break;
            }
        }

        private void DocumentName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                DocumentName.Paste();
            }
        }

        private void searchPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                searchPath.Paste();
            }
        }
    }
}
