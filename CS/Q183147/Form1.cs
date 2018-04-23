using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

namespace Q183147
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.carsDBDataSet.Cars);

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int focusedRowHandle = -1;
            if (e.FocusedRowHandle == GridControl.NewItemRowHandle || e.FocusedRowHandle == GridControl.AutoFilterRowHandle)
                return;
            GridView view = (GridView)sender;
            if (e.FocusedRowHandle < 0)
            {
                if (e.PrevFocusedRowHandle == GridControl.InvalidRowHandle)
                    focusedRowHandle = 0;
                else if (Control.MouseButtons == MouseButtons.Left || Control.MouseButtons == MouseButtons.Right)
                    focusedRowHandle = e.PrevFocusedRowHandle;
                else
                {
                    int prevRow = view.GetVisibleIndex(e.PrevFocusedRowHandle);
                    int currRow = view.GetVisibleIndex(e.FocusedRowHandle);
                    if (prevRow > currRow)
                        focusedRowHandle = e.PrevFocusedRowHandle - 1;
                    else
                        focusedRowHandle = e.PrevFocusedRowHandle + 1;
                    if (focusedRowHandle < 0) focusedRowHandle = 0;
                    if (focusedRowHandle >= view.DataRowCount) focusedRowHandle = view.DataRowCount - 1;
                }
                view.FocusedRowHandle = focusedRowHandle < 0 ? 0 : focusedRowHandle;
            }
        }
    }
}