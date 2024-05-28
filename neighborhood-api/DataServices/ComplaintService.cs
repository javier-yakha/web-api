using Microsoft.Data.SqlClient;
using neighborhood_api.Models;

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

        public async Task<bool> CreateNewComplaintAsync(CreateComplaint complaint)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CREATE_Complaint", sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PersonName",complaint.PersonName );
                cmd.Parameters.AddWithValue("@PersonApartmentCode",complaint.PersonApartmentCode );
                cmd.Parameters.AddWithValue("@Location",complaint.Location );
                cmd.Parameters.AddWithValue("@Category",complaint.Category );
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
