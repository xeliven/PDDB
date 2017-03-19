using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectDocumentDataBase
{
    public static class PublicStatic
    {
        private static string _storePath;
        private static string _UnfiledPath;
        private static string _ModelPath;
        private static string _TempPath;

        public static string StorePath
        {
            get
            {
                return _storePath;
            }

            set
            {
                _storePath = value;
            }
        }

        public static string UnfiledPath
        {
            get
            {
                return _UnfiledPath;
            }

            set
            {
                _UnfiledPath = value;
            }
        }

        public static string ModelPath
        {
            get
            {
                return _ModelPath;
            }

            set
            {
                _ModelPath = value;
            }
        }

        public static string TempPath
        {
            get
            {
                return _TempPath;
            }

            set
            {
                _TempPath = value;
            }
        }

        public static string GetDocNumber(string projTypeShort, string projShort, string docType, DateTime datetime)
        {
            return $"HL-{projTypeShort}-{projShort}-{docType}-{datetime.ToString("yyyyMMdd")}";
        }
        public static String GetDisplaySize(long size)
        {
            //如果字节数少于1024，则直接以B为单位，否则先除于1024，后3位因太少无意义  
            if (size < 1024)
            {
                return size.ToString() + "B";
            }
            else
            {
                size = size / 1024;
            }
            //如果原字节数除于1024之后，少于1024，则可以直接以KB作为单位  
            //因为还没有到达要使用另一个单位的时候  
            //接下去以此类推  
            if (size < 1024)
            {
                return size.ToString() + "KB";
            }
            else
            {
                size = size / 1024;
            }
            if (size < 1024)
            {
                //因为如果以MB为单位的话，要保留最后1位小数，  
                //因此，把此数乘以100之后再取余  
                size = size * 100;
                return (size / 100).ToString() + "." + (size % 100).ToString() + "MB";
            }
            else
            {
                //否则如果要以GB为单位的，先除于1024再作同样的处理  
                size = size * 100 / 1024;
                return (size / 100).ToString() + "." + (size % 100).ToString() + "GB";
            }
        }

    }
}
