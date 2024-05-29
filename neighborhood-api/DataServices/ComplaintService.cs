using System.Data;
using Microsoft.Data.SqlClient;
using neighborhood_api.Models;
using neighborhood_api.Models.Complaints;
using neighborhood_api.Requests;

namespace neighborhood_api.DataServices
{
    public class ComplaintService
    {
        private readonly SqlConnection sqlConnection;

        public ComplaintService()
        {
            sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"));
            sqlConnection.Open();
        }

        ~ComplaintService()
        {
            sqlConnection.Close();
        }

        public async Task<string> CreateNewComplaintAsync(CreateComplaint complaint)
        {
            try
            {
                SqlCommand cmd = new("CREATE_Complaint", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                Guid Id = Guid.NewGuid();

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@PersonName",complaint.PersonName );
                cmd.Parameters.AddWithValue("@PersonApartmentCode",complaint.PersonApartmentCode );
                cmd.Parameters.AddWithValue("@Location",complaint.Location );
                cmd.Parameters.AddWithValue("@Category",complaint.Category );
                cmd.Parameters.AddWithValue("@Description",complaint.Description);

                await cmd.ExecuteScalarAsync();

                return Id.ToString();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return string.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return string.Empty;
            }
        }

        public async Task<bool> UpdateComplaintDataByComplaintIdAsync(UpdateComplaint complaint)
        {
            try
            {
                SqlCommand cmd = new("UPDATE_ComplaintData_By_ComplaintId", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Guid.Parse(complaint.Id));
                cmd.Parameters.AddWithValue("@PersonName", complaint.PersonName);
                cmd.Parameters.AddWithValue("@PersonApartmentCode", complaint.PersonApartmentCode);
                cmd.Parameters.AddWithValue("@Location", complaint.Location);
                cmd.Parameters.AddWithValue("@Category", complaint.Category);
                cmd.Parameters.AddWithValue("@Description",complaint.Description);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public async Task<bool> UpdateComplaintStatusByComplaintIdAsync(UpdateComplaintStatus complaint, DateTime dateTime)
        {
            try
            {
                SqlCommand cmd = new("UPDATE_ComplaintStatus_By_ComplaintId", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", complaint.Id);
                cmd.Parameters.AddWithValue("@CurrentStatus", (int)complaint.CurrentStatus);
                cmd.Parameters.AddWithValue("@LastUpdated", dateTime);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public async Task<List<Complaint>?> ReadAllComplaintsByDateAsync()
        {
            List<Complaint> complaints = [];
            try
            {
                SqlCommand cmd = new("READ_All_Complaints_By_DateActivated", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return complaints;
                }

                while (await reader.ReadAsync())
                {
                    complaints.Add(new Complaint
                    {
                        Id = reader.GetGuid(0).ToString(),
                        PersonName = reader.GetString(1),
                        PersonApartmentCode = reader.GetString(2),
                        Location = (Locations)reader.GetInt32(3),
                        Category = (Categories)reader.GetInt32(4),
                        Description = reader.GetString(5),
                        CurrentStatus = (ActiveStatus)reader.GetInt32(6),
                        DateActivated = reader.GetDateTime(7),
                        LastUpdated = reader.GetValue(8) == DBNull.Value ? null : reader.GetDateTime(8)
                    });
                };
                await reader.CloseAsync();

                return complaints;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public async Task<List<Complaint>?> ReadSearchComplaintByPersonName(string personName)
        {
            List<Complaint> complaints = [];
            try
            {
                SqlCommand cmd = new("READ_Search_Complaint_By_PersonName", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PersonName", personName);

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return complaints;
                }

                while (await reader.ReadAsync())
                {
                    complaints.Add(new Complaint
                    {
                        Id = reader.GetGuid(0).ToString(),
                        PersonName = reader.GetString(1),
                        PersonApartmentCode = reader.GetString(2),
                        Location = (Locations)reader.GetInt32(3),
                        Category = (Categories)reader.GetInt32(4),
                        Description = reader.GetString(5),
                        CurrentStatus = (ActiveStatus)reader.GetInt32(6),
                        DateActivated = reader.GetDateTime(7),
                        LastUpdated = reader.GetValue(8) == DBNull.Value ? null : reader.GetDateTime(8)
                    });
                }
                await reader.CloseAsync();

                return complaints;

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}
