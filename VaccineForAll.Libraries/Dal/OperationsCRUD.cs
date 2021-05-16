using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class OperationsCRUD
    {
        public static int mailSentCount = 21;

        public static SqlConnection GetSqlConnection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = Credentials.SqlDataSource;
                builder.UserID = Credentials.SqlUserID;
                builder.Password = Credentials.SqlPassword;
                builder.InitialCatalog = Credentials.SqlInitialCatalog;
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                return connection;
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
            return null;
        }

        public static int CreateRecord(Citizen citizen)
        {
            SqlConnection connection = GetSqlConnection();

            String query = @"SELECT * FROM [dbo].[CitizenData] WHERE([CitizenEmail] = @CitizenEmail)";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CitizenEmail", citizen.CitizenEmail);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return UpdateTo(citizen);
                }
                else
                {
                    return InsertTo(citizen);
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public static int InsertTo(Citizen citizen)
        {
            SqlConnection connection = GetSqlConnection();

            String query = @"INSERT INTO[dbo].[CitizenData]
                                         ([CitizenEmail]
                                         ,[CitizenDistrictID]
                                         ,[CitizenDistrictName]
                                         ,[CitizenAge]
                                         ,[CitizenConfirmedSlotMailSentCount])
                                VALUES
                                          (@CitizenEmail
                                          ,@CitizenDistrictID
                                          ,@CitizenDistrictName
                                          ,@CitizenAge
                                          ,@CitizenConfirmedSlotMailSentCount)";

            SqlCommand cmd = new SqlCommand(query, connection);

            //Pass values to Parameters
            cmd.Parameters.AddWithValue("@CitizenEmail", citizen.CitizenEmail);
            cmd.Parameters.AddWithValue("@CitizenDistrictID", citizen.CitizenDistrictID);
            cmd.Parameters.AddWithValue("@CitizenDistrictName", citizen.CitizenDistrictName);
            cmd.Parameters.AddWithValue("@CitizenAge", citizen.CitizenAge);
            cmd.Parameters.AddWithValue("@CitizenConfirmedSlotMailSentCount", 0);

            try
            {
                connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Utilities.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public static int UpdateTo(Citizen citizen)
        {
            SqlConnection connection = GetSqlConnection();

            String query = @"UPDATE [dbo].[CitizenData]
                                SET  [CitizenEmail] = @CitizenEmail
                                    ,[CitizenDistrictID] = @CitizenDistrictID
                                    ,[CitizenDistrictName] = @CitizenDistrictName
                                    ,[CitizenAge] = @CitizenAge
                                    ,[CitizenConfirmedSlotMailSentCount] = @CitizenConfirmedSlotMailSentCount
                               WHERE [CitizenEmail] = @CitizenEmail";

            SqlCommand cmd = new SqlCommand(query, connection);

            //Pass values to Parameters
            cmd.Parameters.AddWithValue("@CitizenEmail", citizen.CitizenEmail);
            cmd.Parameters.AddWithValue("@CitizenDistrictID", citizen.CitizenDistrictID);
            cmd.Parameters.AddWithValue("@CitizenDistrictName", citizen.CitizenDistrictName);
            cmd.Parameters.AddWithValue("@CitizenAge", citizen.CitizenAge);
            cmd.Parameters.AddWithValue("@CitizenConfirmedSlotMailSentCount", 0);

            try
            {
                connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Utilities.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public static DataTable ReadData()
        {
            SqlConnection connection = GetSqlConnection();
            String query = @"SELECT * FROM [dbo].[CitizenData] WHERE [CitizenConfirmedSlotMailSentCount] < @MailSentCount ORDER BY [DateInserted] ASC";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@MailSentCount", mailSentCount);

            try
            {
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dataTable = new DataTable("DataTable");
                dataTable.Load(dr);
                return dataTable;
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public static int UpdateMailCountSent(String citizenEmail)
        {
            SqlConnection connection = null;
            int citizenConfirmedSlotMailSentCount = 0;

            #region //Get last count
            try
            {
                connection = GetSqlConnection();
                String query = @"SELECT [CitizenConfirmedSlotMailSentCount] FROM [dbo].[CitizenData] WHERE([CitizenEmail] = @CitizenEmail)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CitizenEmail", citizenEmail);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        citizenConfirmedSlotMailSentCount = Convert.ToInt32(reader["CitizenConfirmedSlotMailSentCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }
            #endregion

            #region //Update the count
            try
            {

                connection = GetSqlConnection();
                String queryUpdate = @"UPDATE [dbo].[CitizenData] 
                                             SET  [CitizenConfirmedSlotMailSentCount] = @CitizenConfirmedSlotMailSentCount
                                            WHERE [CitizenEmail] = @CitizenEmail";
                citizenConfirmedSlotMailSentCount = citizenConfirmedSlotMailSentCount + 1;
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, connection);
                cmdUpdate.Parameters.AddWithValue("@CitizenEmail", citizenEmail);
                cmdUpdate.Parameters.AddWithValue("@CitizenConfirmedSlotMailSentCount", citizenConfirmedSlotMailSentCount);
                connection.Open();
                return cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }
            #endregion

            return 0;
        }
    }
}
