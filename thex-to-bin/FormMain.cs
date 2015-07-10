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

namespace thex_to_bin
{
    public partial class FormMain : Form
    {
        string binFilePath;

        public FormMain()
        {
            InitializeComponent();

            //rtbInput.Text = "# comment\n\n     # and #### \n\n06 A-0 4-1 X2 D 2-2 2-2 @1\n\n\n2-1 D-D X1 D@\n\n";

            //rtbInput.Text = @"EA 05 7C 00 00 31 C0 8E D8 8E C0 88 16 3D 7C BE 2C 7C E8 04 00 F4 E9 FC FF 8A 04 46 08 C0 74 0B B4 0E B7 00 B3 07 CD 10 E9 EE FF C3 4C 6F 61 64 69 6E 67 2E 2E 2E 00 45 72 72 6F 72 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 55 AA";
            rtbInput.Text = @"# Enter hex here, where # is comments

# can use raw hex format, ie:
EA 05 7C 00 00 etc.

# Intel hex format is supported, too:

# (actually not yet...)
";
            EHexType dt = ThexParse.detectHexType(rtbInput.Text);
            byte[] data = ThexParse.convertToBin(rtbInput.Text);

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = ThexParse.convertToBin(rtbInput.Text);

                MessageBox.Show(string.Format("Verified with {0} bytes of binary data.\r\nFormat is: {1}", data.Length, 
                    ThexParse.detectHexType (rtbInput.Text).ToString()));
            }
            catch (ThexParseException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }





        private void btnSetFile_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            byte[] data;

            try
            {
                data = ThexParse.convertToBin(rtbInput.Text);

            }
            catch (ThexParseException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true ;
            if (binFilePath != null && binFilePath.Length > 0)
                sfd.FileName = binFilePath;

            sfd.Filter = "Raw Binary|*.bin|Raw Binary (All)|*.*|DOS COM file|*.COM";
            sfd.Title = "Save a Binary File";

            sfd.ShowDialog();

            if (sfd.FileName != "") 
            {
                binFilePath = sfd.FileName;

                File.WriteAllBytes(binFilePath, data);

                MessageBox.Show(string.Format("Saved {0} bytes of binary data.", data.Length));
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ValidateNames = true;  
            ofd.Filter = "Raw Binary|*.bin|Raw Binary (All)|*.*|DOS COM file|*.COM";
            ofd.Title = "Load a Binary File";

            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                binFilePath = ofd.FileName;
                byte[] data = File.ReadAllBytes(binFilePath);

                string strRawHex = ThexParse.convertToRawHex(data);

                rtbInput.Text = strRawHex;
                MessageBox.Show(string.Format("Loaded {0} bytes of binary data.", data.Length));
            }

        }
    }
}
