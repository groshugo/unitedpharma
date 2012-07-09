using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Constant
/// </summary>
public class Constant
{
    public static string UNDEFINE = "N/A";
    public static List<string> GroupRole = new List<string>(new string[] { "POC", "POS", "TDS" });
    public static string RegionRole = "ROM";
    public static string AreaRole = "SUP";
    public static string LocalRole = "REP";    

    public static string ADM_MANAGEMENT_PAGE = "AdministratorPanel.aspx";

    public static string MSG_CUSTOMER_PASSWORD = "Your password is: ";

    public static int AdminType = 0;
    public static int SalemenType = 1;
    public static int CustomerType = 2;

    public static int inbox = 1;
    public static int outbox = 2;
    public static int failure = 3;
    public static int deleted = 4;

    public static string PromotionHistoryTemplateString = @"<?xml version=""1.0""?><PromotionHistory><Type>{0}</Type><FullName>{1}</FullName><CustomerType>{2}</CustomerType><Channel>{3}</Channel><Group>{4}</Group><Region>{5}</Region><Area>{6}</Area><Local>{7}</Local></PromotionHistory>";

    public const string PromotionHistorySessionName = "PromotionHistorySessionName";
    public const string UpdateStatusSessionName = "UpdateStatusSessionName";
}