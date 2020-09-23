using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AccountManager.UnitTests.Mocks.DataRecords
{
    public abstract class AMockDataRecord : IDataRecord
    {
        protected Dictionary<string, object> __Data = new Dictionary<string, object>();

        public void SetValue(string key, object value)
        {
            __Data.Add(key, value);
        }

        public virtual object this[int i] => __Data[__Data.Keys.ElementAt(i)];

        public virtual T Field<T>(int i) => (T)__Data[__Data.Keys.ElementAt(i)];

        public virtual object this[string name] => __Data[name];

        public virtual T Field<T>(string name) => (T)__Data[name];

        public virtual int FieldCount => __Data.Keys.Count;

        public virtual bool GetBoolean(int i)
        {
            return Field<bool>(i);
        }

        public virtual byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public virtual char GetChar(int i)
        {
            return Field<char>(i);
        }

        public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public virtual IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public virtual string GetDataTypeName(int i)
        {
            return Field<object>(i)?.GetType()?.Name;
        }

        public virtual DateTime GetDateTime(int i)
        {
            return Field<DateTime>(i);
        }

        public virtual decimal GetDecimal(int i)
        {
            return Field<decimal>(i);
        }

        public virtual double GetDouble(int i)
        {
            return Field<double>(i);
        }

        public virtual Type GetFieldType(int i)
        {
            return Field<object>(i)?.GetType();
        }

        public virtual float GetFloat(int i)
        {
            return Field<float>(i);
        }

        public virtual Guid GetGuid(int i)
        {
            return Guid.Parse(Field<string>(i));
        }

        public virtual short GetInt16(int i)
        {
            return Field<short>(i);
        }

        public virtual int GetInt32(int i)
        {
            return Field<int>(i);
        }

        public virtual long GetInt64(int i)
        {
            return Field<long>(i);
        }

        public virtual string GetName(int i)
        {
            return __Data.Keys.ElementAt(i);
        }

        public virtual int GetOrdinal(string name)
        {
            return Field<int>(name);
        }

        public virtual string GetString(int i)
        {
            return Field<string>(i);
        }

        public virtual object GetValue(int i)
        {
            return Field<object>(i);
        }

        public virtual int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsDBNull(int i)
        {
            return Field<object>(i) == null;
        }
    }
}
