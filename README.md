# Developing for Azure with Visual Studio 2022

Hello! And welcome to the code repository for the [Developing for Azure with Visual Studio 2022 course on Pluralsight](https://pluralsight.pxf.io/BX1k79). Use [this link](https://pluralsight.pxf.io/c/1411097/1161410/7490) if you need a free trail to Pluralsight!

The projects contained within are the 2 final projects with all of the Connected Services files already included.

The projects are meant for you to explore, there is a fair amount of Azure setup that you'll need to go through in order to get everything to work.

## Azure Setup

All of the ARM templates are set to deploy to `westus` or `westus2`. You can change that by going into the ARM templates in `Properties\ServiceDependencies\local` or `\<DEPLOYMENT NAME>` and specifying your own value.

### Weather.Web

In order to get this project to run locally, you'll need to provision the following:

1. Azure App Configuration

To gain debug info, you'll need to provision:

1. Azure Application Insights
1. Azure App Service

You can do all of this by opening up the **Manage Connected Services** screen (make sure you've signed into Azure with Visual Studio) and then RESTORE all of the dependencies.

#### Further Setup

You'll need to create the following values in Azure App Configuration.

* `WeatherApp:APIUrl` => the URL of the **Weather.API** instance. For local dev it will be `https://localhost:7071`
* `WeatherApp:CityInfoUrl`=> `/api/CityInfo`
* `WeatherApp:header` => hex code for the header color, for example: `#00ff00`
* `WeatherApp:ReportErrorUrl` => `/api/ReportError`
* `WeatherApp:WeatherForCityUrl` => `/WeatherForecast`

#### Troubleshooting

You may run into issues if you already have a no-cost instance of Azure App Configuration provisioned in your subscription. If that's the case, you can change the SKU of this one by editing the `Properties\ServiceDependencies\local\appConfig1.arm.json` file. Change the `sku` value from `free` to `standard`.

### Weather.API

In order to get this project to run locally, you'll need to provision the following in Azure:

1. Azure Cosmos DB

You'll also need the **Azurite Storage container** which will require Docker Desktop. The Connected Services _should_ download this for you automatically if needed.

To debug you'll need an App Service.

You can provision everything by using the **Restore** functionality of the Connected Services.

#### Further Setup

To get Cosmos DB working properly, you'll need to create a new database and collection. Then fill that collection with some data.

* **Database name:** WeatherInfo
* **Collection name:** AllCities
* **Partition Key:** `/state`

Sample data entry:

```json
{
    "cityId": 1,
    "name": "Seattle",
    "state": "WA"
}
```

#### Troubleshooting

You may have issues with Cosmos provisioning in your preferred region depending on your subscription type. Don't be afraid to mess around with the `Properties\ServiceDependencies\local\cosmosdb1.arm.json` if you have issues deploying.
