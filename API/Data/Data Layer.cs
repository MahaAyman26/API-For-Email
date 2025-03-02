using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Transactions;
using API.Model;

namespace API.Data
{
    public class Data_Layer
    {
        private readonly string _connectionstring;

        public Data_Layer( IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("conn");
        }


        public async Task<Account> GetAccountByUserName( string username)
        {
            Account account = new Account();
           using(SqlConnection con = new SqlConnection(_connectionstring))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_API_GetAccountByUserName", con))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    using (SqlDataReader dr =  await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())

                            return new Account
                            {
                                AccountID = Convert.ToInt64(dr["AccountID"]),
                              CustId = Convert.ToInt64(dr["CustId"]),
                               AccountName = dr["AccountName"].ToString(),
                               DisplayName = dr["DisplayName"].ToString(),
                                MailServerName = dr["MailServerName"].ToString(),
                               Port = Convert.ToInt32(dr["Port"]),
                                UserName = dr["UserName"].ToString(),
                                Password = dr["Password"].ToString(),
                            };
                    }
                }
            }
            return null;

        }


        public bool InsertIntoTableRedy(Account account ,Emailcontent emailcontent)
        {
             using (SqlConnection con = new SqlConnection(_connectionstring))
                    {
                con.Open();
                        using (var command = new SqlCommand("sp_InsertIntoTableRedy", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@AccountID", account.AccountID);
                            command.Parameters.AddWithValue("@CustID", account.CustId); 
                            command.Parameters.AddWithValue("@AccountName", account.AccountName);
                            command.Parameters.AddWithValue("@DisplayName", account.DisplayName);
                            command.Parameters.AddWithValue("@FromEmail", account.UserName);
                            command.Parameters.AddWithValue("@ToEmail", emailcontent.ToEmail);
                            command.Parameters.AddWithValue("@CcEmail", emailcontent.CcEmail);
                            command.Parameters.AddWithValue("@Subject", emailcontent.Subject);
                            command.Parameters.AddWithValue("@Body", emailcontent.Body);
                            command.Parameters.AddWithValue("@BodyFormat", emailcontent.BodyFormat);
                            command.Parameters.AddWithValue("@CampaignId", "");
                            command.Parameters.AddWithValue("@Importance", emailcontent.Importance);
                            command.Parameters.AddWithValue("@Attachment", emailcontent.Attachment);
                            command.ExecuteNonQuery();

                        }
                    
                    return true;
                }
            }
           

    }
}
