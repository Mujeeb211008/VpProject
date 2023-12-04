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
	public class DalUserInfo
	{
		public static ModelUserInfo GetUserInfoSpecific(string currentUserId)
		{
            ModelUserInfo mu = new ModelUserInfo();
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
			using (SqlCommand cmd = new SqlCommand("SP_GetUserInfo", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", currentUserId);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						// Check if there are rows returned and read the data
						mu.FirstName = reader["FirstName"].ToString();
						mu.LastName = reader["LastName"].ToString();
						mu.Phone = reader["Phone"].ToString();
						mu.Email = reader["Email"].ToString();
						mu.Password = reader["Password"].ToString();
						mu.Role = reader["Role"].ToString();
					}
					// Close the SqlDataReader
					reader.Close();
				}
			}

			//         SqlCommand cmd = new SqlCommand("SP_GetUserInfo", con);
			//         cmd.CommandType = CommandType.StoredProcedure;
			//         cmd.Parameters.AddWithValue("UserId", currentUserId);
			//         cmd.ExecuteNonQuery();

			//SqlDataReader sqlDataReader = cmd.ExecuteReader();
			//         mu.FirstName = sqlDataReader["FirstName"].ToString();
			//         mu.LastName = sqlDataReader["LastName"].ToString();
			//         mu.Phone = sqlDataReader["Phone"].ToString();
			//         mu.Email = sqlDataReader["Email"].ToString();
			//         mu.Password = sqlDataReader["Password"].ToString();
			//         mu.Role = sqlDataReader["Role"].ToString();
			con.Close();
            return mu;
        }
		public static void SaveUserInfo(ModelUserInfo mu)
		{
			SqlConnection con = DBHelper.GetConnection();
			con.Open();
			SqlCommand cmd = new SqlCommand("SP_SaveUser", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("FirstName", mu.FirstName);
			cmd.Parameters.AddWithValue("LastName", mu.LastName);
			cmd.Parameters.AddWithValue("Email", mu.Email);
			cmd.Parameters.AddWithValue("Phone", mu.Phone);
			cmd.Parameters.AddWithValue("Password", mu.Password);
			cmd.Parameters.AddWithValue("Role", mu.Role);
			cmd.ExecuteNonQuery();
			con.Close();

		}

		public static void UpdateUserInfo(string currentUserId, ModelUserInfo mu)
		{
			SqlConnection con = DBHelper.GetConnection();
			con.Open();
			SqlCommand cmd = new SqlCommand("SP_UpdateUser", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("UserId", currentUserId);
			cmd.Parameters.AddWithValue("FirstName", mu.FirstName);
			cmd.Parameters.AddWithValue("LastName", mu.LastName);
			cmd.Parameters.AddWithValue("Email", mu.Email);
			cmd.Parameters.AddWithValue("Phone", mu.Phone);
			cmd.Parameters.AddWithValue("Password", mu.Password);
			cmd.Parameters.AddWithValue("Role", mu.Role);
			cmd.ExecuteNonQuery();
			con.Close();

		}

		public static void DeleteUserInfo(int id)
		{
			SqlConnection con = DBHelper.GetConnection();
			con.Open();
			SqlCommand cmd = new SqlCommand("SP_DeleteUser", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("InputUserId", id);

			cmd.ExecuteNonQuery();
			con.Close();

		}



		private static ModelUserInfo UserInfo;
		private int UserId;


		public string? UserName { get; private set; }

		public static List<ModelUserInfo> GetUserInfo()
		{
			var ModelUserInfo = new DalUserInfo();

			SqlConnection con = DBHelper.GetConnection();
			SqlCommand cmd = new("select * from UserInfo", con);
			con.Open();
			SqlDataReader sqlDataReader = cmd.ExecuteReader();
			List<ModelUserInfo> ListUsers = new List<ModelUserInfo>();
			while (sqlDataReader.Read())
			{
				ModelUserInfo modelUserInfo = new ModelUserInfo();
				modelUserInfo.UserId = sqlDataReader["UserId"].ToString();
				modelUserInfo.FirstName = sqlDataReader["FirstName"].ToString();
				modelUserInfo.LastName = sqlDataReader["LastName"].ToString();
				modelUserInfo.Phone = sqlDataReader["Phone"].ToString();
				modelUserInfo.Email = sqlDataReader["Email"].ToString();
				modelUserInfo.Password = sqlDataReader["Password"].ToString();
				Console.WriteLine(modelUserInfo.Email);
				ListUsers.Add(modelUserInfo);

			}
			con.Close();
			return ListUsers;
		}

    }


}


