# GameService - Alpha

| Master                                                                                                                          | Develop                                                                                                                          |
|---------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| [![Build Status](https://travis-ci.com/SkyMen-Lab/GameService.svg?branch=master)](https://travis-ci.com/SkyMen-Lab/GameService) | [![Build Status](https://travis-ci.com/SkyMen-Lab/GameService.svg?branch=develop)](https://travis-ci.com/SkyMen-Lab/GameService) |


## Intro

GameService is a core service for interactions between the [client](https://github.com/SkyMen-Lab/TheP0ngMobileApp) (through [Routers](https://github.com/SkyMen-Lab/RouterFliter)), [Frontend](https://github.com/SkyMen-Lab/GameField) and [GameStorageService](https://github.com/SkyMen-Lab/GameStorageService). 

The principle is following:

- Requests (clicks) come from the routers of the teams as a messages with direction where to move the puddle and the code (school code and game code)
- The service check the data, if it's valid, it sends message to the Frontend app via SignaIR and registers it in the cache database (MongoDB).
- If a team scores, Frontend send a message to the Service and the goal is registered in the db 
- If new user joins the game, router sends a corresponding message to the Service and the number of players in team is incremented and the constant of the puddle speed is recalculated, and updated in db and Frontend
- After the game has finished, necessary data is formatted into json and send to the GameStorageService where it is recorded in SQL database and processed further on


## Tech Stack: 
- ASP.NET Core 3.1 WEB API
- MongoDB (latest)
- SignaIR
- AutoMap
- REST API

## Contribution
All developers are always welcome to contribute to the project and open issues and pull-requests with appropriate messages.

## License
The code is licensed under the GNU General Public License v2.0