using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LoanBook : BaseEntity<int>
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public bool IsReturn { get; set; } = true;
        public DateTime loanDate { get; set; }

        #region Navigation properties
        public virtual Member Member { get; set; }
        public virtual Book Book { get; set; }
        #endregion
    }
}
