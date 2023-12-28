using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer
{
	public class DalListing
	{
		public static void SaveListing(ModelListing ml)
		{
			SqlConnection con = DBHelper.GetConnection();
			con.Open();
			SqlCommand cmd = new SqlCommand("SP_Listing", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Make", ml.Make);
			cmd.Parameters.AddWithValue("Model", ml.Model);
			cmd.Parameters.AddWithValue("Variant", ml.Variant);
			cmd.Parameters.AddWithValue("Year", ml.Year);
			cmd.Parameters.AddWithValue("Mileage", ml.Mileage);
			cmd.Parameters.AddWithValue("Category", ml.Category);
			cmd.Parameters.AddWithValue("ReserveCond", ml.ReserveCond);
			cmd.Parameters.AddWithValue("ReserveAmt", ml.ReserveAmt);
			cmd.Parameters.AddWithValue("VIN", ml.VIN);
			cmd.Parameters.AddWithValue("Description", ml.Description);
			cmd.Parameters.AddWithValue("RegNo", ml.RegNo);
			cmd.Parameters.AddWithValue("EngineDisp", ml.EngineDisp);
			cmd.Parameters.AddWithValue("EngineCyl", ml.EngineCyl);
			cmd.Parameters.AddWithValue("Drivetrain", ml.Drivetrain);
			cmd.Parameters.AddWithValue("Transmission", ml.Transmission);
			cmd.Parameters.AddWithValue("Color", ml.Color);
			cmd.ExecuteNonQuery();
            
			con.Close();
        }

        public static string LastListing()
        {
            SqlConnection con = DBHelper.GetConnection();
            SqlCommand cmd = new("SELECT TOP 1 CarId FROM Cars ORDER BY CarId DESC;", con);
            con.Open();
            
            string lastid="";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Check if there are rows returned and read the data
                    lastid = reader["CarId"].ToString();
                }
                // Close the SqlDataReader
                reader.Close();
            }
            con.Close();
            return lastid;
        }

		public static string GetMakeName(string makeId)
		{
			SqlConnection con = DBHelper.GetConnection();
			SqlCommand cmd = new("SELECT MakeName FROM Make WHERE MakeId = @MakeId", con);
			con.Open();

			string makeName = "";
			cmd.Parameters.AddWithValue("@MakeId", makeId);
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				if (reader.Read())
				{
					// Check if there are rows returned and read the data
					makeName = reader["MakeName"].ToString();
				}
				// Close the SqlDataReader
				reader.Close();
			}
			con.Close();
			return makeName;
		}

		public static string GetModelName(string modelId)
		{
			SqlConnection con = DBHelper.GetConnection();
			SqlCommand cmd = new("SELECT ModelName FROM Model WHERE ModelId = @ModelId", con);
			con.Open();

			string modelName = "";
			cmd.Parameters.AddWithValue("@ModelId", modelId);
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				if (reader.Read())
				{
					// Check if there are rows returned and read the data
					modelName = reader["ModelName"].ToString();
				}
				// Close the SqlDataReader
				reader.Close();
			}
			con.Close();
			return modelName;
		}

		public static string GetVariantName(string variantId)
		{
			SqlConnection con = DBHelper.GetConnection();
			SqlCommand cmd = new("SELECT VariantName FROM Variant WHERE VariantId = @VariantId", con);
			con.Open();

			string variantName = "";
			cmd.Parameters.AddWithValue("@VariantId", variantId);
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				if (reader.Read())
				{
					// Check if there are rows returned and read the data
					variantName = reader["VariantName"].ToString();
				}
				// Close the SqlDataReader
				reader.Close();
			}
			con.Close();
			return variantName;
		}

        public static List<ModelListing> GetListings(int? selectedYear = null, string selectedCategory = null, string selectedTransmission = null)
        {
            var ModelListing = new DalListing();

            SqlConnection con = DBHelper.GetConnection();

            // Build the SQL query based on the provided parameters
            string query = "SELECT * FROM Cars WHERE 1=1";

            if (selectedYear.HasValue)
            {
                query += " AND Year = @SelectedYear";
            }

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                query += " AND Category = @SelectedCategory";
            }

            if (!string.IsNullOrEmpty(selectedTransmission))
            {
                query += " AND Transmission = @SelectedTransmission";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            // Add parameters based on the provided values
            if (selectedYear.HasValue)
            {
                cmd.Parameters.AddWithValue("@SelectedYear", selectedYear.Value);
            }

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                cmd.Parameters.AddWithValue("@SelectedCategory", selectedCategory);
            }

            if (!string.IsNullOrEmpty(selectedTransmission))
            {
                cmd.Parameters.AddWithValue("@SelectedTransmission", selectedTransmission);
            }

            con.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            List<ModelListing> Listings = new List<ModelListing>();
            while (sqlDataReader.Read())
            {
                ModelListing ml = new ModelListing();
                ml.CarId = sqlDataReader["CarId"].ToString();
                ml.Make = sqlDataReader["Make"].ToString();
                ml.Model = sqlDataReader["Model"].ToString();
                ml.Variant = sqlDataReader["Variant"].ToString();
                ml.Year = sqlDataReader["Year"].ToString();
                ml.Mileage = sqlDataReader["Mileage"].ToString();
                ml.Category = sqlDataReader["Category"].ToString();
                ml.ReserveCond = sqlDataReader["ReserveCond"].ToString();
                ml.ReserveAmt = sqlDataReader["ReserveAmt"].ToString();
                ml.VIN = sqlDataReader["VIN"].ToString();
                ml.Description = sqlDataReader["Description"].ToString();
                ml.RegNo = sqlDataReader["RegNo"].ToString();
                ml.EngineDisp = sqlDataReader["EngineDisp"].ToString();
                ml.EngineCyl = sqlDataReader["EngineCyl"].ToString();
                ml.Drivetrain = sqlDataReader["Drivetrain"].ToString();
                ml.Transmission = sqlDataReader["Transmission"].ToString();
                ml.Color = sqlDataReader["Color"].ToString();
                ml.CreatedAt = sqlDataReader["CreatedAt"].ToString();
                ml.EndingAt = sqlDataReader["EndingAt"].ToString();
                Listings.Add(ml);
            }

            con.Close();
            return Listings;
        }

        public static ModelListing GetListingByCarId(string carId)
        {
            var dalListing = new DalListing();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT * FROM Cars WHERE CarId = @CarId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CarId", carId);

                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    ModelListing ml = new ModelListing();
                    ml.CarId = sqlDataReader["CarId"].ToString();
                    ml.Make = sqlDataReader["Make"].ToString();
                    ml.Model = sqlDataReader["Model"].ToString();
                    ml.Variant = sqlDataReader["Variant"].ToString();
                    ml.Year = sqlDataReader["Year"].ToString();
                    ml.Mileage = sqlDataReader["Mileage"].ToString();
                    ml.Category = sqlDataReader["Category"].ToString();
                    ml.ReserveCond = sqlDataReader["ReserveCond"].ToString();
                    ml.ReserveAmt = sqlDataReader["ReserveAmt"].ToString();
                    ml.VIN = sqlDataReader["VIN"].ToString();
                    ml.Description = sqlDataReader["Description"].ToString();
                    ml.RegNo = sqlDataReader["RegNo"].ToString();
                    ml.EngineDisp = sqlDataReader["EngineDisp"].ToString();
                    ml.EngineCyl = sqlDataReader["EngineCyl"].ToString();
                    ml.Drivetrain = sqlDataReader["Drivetrain"].ToString();
                    ml.Transmission = sqlDataReader["Transmission"].ToString();
                    ml.Color = sqlDataReader["Color"].ToString();
                    ml.CreatedAt = sqlDataReader["CreatedAt"].ToString();
                    ml.EndingAt = sqlDataReader["EndingAt"].ToString();
                    con.Close();
                    return ml;
                }
            }

            return null; // Return null if the car with the specified CarId is not found
        }


    }
}
