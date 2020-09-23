# Database Architecture
The project intentionally does not use an ORM and isn't particularly complex, the database is very simple:
1. **Accounts** table to track email addresses and passwords - hashes of passwords.
2. **AccountFind** stored procedure that finds accounts by email address.
3. **AccountUpdate** stored produre that manages inserting and updating new accounts.

## Installation
You will find everything here that you need to create a database for the project.

1. Build the project
2. Open the **Database.publish.xml** file configure a connection to your database server, leave the *Database Name* field blank to create a new database.
3. If you didn't specify a database in your connection settings then set one in *Database Name*.
4. You've downloaded this off the Internet, so hit *Generate Script* and review the table creation script.
5. If you are happt then you can hit *Publish* or run the script manually.
6. Don't forget to configure the permissions.


