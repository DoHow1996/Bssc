using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools
{
    public static class DynamicDgv<T>
    {

        public static void AddColumnToDgv(DataGridView dataGridView, List<T> list)
        {
            dataGridView.Columns.Clear();
            if (list.Count > 0)
            {
                Type type = list[0].GetType();
                List<DgvDict> dgvDicts = new List<DgvDict>();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (var item in propertyInfos)
                {
                    DescriptionAttribute attr = (DescriptionAttribute)item.GetCustomAttribute(typeof(DescriptionAttribute), true);
                    DgvDict dgvDict = new DgvDict()
                    {
                        ColumnText = attr.Description,
                        ColumnName = item.Name
                    };
                    dgvDicts.Add(dgvDict);
                }

                for (int i = 0; i < dgvDicts.Count; i++)
                {
                    int index = dataGridView.Columns.Add(dgvDicts[i].ColumnName, dgvDicts[i].ColumnText);
                    dataGridView.Columns[index].DataPropertyName = dgvDicts[i].ColumnName;
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = list;
                dataGridView.DataSource = bs;
            }
            
        }

    }
    public class DgvDict
    {

        public DgvDict() { }

        public DgvDict(String attrStr, string ColumnName, List<Object> list = null)
        {
            List<String> strs = attrStr.Split('_').ToList();
            this.ColumnName = ColumnName;
            if (strs.Count >= 2)
            {
                ColumnText = strs[0];
                ColumnType = strs[1];
                this.list = list;
            }
            if (strs.Count >= 3)
            {
                ColumnDisPlayMember = strs[2];
            }
            if (strs.Count >= 4)
            {
                ColumnDisPlayMember = strs[3];
            }
        }

        /// <summary>
        /// 列文本
        /// </summary>
        public string ColumnText;
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName;
        /// <summary>
        /// 列类型
        /// </summary>
        public string ColumnType;
        /// <summary>
        /// 列的datasource
        /// </summary>
        public List<Object> list;
        /// <summary>
        /// 下拉框列的显示值
        /// </summary>
        public string ColumnDisPlayMember;
        /// <summary>
        /// 下拉框列的真实值
        /// </summary>
        public string ColumnValueMember;
    }
}
