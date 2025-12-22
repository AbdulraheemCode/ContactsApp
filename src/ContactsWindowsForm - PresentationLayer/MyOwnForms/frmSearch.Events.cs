using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsWindowsForm___PresentationLayer
{
    public partial class frmSearch
    {

        private void tbContactID_TextChanged(object sender, EventArgs e)
        {

            if (tbContactID.Text == "")
                btnSearch.Enabled = false;
            else
                btnSearch.Enabled = true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!clsContact.IsContactExist(Convert.ToInt32(tbContactID.Text)))
            {
                MessageBox.Show("Sorry , Contact is Not Found :-( ");
                tbContactID.Clear();
                return;
            }

            ShowContactInfo ShowContactInfo = new ShowContactInfo(Convert.ToInt32(tbContactID.Text));
            ShowContactInfo.ShowDialog();
            tbContactID.Clear();

        }


    }
}
