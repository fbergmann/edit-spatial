# Edit Spatial 
This project contains a prototype editor for manipulating models using SBML Models using [SBML Level 3](http://sbml.org/Documents/Specifications) with the [Spatial package](http://sbml.org/Community/Wiki/SBML_Level_3_Proposals/Spatial_Geometries_and_Spatial_Processes "Spatial package"). 

## Using the Editor
There is not much there just yet, basically just open the SBML model in it and inspect / modify the geometry. More to come...

## How to build
The project is a .NET project simply open and compile `EditSpatial.sln` with either Visual Studio or MonoDevelop. 

## Libraries
This project requires the following libraries: 

- [libSBML](http://sbml.org/Software/libSBML) along with a supported xml library (xml2, expat, xerces-c)
- [Ookii.Dialogs](http://www.ookii.org/) used for browsing folders, easily

## License
This project is licensed under the BSD license: 

```
Copyright (c) 20135, Frank T. Bergmann  
All rights reserved. 

Redistribution and use in source and binary forms, with or without 
modification, are permitted provided that the following conditions are 
met: 

Redistributions of source code must retain the above copyright notice, 
this list of conditions and the following disclaimer. Redistributions in 
binary form must reproduce the above copyright notice, this list of 
conditions and the following disclaimer in the documentation and/or 
other materials provided with the distribution. THIS SOFTWARE IS 
PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 

```
