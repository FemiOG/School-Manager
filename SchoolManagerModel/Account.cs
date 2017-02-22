//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int AccountNo { get; set; }
        public double CreditBalance { get; set; }
        public double Fees { get; set; }
        public double DebitBalance { get; set; }
        public double BalanceLastTerm { get; set; }
        public double Rebate { get; set; }
        public bool FullyPaid { get; set; }
        public string AccountNumber { get; set; }
        public double SchoolBusFees { get; set; }
    
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual Student Student { get; set; }
    }
}
