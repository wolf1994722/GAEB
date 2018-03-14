# Dangl.AVA.Examples

> An online version of the converter is freely accessible at [my blog](https://blog.dangl.me/gaeb-converter/)  
> A more detailed feature description is available [on my website](https://www.dangl-it.com/products/gaeb-ava-net-library/).

> This project requires **Visual Studio 2017** or newer. Otherwise, the Dotnet CLI is also supported. For **Visual Studio 2013**
> and **Visual Studio 2015**, please see the [vs2013 branch](https://github.com/GeorgDangl/Dangl.AVA.Examples/tree/vs2013).

This is a _demonstration_ library intended to show the usage of the Dangl.AVA and Dangl.GAEB projects.
To build this solution, you need to copy four packages into the `./packages` folder, relative to the root of this project:
* Dangl.AVA
* Dangl.GAEB
* Dangl.AVA.Converter
* Dangl.AVA.Converter.Excel

> If you are already a customer with support contract, please see any of the packages documentation on how to set up the official NuGet feed for **DanglIT** packages.

These packages are not included in this repository.
This project is intended to demonstrate the usage of the Dangl.AVA and Dangl.GAEB libraries.
[Please get in touch with me if you are interested in the libraries](https://www.dangl-it.com/contact/?message=I%27m+interested+in+Dangl.GAEB+%26+Dangl.AVA.+Please+contact+me.).

## CLI Interface

The console application is launched via

    Dangl.AVA.Examples.exe -i <InputFile> -o <OutputFile> -t <Target> [-s] [-e] [-p]

| Parameter | Description |
|-----------|-------------|
| -i | Path to an input file. Can be any GAEB file or an Excel file created by this tool |
| -o | Path of the output file without extension. Will overwrite existing files |
| -t | Transformation target, can be either `Excel`, `Gaeb90`, `Gaeb2000` or `GaebXml`|
| -s | Optional, if included, all prices, taxes and deductions are stripped from the output|
| -e | Optional, if included, the output files will not have a comment with a branding of the library |
| -p | Optional, if included, prints all positions found in the GAEB file to the console |

## Additional Samples

The unit test project in `test/Dangl.AVA.Examples.Tests` contains additional example code,
such as roundtripping and simple reading of GAEB files.

## GAEB Example Files

You can find examples of GAEB files in the `GaebFiles` folder in the solution root directory. They are available in German **DE** or English **EN** language and come in
all three formats - GAEB 90, GAEB 2000 and GAEB XML.

## Library Key Features

* Can read all GAEB90, GAEB2000 and GAEB XML files. The GAEB library includes a lot of code that can recover from errors that were found in files out in the wild
* Hassle-free import: Just pass the `Stream` of the file to the converter, format detection and error recovery happens automatically
* All libraries are available with both .Net and NETStandard targets, making them usable on virtually all platforms (for example on Windows, Linux, Mac and Xamarin)
* **Dangl.GAEB** provides a native interface to all features of GAEB files, allowing native operation directly on the GAEB file
* **Dangl.AVA** offers a unified data model that can be bi-directionally imported or exported to via **Dangl.AVA.Converter** between GAEB, Excel and Json
* Advanced heuristics allow the preservation of most information even when converting to an earlier version of the GAEB standard
* Complete `INotifyPropertyChanged` support in **Dangl.AVA** and event driven messaging makes it directly usable in front end applications - Set the price of an item and the whole bill of quantity is automatically updated
* Over **120.000** tests are run automatically on every commit. The tests cover 7 frameworks (both full .Net and .Net Core) and over 200 GAEB files

## Structure

![Library Structure](docs/structure.png)
