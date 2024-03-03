using Newtonsoft.Json;

namespace WebUI.Utils;

public static class SessionUtils
{
    public static string LOGGED_IN_USER_KEY = "loggedInUser";

    public static bool IsUserLoggedIn(this ISession session)
    {
        var loggedInUsers = session.GetString(LOGGED_IN_USER_KEY);
        return !string.IsNullOrEmpty(loggedInUsers);
    }

    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }

}
