using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGBL.Data.Models
{
    [Table("Loans")]
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
     
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
