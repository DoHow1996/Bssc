using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class PilePositionView
    {

        [HeaderText("墩序号")]
        public string dunxuhao { get; set; }
        [HeaderText("墩台号")]
        public string duntaihao { get; set; }
        [HeaderText("桩基编号")]
        public int zhuangjibianhao { get; set; }
        [HeaderText("桩基坐标X")]
        public double zhuangjizuobiaoX { get; set; }
        [HeaderText("桩基坐标Y")]
        public double zhuangjizuobiaoY { get; set; }

    }
}
