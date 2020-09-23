# Domain scoped models
The project isn't particularly complex, so the domain model is very simple:

### Models/AccountCredentials
This class models an `EmailAddress` and `PlainTextPassword`. 

`AccountCredentials.TryValidate` throws `ArgumentNullException` if either of the properties is invalid.
