using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


//teszt indítás: MatekService jobb klikk -> Debug -> start new instance
//MatekService.csproj-ban lehet átírni a portot!
//Megjelenik html-be, kattintsunk rá a WebService1.asmx-re és lehet tesztelni az itt implementált metódusokat!
//"szolgáltatás leírásában" katt után látható a kreált wsdl!

namespace MatekService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public int Add(int A, int B)
        {
            return A + B;
        }

        [WebMethod]
        public int Sub(int A, int B)
        {
            return A - B;
        }

        [WebMethod]
        public int Multiple(int A, int B)
        {
            return A * B;
        }

        [WebMethod] //ha ez nincs itt, akkor távolról nem lehet meghívni!
        public float Divi(int A, int B)
        {
            float res = 0.0f;
            try
            {
                res = (float)A / B;
            }
            catch (Exception e)
            {
                res = 0;
            }

            return res;
        }
    }
}
