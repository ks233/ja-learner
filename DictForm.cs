using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ja_learner
{
    public partial class DictForm : Form
    {
        public Form mainForm;
        public DictForm()
        {
            InitializeComponent();
        }

        public DictForm(Form _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
        }

    }
}
