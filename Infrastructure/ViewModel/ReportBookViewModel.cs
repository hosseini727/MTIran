using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel
{
    public  class ReportBookViewModel
    {
        public string FullName { get; set; }
        public string NCode { get; set; }
        public string PhoneNumber { get; set; }
        public int BookNo { get; set; }
        public string Title { get; set; }
        public DateTime loanDate { get; set; }


    }
}
