using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace ContactsWindowsForm___PresentationLayer
{
    public partial class frmListContacts : Form
    {
        public frmListContacts()
        {
            InitializeComponent();
        }

        private void _RefreshAllContacts()
        {
            dgvAllContacts.DataSource = clsContact.GetAllContact();
            //dgvAllContacts.DataSource = clsCountry.GetAllCountries();
        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshAllContacts();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Edit_Contact Add_Edit = new Add_Edit_Contact((int)dgvAllContacts.CurrentRow.Cells[0].Value); 
            Add_Edit.ShowDialog();

            _RefreshAllContacts();

        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            Add_Edit_Contact Add_Edit = new Add_Edit_Contact(-1);
            Add_Edit.ShowDialog();

            _RefreshAllContacts();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Delete " + dgvAllContacts.CurrentRow.Cells[1].Value.ToString() + "Contact",
                "Message",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                if (clsContact.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    _RefreshAllContacts();
                    MessageBox.Show("Deleted Contact is Successfully :-)");
                }
                else
                {
                    MessageBox.Show("Deleted Contact is Not Successfully :-(");
                }

            }

        }

        private void showContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowContactInfo ShowContact = new ShowContactInfo((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            ShowContact.ShowDialog();
        }

    }
}
