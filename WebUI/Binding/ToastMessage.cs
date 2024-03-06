using System.Reflection;
using WebUI.Utils;

namespace WebUI.Binding;

public class ToastMessage
{
    public string? Title { get; set; }
    public string? Message { get; set; }
    public ToastType ToastType { get; set; }
    public string ToastTypeString
    {
        get
        {
            return ToastType.GetStringValue() ?? "body";
        }
    }
}

public enum ToastType
{
    [StringValue("body")]
    None,
    [StringValue("success")]
    Success,
    [StringValue("danger")]
    Error
}

public class StringValueAttribute : Attribute
{

    #region Properties

    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string StringValue { get; protected set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public StringValueAttribute(string value)
    {
        this.StringValue = value;
    }

    #endregion
}