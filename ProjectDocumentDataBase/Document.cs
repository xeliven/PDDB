//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectDocumentDataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int id { get; set; }
        public int msgflag { get; set; }
        public System.DateTime msgdatetime { get; set; }
        public string docname { get; set; }
        public string doctypeshort { get; set; }
        public string projname { get; set; }
        public string projshort { get; set; }
        public string docnumber { get; set; }
        public string filepath { get; set; }
        public Nullable<System.DateTime> modifydatetime { get; set; }
    }
}
