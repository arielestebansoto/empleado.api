static public class Conversiones
{
    public static string DateTimeNulleableToStringDateOnly(DateTime? dateTime)
    {
        var str = dateTime.ToString();
        if (str == null)
        {
            return "";
        }

        string[] tmp = str.Split(' ');

        if(!tmp[0].Equals("1/1/0001"))
        {
            return tmp[0];
        }  
        else
        {
            return "";
        }
    }
}