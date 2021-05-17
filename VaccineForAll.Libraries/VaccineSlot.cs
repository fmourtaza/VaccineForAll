using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class VaccineSlot
    {
        public static int dayCount = 5;

        /// <summary>
        /// Lookup for available slots
        /// </summary>
        public void LookupAvailableSlots()
        {
            try
            {
                DataTable sourceTable = OperationsCRUD.ReadData();
                if (sourceTable != null)
                {
                    //Process for each Citizen
                    foreach (DataRow row in sourceTable.Rows)
                    {
                        try
                        {
                            string district_id = Convert.ToString(row["CitizenDistrictID"]);
                            string district_name = Convert.ToString(row["CitizenDistrictName"]);
                            string citizenEmail = Convert.ToString(row["CitizenEmail"]);
                            string citizenDoseChoice = Convert.ToString(row["CitizenDoseChoice"]);
                            int citizenAge = Convert.ToInt32(row["CitizenAge"]);

                            Console.WriteLine(string.Format(" == Processing data for citizenEmail:{0} - district_name:{1} - citizenAge:{2}.", citizenEmail, district_name, citizenAge));

                            //Fetch data
                            DataTable appoitmentData = LookupConsecutiveDays(district_id, citizenAge, citizenDoseChoice);
                            if (appoitmentData.Rows.Count > 0)
                            {

                                Console.WriteLine(String.Format("  = Rows.Count:{0}", appoitmentData.Rows.Count));
                                //Sort data
                                DataView dataView = appoitmentData.DefaultView;
                                dataView.Sort = "session_date asc";
                                DataTable sortedAppoitmentData = dataView.ToTable();

                                //Convert to Html table
                                String html = Utilities.toHTML_Table(sortedAppoitmentData, citizenEmail, citizenAge, district_name, citizenDoseChoice);

                                //Mail report
                                bool IsSuccess = Utilities.SendMail(citizenEmail, "VaccineForAll Report", html);
                                if (IsSuccess) OperationsCRUD.UpdateMailCountSent(citizenEmail);
                                Console.WriteLine(String.Format("  = Report sent successfully"));
                            }
                            else
                            {
                                //Inform Citizen on a daily basis if no data found
                                var istDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                                if (istDateTime.Hour >= 21 || istDateTime.Hour < 22)
                                {
                                    int citizenDailyMailSentCount = OperationsCRUD.ReadDataNoDailyMailSentCount(citizenEmail);
                                    if (citizenDailyMailSentCount == 0)
                                        Utilities.SendNoDailyMailCount(citizenEmail, "VaccineForAll - Let us get vaccinated!", district_name, citizenAge, citizenDoseChoice);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utilities.HandleException(ex);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        /// <summary>
        /// Lookup for available slots for "n" consecutives days where [n = dayCount]
        /// </summary>
        /// <param name="district_id"></param>
        /// <param name="citizenEmail"></param>
        /// <param name="citizenAge"></param>
        /// <returns></returns>
        public DataTable LookupConsecutiveDays(String district_id, int citizenAge, String citizenDoseChoice)
        {
            DataTable appointmentDataTable = CreateDataTable();
            for (int i = 1; i <= dayCount; i++)
            {
                try
                {
                    //Lookup for next consecutive 5 days
                    string date = DateTime.Today.AddDays(i).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    appointmentDataTable = GetAppointments(appointmentDataTable, date, district_id, citizenAge, citizenDoseChoice);
                }
                catch (Exception ex)
                {
                    Utilities.HandleException(ex);
                }

            }

            return appointmentDataTable;
        }

        /// <summary>
        /// Query the web api to get center and session available based on Citizen Age & Available Capacity Dose
        /// </summary>
        /// <param name="table"></param>
        /// <param name="date"></param>
        /// <param name="district_id"></param>
        /// <param name="citizenAge"></param>
        /// <returns></returns>
        public DataTable GetAppointments(DataTable table, String date, String district_id, int citizenAge, String citizenDoseChoice)
        {
            try
            {
                string requestUrl = string.Format("https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByDistrict?district_id={0}&date={1}", district_id, date);
                WebApi webApi = new WebApi();
                string json = webApi.Get(requestUrl);
                if (string.IsNullOrEmpty(json)) throw new Exception("Web requestion is null");
                Appointment appointment = JsonConvert.DeserializeObject<Appointment>(json);

                foreach (Center center in appointment.centers)
                {
                    foreach (Session session in center.sessions)
                    {
                        //Primary check: Age eligibility
                        if (citizenAge >= session.min_age_limit)
                        {
                            //Secondary check: available_capacity_dose1 or available_capacity_dose2
                            switch (citizenDoseChoice)
                            {
                                case "dose1":
                                    if (session.available_capacity_dose1 > 0)
                                    {
                                        DataRow row = table.NewRow();
                                        row["session_date"] = session.date;
                                        row["session.min_age_limit"] = session.min_age_limit;
                                        row["session.available_capacity_dose1"] = session.available_capacity_dose1;
                                        row["session.available_capacity_dose2"] = "non applicable";
                                        row["session.vaccine"] = session.vaccine;
                                        row["center.district_name"] = center.district_name;
                                        row["center.address"] = center.address;
                                        row["center.pincode"] = center.pincode;
                                        table.Rows.Add(row);
                                    }
                                    break;
                                case "dose2":
                                    if (session.available_capacity_dose2 > 0)
                                    {
                                        DataRow row = table.NewRow();
                                        row["session_date"] = session.date;
                                        row["session.min_age_limit"] = session.min_age_limit;
                                        row["session.available_capacity_dose1"] = "non applicable";
                                        row["session.available_capacity_dose2"] = session.available_capacity_dose2;
                                        row["session.vaccine"] = session.vaccine;
                                        row["center.district_name"] = center.district_name;
                                        row["center.address"] = center.address;
                                        row["center.pincode"] = center.pincode;
                                        table.Rows.Add(row);
                                    }
                                    break;
                                case "both":
                                default:
                                    if (session.available_capacity_dose1 > 0 || session.available_capacity_dose2 > 0)
                                    {
                                        DataRow row = table.NewRow();
                                        row["session_date"] = session.date;
                                        row["session.min_age_limit"] = session.min_age_limit;
                                        row["session.available_capacity_dose1"] = session.available_capacity_dose1;
                                        row["session.available_capacity_dose2"] = session.available_capacity_dose2;
                                        row["session.vaccine"] = session.vaccine;
                                        row["center.district_name"] = center.district_name;
                                        row["center.address"] = center.address;
                                        row["center.pincode"] = center.pincode;
                                        table.Rows.Add(row);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }

            return table;
        }

        /// <summary>
        /// Datatable to hold the appointment data
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("session_date");
            dt.Columns.Add("session.min_age_limit");
            dt.Columns.Add("session.available_capacity_dose1");
            dt.Columns.Add("session.available_capacity_dose2");
            dt.Columns.Add("session.vaccine");
            dt.Columns.Add("center.district_name");
            dt.Columns.Add("center.address");
            dt.Columns.Add("center.pincode");
            return dt;
        }

        /// <summary>
        /// Show report on the web page itself
        /// </summary>
        /// <param name="district_id"></param>
        /// <param name="district_name"></param>
        /// <param name="citizenAge"></param>
        /// <returns></returns>
        public DataTable ReportViewer(String district_id, String district_name, int citizenAge, String citizenDoseChoice)
        {
            try
            {
                //Fetch data
                return LookupConsecutiveDays(district_id, citizenAge, citizenDoseChoice);
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }

            return null;
        }
    }
}
