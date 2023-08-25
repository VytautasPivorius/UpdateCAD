using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.CompilerServices;

namespace UpdateCAD
{
    public partial class UpdateCAD : Form
    {
        string Destination = @"C:\Users\vytautas.pivorius\OneDrive - RP\Desktop\To";
        string FromSubassemblies = @"";
        string ToSubassemblies = @"";
        string FromImportedTools = @"";
        string ToImportedTools = @"";
        string FromTemplates = @"";
        string ToTemplates = @"";
        string FromDynamoScripts = @"";
        string ToDynamoScripts = @"";
        string FromDynamoPackages = @"";
        string ToDynamoPackages = @"";
        string FromAll = @"";
        string ToAll = @"";

        public UpdateCAD()
        {
            InitializeComponent();
        }
        // Close button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Update button
        private void button2_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true) // If selected EE directory
            {
                FromSubassemblies = @"G:\Common Data\CAD\_General\Toolpalette";
                ToSubassemblies = @"C:\CAD\_General\Toolpalette";
                FromImportedTools = @"G:\Common Data\PROGRAMDATA\Imported Tools\SE";
                FromTemplates = @"G:\Common Data\CAD\EE\Templates";
                ToTemplates = @"C:\CAD\EE\Templates";
                FromDynamoScripts = @"G:\Common Data\CAD\_General\Dynamo scripts";
                ToDynamoScripts = @"C:\CAD\_General\Dynamo scripts";
                FromDynamoPackages = @"G:\Common Data\PROGRAMDATA\Dynamo packages";
                ToDynamoPackages = @"C:\CAD\_General\Dynamo packages";
                FromAll = @"G:\Common Data\CAD\EE";
                ToAll = @"C:\CAD\EE";
            }
            
            if (radioButton2.Checked == true) // If selected LT directory
            {
                FromSubassemblies = @"G:\Common Data\CAD\LT\Toolpalette";
                ToSubassemblies = @"C:\CAD\LT\Toolpalette";
                FromImportedTools = @"G:\Common Data\PROGRAMDATA\Imported Tools\LT\2022";
                FromTemplates = @"G:\Common Data\CAD\LT\Templates";
                ToTemplates = @"C:\CAD\LT\Templates";
                FromDynamoScripts = @"G:\Common Data\CAD\_General\Dynamo scripts";
                ToDynamoScripts = @"C:\CAD\_General\Dynamo scripts";
                FromDynamoPackages = @"G:\Common Data\PROGRAMDATA\Dynamo packages";
                ToDynamoPackages = @"C:\CAD\_General\Dynamo packages";
                FromAll = @"G:\Common Data\CAD\LT";
                ToAll = @"C:\CAD\LT";
            }

            if (radioButton3.Checked == true) // If selected LV directory
            {
                FromSubassemblies = @"G:\Common Data\CAD\LV\Toolpalette";
                ToSubassemblies = @"C:\CAD\LV\Toolpalette";
                FromImportedTools = @"G:\Common Data\PROGRAMDATA\Imported Tools\LV\2022";
                FromTemplates = @"G:\Common Data\CAD\LV\Templates";
                ToTemplates = @"C:\CAD\LV\Templates";
                FromDynamoScripts = @"G:\Common Data\CAD\_General\Dynamo scripts";
                ToDynamoScripts = @"C:\CAD\_General\Dynamo scripts";
                FromDynamoPackages = @"G:\Common Data\PROGRAMDATA\Dynamo packages";
                ToDynamoPackages = @"C:\CAD\_General\Dynamo packages";
                FromAll = @"G:\Common Data\CAD\LV";
                ToAll = @"C:\CAD\LV";
            }

            if (radioButton4.Checked == true) // If selected SE directory
            {
                FromSubassemblies = @"G:\Common Data\CAD\LT\Toolpalette";
                ToSubassemblies = @"C:\CAD\_General\Toolpalette";
                FromImportedTools = @"G:\Common Data\PROGRAMDATA\Imported Tools\SE";
                FromTemplates = @"G:\Common Data\CAD\LT\Templates";
                ToTemplates = @"C:\CAD\SE\Templates";
                FromDynamoScripts = @"G:\Common Data\CAD\_General\Dynamo scripts";
                ToDynamoScripts = @"C:\CAD\_General\Dynamo scripts";
                FromDynamoPackages = @"G:\Common Data\PROGRAMDATA\Dynamo packages";
                ToDynamoPackages = @"C:\CAD\_General\Dynamo packages";
                FromAll = @"G:\Common Data\CAD\SE";
                ToAll = @"C:\CAD\SE";
            }

            // Drop down menu for version selection

            ToImportedTools = $@"C:\ProgramData\Autodesk\C3D {comboBox1.SelectedItem}\enu\Imported Tools";
            

            if (checkBox1.Checked) // Copy subassemblies and imported tools files
            {
                CopyFiles(FromSubassemblies, ToSubassemblies);
                CopyFiles(FromImportedTools, ToImportedTools);
            }
            if (checkBox2.Checked) // Copy Templates files
            {
                CopyFiles(FromTemplates, ToTemplates);
            }
            if (checkBox3.Checked) // Copy Dynamo script files
            {
                CopyFiles(FromDynamoScripts, ToDynamoScripts);
            }
            if (checkBox4.Checked) // Copy Dynamo packages files
            {
                CopyFiles(FromDynamoPackages, ToDynamoPackages);
            }
            if (checkBox5.Checked) // Copy all information files
            {
                CopyFiles(FromAll, ToAll);
                CopyFiles(FromImportedTools, ToImportedTools);
                CopyFiles(FromDynamoScripts, ToDynamoScripts);
            }

            }

        // Copy functions
        void CopyFiles(string source, string destination)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(source);
            DirectoryInfo destinationDir = new DirectoryInfo(destination);
            
            if (!sourceDir.Exists) // If source directory doesnt exist
            {
                MessageBox.Show($"Source directory '{source}' does not exist.");
                return;
            }
            if (!destinationDir.Exists) // If destination directory doesnt exist
            {
                destinationDir.Create();
            }

            int totalItems = sourceDir.GetFiles().Length + sourceDir.GetDirectories().Length;
            int copiedItems = 0;

            if(totalItems == 0) //If source directory is empty show message
            {
                MessageBox.Show($"Source directory '{source}' is empty.");
                return;
            }

            //Copy files
            foreach (FileInfo file in sourceDir.GetFiles())
            {
                string destinationFilePath = Path.Combine(destinationDir.FullName, file.Name);
                file.CopyTo(destinationFilePath, overwrite: true);

                copiedItems++;
                UpdateProgressBar(copiedItems, totalItems);
            }
            //Copy directories
            foreach (DirectoryInfo subDir in sourceDir.GetDirectories())
            {
                string subDirDestination = Path.Combine(destinationDir.FullName, subDir.Name);
                CopyFiles(subDir.FullName, subDirDestination);

                copiedItems++;
                UpdateProgressBar(copiedItems, totalItems);
            }
        }
        // Need to correct progress bar!!!!
        private void UpdateProgressBar(int current, int total)
        {
            int progressPercentage = (current * 100) / total;
            progressBar2.Value = progressPercentage;
            label4.Text = $"Completed: {progressPercentage}%";
            return;
        }

        // Default from Form1
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UpdateCAD_Load(object sender, EventArgs e)
        {

        }
    }
}