using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ConstantsCommon
    {
        public const string Role_Admin = "Admin";
        public const string Role_Client = "Client";
        public const string Role_Employee = "Employee";

        //LOCAL STORAGE
        public const string LS_Author = "Initial_Author";
        public const string LS_OrderDetails = "Order_Details";
        public const string LS_Jwt = "JWT Token";

        //PAYMENT STATUS
        public const string PS_Pending = "Pending";
        public const string PS_Canceled = "Canceled";
        public const string PS_Paid = "Paid";


    }
}
