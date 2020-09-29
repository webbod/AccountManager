# SQL DataStore
This is where most of the complexity in the solution sits.


**SqlDataStoreConfigurationSettings** models a set of configuration settings that would be in an *appsettings.json* file.


#### Data transfer objects (DTO)
1. **Account** models an Account database record, all of the constructors are marked as internal - this is a DTO ought not be created outside of this assembly.

#### Services
1. **HashingService** [this has moved into AccountManager.Domain]
2. **QueryService** encapsulates all of the SQL connection/command object with a simple mapping method.

#### Repositories
1. **AccountRepository** acts as a facade to the Account data store.

#### Repositories \ Mappers
1. **AccountMapper** maps fields in a DataRecord in to an Account.

#### Repositories \ Queries
1. **Account_Find** Models a command to find an Account
2. **Account_Delete** Models a command to delete an Account
3. **Account_Insert** Models a command to insert an Account
4. **Account_Update** Models a command to update an Account
