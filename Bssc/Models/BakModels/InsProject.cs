﻿using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsProject
    {
        /// <summary>
        /// 工程唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// 工程名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 工程编号
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 工程创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 工程创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 工程修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }


    }
}
