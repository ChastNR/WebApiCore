namespace UniversalWebApi.FilterModels
{
    public class ActionResultModel
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IpAddress { get; set; }
        public int StatusCode { get; set; }
        public string Result { get; set; }
    }
}