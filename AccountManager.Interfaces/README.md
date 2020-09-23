# Shared Interfaces
The project isn't particularly complex:

#### DataStore
1. **IDataMapper** describes a way to map from a dataRecord to an object, this is in lieu of using an automapper and is a consequence of not being able to use an ORM.
2. **ISetofConfigurationOptions** describes settings to configure a data store.

#### Accounts
1. **IAccount** describes an Account
2. **IAccountCredentials** describes a set of insecure account credentials.
3. **IAccountFinder** describes a mechanism for finding stored Accounts.
4. **IAccountUpdater** describes a mechanism for updating stored Accounts.
