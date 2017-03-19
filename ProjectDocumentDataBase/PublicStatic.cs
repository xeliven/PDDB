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


    }
}
