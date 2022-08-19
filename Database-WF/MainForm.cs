// Use for Dapper
using MS_SQL_Dapper;
using MS_SQL_Dapper.Interfaces;

//// Use for Entity Framework
//using MS_SQL_Entity_Framework;
//using MS_SQL_Entity_Framework.Interfaces;
//using MS_SQL_Entity_Framework.Models;

using System.Text.RegularExpressions;

namespace Database_WF
{
    public partial class MainForm : Form
    {
        List<IPerson> people = new();
        readonly IDataAccess db = Factory.CreateDataAccess();
        int selectedIndex = -1;
        readonly IPerson _person;

        public MainForm(IPerson person)
        {
            InitializeComponent();
            UpdateListBox();
            btnInsertTxt.Enabled = false;
            btnUpdate.Enabled = false;
            btnInsert.Enabled = false;
            btnDelete.Enabled = false;
            _person = person;
        }

        public void UpdateListBox()
        {
            listBox.DataSource = people;
            listBox.DisplayMember = "FullInfo";
            lblTotalRecords.Text = $"Total records: {people.Count}";
            listBox.SelectedItems.Clear();
        }

        public void ClearInsertTextBoxes()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            nudAge.Value = 0;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchPhrase = tbSearch.Text;

            if (searchPhrase == "")
                people = db.Get().ToList();
            else
            {
                Regex reg = new("[({!@#$%^&*};:'',<.>/?)]");
                searchPhrase = reg.Replace(searchPhrase, string.Empty);
                var searchElements = searchPhrase.Split(" ").ToList();

                people = db.GetBySearch(
                   searchElements.FirstOrDefault(s => s.All(char.IsLetter)),
                   searchElements.Where(s => s.All(char.IsLetter)).Count() <= 1 ? "" : searchElements.Where(s => s.All(char.IsLetter)).Skip(1).First(),
                   !searchElements.Where(n => int.TryParse(n, out int parsed) == true).Any() ?
                                            null : Int32.Parse(searchElements.Where(n => Int32.TryParse(n, out int parsed) == true).First())
                   ).ToList();
            }
            UpdateListBox();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            bool result = false;
            var validatedList = ValidateInputString.ValidateFields(string.Join(" ", tbLastName.Text, tbFirstName.Text, nudAge.Value.ToString()));

            if (validatedList != null)
            {
                _person.FirstName = validatedList[1];
                _person.LastName = validatedList[0];
                _person.Age = Int32.Parse(validatedList[2]);
                result = db.Insert(_person);
            }

            if (result)
                MessageBox.Show("Person inserted", "Success");

            StatusUpdate.LabelUpdate(result, lbStatus);
            ClearInsertTextBoxes();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var validatedList = ValidateInputString.ValidateFields(string.Join(" ", tbLastName.Text, tbFirstName.Text, nudAge.Value.ToString()));

            if (validatedList != null)
            {
                _person.PersonId = people[selectedIndex].PersonId;
                _person.FirstName = validatedList[1];
                _person.LastName = validatedList[0];
                _person.Age = Int32.Parse(validatedList[2]);

                var result = db.Update(_person);

                StatusUpdate.LabelUpdate(result, lbStatus);

                if (result)  // Update people list without database connection
                {
                    people[selectedIndex].FirstName = validatedList[1];
                    people[selectedIndex].LastName = validatedList[0];
                    people[selectedIndex].Age = Int32.Parse(validatedList[2]);
                    people[selectedIndex].FullInfo = $"{validatedList[1]} {validatedList[0]} Age: {validatedList[2]}";

                    listBox.DataSource = null; // cant modify list which is bounded to datasource
                    UpdateListBox();

                    MessageBox.Show("Person updated", "Success");
                }
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new()
            {
                Title = "Browse Text File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true,
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                tbPath.Text = path;
            }
        }

        private void BtnInsertTxt_Click(object sender, EventArgs e)
        {
            StreamReader sr = new(tbPath.Text);

            string fileData = sr.ReadToEnd();
            var validatedList = ValidateInputString.ValidateFields(fileData);

            if (validatedList == null) { var formImportError = new DialogForm(); formImportError.ShowDialog(); }
            else
            {
                _person.FirstName = validatedList[1];
                _person.LastName = validatedList[0];
                _person.Age = Int32.Parse(validatedList[2]);

                bool result = db.Insert(_person);

                StatusUpdate.LabelUpdate(result, lbStatus);
                tbPath.Text = "";

                if (result)
                    MessageBox.Show("Person inserted", "Success");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var result = db.Delete(people[selectedIndex].PersonId);

            StatusUpdate.LabelUpdate(result, lbStatus);

            if (result)
            {
                people.RemoveAt(selectedIndex);
                listBox.DataSource = null; // cant modify list which is bounded to datasource
                UpdateListBox();
                MessageBox.Show("Person deleted", "Success");
            }
        }

        private void TbPath_TextChanged(object sender, EventArgs e)
        {
            btnInsertTxt.Enabled = File.Exists(tbPath.Text);
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = listBox.SelectedIndex;

            if (selectedIndex > -1)
            {
                tbLastName.Text = people[selectedIndex].LastName;
                tbFirstName.Text = people[selectedIndex].FirstName;
                nudAge.Value = (int)people[selectedIndex].Age;
                btnDelete.Enabled = true;
            }

            if (selectedIndex < 0) { ClearInsertTextBoxes(); btnDelete.Enabled = false; }
        }

        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > listBox.ItemHeight * listBox.Items.Count)
                listBox.SelectedItems.Clear();
        }

        private void TbLastName_TextChanged(object sender, EventArgs e)
        {
            btnInsert.Enabled = tbLastName.Text.Length > 0 && tbFirstName.Text.Length > 0 && selectedIndex < 0;
            btnUpdate.Enabled = tbLastName.Text.Length > 0 && tbFirstName.Text.Length > 0 && selectedIndex > -1;
        }

        private void TbFirstName_TextChanged(object sender, EventArgs e)
        {
            btnInsert.Enabled = tbLastName.Text.Length > 0 && tbFirstName.Text.Length > 0 && selectedIndex < 0;
            btnUpdate.Enabled = tbLastName.Text.Length > 0 && tbFirstName.Text.Length > 0 && selectedIndex > -1;
        }
    }
}