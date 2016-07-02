using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marimba.Forms
{
    public partial class ResettedPasswordsForm : Form
    {
        IList<string[]> currentData;
        public ResettedPasswordsForm()
        {
            InitializeComponent();
        }

        public void Populate(IList<string[]> usersAndPasswords)
        {
            currentData = usersAndPasswords;
            lvResettedPasswords.Items.Clear();
            foreach (string[] entry in usersAndPasswords)
            {
                var listViewItem = new ListViewItem(entry);
                lvResettedPasswords.Items.Add(listViewItem);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.Title = "Save Passwords";
            svd.Filter = "CSV Files|*.csv";
            svd.ShowDialog();
            if (svd.FileName == "")
            {
                if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
                return;
            }

            using (CsvFileWriter writer = new CsvFileWriter(svd.FileName))
            {
                CsvRow firstRow = new CsvRow();
                firstRow.Add("User");
                firstRow.Add("Password");
                writer.WriteRow(firstRow);

                foreach (string[] row in currentData) {
                    CsvRow currentRow = new CsvRow();
                    currentRow.Add(row[0]);
                    currentRow.Add(row[1]);
                    writer.WriteRow(currentRow);
                }
            }

            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
        }
    }
}
