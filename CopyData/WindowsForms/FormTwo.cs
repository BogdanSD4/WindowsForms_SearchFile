using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormTwo : Form
    {
        private Point mouse;
        public TextBox box;
        public FormTwo()
        {
            InitializeComponent();
            box = TextBoxConsole;
        }

        private void FormTwo_Load(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) mouse = e.Location;
            Location = new Point((Size)Location - (Size)mouse + (Size)e.Location);
        }
    }
}
