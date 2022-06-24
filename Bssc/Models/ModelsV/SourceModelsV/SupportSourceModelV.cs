using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class SupportSourceModelV
    {

        public string res_support_id { get; set; }
        public string designation { get; set; }
        public double support_height { get; set; }
        public double top_board_width_a { get; set; }
        public double top_board_length_a1_z50 { get; set; }
        public double top_board_length_a1_z100 { get; set; }
        public double top_board_length_a1_z150 { get; set; }
        public double top_board_length_a1_z200 { get; set; }
        public double top_board_length_a1_z250 { get; set; }
        public double top_board_length_a1_z300 { get; set; }
        public double bottom_board_length_c { get; set; }
        public double top_cushion_width_b_preset { get; set; }
        public double top_cushion_length_a_preset_z50 { get; set; }
        public double top_cushion_length_a_preset_z100 { get; set; }
        public double top_cushion_length_a_preset_z150 { get; set; }
        public double top_cushion_length_a_preset_z200 { get; set; }
        public double top_cushion_length_a_preset_z250 { get; set; }
        public double top_cushion_length_a_preset_z300 { get; set; }
        public double bottom_cushion_size { get; set; }
        public double transverse_displacement { get; set; }
    }
}
