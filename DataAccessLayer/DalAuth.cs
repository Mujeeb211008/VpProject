using System.Data.SqlClient;
using System.Data;
using ModelLayer;
namespace DataAccessLayer
{
	public class DalAuth
	{
		public static ModelUserInfo Auth(string Email, string Password)
		{
			try
			{
				SqlConnection con = DBHelper.GetConnection();
				SqlCommand cmd = new SqlCommand("SP_Auth", con);
				cmd.Parameters.AddWithValue("@Email", Email);
				cmd.Parameters.AddWithValue("@Password", Password);
				cmd.CommandType = CommandType.StoredProcedure;
				con.Open();
				SqlDataReader sdr = cmd.ExecuteReader();
				ModelUserInfo modelUserAccount = new ModelUserInfo();
				while (sdr.Read())
				{
					modelUserAccount.UserId = sdr["UserId"].ToString();
					modelUserAccount.Role = sdr["Role"].ToString();
					modelUserAccount.FirstName = sdr["FirstName"].ToString();
				}
				sdr.Close();
				con.Close();
				return modelUserAccount;
			}
			catch
			{
				return new ModelUserInfo();
			}
		}
	}
}