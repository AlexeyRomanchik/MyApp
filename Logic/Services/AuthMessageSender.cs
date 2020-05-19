using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Logic.Services
{
    public class AuthMessageSender
    {
        public Task SendSmsAsync(string number, string message)
        {
        
            var accountSid = "AC1e438713a2701b0d47f0ed8af51bac87";
    
            var authToken = "b7d9e642d0f8a99f977b258a03ee1a66";

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
                to: new PhoneNumber(number),
                @from: new PhoneNumber("+15013563107"),
                body: message);
        }
    }
}
