using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Interfaces.Attributes
{
    /// <summary>
    /// Allows you to mark-up and assess dependencies on the database structure as part of a code review
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false, AllowMultiple = true)]
    public sealed class DatabaseDependency : Attribute
    {
        public enum Element
        {
            Table,
            StoredProcedure,
            Function,
            Column
        }

        [Flags]
        public enum MaintainenceRisk
        {
            NothingMuch,
            ParameterDrift,
            RawSQLStatement,
            ComplexLogic,
            CrossTableDependency,
            MissingTransaction
        }

        public string Name
        {
            get; private set;
        }
        
        public Element Dependency
        {
            get; private set;
        }

        public MaintainenceRisk Risk 
        {
            get; private set;
        }

        public string Notes
        {
            get; private set;
        }

        // This is a positional argument
        public DatabaseDependency(string name, Element dependency, MaintainenceRisk risk = MaintainenceRisk.NothingMuch, string notes = null)
        {
            Name = name;
            Dependency = dependency;
            Risk = risk;
            Notes = notes;
        }
    }
}
