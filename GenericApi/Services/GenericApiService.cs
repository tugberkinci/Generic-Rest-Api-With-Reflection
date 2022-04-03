using GenericApi.IServices;
using System.Reflection;

namespace GenericApi.Services
{

    public class GenericModel
    {
        public string? Command { get; set; }
        public string? Name { get; set; }
        public string? Issue { get; set; }
    }

    public static class ObjectExtensions
    {

        public static T ToObject<T>(this IDictionary<string, string> source, string? caseOption)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
               
                if (String.IsNullOrEmpty(caseOption))
                {
                    if (someObjectType.GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                        someObjectType.GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(someObject, item.Value, null);
                }
                else if (String.Equals(caseOption.ToUpperInvariant(), "Upper".ToUpperInvariant()))
                {
                    if (someObjectType.GetProperty(item.Key.ToUpperInvariant()) != null)
                        someObjectType.GetProperty(item.Key.ToUpperInvariant()).SetValue(someObject, item.Value, null);
                }
                else if (String.Equals(caseOption.ToUpperInvariant(), "Lower".ToUpperInvariant()))
                {
                    if (someObjectType.GetProperty(item.Key.ToLowerInvariant()) != null)
                        someObjectType.GetProperty(item.Key.ToLowerInvariant()).SetValue(someObject, item.Value, null);
                }
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

       
    }
    public class GenericApiService : IGenericApiService
    {

        private static string name;

        public GenericApiService() { }

        public string SayMyName(string name)
        {
            GenericApiService.name= name;  
            return "Your name is \t \""+GenericApiService.name+"\""+ $".Now you can use \"GimmeAdvice\" command with \"issue\" parameter so I can help you about decision. ";
        }

        public string GimmeAdvice(string issue)
        {
            Random random = new Random();
            int rand= random.Next(0,2);
            if (string.IsNullOrEmpty(GenericApiService.name))
                return $"Sorry mate, I can't help you abaout this topic without knowing your name. User \"SayMyName\" command with \"name\" parameter before use this command.";
            if (rand == 0)
                return "You should not do this. It's better for you ";
            else if (rand == 1)
                return "Do it!! I belive you can handle it.";

            return "I didn't get you :)";
        }

        public string GenericPicker(Dictionary<string,string> data)
        {
            var model=data.ToObject<GenericModel>(null);

            if (String.IsNullOrEmpty(model.Command))
                return "Command does not exist. Use \"command\" parameter";
            else if (String.Equals(model.Command, "SayMyName", StringComparison.InvariantCultureIgnoreCase))
            {
                if (String.IsNullOrEmpty(model.Name))
                    return "You should tell me your name with \"name\" parameter";

                return SayMyName(model.Name);
            }
            else if(String.Equals(model.Command,"GimmeAdvice",StringComparison.InvariantCultureIgnoreCase))
            {
                if (String.IsNullOrEmpty(model.Issue))
                    return "You should tell me your problem with \"issue\" parameter";

                return GimmeAdvice(model.Issue);
            }
            else 
                return "Command does not exist.Available commands are \"SayMyName\" and \"GimmeAdvice\" ";



        }
    }
}
