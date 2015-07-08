using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thex_to_bin
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            rtbInput.Text = "# comment\n\n     # and #### \n\n06 A-0 4-1 X2 D 2-2 2-2 @1\n\n\n2-1 D-D X1 D@\n\n";

            rtbInput.Text = @"EA 05 7C 00 00 31 C0 8E D8 8E C0 88 16 3D 7C BE 2C 7C E8 04 00 F4 E9 FC FF 8A 04 46 08 C0 74 0B B4 0E B7 00 B3 07 CD 10 E9 EE FF C3 4C 6F 61 64 69 6E 67 2E 2E 2E 00 45 72 72 6F 72 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 55 AA";
            EHexType dt = ThexParse.detectHexType(rtbInput.Text);
            byte[] data = ThexParse.convertToBin(rtbInput.Text);

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {

        }

        private void btnSetFile_Click(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {

        }
    }
}
