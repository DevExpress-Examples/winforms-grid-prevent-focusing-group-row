using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Windows.Forms;

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
            Random rand = new Random();
            var list = new BindingList<Item>();
            for (int i = 0; i < 50; i++)
                list.Add(new Item() { ID = i, Name = "Name" + i, Category = rand.Next(0, 5) });
            gridControl1.DataSource = list;
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
        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Category { get; set; }
        }
    }
}