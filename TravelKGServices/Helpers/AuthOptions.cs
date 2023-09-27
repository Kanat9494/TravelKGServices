namespace TravelKGServices.Helpers;

public class AuthOptions
{
    public const string ISSUER = "http://localhost"; // издатель токена
    public const string AUDIENCE = "http://localhost"; // потребитель токена
    const string KEY = "ASDasdE2foEFA2408dieEClkeOIUer234mbnJHOIUO8909MmnjoiuweOIJK2890KOIkjeio82dfaeaq41faieLm";   // ключ для шифрации
    public const int LIFE_TIME = 30; // время жизни токена - 1 минута

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
