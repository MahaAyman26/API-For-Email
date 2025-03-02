using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/EmailAPI")]
    [ApiController]
    public class EmailAPIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Data_Layer _dataLayer;
        public EmailAPIController(IConfiguration configuration, Data_Layer data)
        {
            _configuration = configuration;
            _dataLayer = data;
        }




        [Route("Get Account by UserName")]
        [HttpGet]

        public async Task<IActionResult> GetAccountByAccountUserName(string UserName)
        {
            var account = await _dataLayer.GetAccountByUserName(UserName);

            if (account == null)
            {
                return NotFound(new { Message = "Account Not Found" });
            }
            return Ok(account);

        }


        [Route("EmailBody")]

        [HttpPost]
        public async Task<IActionResult> SendMessgeBody([FromBody] Emailcontent email)
        {

            if (email == null)
                return BadRequest(new { Message = "Invalid Req" });

            var account = await _dataLayer.GetAccountByUserName(email.FromEmail);
            if (account == null)
            {
                return StatusCode(500, new { Message = "Account Not Found " });
            }

            else
            {
                bool isInsert = _dataLayer.InsertIntoTableRedy(account, email);

                if (isInsert)
                {
                    return Ok(new { Message = "Sucess Insert" });
                }
                else
                {
                    return StatusCode(500, new { Message = "error in Insert " });
                }
            }
        }



        [Route("SentEmailWithAttch")]
        [HttpPost]

        public async Task<IActionResult> SendEmailWithAttchment([FromForm] Emailcontent email, IFormFile? attachment)
        {
            try
            {
                if (email == null)
                    return BadRequest(new { Message = "Invalid Req: Email content is missing" });


                if (string.IsNullOrEmpty(email.FromEmail) || string.IsNullOrEmpty(email.ToEmail))
                {
                    return BadRequest(new { Message = "Invalid Req: empty From or empty To " });

                }

                var account = await _dataLayer.GetAccountByUserName(email.FromEmail);
                if (account == null)
                {
                    return StatusCode(404, new { Message = "Account Not Found " });
                }

                if (attachment != null)
                {
                    email.Attachment = new UploadHandler().Uploud(attachment);
                }
                else
                {
                    email.Attachment = "";
                }

                bool isInsert = _dataLayer.InsertIntoTableRedy(account, email);

                if (isInsert)
                {
                    return Ok(new { Message = "Sucess Insert" });
                }
                else
                {
                    return StatusCode(500, new { Message = "error in Insert " });
                }
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message} ");

            }





        }
    }
}
