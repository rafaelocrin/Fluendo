# Fluendo
<b>Fluendo App</b>

This application is a sample Fluendo project to interact with PUGB API to obtain the top 100 players given game mode and  the players statistics given an account id.

It's a .Net Core Application consisting of the following components with respectives end points:

- Fluendo Infrastructure Authentication - Token Generator  

- Fluendo Core Web Api - Default endpoint, responsible to call Updater Service to get Leaderboard top players and players statistics, then caching requests.  

- Fluendo Stats Updater Service - Responsible to call PUGB API to to get Leaderboard top players and players statistics, then store those requests into Non relational Database.  

Download and Launch the application

<b>MongoDB</b>
- Acces MongoDB web page to Pull and run MongoDB Docker
    web page: https://hub.docker.com/_/mongo
    - docker commands:
         docker pull mongo
         docker run -p 127.0.0.1:27017:27017 --name fluendo_mongo -d mongo

<b>Redis</b>
- From Docker for windows or accessing the Redis web page, pull and run Redis
    web page tutorial for install by Docker for Windows: https://koukia.ca/installing-redis-on-windows-using-docker-containers-7737d2ebc25e
    web page: https://github.com/MicrosoftArchive/redis/releases
    - docker commands:
           docker pull redis
           docker run --name redis -d redis
 
<b>Fluendo Platform</b> 
- Download and extract the Fluendo Application from this repository
   https://github.com/rafaelocrin/Fluendo 

- Open solution in Visual Studio then run the following projects

  Fluendo.FluendoPlatform.Infrastructure\Fluendo.FluendoPlatform.Infrastructure.Authentication
  Fluendo.FluendoPlatform.Core\Fluendo.FluendoPlatform.Core.WebApi
  Fluendo.FluendoPlatform.StatsService\Fluendo.FluendoPlatform.StatsService.WebApi

- Access API's 

  Fluendo Infrastructure Authentication
  http://localhost:50541/swagger/index.html
     - /api/token (token generator)

  Fluendo Core Web Api
  http://localhost:50708/swagger/index.html
     - /api/Leaderboard/{ gameMode } (Get 100 Top players by game mode)
     - /api/Player/{ accountId }/seassons/lifetime (Get player statistics by account id)

  Fluendo Stats Updater Service
  http://localhost:51433/swagger/index.html
     - /api/Leaderboard/{ gameMode } (Get 100 Top players by game mode)
     - /api/Player/{ accountId }/seassons/lifetime (Get player statistics by account id)

<b>Final comments:</b>

Asp.Net Core Backend - done

Leaderboard Stats Updater Service - done

Token Authentication - done

Redis Cache - done

Non Relational Database - done

Swagger - done

Github - done

--------------------------------------

Unit Tests
   - Fluendo.FluendoPlatform.Infrastructure.Authetication.Test/AuthenticationTest.cs (done)

Docker containers / docker compose - pending

Rate limiting - pending

