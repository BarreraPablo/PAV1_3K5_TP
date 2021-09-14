﻿using PAV_3K5_TP.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV_3K5_TP
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();

            using(frmLogin frm = new frmLogin())
            {
                if (frm.ShowDialog() != DialogResult.OK) {
                    Environment.Exit(0);
                };
            }
        }

        private void gestionBarriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(frmBarrio frm = new frmBarrio())
            {
                frm.ShowDialog();
            }
        }
    }
}
