using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DalMakeList
    {
        public static List<ModelMake> GetAllMakes()
        {
            try
            {
                var modelMake = new DalDropDown();

                SqlConnection con = DBHelper.GetConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Make", con);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                List<ModelMake> listMakes = new List<ModelMake>();
                while (sqlDataReader.Read())
                {
                    ModelMake make = new ModelMake();
                    make.MakeId = sqlDataReader["MakeId"].ToString();
                    make.MakeName = sqlDataReader["MakeName"].ToString();

                    listMakes.Add(make);
                }
                con.Close();
                return listMakes;
            }
            catch
            {
                throw;
            }
        }

        public static List<ModelModel> GetModelsByMakeId(int makeId)
        {
            try
            {
                var modelModel = new DalDropDown();

                SqlConnection con = DBHelper.GetConnection();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Model WHERE MakeId = {makeId}", con);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                List<ModelModel> listModels = new List<ModelModel>();
                while (sqlDataReader.Read())
                {
                    ModelModel model = new ModelModel();
                    model.ModelId = sqlDataReader["ModelId"].ToString();
                    model.ModelName = sqlDataReader["ModelName"].ToString();

                    listModels.Add(model);
                }
                con.Close();
                return listModels;
            }
            catch
            {
                throw;
            }
        }

        public static List<ModelVariant> GetVariantsByModelId(int modelId)
        {
            try
            {
                var modelVariant = new DalDropDown();

                SqlConnection con = DBHelper.GetConnection();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Variant WHERE ModelId = {modelId}", con);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                List<ModelVariant> listVariants = new List<ModelVariant>();
                while (sqlDataReader.Read())
                {
                    ModelVariant variant = new ModelVariant();
                    variant.VariantId = sqlDataReader["VariantId"].ToString();
                    variant.VariantName = sqlDataReader["VariantName"].ToString();

                    listVariants.Add(variant);
                }
                con.Close();
                return listVariants;
            }
            catch
            {
                throw;
            }
        }


    }
}
