using AccountManager.Interfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Data
{
    public class SqlDataStoreConfigurationSettings : ISetOfConfigrationSettings
    {
        public string ConnectionString { get; set; }
    }
}
