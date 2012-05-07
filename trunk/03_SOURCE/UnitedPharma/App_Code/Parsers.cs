namespace ASP.App_Code
{
    public class Parsers
    {
         public static int ToInt(string value)
         {
             var result = -1;
             if(int.TryParse(value, out result))
             {
                 return result;
             }

             return -1;
         }
    }
}