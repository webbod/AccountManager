namespace AccountManager.UnitTests.Mocks.DataRecords
{
    public class MockAccountRecord : AMockDataRecord
    {
        public MockAccountRecord(int id, string emailAddress, string hashedPassword)
        {
            // the keys need to match the field names returned by the query
            SetValue("Id", id);
            SetValue("EmailAddress", emailAddress);
            SetValue("HashedPassword", hashedPassword);
        }
    }
}
