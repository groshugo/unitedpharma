using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

    public string GetGroupBySalemenId(int salemenId)
    {
        var u = new Utility();
        string sqlGroup = "select GroupId from salesgroup where SalesmenId = " + salemenId + " group by GroupId";
        var dt = u.GetList(sqlGroup);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }

    public string SalesRegionList(string strGroupIdList, int salemenId)
    {
        if (!string.IsNullOrEmpty(strGroupIdList))
        {
            var u = new Utility();
            string SqlRegion = "select Id from Region where GroupId in (" + strGroupIdList + ") group by Id";
            var dt = u.GetList(SqlRegion);
            string result = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    result += dt.Rows[i][0] + ",";
            }
            string sqlRegionId = GetRegionBySalemenId(salemenId);
            if (sqlRegionId != "")
                result += sqlRegionId;

            return result == "" ? result : result.Substring(0, result.Length - 1);
        }
        return string.Empty;
    }

    public string SalesAreaList(string strRegionIdList, int salemenId)
    {
        var u = new Utility();
        string SqlRegion = "select Id from Area where RegionId in (" + strRegionIdList + ") group by Id";
        var dt = u.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                result += dt.Rows[i][0] + ",";
        }
        string sqlRegionId = GetAreaBySalemenId(salemenId);
        if (sqlRegionId != "")
            result += sqlRegionId;

        return result == "" ? result : result.Substring(0, result.Length - 1);
    }
    
    public string SalesLocalList(string strAreaIdList, int salemenId)
    {
        var u = new Utility();
        string SqlRegion = "select Id from Local where AreaId in (" + strAreaIdList + ") group by Id";
        var dt = u.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                result += dt.Rows[i][0] + ",";
        }
        string sqlRegionId = GetLocalBySalemenId(salemenId);
        if (sqlRegionId != "")
            result += sqlRegionId;

        return result == "" ? result : result.Substring(0, result.Length - 1);
    }

    

    public List<int> GetLocalIdsStringByGroupId(int groupId)
    {
        var u = new Utility();
        var sql = string.Format("select Id from Local where AreaId in (select Id from Area where RegionId in (select Id from Region where GroupId = {0}))", groupId);
        var dt = u.GetList(sql);

        return GetListByDatatableResult(dt);
    }

    public List<int> GetLocalIdsStringByRegionId(int regionId)
    {
        var u = new Utility();
        var sql = string.Format("select Id from Local where AreaId in (select Id from Area where RegionId = {0})", regionId);
        var dt = u.GetList(sql);

        return GetListByDatatableResult(dt);
    }

    public List<int> GetLocalIdsStringByAreaId(int areaId)
    {
        var u = new Utility();
        var sql = string.Format("select Id from Local where AreaId in (select Id from Area where RegionId = {0})", areaId);
        var dt = u.GetList(sql);

        return GetListByDatatableResult(dt);
    }

    #region private

    private static List<int> GetListByDatatableResult(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0) return new List<int>();

        var result = new List<int>();
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            result.Add(int.Parse(dt.Rows[i][0].ToString()));
        }

        return result;
    }

    private string GetRegionBySalemenId(int salemenId)
    {
        var u = new Utility();
        string SqlRegion = "select RegionId from SalesRegion where salesmenId = " + salemenId + " group by RegionId";
        var dt = u.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }

        return result;
    }

    private string GetAreaBySalemenId(int salemenId)
    {
        var u = new Utility();
        string SqlRegion = "select AreaId from SalesArea where salesmenId = " + salemenId + " group by AreaId";
        var dt = u.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }

        return result;
    }

    private string GetLocalBySalemenId(int salemenId)
    {
        var u = new Utility();
        string SqlRegion = "select LocalId from SalesLocal where salesmenId = " + salemenId + " group by LocalId";
        var dt = u.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }

        return result;
    }

    #endregion
}