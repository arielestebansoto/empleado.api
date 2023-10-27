using System.Globalization;


/*  
 * Cree estas clases para validar si una string contenia formato fecha
*/
static class Validaciones
{
    private static string[] formatosFecha = {"yyyy-mm-dd", "yyyy/mm/dd", "yyyy-mm-dd hh:mm:ss", "yyyy/mm/dd hh:mm:ss" };

    public static bool EsFecha(string fecha)
    {
        if(DateTime.TryParseExact(fecha, formatosFecha, new CultureInfo("es-AR"), DateTimeStyles.None, out _))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}