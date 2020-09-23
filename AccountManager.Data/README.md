# SQL DataStore
This is where most of the complexity in the solution sits.

#### Data transfer objects (DTO)
1. **Account** models an Account database record.

#### Services
1. **HashingService** a simple alternative to MD5 that generates deterministic garbage - feel free to customise the prime numbers **before** you hash any data.
2. **QueryService** encapsulates all of the SQL connection/command object with a simple mapping method.

### Repositories \ Mappers
1. **AccountMapper** maps fields in a DataRecord in to an Account.

### Repositories \ Queries
1. **Account_Find** Models a command to find an Account
2. **Account_Insert** Models a command to insert an Account
3. **Account_Update** Models a command to update an Account