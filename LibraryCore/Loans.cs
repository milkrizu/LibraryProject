//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryCore
{
    using System;
    using System.Collections.Generic;
    
    public partial class Loans
    {
        public int LoanID { get; set; }
        public Nullable<int> BookID { get; set; }
        public Nullable<int> ReaderID { get; set; }
        public System.DateTime LoanDate { get; set; }
        public System.DateTime ReturnDate { get; set; }
    
        public virtual Books Books { get; set; }
        public virtual Readers Readers { get; set; }
    }
}
