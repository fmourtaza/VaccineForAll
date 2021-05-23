using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using VaccineForAll.Libraries;

namespace VaccineForAll.WebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            //Samples
            //curl -X GET "https://cdn-api.co-vin.in/api/v2/admin/location/states" -H "accept: application/json" -H "Accept-Language: hi_IN"
            //string states = WebApi.Get("https://cdn-api.co-vin.in/api/v2/admin/location/states");

            //curl - X GET "https://cdn-api.co-vin.in/api/v2/admin/location/districts/16" - H "accept: application/json" - H "Accept-Language: hi_IN"
            //string districts = WebApi.Get("https://cdn-api.co-vin.in/api/v2/admin/location/districts/16");

            //curl -X GET "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByDistrict?district_id=512&date=31-03-2021" -H "accept: application/json" -H "Accept-Language: hi_IN"
            //string appointments = WebApi.Get("https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByDistrict?district_id=363&date=15-05-2021");

            //SendTeamUpdates if required to all Citizens
            //VaccineSlot.SendTeamUpdates(Credentials.MailSubjectTeamUpdates);
            //Console.ReadLine();

            //Main
            Console.WriteLine(" ***** VaccineForAll.WebJob - Start *****");
            VaccineSlot vaccineSlot = new VaccineSlot();
            vaccineSlot.LookupAvailableSlots();
            Console.WriteLine(" ***** VaccineForAll.WebJob - End *****");
        }
    }
}
