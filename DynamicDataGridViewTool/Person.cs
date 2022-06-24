using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGridViewTool
{
    public class Person
    {
        public Person()
        {
        }

        public Person(int id, string name, int age, bool sex, 
            double heigth, double weight, string educationBackGround, string nativePlace)
        {
            Id = id;
            Name = name;
            Age = age;
            Sex = sex;
            Heigth = heigth;
            Weight = weight;
            EducationBackGround = educationBackGround;
            NativePlace = nativePlace;
        }

        [HeaderText("身份证号码")]
        [Frozen(true)]
        [ReadOnly(true)]
        [Visible(true)]
        public int Id { get; set; }
        [HeaderText("姓名")]
        [Visible(false)]
        public string Name { get; set; }
        [HeaderText("年龄")]
        public int Age { get; set; }
        [HeaderText("性别")]
        public bool Sex { get; set; }
        [HeaderText("身高")]
        public double Heigth { get; set; }
        [HeaderText("体重")]
        public double Weight { get; set; }
        [HeaderText("学历")]
        public string EducationBackGround { get; set; }
        [HeaderText("籍贯")]
        public string NativePlace { get; set; }
    }
}
