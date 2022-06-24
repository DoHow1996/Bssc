using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Bssc.Control.Tools
{
    /// <summary>
    /// 为datagridview设置右键菜单 复制粘贴 删除行 插入行 添加行 等
    /// </summary>
    public class ContextMenuWrapForDatagridview
    {
        public ContextMenuStrip contextMenuStrip;

        public ToolStripMenuItem copyContextMenuItem;

        public ToolStripMenuItem pasteContextMenuItem;

        public ToolStripMenuItem deleteContextMenuItem;

        public ToolStripSeparator toolStripSeparator8;

        public ToolStripMenuItem addRowContextMenuItem;

        public ToolStripMenuItem insertRowContextMenuItem;

        public ToolStripMenuItem deleteRowContextMenuItem;

        public ToolStripMenuItem deleteColumnContextMenuItem;

        private DataGridView dataGridView;

        private bool m_IsPasteAutoRows;

        public bool IsPasteAutoIncreaseRows
        {
            get
            {
                return m_IsPasteAutoRows;
            }
            set
            {
                m_IsPasteAutoRows = value;
            }
        }

        public bool EnableCopy
        {
            get
            {
                return copyContextMenuItem.Enabled;
            }
            set
            {
                copyContextMenuItem.Enabled = value;
            }
        }

        public bool EnablePaste
        {
            get
            {
                return pasteContextMenuItem.Enabled;
            }
            set
            {
                pasteContextMenuItem.Enabled = value;
            }
        }

        public bool EnableDelete
        {
            get
            {
                return deleteContextMenuItem.Enabled;
            }
            set
            {
                deleteContextMenuItem.Enabled = value;
            }
        }

        public bool EnableAddRow
        {
            get
            {
                return addRowContextMenuItem.Enabled;
            }
            set
            {
                addRowContextMenuItem.Enabled = value;
            }
        }

        public bool EnableDeleteRow
        {
            get
            {
                return deleteRowContextMenuItem.Enabled;
            }
            set
            {
                deleteRowContextMenuItem.Enabled = value;
            }
        }

        public bool EnableInsertRow
        {
            get
            {
                return insertRowContextMenuItem.Enabled;
            }
            set
            {
                insertRowContextMenuItem.Enabled = value;
            }
        }

        public ContextMenuWrapForDatagridview(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
            contextMenuStrip = new ContextMenuStrip();
            copyContextMenuItem = new ToolStripMenuItem();
            pasteContextMenuItem = new ToolStripMenuItem();
            deleteContextMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            addRowContextMenuItem = new ToolStripMenuItem();
            insertRowContextMenuItem = new ToolStripMenuItem();
            deleteRowContextMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            contextMenuStrip.Items.AddRange(new ToolStripItem[7] { copyContextMenuItem, pasteContextMenuItem, deleteContextMenuItem, toolStripSeparator8, addRowContextMenuItem, insertRowContextMenuItem, deleteRowContextMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(107, 120);
            copyContextMenuItem.Name = "copyContextMenuItem";
            copyContextMenuItem.Size = new Size(106, 22);
            copyContextMenuItem.ShortcutKeys = Keys.C | Keys.Control;
            copyContextMenuItem.Text = "复制";
            pasteContextMenuItem.Name = "pasteContextMenuItem";
            pasteContextMenuItem.Size = new Size(106, 22);
            pasteContextMenuItem.ShortcutKeys = Keys.V | Keys.Control;
            pasteContextMenuItem.Text = "粘贴";
            deleteContextMenuItem.Name = "deleteContextMenuItem";
            deleteContextMenuItem.Size = new Size(106, 22);
            deleteContextMenuItem.ShortcutKeys = Keys.Delete;
            deleteContextMenuItem.Text = "删除";
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(103, 6);
            addRowContextMenuItem.Name = "addRowContextMenuItem";
            addRowContextMenuItem.Size = new Size(106, 22);
            addRowContextMenuItem.Text = "添加行";
            insertRowContextMenuItem.Name = "insertRowContextMenuItem";
            insertRowContextMenuItem.Size = new Size(106, 22);
            insertRowContextMenuItem.Text = "插入行";
            deleteRowContextMenuItem.Name = "deleteRowContextMenuItem";
            deleteRowContextMenuItem.Size = new Size(106, 22);
            deleteRowContextMenuItem.Text = "删除行";
            contextMenuStrip.ResumeLayout(performLayout: false);

            this.SetContextMenuStrip(dataGridView);

        }

        public void SetContextMenuStrip(DataGridView dataGridView2)
        {
            dataGridView = dataGridView2;
            dataGridView.ContextMenuStrip = contextMenuStrip;
            copyContextMenuItem.Click += copyContextMenuItem_Click;
            pasteContextMenuItem.Click += pasteContextMenuItem_Click;
            deleteContextMenuItem.Click += deleteContextMenuItem_Click;
            addRowContextMenuItem.Click += addRowContextMenuItem_Click;
            insertRowContextMenuItem.Click += insertRowContextMenuItem_Click;
            deleteRowContextMenuItem.Click += deleteRowContextMenuItem_Click;
        }

        private void copyContextMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.DataObject clipboardContent = dataGridView.GetClipboardContent();
                System.Windows.Forms.DataObject dataObject = new System.Windows.Forms.DataObject();
                string text = clipboardContent.GetText(System.Windows.Forms.TextDataFormat.UnicodeText);
                dataObject.SetText(text, System.Windows.Forms.TextDataFormat.UnicodeText);
                Clipboard.SetDataObject(dataObject);
            }
            catch (Exception)
            {
            }
        }

        private void pasteContextMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell == null)
            {
                return;
            }
            int rowIndex = dataGridView.CurrentCell.RowIndex;
            int columnIndex = dataGridView.CurrentCell.ColumnIndex;
            string text = Clipboard.GetText();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            string[] array = text.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (IsPasteAutoIncreaseRows)
            {
                int num = (dataGridView.AllowUserToAddRows ? 1 : 0);
                dataGridView.RowCount = Math.Max(dataGridView.RowCount, rowIndex + array.Length + num);
            }
            for (int i = 0; i < array.Length; i++)
            {
                string text2 = array[i];
                string[] array2 = text2.Split(new char[1] { '\t' }, StringSplitOptions.None);
                for (int j = 0; j < array2.Length; j++)
                {
                    if (rowIndex + i < dataGridView.RowCount && columnIndex + j < dataGridView.ColumnCount)
                    {
                        dataGridView.Rows[rowIndex + i].Cells[columnIndex + j].Value = array2[j].ToString();
                    }
                }
            }
        }

        private void deleteContextMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
            for (int i = 0; i < selectedCells.Count; i++)
            {
                if (selectedCells[i] is DataGridViewTextBoxCell)
                {
                    selectedCells[i].Value = "";
                }
            }
        }

        private void addRowContextMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView.Rows.Add();
        }

        private void insertRowContextMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
            if (selectedCells.Count != 0)
            {
                dataGridView.Rows.Insert(selectedCells[0].RowIndex, new DataGridViewRow());
            }
        }

        private void deleteRowContextMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
            List<int> list = new List<int>();
            for (int i = 0; i < selectedCells.Count; i++)
            {
                if (!list.Contains(selectedCells[i].RowIndex))
                {
                    list.Add(selectedCells[i].RowIndex);
                }
            }
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j] != dataGridView.RowCount - 1 || !dataGridView.AllowUserToAddRows)
                {
                    DataGridViewRow dataGridViewRow = dataGridView.Rows[list[j]];
                    dataGridView.Rows.Remove(dataGridViewRow);
                }
            }
        }

        //private void deleteColumnContextMenuItem_Click(object sender, EventArgs e)
        //{
        //    DataGridViewSelectedColumnCollection selectedColumns = dataGridView.SelectedColumns;
        //    for (int i = 0; i < selectedColumns.Count; i++)
        //    {
        //        dataGridView.Columns.Remove(selectedColumns[i]);
        //    }
        //}


    }
}
