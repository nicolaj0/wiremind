using System.Collections.Generic;

namespace ConsoleApplication2.Browser
{
    public class Headers : Dictionary<string,string>
    {
        public Headers()
        {
            Add( "Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            Add( "UserAgent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36");
            Add( "Accept-Language","fr-FR,fr;q=0.9,en-US;q=0.8,en;q=0.7,de;q=0.6");
            
        }
    }
}