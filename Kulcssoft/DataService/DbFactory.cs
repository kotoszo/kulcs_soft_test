using System;

namespace DataService
{
    public static class DbFactory
    {
        public static DbService GetService()
        {
            DbService service;
            try
            {
               service = new DbService(new MsSql());
            }
            catch (Exception)
            {
                service = new DbService(new MemDb());
            }
            return service;

        }
    }
}
