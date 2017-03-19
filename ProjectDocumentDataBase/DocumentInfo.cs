using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjectDocumentDataBase
{
   public  class DocumentInfo
    {

        private string _文件名;
        private string _文件地址;
        private string _档案编号;

        private string _项目类别;
        private string _项目类别缩写;
        private string _项目名称;
        private string _项目名称缩写;
        private string _文档类别;
        private string _文档类别缩写;

        private DateTime _文档建立时间;
        private DateTime _文档归档时间;
        private DateTime _文档最后修改时间;
        private DateTime _文档最后访问时间;

        private string _文档大小;


        [CategoryAttribute("基本"), DescriptionAttribute("文件名称")]
        public string 文件名
        {
            get
            {
                return _文件名;
            }

            set
            {
                _文件名 = value;
            }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("文件地址")]
        public string 文件地址
        {
            get
            {
                return _文件地址;
            }

            set
            {
                _文件地址 = value;
            }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("档案编号")]
        public string 档案编号
        {
            get
            {
                return _档案编号;
            }

            set
            {
                _档案编号 = value;
            }
        }
        [CategoryAttribute("分组"), DescriptionAttribute("项目类别")]
        public string 项目类别
        {
            get
            {
                return _项目类别;
            }

            set
            {
                _项目类别 = value;
            }
        }
        [CategoryAttribute("分组"), DescriptionAttribute("项目类别缩写")]
        public string 项目类别缩写
        {
            get
            {
                return _项目类别缩写;
            }

            set
            {
                _项目类别缩写 = value;
            }
        }
        [CategoryAttribute("分组"), DescriptionAttribute("项目名称")]
        public string 项目名称
        {
            get
            {
                return _项目名称;
            }

            set
            {
                _项目名称 = value;
            }
        }
        [CategoryAttribute("分组"), DescriptionAttribute("项目名称缩写")]
        public string 项目名称缩写
        {
            get
            {
                return _项目名称缩写;
            }

            set
            {
                _项目名称缩写 = value;
            }
        }
        [CategoryAttribute("分组"), DescriptionAttribute("文档类别")]
        public string 文档类别
        {
            get
            {
                return _文档类别;
            }

            set
            {
                _文档类别 = value;
            }
        }
        [CategoryAttribute("分组"), DescriptionAttribute("文档类别缩写")]
        public string 文档类别缩写
        {
            get
            {
                return _文档类别缩写;
            }

            set
            {
                _文档类别缩写 = value;
            }
        }
        [CategoryAttribute("时间"), DescriptionAttribute("文档建立时间")]
        public DateTime 文档建立时间
        {
            get
            {
                return _文档建立时间;
            }

            set
            {
                _文档建立时间 = value;
            }
        }
        [CategoryAttribute("时间"), DescriptionAttribute("文档归档时间")]
        public DateTime 文档归档时间
        {
            get
            {
                return _文档归档时间;
            }

            set
            {
                _文档归档时间 = value;
            }
        }
        [CategoryAttribute("时间"), DescriptionAttribute("文档最后修改时间")]
        public DateTime 文档最后修改时间
        {
            get
            {
                return _文档最后修改时间;
            }

            set
            {
                _文档最后修改时间 = value;
            }
        }
        [CategoryAttribute("时间"), DescriptionAttribute("文档最后访问时间")]
        public DateTime 文档最后访问时间
        {
            get
            {
                return _文档最后访问时间;
            }

            set
            {
                _文档最后访问时间 = value;
            }
        }
        [CategoryAttribute("信息"), DescriptionAttribute("文档大小")]
        public string 文档大小
        {
            get
            {
                return _文档大小;
            }

            set
            {
                _文档大小 = value;
            }
        }
    }
}
