using System;
using System.Collections.Generic;
using System.Linq;
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
}