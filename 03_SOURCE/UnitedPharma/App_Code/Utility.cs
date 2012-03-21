using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Serialization;
using System.Linq.Expressions;

public class Utility
{
	public Utility()
	{
		
	}

    public static void SetCurrentMenu(string currentItem)
    {
        HttpContext.Current.Session["currentMenu"] = currentItem;
    }

    public static string EncodeString(string input)
    {
        if (input == null || input == "")
        {
            return "";
        }
        try
        {
            Byte[] Bytes = System.Text.Encoding.ASCII.GetBytes(input);
            return System.Convert.ToBase64String(Bytes);
        }
        catch
        {
        }
        return "";
    }

    public static string DecodeString(string input)
    {
        if (input == null || input == "")
        {
            return "";
        }
        try
        {
            Byte[] Bytes = System.Convert.FromBase64String(input);
            return System.Text.Encoding.ASCII.GetString(Bytes);
        }
        catch
        {
        }
        return "";
    }

    public static string GetDefaultText(int? Id, string value)
    {
        if (Id == null)
        {
            return "Undefined";
        }
        else
        {
            return value;
        }
    }

    public static string GenerateSMSCode()
    {
       return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "");
    }

    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UPIConnectionString"].ConnectionString);
    public DataTable GetList(string sql)
    {        
        DataTable ds = new DataTable();
        connection.Open();
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();
        connection.Close();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return ds;
    }
    public void SetList(string sql)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public static DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

    public static double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        TimeSpan diff = date - origin;
        return Math.Floor(diff.TotalSeconds);
    }

    public static string CreateRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }  
    
}