using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Interfaces.DataStore
{
    public interface IInitialisable
    {
        void Initalise(ISetOfConfigrationSettings options);
    }
}
