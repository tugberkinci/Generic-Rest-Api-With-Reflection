namespace GenericApi.IServices
{
    public interface IGenericApiService
    {
        //string GimmeAdvice(string issue);
        string GenericPicker(Dictionary<string, string> data);
    }
}
