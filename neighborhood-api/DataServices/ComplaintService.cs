using System.Data;
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

                string? outputIdString = cmd.Parameters["@OutputId"].Value.ToString();

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

        public async Task<bool> UpdateComplaintDataByComplaintId(UpdateComplaint complaint)
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
    }
}
