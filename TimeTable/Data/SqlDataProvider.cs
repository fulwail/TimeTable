using TimeTable.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Data
{
    public class SqlDataProvider : ISqlDataProvider
    {
        private ISqlContext _sqlContext;
        public ISqlContext SqlContext
        {
            get { return _sqlContext ?? (_sqlContext = new SqlContext()); }
        }

        public IDisposable CreateContext()
        {
            return new SqlContext();
        }
    }
}