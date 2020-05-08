using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WebApplication.Services
{
    public class AuthMessageSender
    {
        public Task SendSmsAsync(string number, string message)
        {
        
            var accountSid = "AC1e438713a2701b0d47f0ed8af51bac87";
    
            var authToken = "e2d0bb0b780c93f45a088fc5d634e593";

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
                to: new PhoneNumber(number),
                from: new PhoneNumber("+15013563107"),
                body: message);
        }
    }
}
