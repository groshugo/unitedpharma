using System;
using System.Configuration;
using System.Text;
using Resources;

public partial class SMS_SMSReceiving : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var message = Request.QueryString["message"] ?? string.Empty;
            var phone = Request.QueryString["phone"] ?? string.Empty;
            var service = Request.QueryString["service"] ?? string.Empty;
            var port = Request.QueryString["port"] ?? string.Empty;
            var main = Request.QueryString["main"] ?? string.Empty;
            var sub = Request.QueryString["sub"] ?? string.Empty;

            var smsId = Guid.NewGuid().ToString();

            var systemSmsPort = ConfigurationManager.AppSettings["SMSPort"] ?? string.Empty;

            if (!string.IsNullOrEmpty(port) &&
                string.Compare(port, systemSmsPort, StringComparison.OrdinalIgnoreCase) == 0)
            {
                // validate the format of Message
                var smsMessage = UtilitiesHelpers.Instance.GetRightFormatOfMessage(message);
                if (smsMessage == null)
                {
                    PrintMessage(phone, Pharma.SMS_SMSReceiving_Invalid_sms_structure, smsId, service);
                }
                else
                {
                    // write to DB
                    var smsRepo = new SMSObjRepository();
                    smsRepo.InsertSMS(smsId, -1, phone, 1, smsMessage[1], -1, DateTime.Now, string.Empty, smsMessage[2],
                                      true, false, false, 1, -1);

                    // print result to screen
                    PrintMessage(phone, Pharma.SMS_SMSReceiving_Thank_For_Using, smsId, service);
                }
            }
            else
            {
                PrintMessage(phone, Pharma.SMS_SMSReceiving_Wrong_Root_Number, smsId, service);
            }
        }
    }

    private void PrintMessage(string phone, string message, string smsId, string service)
    {
        var resultString = new StringBuilder();
        resultString.AppendFormat("<ClientResponse><Message><PhoneNumber>{0}</PhoneNumber>", phone);
        resultString.AppendFormat("<Message>{0}</Message><SMSID>{1}</SMSID>", message, smsId);
        resultString.AppendFormat("<ServiceNo>{0}</ServiceNo></Message></ClientResponse>", service);
        Response.Write(resultString.ToString());
    }
}
