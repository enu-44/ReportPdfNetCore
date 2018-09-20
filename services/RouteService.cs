namespace pmacore_api.services
{
    public class RouteService
    {
          //Singleton permite crear una instancia de la clase
        #region Singleton

        static RouteService instance;

        public static RouteService GetInstance()
        {
            if (instance == null)
            {
                instance = new RouteService();
            }
            return instance;
        }

        #endregion

        #region Properties
        public string RouteBaseAddress { get; set; }
        #endregion

        #region Constructor
        public RouteService()
        {
            //Singleton instance
            instance = this;
            RouteBaseAddress = "http://54.172.128.205/";
        }
        #endregion
    }
}