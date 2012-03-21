using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using UpiSmsService;
using System.Xml;

/// <summary>
/// Summary description for SmsHandler
/// </summary>
public class SmsHandler
{
    public static string clientNo; 
    public static string clientPass; 
    public static int serviceType; 
    public ServiceSoapClient upiSrv; 
	public SmsHandler()
	{	
	    clientNo = ConfigurationManager.AppSettings["ClientNo"];
        clientPass = ConfigurationManager.AppSettings["ClientPass"];
        serviceType = Convert.ToInt32(ConfigurationManager.AppSettings["ServiceType"]);
        upiSrv = new ServiceSoapClient("ServiceSoap");
	} 

    public void SendSMS(string Phone, string Content)
    {
        //string rs = upiSrv.SendMaskedSMS(clientNo, clientPass, "UPI", Phone, Content, Guid.NewGuid().ToString(), serviceType);
    }

    
}