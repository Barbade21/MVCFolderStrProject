using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace MVCFolderStrProjectLibrary
{
    public class BALUser
    {

        SqlConnection con = new SqlConnection("Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=UserManagement;Integrated Security=True");
       
        public async Task  AddUser(User obj)
        {

            Dictionary<string, string> param = new Dictionary<string, string>();            
            param.Add("@flag", "RegisterUser");
            param.Add("@UserName", obj.UserName);
            param.Add("@Gender", obj.Gender);
            param.Add("@Address", obj.Address);
            param.Add("@CityId", obj.CityId.ToString());
            MSSQL DBHelper = new MSSQL();
            await DBHelper.ExecuteStoreProcedure("UserMVCRegi", param);

            //con.Open();
            //SqlCommand cmd = new SqlCommand("UserMVCRegi", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@flag", "RegisterUser");
            ////  cmd.Parameters.AddWithValue("@UserType", obj.UserId);
            //cmd.Parameters.AddWithValue("@UserName", obj.UserName);
            //cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            //cmd.Parameters.AddWithValue("@Address", obj.Address);
            ////  cmd.Parameters.AddWithValue("@UserState", obj.StateId);
            //cmd.Parameters.AddWithValue("@CityId", obj.CityId);
            //cmd.ExecuteNonQuery();
            //con.Close();
        }

        public async Task<DataSet> Countries()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@flag", "Countries");
            MSSQL DBHelper = new MSSQL();
            DataSet ds = new DataSet();
            ds=await DBHelper.ExecuteStoreProcedureReturnDS("UserMVCRegi", param);
            return ds;
        }

        public async Task<DataSet> States(int CountryId)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@flag", "States");
            param.Add("@CountryId", CountryId.ToString());
            MSSQL DBHelper = new MSSQL();
            DataSet ds = new DataSet();
            ds=await DBHelper.ExecuteStoreProcedureReturnDS("UserMVCRegi", param);
            return ds;
        }

        public async Task<DataSet> Cities(int StateId)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@flag", "Cities");
            param.Add("@StateId", StateId.ToString());
            MSSQL DBHelper = new MSSQL();
            DataSet ds = new DataSet();
            ds=await DBHelper.ExecuteStoreProcedureReturnDS("UserMVCRegi", param);
            return ds;

            //con.Open();
            //SqlCommand cmd = new SqlCommand("UserMVCRegi", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@flag", "Cities");
            //cmd.Parameters.AddWithValue("@StateId", StateId);
            //DataSet ds = new DataSet();
            //SqlDataAdapter adpt = new SqlDataAdapter();
            //adpt.SelectCommand = cmd;
            //adpt.Fill(ds);
            //return ds;
            //con.Close();
        }
        /// <summary>
        /// This List Gives Details of All User in List
        /// </summary>
        /// <returns></returns>


        public async Task<List<User>> GetAllUsers()
        {

            Dictionary<string, string> param = new Dictionary<string, string>();
            var users = new List<User>();
            param.Add("@flag", "GetAllUsers");
            MSSQL DBHelper = new MSSQL();
           
            using (SqlDataReader reader = await DBHelper.ExecuteStoreProcedureReturnDataReader("UserMVCRegi", param))
            {
                while (await reader.ReadAsync())
                {
                    User user = new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Address = reader["Address"].ToString(),
                        CountryName = reader["CountryName"].ToString(),
                        StateName = reader["StateIName"].ToString(),
                        CityName = reader["CityName"].ToString(),
                       
                    };
                    users.Add(user);
                }
            }
        
            return users;
        }

        public async Task DeleteUser(int? id)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@flag", "DeleteUser");
            param.Add("@UserId", id.ToString());
            MSSQL DBHelper = new MSSQL();
            await DBHelper.ExecuteStoreProcedure("UserMVCRegi", param);       

        }

        public async Task<User> GetUserById(int userId)
        {
            User user = null;

            Dictionary<string, string> param = new Dictionary<string, string>();
            var users = new List<User>();
            param.Add("@flag", "ShowUserDetails");
            param.Add("@UserId", userId.ToString());
            MSSQL DBHelper = new MSSQL();

            using (SqlDataReader reader = await DBHelper.ExecuteStoreProcedureReturnDataReader("UserMVCRegi", param))
            {
                if (await reader.ReadAsync())
                {            
                    user = new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Address = reader["Address"].ToString(),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        CountryName = reader["CountryName"].ToString(),
                        StateId = Convert.ToInt32(reader["StateId"]),
                        StateName = reader["StateIName"].ToString(),
                        CityId = Convert.ToInt32(reader["CityId"]),
                        CityName = reader["CityName"].ToString(),


                    };
                }
            }            
            return user;
        }


        public async Task EditUser(User obj)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@flag", "EditUser");
            param.Add("@UserId", obj.UserId.ToString());
            param.Add("@UserName", obj.UserName);
            param.Add("@Gender", obj.Gender);
            param.Add("@Address", obj.Address);
            param.Add("@CityId", obj.CityId.ToString());
            MSSQL DBHelper = new MSSQL();
            await DBHelper.ExecuteStoreProcedure("UserMVCRegi", param);

         
        }

       
    }

}
