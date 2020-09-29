# Domain scoped models
The project isn't particularly complex, so the domain model is very simple:

#### Services
1. **HashingService** a simple alternative to MD5 that generates deterministic garbage - feel free to customise the prime numbers **before** you hash any data.

#### Models/AccountCredentials
This class models an `EmailAddress` and `PlainTextPassword`. 
