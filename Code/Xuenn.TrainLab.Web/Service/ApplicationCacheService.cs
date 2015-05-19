using System.Web.Caching;

namespace Xuenn.TrainLab.Web.Service
{
    public class ApplicationCacheService
    {
        public static void Initialize()
        {
            LoadDefaultMssqlConnection();
        }

        #region Private

        private static void LoadDefaultMssqlConnection()
        {
            //Cache["MSSQLDB"]=
        }

        #endregion
    }
}