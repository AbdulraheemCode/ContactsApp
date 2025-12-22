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
    public partial class ShowContactInfo : Form
    {

        int _ContactID = -1;
        clsContact _Contact;

        public ShowContactInfo(int ContactID)
        {
            InitializeComponent();
            _ContactID = ContactID;
        }

        private void ShowContactInfo_Load(object sender, EventArgs e)
        {


            _Contact = clsContact.Find(_ContactID);

            if (_Contact == null)
            {
                return;
            }

            lblContactID.Text = _Contact.ID.ToString(); 
            lblFirstName.Text = _Contact.FirstName.ToString();
            lblLastName.Text = _Contact.LastName.ToString();
            lblEmail.Text = _Contact.Email.ToString();
            lblPhone.Text = _Contact.Phone.ToString();
            lblDateOfBirth.Text = _Contact.DateOfBirth.ToString();
            lblCountry.Text = _Contact.CountryID.ToString();
            lblAddress.Text = _Contact.Address.ToString();

        }

       
    }
}
