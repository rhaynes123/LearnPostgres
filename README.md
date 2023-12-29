# LearnPostgres
This project exists to help keep track of learning some of the basics of postgres

## Step 0. Installation
So since we will be using Postgres we need to make sure the postgres is installed on your system of choice. I will be installing it locally on my Mac so I will be using the [Mac Download](https://postgresapp.com/downloads.html) and [Install steps](https://postgresapp.com) however postgres can be installed locally on a number of different systems or could be installed via [docker](https://www.docker.com/blog/how-to-use-the-postgres-docker-official-image/). Any database engine can be run via the command line however to make life easier for many database users here are two graphical user interfaces that a Mac user could use for free, [TablePlus](https://tableplus.com) and [Valentina Studio](https://apps.apple.com/us/app/valentina-studio/id604825918?mt=12). 

## Step 1. Table Creation
First and foremost it is important to understand the bascis of how to create and store data. This means we are going to start by creating a table and then inserting data.
```SQL
CREATE TABLE Items(
Id SERIAL PRIMARY Key,
ItemName text UNIQUE NOT NULL,
Price NUMERIC(10,2) DEFAULT 0.00,
Created DATE DEFAULT CURRENT_TIMESTAMP
);
```
The above SQL syntax will allow us to build a table and the key words we have used will allow that table to be built with a few key "instructions". First we will have an id value which because have the keyword serial behind the Id column it will be a number value that will automatically increment by 1 each time something is inserting meaning we don't have to provide that value. The next thing is a column called ItemName which is of a basic text type but we have used the unqiue keyword meaning once something is in the table no other column can have that same value. We have a column called Price which is a numerical value that we have instructured to fit a format of ten digits before a decimal point and two after which is a good format for money. We have added a default value of 0.00 meaning if we don't insert that value it will be 0.00.

## Step 2. Insert Data
All data is added to a database using an insert statement.
```SQL

INSERT into Items 
(ItemName, Price)
VALUES('HNR Pizza', 5.99);
INSERT into Items 
(ItemName)
VALUES('Napkin');
INSERT into Items
(ItemName, Price)
VALUES('Pepsi', 2.39);
INSERT into Items
(ItemName, Price)
VALUES('Plain Lays Chips', 2.39);
```
The above insert statement when executed will add four different items to our database. A item for a HNR Pizza for 5.99, one for a Napkin which will be 0.00 for its price since we didn't specify that, a Pepsi and a bag of Lays Chips both at 2.39 . 
## Step 3. Selecting data

```SQL
SELECT * FROM Items; -- Selects all the rows for the data possible. Because of how much data real world tables may contain it is advised to avoid this in many situations.
```
```SQL
SELECT ItemName FROM Items WHERE price = 0.00 -- This will return items with a price of 0.00, so only free items. Note we are only selecting the name and nothing else.
```
```SQL
SELECT ItemName FROM Items WHERE price > 0.00 -- This will get us all the non-free items since they all would have a dollar value greater than 0.00


SELECT ItemName FROM Items WHERE price <> 0.00 -- Can achieve the same goal however if our database had negative numbers that would be returned as well from this query. The use of <> will return anything simply isn't 0.00 which would ne negatives.
```
## Step 4. Altering data
Something that is always good to know how to do is alter a table to allow for more data than existed before.
```SQL
ALTER table Items ADD COLUMN IF NOT EXISTS Active Boolean NOT NULL DEFAULT true;
```
The above alter statement is doing quite a bit. We are going to add a new column to all of our records if it doesn't exist called Active and it will be of a boolea data type meaning its values will eithe be true or false and we have told the column to not allow null meanig it also can't have null. The default in our case will be true since it does make since that when something is added it be immediatly active as well as the previous items also being active. 

## Step 5. Updating data
Now that we have our new column we are going to deactivate something in this case we will deactive the Lays chips record.

```SQL

UPDATE Items SET Active = false WHERE Id = 4;

```
Since we specifically want to deactivate the lays chips record we are going to use the above command. You might be asking "since we know we want the chips why didn't we just use 
UPDATE Items SET Active = false WHERE ItemName = 'Plain Lays Chips'; ?". In this case you would be right but there are a few reasons that query will work but many reasons to avoid a query like that.
1st. While the above query is indeed easy to understand it's checking for a string of text which are very error prone in programming. They are often not unique we made them unique for our needs but strings of text often have good reasons to have duplications. For example if we had a table keep track of people a person name can't be marked unique because unque humans may have the same name. Social secutiry numbers are good values to make unique but the text around a person name can't be marked unique if you did you'd eventually run into issues.
2nd. Strings are error prone because they have to be very very precise for example look at the below queries

```SQL

SELECT * FROM Items WHERE ItemName = 'Plain Lays Chips';
```
The query above will return data for the chips but the below query wont because string based searching is case sensitive and in the below query the word 'plain' has been spelt lower case but the name was saved with a capital P so those are treated as completely different strings. 

```SQL

SELECT * FROM Items WHERE ItemName = 'plain Lays Chips';
```
Now be mindful as there are indeed ways to write queries in such a way where yes you could use strings regardless of their casing but there are also empty spaces and empty spaces also factor into the string uniqueness. It is always advised to query record by the most specific value you can to acieve your goals. This may mean you write a query with the less intiative value but only because it is the most specific and accurate value to use.

