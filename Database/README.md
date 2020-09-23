# Database Architecture
The project intentionally does not use an ORM and isn't particularly complex, the database is very simple:
1. **Accounts** table to track email addresses and passwords - hashes of passwords.
2. **AccountFind** stored procedure that finds accounts by email address.
3. **AccountUpdate** stored produre that manages inserting and updating new accounts.

## Installation
You will find everything here that you need to create a database for the project.

*You may lose data while running these scripts if you run them in an existing database containing data, be careful*

1. Firstly you will need to build the project
2. Open the **Database.publish.xml** file configure and a connection to your database server.
    * If you already have an **empty** database then fill in the *Database Name* field.
3. If you didn't specify a database in your connection settings then fill in *Database Name* to create a new one.
4. You've downloaded this off the Internet, so hit *Generate Script* and review the table creation script.
5. If you are happt then you can hit *Publish* or run the script manually.
6. Don't forget to configure the permissions.



