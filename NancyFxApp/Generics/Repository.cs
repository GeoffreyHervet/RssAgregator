using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco.Internal;

namespace NancyFxApp.Generics
{
    public class Repository
    {
        internal DatabaseType _dbType;
        private PetaPoco.Database _databaseConnexion;
        private static List<KeyValuePair<string, Repository>> _singletonInstances = new List<KeyValuePair<string, Repository>>();
        
        private Repository(string cnxName)
        {
            this._databaseConnexion = new PetaPoco.Database(cnxName);
            this._dbType = DatabaseType.Resolve(this._databaseConnexion.GetType().Name, "System.Data.SqlClient");
        }

        public static Repository getInstance()
        {
            return Repository.getInstance("AzureConnexion");
        }

        public static Repository getInstance(string name)
        {
            foreach(KeyValuePair<string, Repository> kvp in _singletonInstances)
            {
                if (kvp.Key.Equals(name))
                {
                    return kvp.Value;
                }
            }

            var repo = new Repository(name);
            _singletonInstances.Add(new KeyValuePair<string,Repository>(name, repo));

            return repo;
        }

        public T find<T>(int id, string identifier = "id")
        {
            return this._databaseConnexion.SingleOrDefault<T>("WHERE " + identifier + "=@0", id);
        }
        public int delete<T>(int id, string identifier = "id")
        {
            return this._databaseConnexion.Delete<T>("WHERE " + identifier + "=@0", id);
        }

        public PetaPoco.Page<T> findAll<T>(int page, int pageSize = 20)
        {
            var pd = PocoData.ForType(typeof(T));
            var str = String.Format("SELECT * FROM {0}", _dbType.EscapeTableName(pd.TableInfo.TableName).Replace('[', '`').Replace(']', '`'));
            Console.WriteLine(str);
            return this._databaseConnexion.Page<T>(page, pageSize, str);
        }
        
        public Object insert(Object model)
        {
            return this._databaseConnexion.Insert(model);
        }

        public Object update(Object model)
        {
            return this._databaseConnexion.Update(model);
        }
    }
}