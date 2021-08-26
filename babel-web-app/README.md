# Policy Difference Engine Front-End Web Application

This project is the front-end web application for the Policy Difference Engine (PDE). It allows users (such as policy analysts) to enter proposed changes to the maternity benefits entitlement calculation, and displays the results from a simulation of the proposed change.

The web application is connected to the PDE Simulation Engine API using the API Url, which is stored as a config setting. When a simulation is requested, the web app will create a request object from the user input, and send off a simulation request to the Simulation Engine. The Simulation Engine contains stored data (individuals) to run the simulation on. Once the simulation is complete, it will generate a unique GUID, corresponding to the simulation. The results of the simulation can then be retrieved, viewed and visualized.

The visualization is currently done within the web app itself. We have experimented with using PowerBI as a more sophisticated visualization tool. It is quite powerful, but there are some considerations:
- The simulation data must be stored in a database (as opposed to a cache)
- Anyone who you wish to give access to viewing the PowerBI simulations must be granted the proper PowerBI access
- There may be extra work involved in ensuring that PowerBI is fully accessible and bilingual. 

## Development

### Running Locally

When developing locally, ensure the Simulation URL is set properly set in the appsettings.Development.json file.

Use `dotnet run` to run the project.

Note: If running this project locally alongside related web APIs (such as the Simulation Engine), ensure you are specifying the projects to run on separate ports in the launchSettings.json file (in VS Code)

### Running in Docker
- `docker build -t babel-web-app .`
- `docker run -it --rm -p 5000:80 babel-web-app`

### Deployment

There are currently multiple deployments of the Web app in Microsoft Azure (Azure App Service). Each deployment is connected to a separate simulatione engine, which are also all deployed as Azure App Services. Deployments are set up using github actions, based on manually clicking a button. Go to the github actions, choose the workflow, and then run it.

The config values (e.g. Simulation Url) are set in the Azure App Service configuration and injected as environment variables.

## Language and Accessibility

Accessibility is facilitated by following WET-BOEW and GCWeb guidelines:
- https://wet-boew.github.io/wet-boew/index-en.html
- https://wet-boew.github.io/GCWeb/index-en.html

Bilingualism is done using .NET's built-in localization framework. Resource files (in the Resources folder) represent the different translations that will be used by the localizer. The localizer is then injected into the views to render the appropriate text. Since the resource files (.resx) are xml-like, it is useful to have an IDE plugin that helps you visualize and update these files (In VS Code, you can use the ResX editor plugin)
