# Web Application
This is a very simple web application - there are no automated tests for this, just try creating some accounts.


### Configuration
1. Update the *appsettings.json / DefaultConnection / ConnectionString* to point to your database.
2. Build and Run

#### AccountController
1. **/Account/Index** handles creating new accounts.
2. **/Account/CheckIfEmailIsNotInUse** validates that the email is available - the worst that would happen would be that the password would be changed.

#### CredentialsViewModel
This manages all of the presentation and validation logic, WasSaved flips to true if the account was created.
 