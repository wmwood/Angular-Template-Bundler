# Angular Template Bundler

A simple ASP.NET MVC bundle that helps you combine all of your angular html templates into one file and add to your app's template cache.

## Installation

``` Powershell
PM> Install-Package Angular-Template-Bundler
```
[see nuget](https://www.nuget.org/packages/Angular-Template-Bundler/)

## Usage

#### Add this line to your BundleConfig.cs:
``` C#
bundles.Add(new AngularTemplateBundle("VIRTUAL_PATH", "ANGULAR_MODULE_NAME")
                       .IncludeDirectory("PATH_TO_APP_FOLDER", "SEARCH_PATTERN", SEARCH_SUBFOLDERS));
```

An example usage would be:
``` C#
bundles.Add(new AngularTemplateBundle("~/bundles/app/templates", "myApp")
                       .IncludeDirectory("~/Assets/js/app", "*.tpl.html", true));
```

#### Then, add this line to your main razor page:

``` C#
@Scripts.Render("~/bundles/app/templates")
```

## License
[see LICENSE.md](LICENSE.md)