using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace myMacros
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class App : IExternalApplication
    {
        public static string ExecutingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public Result OnStartup(UIControlledApplication application)
        {
            // Todo el codigo para crear los botonoes en la Ribbon. crear panel  
            RibbonPanel panel1 = application.CreateRibbonPanel("Dynoscript");

            // agregar un boton
            PushButton button1 = panel1.AddItem(new PushButtonData("button1", "My Macro", ExecutingAssemblyPath, "myMacros.ThisApplication")) as PushButton;


            // agregar la imagen al button1
            button1.LargeImage = new BitmapImage(new Uri("pack://application:,,,/myMacros;component/Dynoscript_favicon (1).png"));

            button1.ToolTip = "My Macros";
            button1.LongDescription = "Inserta la vista activa dentro de la Lamina o Sheet que desees. Solo escribe el código de la laminda o sheet en el recuadro que se abre";
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


    }
}
