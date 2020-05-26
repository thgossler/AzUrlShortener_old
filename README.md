# Azure Url Shortener (AzUrlShortener)

[![Deploy to Azure](https://img.shields.io/badge/Deploy%20To-Azure-blue?logo=microsoft-azure)](https://portal.azure.com/?WT.mc_id=urlshortener-github-tgossler#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Ftgossler%2FAzUrlShortener%2Fmaster%2Fdeployment%2FazureDeploy.json)

A simple and easy to use and to deploy budget-friendly Url Shortener for everyone. It runs in Azure (Microsoft cloud) in your subscription.  

## Changes from the original version of FBoucher

The original implementation can be found [here](https://github.com/FBoucher/AzUrlShortener). Please refer to it for further updates. This fork might be a dead end.

Changes:
- Added support for "Azure Let's Encrypt"
- Added support for deleting URLs

## How To Deploy

To deploy YOUR version of **Azure Url Shortener** you could fork this repo, but if you are looking for the easy way just click on the "Deploy to Azure".

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/?WT.mc_id=urlshortener-github-tgossler#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Ftgossler%2FAzUrlShortener%2Fmaster%2Fdeployment%2FazureDeploy.json)

To have all details and alternative deployment refer to the [Deployment Details](azFunctions-deployment.md) page.

#### Post Deployment Configuration

A good Url Shortener wouldn't be completed without a custom domain name. To know how to add it and other useful post-deployment configurations refer to the [post-deployment-configuration](post-deployment-configuration.md) page.


### How to Update / Upgrade

You deployed the Azure Url Shortener and it's now running in your Azure Subscription, but you would like to have the new feature(s). Updating your current version is in fact really simple.  

#### Update the Azure Functions

Navigate to the Azure portal (azure.portal.com) and select the Azure Function instance, for this project.

From the left panel, click on the **Deployment Center**, then the **Sync** button. This will start a synchronization between GitHub and the App Service (aka Azure Function) local Git. 

IF you are using the Admin Blazor Website, repeat the same operation but selecting the App Service with the name starting by "adm".

---


## How To Use It

There is many different way to manage your Url Shortener, from a direct HTTP call to a fancy website. 
[See the complete list of admin frontends here](src/adminTools/README.md), with the instructions to deploy and use them. There is also instructions or [guidance](src/adminTools/README.md#how-to-add-a-new-frontend) if you would like to create a new one and collaborate to this project.

---


## How It Works

If you are interested to learn more about what's under the hood, and get more details on each Azure Function, read the [How it works](how-it-works.md) page.


---



## Contributing

Check out our [Code of Conduct](CODE_OF_CONDUCT.md) and [Contributing](CONTRIBUTING.md) docs. This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification.  Contributions of any kind welcome!
