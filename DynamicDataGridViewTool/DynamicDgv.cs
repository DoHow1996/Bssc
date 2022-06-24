using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicDataGridViewTool
{
    public class DynamicDgv
    {

        public static void GenerateDynamicDgv<T>(DataGridView dgv,List<T> list)
        {
            dgv.Columns.Clear();
            //设置DataGridView文本居中
            
            dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (list.Count > 0)
            {
                Type type = list[0].GetType();

                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (var item in propertyInfos)
                {
                    object[] cstAttrs = item.GetCustomAttributes(false);
                    int index = dgv.Columns.Add(item.Name,"tempText");
                    DataGridViewColumn dgvC = dgv.Columns[index];

                    Type dgvCType = dgvC.GetType();
                    dgvC.DataPropertyName = item.Name;
                    dgvC.Width = 5;

                    for (int i = 0; i < cstAttrs.Length; i++)
                    {
                        Attribute cstAttr = (Attribute)cstAttrs[i];
                        Type cstAttrType = cstAttr.GetType();
                        string cstAttrName = cstAttrType.Name.Replace("Attribute","");
                        PropertyInfo[] cstAttrPropertyInfos = cstAttrType.GetProperties();
                        object value = cstAttrPropertyInfos[0].GetValue(cstAttr,null);
                        PropertyInfo dgvCPropertyInfo = dgvCType.GetProperty(cstAttrName);
                        dgvCPropertyInfo.SetValue(dgvC,value);
                    }
                }



                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    if (column.Width == 5)
                    {
                        column.Width = column.HeaderText.Length * 20;
                    }
                }



                BindingSource bs = new BindingSource();
                bs.DataSource = list;
                dgv.DataSource = bs;
                //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

    }
}
