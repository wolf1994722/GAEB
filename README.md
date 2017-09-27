# Dangl.AVA.Examples

This is a _demonstration_ library intended to show the usage of the Dangl.AVA and Dangl.GAEB projects.
To build this solution, you need to copy four packages into the `./packages`, relative to the root of this project:
* Dangl.AVA
* Dangl.GAEB
* Dangl.AVA.Converter
* Dangl.AVA.Converter.Excel

These packages are not included in this repository.

## CLI Interface

The console application is launched via

    Dangl.AVA.Examples.exe -i <InputFile> -o <OutputFile> -t <Target> [-s]

| Parameter | Description |
|-----------|-------------|
| -i | Path to an input file. Can be any GAEB file or an Excel file created by this tool |
| -o | Path of the output file without extension. Will overwrite existing files |
| -t | Transformation target, can be either `Excel`, `Gaeb90`, `Gaeb2000` or `GaebXml`|
| -s | Optional, if included, all prices, taxes and deductions are stripped from the output|
