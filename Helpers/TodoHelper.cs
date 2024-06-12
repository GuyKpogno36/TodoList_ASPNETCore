using Newtonsoft.Json;

namespace Todo_List_ASPNETCore.Helpers
{
    public class TodoHelper<T>
    {
        public static T DecryptageData(string Chaine)
        {
            Chaine = "{ " + Chaine.Replace("%", ",").Replace("*", "\'").Replace("$", ":").Replace("#", "\\\'") + "}".Replace("~", "<").Replace("§", ">").Replace("!", "/");
            //.replace(/</g, '~').replace(/>/g, '§').replace('/', '!')
            T model = (T) JsonConvert.DeserializeObject(Chaine);
            
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //T model = js.Deserialize<T>(Chaine);
            return model;
        }
    }
}
