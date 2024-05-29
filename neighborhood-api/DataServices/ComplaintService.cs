using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using neighborhood_api.Models;
using neighborhood_api.Models.Complaints;

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
                SqlCommand cmd = new SqlCommand("CREATE_Complaint", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outputIdParameter = new SqlParameter("@OutputId", SqlDbType.UniqueIdentifier);
                outputIdParameter.Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@PersonName",complaint.PersonName );
                cmd.Parameters.AddWithValue("@PersonApartmentCode",complaint.PersonApartmentCode );
                cmd.Parameters.AddWithValue("@Location",complaint.Location );
                cmd.Parameters.AddWithValue("@Category",complaint.Category );
                cmd.Parameters.AddWithValue("@Description",complaint.Description);

                cmd.Parameters.Add(outputIdParameter);

                await cmd.ExecuteScalarAsync();

                string? outputIdString = cmd.Parameters[parameterName: "@OutputId"].Value.ToString();

                return outputIdString ?? string.Empty;
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
                SqlCommand cmd = new SqlCommand("UPDATE_ComplaintData_By_ComplaintId", sqlConnection);
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

        public async Task<(bool, DateTime?)> UpdateComplaintStatusByComplaintIdAsync(UpdateComplaintStatus complaint)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE_ComplaintStatus_By_ComplaintId", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outputDate = new("@OutputDateDeActivated", SqlDbType.DateTime);
                outputDate.Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@Id", complaint.Id);
                cmd.Parameters.AddWithValue("@Status", (int)complaint.CurrentStatus);
                cmd.Parameters.Add(outputDate);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    DateTime outputDateResponse = (DateTime)cmd.Parameters[parameterName: "@OutputDateDeActivated"].Value;

                    return (true, outputDateResponse.ToUniversalTime());
                }

                return (false, null);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return (false, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return (false, null);
            }
        }

        public async Task<(bool, List<Complaint>)> ReadAllComplaintsByDateAsync()
        {
            List<Complaint> complaints = new List<Complaint>();
            try
            {
                SqlCommand cmd = new SqlCommand("READ_All_Complaints_By_DateActivated", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return (true, complaints);
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
                        CurrentStatus = (Status)reader.GetInt32(6),
                        DateActivated = reader.GetDateTime(7),
                        DateDeActivated = reader.GetValue(8) == DBNull.Value ? null : reader.GetDateTime(8)
                    });
                };
                await reader.CloseAsync();

                return (true, complaints);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return (false, complaints);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return (false, complaints);
            }
        }

        public async Task<(bool, List<Complaint>)> ReadSearchComplaintByPersonName(string personName)
        {
            List<Complaint> complaints = new();
            try
            {
                SqlCommand cmd = new SqlCommand("READ_Search_Complaint_By_PersonName", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PersonName", personName);

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return (true, complaints);
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
                        CurrentStatus = (Status)reader.GetInt32(6),
                        DateActivated = reader.GetDateTime(7),
                        DateDeActivated = reader.GetValue(8) == DBNull.Value ? null : reader.GetDateTime(8)
                    });
                }
                await reader.CloseAsync();

                return (true, complaints);

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return (false, complaints);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return (false, complaints);
            }
        }
    }
}
