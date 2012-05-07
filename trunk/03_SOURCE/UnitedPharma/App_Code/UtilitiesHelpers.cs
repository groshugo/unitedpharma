using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Web.UI;

/// <summary>
/// Summary description for Helpers
/// </summary>
public class UtilitiesHelpers
{
    // singleton
    private static UtilitiesHelpers instance; 

    private UtilitiesHelpers() {}

    public static UtilitiesHelpers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UtilitiesHelpers();
            }
            return instance;
        }
    }

    public int GetSelectedValueOfCombo(RadComboBox combo)
    {
        if (combo.Items.Count > 0 && combo.SelectedIndex > 0)
        {
            return int.Parse(combo.SelectedValue);

        }
        return 0;
    }

    public void ClearComboData(RadComboBox combo)
    {
        if(combo != null && combo.Items != null)
        {
            combo.Items.Clear();
        }
    }

    public bool IsRepRole(int roleId)
    {
        if (roleId == (int)SalesmenRole.TPR || roleId == (int)SalesmenRole.PSR1 || roleId == (int)SalesmenRole.PSR2)
            return true;

        return false;
    }

    /// <summary>
    /// right format: UPI Phone_Number Message 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public  string[] GetRightFormatOfMessage(string message)
    {
        if (message == null) throw new ArgumentNullException("message");

        if (message.Length < 14) return null;

        var results = new string[3];

        results[0] = message.Substring(0, message.IndexOf(' '));
        if (results[0].ToLower() != "upi") return null;

        message = message.Substring(message.IndexOf(' ')).Trim();
        results[1] = message.Substring(0, message.IndexOf(' '));
        var regex = new Regex(@"^\d+$");
        if (!regex.IsMatch(results[1])) return null;

        results[2] = message.Substring(message.IndexOf(' ')).Trim();

        return results;
    }
}