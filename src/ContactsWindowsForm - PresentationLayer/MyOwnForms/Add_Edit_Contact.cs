using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsWindowsForm___PresentationLayer
{
    public partial class Add_Edit_Contact : Form
    {

        private enum enMode { AddNew = 1, Update = 2 }
        enMode _Mode = enMode.AddNew;

        int _ContactID = -1;
        clsContact _Contact;

        public Add_Edit_Contact(int ContactID)
        {
            InitializeComponent();

            _ContactID = ContactID;

            if (_ContactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

        }

        private void Add_Edit_Contact_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _FillCountryToComboBox()
        {

            DataTable CountryDataTable = clsCountry.GetAllCountries();

            foreach(DataRow row in CountryDataTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

            cbCountry.SelectedIndex = 0;

        }

        private void _LoadData()
        {
            
            _FillCountryToComboBox();

            if (_Mode == enMode.AddNew)
            {

                lblMode.Text = "Add New Contact";
                _Contact = new clsContact();
                return;

            }


            _Contact = clsContact.Find(_ContactID); 

            lblMode.Text = "Edit Contact ID = " + _ContactID;
            lblContactID.Text = _ContactID.ToString();
            tbFirstName.Text = _Contact.FirstName;
            tbLastName.Text = _Contact.LastName;
            tbEmail.Text = _Contact.Email;
            tbPhone.Text = _Contact.Phone;
            dateTimePicker1.Value = _Contact.DateOfBirth;
            tbAddress.Text = _Contact.Address;

            if (_Contact.ImagePath != System.DBNull.Value.ToString())
            {
                pictureBox1.Load(_Contact.ImagePath);
            }
            else
            {
                pictureBox1.ImageLocation = null;
            }

            llRemoveImage.Visible = (_Contact.ImagePath != "");

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Contact.CountryID).CountryName);
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int CountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Contact.FirstName = tbFirstName.Text;
            _Contact.LastName = tbLastName.Text;
            _Contact.Email = tbEmail.Text;
            _Contact.Phone = tbPhone.Text;
            _Contact.Address = tbAddress.Text;
            _Contact.DateOfBirth = dateTimePicker1.Value;
            _Contact.CountryID = CountryID;
            

            if (pictureBox1.ImageLocation != null)
            {
                _Contact.ImagePath = pictureBox1.ImageLocation;
            }
            else
            {
                _Contact.ImagePath = null;
            }


            if (_Contact.Save())
            {
                MessageBox.Show("Data Saved Successfully");
                //this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data is Not Saved Successfully");
                //this.Close();
            }
            


            _Mode = enMode.Update;
            lblMode.Text = "Edit ContactID = " + _Contact.ID;
            lblContactID.Text = _Contact.ID.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llOpenDialog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openFileDialog1.Filter = @"Image File |*.jpg;*.jpeg;*.png";
            openFileDialog1.Title = "Choice Image";
            openFileDialog1.DefaultExt = "jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                llRemoveImage.Visible = true;
            }


        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pictureBox1.ImageLocation = null;
            llRemoveImage.Visible = false;


        }
    }
}
