# Vy Lowfare
Tool to find best train prices in Norway. 
> Built with Vue, [Buefy](https://buefy.org/), and .NET Core.
![preview](./preview)

## Installing / Getting started

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

This means that the first lookup for a given journey might take a couple minutes, but
subsequent queries for the same journey shoul be much faster.

## Contributing

If you want to contribute, please for the repository and use a feature branch. Pull requests
are very welcome and should be merged to the develop branch :)

## Links

Useful links:
- Project homepage: https://github.com/AlfHou/Vy-Lowfare
- Issue tracker: https://github.com/AlfHou/Vy-Lowfare/issues


## Licensing

The code in this project is licensed under MIT license.
