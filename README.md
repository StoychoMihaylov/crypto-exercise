# crypto-exercise

## Introduction

Welcome to the backend challange! In this challange we will test your knowledge about C#, ASP.NET, Microservices and programming in general.

Over the course of 3-4 hours you will write a small microservice which exposes a REST Api, persists data to a DB, fetches additional data from an external API and does some calculations.

The code should be written as clean as possible, without overengineering anything. Basic things such as logging are required.

The challange is also designed to be open, so feel free to implement things the way you think they should be done and see this documentation more as a guideline.

## Prerequisites

Please clone the following repository before continuing. [https://gitlab.com/imdbere/backend-challange-junior1](https://gitlab.com/imdbere/backend-challange-junior1)

In the linked repository you will find a docker-compose.yaml file that sets up PostgreSQL (TimescaledDB) for you locally. You will use this for developing your application. You have to have docker installed of course

In the BackendChallange subfolder you will find the project already set up, your job is to complete the missing Models, Controllers and Services.

# The challange

The job of the service you write is the following: For every user we save the amount of each crypto currency he owns. The service then periodically fetches crypto prices from an external source (Coingecko) and calculates the EUR value of each separate coin and the total portfolio of the user. Both values are persisted in a Database (PostgreSQL)

## Models

The challange consists of 4 models, two are already set up for reference. You should create the missing models and then generate the necessary migrations to apply them to the database.

### **User:**

This model already exists. All other models belong to a user

### **PortfolioEntry**:

This model already partially exists. It describes how much of every cryptocurrency (e.g. 0.001 BTC) every user has. If that value changes, the respective row has to be updated. There is only one row per user and cryptocurrency.

You should add a combined unique constraint to UserId and Coin.

### **PortfolioValue:**

This model describes the value of the total user portfolio (all coins combined) at each point in time. Rows are only added to this table, never modified or removed. 
The model should contain the following fields:

- PortfolioValue
    - Id (int)
    - CreatedOn (DateTime)
    - Time (DateTime)
    - User (1:N relation)
    - Value (decimal)

### **CoinValue:**

This model is similar to PortfolioValue but it contains the (EUR) value of a **single** coin at each point in time.
It should contain the following fields:

- CoinValue
    - Id (int)
    - CreatedOn (DateTime)
    - Time (DateTime)
    - User (1:N relation)
    - Value (decimal)
    - Coin (string)

**Additional stuff:**

- Some models contain a CreatedOn field. This field should be configured in a way that the database automatically populates it on the first insert
- Some models contain a Coin field. This field should be configured so that string comparisons are case insensitive

## Controllers

Some controllers are already written, some need modification and some need to be created. You can use the preinstalled Swagger to test your controllers ([http://localhost:5000/swagger](http://localhost:5000/swagger))

- Modify CoinValuesController
    - Add GET route (int userId, string coin, DateTime from, DateTime to)
        - Return all coinValues for user and coin between **from** (inclusive) and **to** (exclusive)
- Modify PortfolioEntriesController
    - Add POST route (int userId, string coin, decimal value)
        - Insert or update PortfolioEntry for specific user and coin to value, also update LastUpdateTime
- Create PortfolioValuesController (route: api/users/{userId}/portfolioValues)
    - GetCurrentPortfolioValue (int userId)
        - Return the latest portfolio value for user (by time field)

## Statistic Service

In the Services folder you can find a file called StatisticsService. This file should be extended to achieve the following functionality:

Every 1 minute the service should fetch the current market price for a list of predefined coins. The time of fetching should be **aligned with the clock**, so the fetching should always occur at 00:00, 00:01, 00:02, 00:03 and so on and should not depend on the exact moment the service is started. (hint: [https://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes](https://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes))

### Market price fetching

For market price fetching, you can use the free API of **Coingecko**. Please refer to the [documentation](https://www.coingecko.com/api/documentations/v3#/) for the routes and models but as an overview, you basically should use this two routes:

- GET [https://api.coingecko.com/api/v3/coins/list](https://api.coingecko.com/api/v3/coins/list) (get a list of coins with IDs)
- GET [https://api.coingecko.com/api/v3/coins/{coinId}](https://api.coingecko.com/api/v3/coins/%7BcoinId%7D) (fetch information of a specific coin id)
    
    From the response you can extract a lot of information for the specified coin, we are interested in the current price of the cryptocurrency in *EUR* (found in market_data.current_price.eur).
    

Feel free to use any third party libraries such as [Flurl](https://flurl.dev/) or [Json.NET](https://www.newtonsoft.com/json) for making the HTTP request and deserializing the response.

### Portfolio Value calculation

After fetching the market price, you should do the following calculation for each user:

- Get his current portfolio by fetching the **PortfolioEntries**
- Calculate the current EUR value of all his coins using the prices you fetched previously and save it to the **CoinValues** table
- Add up the calculated value of all coins and save it to the **PortfolioValues** table

### Additional challange (TimescaleDB)

If you reach this point, first of all, congratulations 🎉

This description is not complete, so please ask for help if you read this.

- Generate neccessary migrations
    - Add "SELECT create_hypertable('portfolio_values', 'time');"
    - Add "SELECT create_hypertable('coin_values', 'time');"
- ListPortfolioValues (userId, from, to, interval)
    - Return all portfolio values for user between from (inclusive) and to (exclusive). group results by interval (use time_bucket query for that)
