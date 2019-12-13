using System;
using Autodesk;
using Autodesk.Revit;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI.Macros;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using System.Linq;
using forms = System.Windows.Forms;




namespace myMacros
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class ThisApplication : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            View activeView = doc.ActiveView;
            string ruta = App.ExecutingAssemblyPath;

            using (var form = new Form1())
            {
                //use ShowDialog to show the form as a modal dialog box. 
                form.ShowDialog();

                //if the user hits cancel just drop out of macro
                if (form.DialogResult == forms.DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }
                

                //create a new variable to store the user input text
                string sheetNumber = form.textString.ToString();


                ViewSheet viewSh = null;

                FilteredElementCollector sheets = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet));

                foreach (ViewSheet sht in sheets)

                {
                    if (sht.SheetNumber.ToString() == sheetNumber) //find the sheet that matches the user input text
                        viewSh = sht;
                }

                //TaskDialog.Show("out","Selected sheet Id: " + viewSh.Id.ToString());

                using (Transaction t = new Transaction(doc))

                {

                    t.Start("Add view to sheet");

                    try
                    {
                        Viewport vp = Viewport.Create(doc, viewSh.Id, activeView.Id, new XYZ(0, 0, 0));

                        t.Commit();
                    }

                    /*
                                    catch (System.NullReferenceException ex)
                                {
                                    TaskDialog.Show("Exception Caught", "The Sheet Number does not exists");
                                    t.RollBack();
                                    placeActiveView();
                                }

                    */
                    catch
                    {
                        if (sheetNumber == "")
                        {
                            TaskDialog.Show("Warning", "Please enter a sheet number");
                            //t.RollBack();
                            
                        }

                        else if (viewSh == null)
                        {
                            TaskDialog.Show("Warning", "The sheet number does not exist");
                            t.RollBack();
                           
                        }

                        else
                        {
                            TaskDialog.Show("Warning", "The view is already placed on another sheet");
                            t.RollBack();
                            
                        }
                    }

                    //close using
                }
                //close Form1
            }
            //close placeActiveView

            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }
    }
}