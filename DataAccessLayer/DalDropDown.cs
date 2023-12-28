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
	public class DalDropDown
	{
		public static List<ModelCountry> GetAllCountries()
		{
			try
			{
				var ModelCountry = new DalDropDown();

				SqlConnection con = DBHelper.GetConnection();
				SqlCommand cmd = new("select * from Countries", con);
				con.Open();
				SqlDataReader sqlDataReader = cmd.ExecuteReader();
				List<ModelCountry> ListCountries = new List<ModelCountry>();
				while (sqlDataReader.Read())
				{
					ModelCountry mc = new ModelCountry();
					mc.CountryId = sqlDataReader["CountryId"].ToString();
					mc.CountryName = sqlDataReader["CountryName"].ToString();
					
		
					ListCountries.Add(mc);

				}
				con.Close();
				return ListCountries;
			}
			catch
			{
				throw;
			}
		}

		public static List<ModelCity> GetCityData(string countryId)
		{
			try
			{
				var ModelCity = new DalDropDown();
				

				SqlConnection con = DBHelper.GetConnection();
				SqlCommand cmd = new SqlCommand($"select * from Cities where CountryId = '{countryId}'", con);
				con.Open();
				SqlDataReader sqlDataReader = cmd.ExecuteReader();
				List<ModelCity> ListCities = new List<ModelCity>();
				while (sqlDataReader.Read())
				{
					ModelCity city = new ModelCity();
					city.CityId = sqlDataReader["CityId"].ToString();
					city.CityName = sqlDataReader["CityName"].ToString();

					ListCities.Add(city);
				}
				con.Close();
				return ListCities;
			}
			catch
			{
				throw;
			}
		}


	}
}

