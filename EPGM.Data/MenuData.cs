using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGM.Data
{
    public class MenuData
    {
        public static List<SPGetMessages_Result> GetMessagesMenu(string UserType)
        {
            using (var context = new DBEntities())
            {
                return context.SPGetMessages(UserType).ToList();
            }
        }
    }
}
