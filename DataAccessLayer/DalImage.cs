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
    public class DalImage
    {
        public static void SaveImages(ModelImage mi)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SaveImage", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ImageName", mi.ImageName);
            cmd.Parameters.AddWithValue("@ImagePath", mi.ImagePath);
            cmd.Parameters.AddWithValue("@CarId", mi.CarId);
            cmd.ExecuteNonQuery();
            con.Close();
            
        }

        public static List<ModelImage> GetImage(string carId)
        {
            List<ModelImage> images = new List<ModelImage>();
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            using (SqlCommand cmd = new SqlCommand("GetImages", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarId", carId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ModelImage mi = new ModelImage();
                        mi.ImagePath = reader["ImagePath"].ToString();
                        images.Add(mi);
                    }
                    reader.Close();
                }
            }
            con.Close();
            return images;
        }

        public static ModelImage GetFirstImage(string carId)
        {
            ModelImage firstImage = null;

            SqlConnection con = DBHelper.GetConnection();
            con.Open();

            using (SqlCommand cmd = new SqlCommand("GetImages", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarId", carId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        firstImage = new ModelImage();
                        firstImage.ImagePath = reader["ImagePath"].ToString();
                    }

                    reader.Close();
                }
            }

            con.Close();
            return firstImage;
        }



    }
}
