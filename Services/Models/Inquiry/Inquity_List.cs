using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Services.Models.Inquiry
{
    public class Inquity_List
    {
        public int Id { get; set; } // الرقم التعريفي للاستفسار (Primary Key)
        public int UserId { get; set; } // الرقم التعريفي للمستخدم اللي بعت الاستفسار (Foreign Key)
        public int PropertyId { get; set; } // الرقم التعريفي للعقار اللي عليه الاستفسار (Foreign Key)
        public string UserName { get; set; }
        public string PropertyName { get; set; }
        public string PhoneNumber { get; set; } // رقم الهاتف للشخص الذي يريد الاستفسار
        public string Message { get; set; } // الرسالة أو الاستفسار
        public DateTime DateSent { get; set; } // تاريخ إرسال الاستفسار
    }
}
