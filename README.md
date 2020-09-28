# Duplicate images finder

A simple console application to find duplicate images.

## Pre-requisites
.DotNet Sdk 3.1.7 installed. [Download here](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1.7/3.1.7.md)

## How to run?
- Clone the repo
- Open up a command prompt or powershell window in the repo directory.
- Execute the following commands to build and publish the project
	- `dotnet restore .\DuplicateImagesFinder\DuplicateImagesFinder.sln`
	- `dotnet build --configuration Release --no-restore .\DuplicateImagesFinder\DuplicateImagesFinder.sln`
- Execute the following command to find duplicates in a given directory.
    - `dotnet .\DuplicateImagesFinder\bin\Release\netcoreapp3.1\DuplicateImagesFinder.dll [FullPathToDirectoryHere]`
		- Example:
		```
		dotnet .\DuplicateImagesFinder\bin\Release\netcoreapp3.1\DuplicateImagesFinder.dll 'D:\\Repos\\Cogent\\Code Test\\'
		```
		- Make sure the path passed in as argument has escaped backslashes.

	- You should see an result that looks something like the image below.
	  ![Results](.\example_results.png)

## How to run unit tests?
- Open up a command prompt or powershell window in the repo directory.
- Execute the following commands to build and publish the project
	- `dotnet restore .\DuplicateImagesFinder\DuplicateImagesFinder.sln`
	- `dotnet test .\DuplicateImagesFinder\DuplicateImagesFinder.sln`


## Assumptions
- Images with only the following extensions are processed:
  - jpg,jpeg,tiff,png
- Images have to be an exact match, including dimensions and image quality.

## Considerations
* What if this same solution was used on a really large set of photos? What if it was a thousand photos? Or tens of thousands?

It should work, but is likely to eventually run out of memory based on the number of images. The larger the images, the longer it will take.

* What if this was a three-way merge, with triplicates? Does your solution account for this?

This has been considered.


* Some of these files may have had their filename changed.
Shouldn't be an issue. We don't care for the filenames.


* Some of these may have only their extension changed.
Shouldn't be an issue either, we open up the images and convert the opened image to base64.