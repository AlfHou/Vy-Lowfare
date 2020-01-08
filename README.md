# Vy Lowfare
[~~Link to deployed project~~](http://lowfare-train.alfhouge.no) As of 08.01.20, Vy has blocked the IP of the server that the project is hosted on. I therefore need to transfer the backend to call the Entur API instead to get the project back up...

Tool to find best train prices in Norway. 
> Built with Vue, [Buefy](https://buefy.org/), and .NET Core.
![preview](/Media/preview.png)

## Installing / Getting started

### Requirements
* .NET core 3.0
* npm
* docker (optional)

### Docker
The easiest way to get this project running (locally) is by using docker.
Make sure you have docker engine and docker-compose installed. Then
simply open a terminal and navigate to the root folder of the project.
Then run the command 
```shell
docker-compose build
docker-compose run
```
you should now be able to open http://localhost in your browser and see the app.


### Manually
To run this project you should have npm installed

Navigate to the `Backend` folder and run the following:
```shell
dotnet restore
dotnet run
```

This installs the backend dependencies and runs the backend.
You can also start the backend by pressing 'F5' in Visual Studio or Visual Studio Code.


In another terminal navigate to the `Frontend` folder and run the following:
```shell
npm install
npm run serve
```

This installs the frontend dependencies and runs the Frontend. 


### Backend
To build and run the backend Docker container

Navigate to the `Backend` folder and build the docker image with the following command:
```shell
docker build -t vy-backend-img .
```
Then run the docker image with this command:
```shell
docker run -d -p 5000:80 --name vy-backend vy-backend-img
```
The backend is now running in a detached docker container


### Frontend
To build and run the frontend Docker container

Navigate to the `Frontend` folder and build the docker image with the following command:
```shell
docker build -t vy-frontend-img .
```

Then run the docker image with this command:
```shell
docker run -it -p 80:80 --rm --name vy-frontend vy-frontend-img
```
The frontend container is now running and can be accessed in your web browser
by navigating to http://127.0.0.1



## Developing
To develop the project further:

```shell
git clone https://github.com/AlfHou/Vy-Lowfare.git
cd vy-lowfare/
cd Frontend
npm install
cd ../Backend
dotnet restore
```

This clones the repository into a folder named `vy-lowfare` on your computer and installs all dependencies


## Features
This project is inspired by the plane company Norwegian's lowfare calendar for the flight.
I wanted similar functionality for the Norwegian train company Vy's journeys.

This SPA shows:
* The lowest price for each given day in the selected month
* The backend queries Vy for the journeys to and from Oslo S and the 5 largest cities in Norway for 
  every date 3 months into the future and stores the prices in cache every 2nd hour 
* Stores all results from Vy in cache for 2 hours

This means that the first lookup for a given journey might take a couple seconds, but
subsequent queries for the same journey should be much faster.

## Contributing
If you want to contribute, please fork the repository and use a feature branch. Pull requests
are very welcome and should be merged to the develop branch :)

## Links
Useful links:
- Project homepage: https://github.com/AlfHou/Vy-Lowfare
- Issue tracker: https://github.com/AlfHou/Vy-Lowfare/issues


## Licensing
The code in this project is licensed under MIT license.
