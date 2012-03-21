using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ValidationFields
/// </summary>
public class ValidationFields
{
	public ValidationFields()
	{
		
	}
    public bool phoneFormat(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, "^([0]{1}[1-9]{1}[0-9]{8})|([0]{1}[1-9]{1}[0-9]{9})$");
    }
}

public class MultiLineTextBoxColumnEditor : GridTextColumnEditor
{
    private TextBox textBox;
    protected override void LoadControlsFromContainer()
    {
        this.textBox = this.ContainerControl.Controls[0] as TextBox;
    }
    public override bool IsInitialized
    {
        get { return this.textBox != null; }
    }

    public override string Text
    {
        get { return this.textBox.Text; }
        set { this.textBox.Text = value; }
    }

    protected override void AddControlsToContainer()
    {
        this.textBox = new TextBox();
        this.textBox.TextMode = TextBoxMode.MultiLine;
        this.textBox.Rows = 4;
        this.textBox.Columns = 40;
        this.ContainerControl.Controls.Add(this.textBox);
    }
}