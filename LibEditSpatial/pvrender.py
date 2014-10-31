# simple paraview python script for generating screenshots
from paraview.simple import *
import sys

def main(args):  
   if (len(args) != 4):
      print("Usage: renderFile filename variable output")
      return 1
   filename = args[1]
   variable = args[2]
   result = args[3]
   renderFile(filename, variable, result)
  
def renderFile(fileName, variable, output):
  #read a file
  reader = XMLUnstructuredGridReader(FileName=fileName)
  
  # select variable
  reader.PointArrayStatus = [variable]
  
  #position camera
  view = GetActiveView()
  if not view:
      view = CreateRenderView()
   
  #draw the object
  Show()
    
  dp = GetDisplayProperties()
  dp.PointSize = 2
  dp.Representation = "Surface"
  
  # render 
  Render()
   
  #save screenshot
  SaveScreenshot(output)

if __name__ == '__main__':
  main(sys.argv)
