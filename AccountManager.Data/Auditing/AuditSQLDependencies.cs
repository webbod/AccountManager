using AccountManager.Interfaces.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AccountManager.Data.Auditing
{
    public class AuditSQLDependencies
    {
        public struct SQLAudit
        {
            public string Name { get; set; }
            public string SQLElement { get; set; }
            public DatabaseDependency.Element SQLElementType { get; set; }
            public DatabaseDependency.MaintainenceRisk Risk {get; set;}
        }

        public static List<SQLAudit> FindDependentMethods()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var methods = assembly.GetTypes()
                      .SelectMany(t => t.GetMethods())
                      .Where(m => m.GetCustomAttributes(typeof(DatabaseDependency), false).Length > 0)
                      .ToList();

            var output = new List<SQLAudit>();

            foreach(var method in methods)
            {
                var customAttribute = (DatabaseDependency)method.GetCustomAttributes(typeof(DatabaseDependency), false).FirstOrDefault();
                output.Add(new SQLAudit { 
                    Name = method.ReflectedType.FullName, 
                    SQLElement = customAttribute.Name, 
                    SQLElementType = customAttribute.Dependency, 
                    Risk = customAttribute.Risk 
                });
            }

            return output;
        }
    }
}
