using System.Data;

namespace DemoCustomerSvc.CoreLogic
{
    public static class CoreSvc
    {
        public static List<T> GetList<T>(IDataReader reader)
        {
            List<T> listObject = new List<T>();

            while (reader.Read())
            {
                var typ = typeof(T);
                T obj = (T)Activator.CreateInstance(typ);
                foreach (var prop in typ.GetProperties())
                {
                    var proptype = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(),proptype), null);
                }
                listObject.Add(obj);
            }

            return listObject;
        }

    }
}
