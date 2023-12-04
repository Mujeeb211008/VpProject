using System.Data.SqlClient;
namespace DataAccessLayer
{
	public class DBHelper
	{
		public static SqlConnection GetConnection()
		{
			string con = "Data Source=DESKTOP-3DPDO1H\\INSTANCE1;Initial Catalog=CarsAndBids;Integrated Security=true";
			return new SqlConnection(con);
		}

	}
}
