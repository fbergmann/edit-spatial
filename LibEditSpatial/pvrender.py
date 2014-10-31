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
  data = reader.PointData[variable]
  range = data.GetRange()
  reader.PointArrayStatus = [variable]
  
  #position camera
  view = GetActiveView()
  if not view:
      view = CreateRenderView()  
    
  #lut, pws= return_colours(variable)
  dp = GetDisplayProperties()
  
  dp.PointSize = 2
  dp.Representation = "Surface"
  dp.ColorArrayName = variable
  
  dp.LookupTable = MakeBlueToRedLT(range[0], range[1])
  dp.LookupTable.ColorSpace = 'Diverging'
  dp.LookupTable.VectorMode = 'Magnitude' 
  
  bar = CreateScalarBar(LookupTable=dp.LookupTable, Title=variable, TitleFontSize = 10, LabelFontSize = 10)
  bar.AutomaticLabelFormat = 0
  bar.LabelFormat = '%6.3f'
  bar.RangeLabelFormat = '%6.3f'
  bar.Title = variable
  bar.ComponentTitle = ''
  view.Representations.append(bar)

  #draw the object
  Show()

  # render 
  Render()
   
  #save screenshot
  SaveScreenshot(output)

if __name__ == '__main__':
  main(sys.argv)
