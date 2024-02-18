using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Book : BaseEntity<int>
    {
        public int BookNo { get; set; }
        public string Title { get; set; }
        public virtual ICollection<LoanBook> LoanBooks { get; set; }
    }
}
