/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pmacore_api.Controllers.Pdfreport;
using pmacore_api.Models;*/
//using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using pmacore_api.Models.pma;
using System.IO;
using pmacore_api.Controllers.Pdfreport;
using DinkToPdf.Contracts;
using DinkToPdf;
using pmacore_api.Controllers.Pdfreport.PdfTin.pma;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Internal;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using pmacore_api.Models.pma.excell;

namespace pmacore_api.Controllers.pma
{
    [Route("api/[controller]")]
    //[ApiController]
    public class GeneratepdfController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
         private IConverter _converter;

        public GeneratepdfController(  IHostingEnvironment hostingEnvironment,IConverter converter){
            _hostingEnvironment = hostingEnvironment;
             _converter = converter;
        }

        /* [HttpGet]
        [Route("GetPdfLegalizacion")] 
        public async Task<IActionResult>  GetPdfLegalizacion()*/
        [HttpPost]
        [Route("PostPdfAutorizacion")] 
        public async Task<FileResult>  PostPdfAutorizacion([FromBody] ResponseApiPma requestPma )
        {
            var list=requestPma;
            var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 6 },
                    DocumentTitle = "Formato Legalizacion"
                };


                var templatePdfAutorizacion= new TemplatePdfAutorizacion();
                //var template= await templatePdfAutorizacion.GetHTMLString(requestPma,_hostingEnvironment.WebRootPath);
            
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                   // Page= "http://interedes.co/",
                    HtmlContent = await templatePdfAutorizacion.GetHTMLString(requestPma,_hostingEnvironment.WebRootPath),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
                };
            
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
            
                var file = _converter.Convert(pdf);
               /* var ruta= await postUploadImage(file);
                var stream = new FileStream(_hostingEnvironment.WebRootPath+ruta,FileMode.Open);
                HttpContext.Response.ContentType = "application/pdf";
                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "Formato_Autorizacion.pdf"
                };
                */

                return File(file, "application/pdf", "Formato_Autorizacion.pdf");
                //return File(file, "application/pdf");
        }


        [HttpPost]
        [Route("PostPdfLegalizacion")] 
        public async Task<FileResult>  PostPdfLegalizacion([FromBody] ResponseApiPma requestPma )
        {
            var list=requestPma;
            var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 6 },
                    DocumentTitle = "Formato Legalizacion"
                };

                var templatePdfLegalizacion= new TemplatePdfLegalizacion();

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                   // Page= "http://interedes.co/",
                    HtmlContent = await templatePdfLegalizacion.GetHTMLString(requestPma,_hostingEnvironment.WebRootPath),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
                };
            
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
            
                var file = _converter.Convert(pdf);

            
               /* var ruta= await postUploadImage(file);

   
                var stream = new FileStream(_hostingEnvironment.WebRootPath+ruta,FileMode.Open);
                HttpContext.Response.ContentType = "application/pdf";
                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "Formato_Legalizacion.pdf"
                };*/

                //return File(file, "application/pdf");
               return File(file, "application/pdf", "Formato_Legalizacion.pdf");
        }




        [HttpGet]
        [Route("GetExcelExample")] 
        public  async Task<FileResult>  GetExcelExample()
        {
            Stream result;
            string empresa= string.Empty;
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"FORMATO_CAUSACION.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

            var memory = new MemoryStream();                
           

            if (!file.Exists)
            {

            }else{

                 
           /* 
            //var archivo=file.OpenRead();
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            */

            using (
                FileStream rstrEmpty = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbookEmpty;
                   // rstr.CopyTo(memory);
                  
                   // memory.Position = 0;
                   workbookEmpty = new XSSFWorkbook(rstrEmpty);
                   ISheet excelSheetEmpty = workbookEmpty.GetSheet("Relacion_Causion_Viaticos");

                    var style1 = workbookEmpty.CreateCellStyle();
                    style1.BorderBottom = BorderStyle.Thin;
                    style1.BorderLeft =BorderStyle.Thin;
                    style1.BorderRight =BorderStyle.Thin;
                    style1.BorderTop =BorderStyle.Thin;

                   using (FileStream wstrEmpty = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
                   {
                    //Realizar consulta y recorrer por cada elemento
                    //llenar el archivo de excel

                     var f=0;
                    for(int i=0; i<=200;i++){
                        //IRow row = excelSheetEmpty.GetRow(11+l);
                        ///row.Height = 20;
                        var cell = excelSheetEmpty.CreateRow(8+f);
                        cell.Height=30 * 10;
                        cell.CreateCell(0).SetCellValue("");
                        cell.GetCell(0).CellStyle=style1;
                        //cell.GetCell(0).CellType=CellType.Unknown;
                        //HSSFDateUtil.isCellDateFormatted();

                        cell.CreateCell(1).SetCellValue("");
                        cell.GetCell(1).CellStyle=style1;

                        cell.CreateCell(2).SetCellValue("");
                        cell.GetCell(2).CellStyle=style1;

                        cell.CreateCell(3).SetCellValue("");
                        cell.GetCell(3).CellStyle=style1;

                        cell.CreateCell(4).SetCellValue("");
                        cell.GetCell(4).CellStyle=style1;

                        cell.CreateCell(5).SetCellValue("");
                        cell.GetCell(5).CellStyle=style1;

                        cell.CreateCell(6).SetCellValue("");
                        cell.GetCell(6).CellStyle=style1;

                        cell.CreateCell(7).SetCellValue("");
                        cell.GetCell(7).CellStyle=style1;

                        cell.CreateCell(8).SetCellValue("");
                        cell.GetCell(8).CellStyle=style1;

                        cell.CreateCell(9).SetCellValue("");
                        cell.GetCell(9).CellStyle=style1;

                        cell.CreateCell(10).SetCellValue("");
                        cell.GetCell(10).CellStyle=style1;
                        
                        cell.CreateCell(11).SetCellValue("");
                        cell.GetCell(11).CellStyle=style1;

                        cell.CreateCell(12).SetCellValue("");
                        cell.GetCell(12).CellStyle=style1;

                        cell.CreateCell(13).SetCellValue("");
                        cell.GetCell(13).CellStyle=style1;

                        cell.CreateCell(14).SetCellValue("");
                        cell.GetCell(14).CellStyle=style1;

                        f++;
                    }

                    
                    var l=0;
                    for(int i=0; i<=200;i++){
                        //IRow row = excelSheetEmpty.GetRow(11+l);
                        ///row.Height = 20;
                        var cell = excelSheetEmpty.GetRow(8+l);
                        //cell.Height=30 * 13;
                        cell.GetCell(0).SetCellValue("");
                        //cell.GetCell(0).CellStyle=style1;

                        cell.GetCell(1).SetCellValue("");
                        //cell.GetCell(1).CellStyle=style1;

                        cell.GetCell(2).SetCellValue("");
                        //cell.GetCell(2).CellStyle=style1;

                        cell.GetCell(3).SetCellValue("");
                        //cell.GetCell(3).CellStyle=style1;

                        cell.GetCell(4).SetCellValue("");
                        //cell.GetCell(4).CellStyle=style1;

                        cell.GetCell(5).SetCellValue("");
                       // cell.GetCell(5).CellStyle=style1;

                        cell.GetCell(6).SetCellValue("");
                        //cell.GetCell(6).CellStyle=style1;

                        cell.GetCell(7).SetCellValue("");
                        //cell.GetCell(7).CellStyle=style1;

                        cell.GetCell(8).SetCellValue("");
                        //cell.GetCell(8).CellStyle=style1;

                        cell.GetCell(9).SetCellValue("");
                        //cell.GetCell(9).CellStyle=style1;

                        cell.GetCell(10).SetCellValue("");
                        //cell.GetCell(10).CellStyle=style1;
                        
                        cell.GetCell(11).SetCellValue("");
                        //cell.GetCell(11).CellStyle=style1;

                        cell.GetCell(12).SetCellValue("");
                        //cell.GetCell(12).CellStyle=style1;

                        cell.GetCell(13).SetCellValue("");
                        //cell.GetCell(13).CellStyle=style1;

                        cell.GetCell(14).SetCellValue("");
                        //cell.GetCell(14).CellStyle=style1;

                        l++;
                    }
                    workbookEmpty.Write(wstrEmpty);
                    wstrEmpty.Close();
                    rstrEmpty.Close();
                   }
                }
          
                using (
                FileStream rstr = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook;
                   // rstr.CopyTo(memory);
                   // memory.Position = 0;
                    workbook = new XSSFWorkbook(rstr);
                    
                   ISheet excelSheet = workbook.GetSheet("Relacion_Causion_Viaticos");
                   using (FileStream wstr = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
                   {
                    //Realizar consulta y recorrer por cada elemento
                    //llenar el archivo de excel

                    

                    IRow FechaInicio = excelSheet.GetRow(3);
                    FechaInicio.GetCell(11).SetCellValue("12/12/2018");

                    IRow FechaFin = excelSheet.GetRow(4);
                    FechaFin.GetCell(11).SetCellValue("12/12/2019");

                    IRow Empresa = excelSheet.GetRow(4);
                    Empresa.GetCell(13).SetCellValue("54");

                    empresa= "";
                 
                    /* 
                    var style2 = workbook.CreateCellStyle();
                    style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                    style2.FillPattern = FillPattern.SolidForeground;*/

                    //var numero_apoyo=0;

                    //var count=0;
                    var j = 0;
                    for(int i=0; i<=100;i++){
                   // foreach (var item in dataList)
                       // count++;
                       // numero_apoyo= numero_apoyo+1;
                            //for(int i=0; i<=dataList.Count;i++){
                            IRow row = excelSheet.GetRow(8+j);
                            row.GetCell(0).SetCellValue(j);
                            row.GetCell(1).SetCellValue("1082778631");
                            row.GetCell(2).SetCellValue("Enuar Muñoz");
                            row.GetCell(3).SetCellValue("ADMIN");
                            row.GetCell(4).SetCellValue("PMA");


                           

                            row.GetCell(5).SetCellValue("12/12/2018");
                            row.GetCell(6).SetCellValue("12/12/2019");
                            row.GetCell(7).SetCellValue("54");
                            row.GetCell(8).SetCellValue(3);
                            row.GetCell(9).SetCellValue(23800);
                            row.GetCell(10).SetCellValue(30000);
                            row.GetCell(11).SetCellValue(25000);
                            row.GetCell(12).SetCellValue(44000);
                            row.GetCell(13).SetCellValue(0);
                            row.GetCell(14).SetCellValue(1200);
                            j++;
                       // }
                    }

                    workbook.Write(wstr);
                    wstr.Close();
                    rstr.Close();
                }
            }

       ///  archivo=file.OpenRead();
         

            }




             
            //var archivo=file.OpenRead();
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            

           //return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

           
        ///var empresaNameFormated= RemoveDiacritics(empresa);

       // Context.ReturnFile(archivo, string.Format("Inventario_{0}.xlsx",empresaNameFormated), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    
            /* 
            result = new FileStream( 
                        path: file.FullName, 
                        mode: FileMode.Open, 
                        access: FileAccess.Read, 
                        share: FileShare.None,
                        bufferSize: 4096,
                        options: FileOptions.DeleteOnClose );
            Console.Write("Consulta: ", result.ToString());*/
            /*
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName); */
               
           /// return File(result, "application/vnd.ms-excel");

           // var bytepdf= ReadToEnd(archivo);

            //return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Formato_Excel.xlsx");
            //return File(bytepdf, "application/vnd.ms-excel");
        }


        [HttpPost]
        [Route("PostExcelViaticos")] 
        public  async Task<FileResult>  PostExcelViaticos([FromBody] Viatico requestViaticos)
        {
            Stream result;
            string empresa= string.Empty;
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"FORMATO_CAUSACION.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

            var memory = new MemoryStream();   
            if (!file.Exists)
            {

            }else{

            using (
                FileStream rstrEmpty = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbookEmpty;
                   // rstr.CopyTo(memory);
                   // memory.Position = 0;
                   workbookEmpty = new XSSFWorkbook(rstrEmpty);
                   ISheet excelSheetEmpty = workbookEmpty.GetSheet("Relacion_Causion_Viaticos");

                    var  style1 = workbookEmpty.CreateCellStyle();
                    style1.BorderBottom = BorderStyle.Thin;
                    style1.BorderLeft =BorderStyle.Thin;
                    style1.BorderRight =BorderStyle.Thin;
                    style1.BorderTop =BorderStyle.Thin;
                  
                 
                    

                   using (FileStream wstrEmpty = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
                   {
                    //Realizar consulta y recorrer por cada elemento

                    var addNewRoows= requestViaticos.Data.Count+4;
                    var rowsExist= excelSheetEmpty.PhysicalNumberOfRows;

                    if(addNewRoows<excelSheetEmpty.PhysicalNumberOfRows){
                        var f=0;
                        for(int i=0; i<=(rowsExist-4);i++){
                            //IRow row = excelSheetEmpty.GetRow(11+l);
                            ///row.Height = 20;
                            
                            var cell = excelSheetEmpty.CreateRow(8+f);
                            cell.Height=30 * 10;
                            cell.CreateCell(0).SetCellValue("");
                            cell.GetCell(0).CellStyle=style1;
                            //cell.GetCell(0).CellType=CellType.Unknown;
                            //HSSFDateUtil.isCellDateFormatted();

                            cell.CreateCell(1).SetCellValue("");
                            cell.GetCell(1).CellStyle=style1;

                            cell.CreateCell(2).SetCellValue("");
                            cell.GetCell(2).CellStyle=style1;

                            cell.CreateCell(3).SetCellValue("");
                            cell.GetCell(3).CellStyle=style1;

                            cell.CreateCell(4).SetCellValue("");
                            cell.GetCell(4).CellStyle=style1;

                            cell.CreateCell(5).SetCellValue("");
                            cell.GetCell(5).CellStyle=style1;

                            cell.CreateCell(6).SetCellValue("");
                            cell.GetCell(6).CellStyle=style1;

                            cell.CreateCell(7).SetCellValue("");
                            cell.GetCell(7).CellStyle=style1;

                            cell.CreateCell(8).SetCellValue("");
                            cell.GetCell(8).CellStyle=style1;

                            cell.CreateCell(9).SetCellValue("");
                            cell.GetCell(9).CellStyle=style1;

                            cell.CreateCell(10).SetCellValue("");
                            cell.GetCell(10).CellStyle=style1;
                            
                            cell.CreateCell(11).SetCellValue("");
                            cell.GetCell(11).CellStyle=style1;

                            cell.CreateCell(12).SetCellValue("");
                            cell.GetCell(12).CellStyle=style1;

                            cell.CreateCell(13).SetCellValue("");
                            cell.GetCell(13).CellStyle=style1;

                            cell.CreateCell(14).SetCellValue("");
                            cell.GetCell(14).CellStyle=style1;

                            f++;
                        }
                    }else{
                        var f=0;
                        for(int i=0; i<=addNewRoows;i++){
                            //IRow row = excelSheetEmpty.GetRow(11+l);
                            ///row.Height = 20;
                            var cell = excelSheetEmpty.CreateRow(8+f);
                            cell.Height=30 * 10;
                            cell.CreateCell(0).SetCellValue("");
                            cell.GetCell(0).CellStyle=style1;
                            //cell.GetCell(0).CellType=CellType.Unknown;
                            //HSSFDateUtil.isCellDateFormatted();

                            cell.CreateCell(1).SetCellValue("");
                            cell.GetCell(1).CellStyle=style1;

                            cell.CreateCell(2).SetCellValue("");
                            cell.GetCell(2).CellStyle=style1;

                            cell.CreateCell(3).SetCellValue("");
                            cell.GetCell(3).CellStyle=style1;

                            cell.CreateCell(4).SetCellValue("");
                            cell.GetCell(4).CellStyle=style1;

                            cell.CreateCell(5).SetCellValue("");
                            cell.GetCell(5).CellStyle=style1;

                            cell.CreateCell(6).SetCellValue("");
                            cell.GetCell(6).CellStyle=style1;

                            cell.CreateCell(7).SetCellValue("");
                            cell.GetCell(7).CellStyle=style1;

                            cell.CreateCell(8).SetCellValue("");
                            cell.GetCell(8).CellStyle=style1;

                            cell.CreateCell(9).SetCellValue("");
                            cell.GetCell(9).CellStyle=style1;

                            cell.CreateCell(10).SetCellValue("");
                            cell.GetCell(10).CellStyle=style1;
                            
                            cell.CreateCell(11).SetCellValue("");
                            cell.GetCell(11).CellStyle=style1;

                            cell.CreateCell(12).SetCellValue("");
                            cell.GetCell(12).CellStyle=style1;

                            cell.CreateCell(13).SetCellValue("");
                            cell.GetCell(13).CellStyle=style1;

                            cell.CreateCell(14).SetCellValue("");
                            cell.GetCell(14).CellStyle=style1;
                            f++;
                        }
                    }
                    
                    workbookEmpty.Write(wstrEmpty);
                    wstrEmpty.Close();
                    rstrEmpty.Close();
                   }
                }

                
          
                using (
                FileStream rstr = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook;
                   // rstr.CopyTo(memory);
                   // memory.Position = 0;
                    workbook = new XSSFWorkbook(rstr);
                    
                   ISheet excelSheet = workbook.GetSheet("Relacion_Causion_Viaticos");
                   using (FileStream wstr = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
                   {
                    //Realizar consulta y recorrer por cada elemento
                    //llenar el archivo de excel

                    IRow FechaInicio = excelSheet.GetRow(3);
                    FechaInicio.GetCell(11).SetCellValue(requestViaticos.FechaInicial);

                    IRow FechaFin = excelSheet.GetRow(4);
                    FechaFin.GetCell(11).SetCellValue(requestViaticos.FechaFinal);

                    IRow Contrato = excelSheet.GetRow(3);
                    Contrato.CreateCell(2).SetCellValue(requestViaticos.Contrato);

                    IRow Base = excelSheet.GetRow(4);
                    Base.CreateCell(2).SetCellValue(requestViaticos.Base);

                    IRow Relacion = excelSheet.GetRow(3);
                    Relacion.GetCell(13).SetCellValue(requestViaticos.Relacion);

                    /* 
                    var style2 = workbook.CreateCellStyle();
                    style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                    style2.FillPattern = FillPattern.SolidForeground;*/
                    //var numero_apoyo=0;

                    double totalViaticoPermanente= 0;
                    double totalViaticoOcasional= 0;
                    double totalSaldoAnticipo= 0;
                    double totalDescAlim= 0;
                    double totalDescTran= 0;

                    //var count=0;
                    var j = 0;
                    ///for(int i=0; i<=100;i++){
                    foreach (var item in requestViaticos.Data){

                        totalViaticoPermanente=totalViaticoPermanente+double.Parse(item.ViaticoPermanente,System.Globalization.CultureInfo.InvariantCulture);
                        totalViaticoOcasional=totalViaticoOcasional+double.Parse(item.ViaticoOcasional,System.Globalization.CultureInfo.InvariantCulture);
                        totalSaldoAnticipo=totalSaldoAnticipo+double.Parse(item.SaldoAnticipo,System.Globalization.CultureInfo.InvariantCulture);
                        totalDescAlim=totalDescAlim+double.Parse(item.DescAlim,System.Globalization.CultureInfo.InvariantCulture);
                        totalDescTran=totalDescTran+double.Parse(item.DescTran,System.Globalization.CultureInfo.InvariantCulture);

                       // count++;
                       // numero_apoyo= numero_apoyo+1;
                            //for(int i=0; i<=dataList.Count;i++){
                            IRow row = excelSheet.GetRow(8+j);
                            row.GetCell(0).SetCellValue(item.Consecutivo);
                            row.GetCell(1).SetCellValue(item.Cedula);
                            row.GetCell(2).SetCellValue(string.Format("{0}",item.Nombre));
                            row.GetCell(3).SetCellValue(item.Cargo);
                            row.GetCell(4).SetCellValue(item.Sucursal);

                            row.GetCell(5).SetCellValue(item.FechaInicial);
                            row.GetCell(6).SetCellValue(item.FechaFinal);
                            row.GetCell(7).SetCellValue(item.Orden);
                            row.GetCell(8).SetCellValue(item.TotalDias);
                            row.GetCell(9).SetCellValue(item.Incidencia);


                            row.GetCell(10).SetCellValue(string.Format("$ {0:N2}", double.Parse(item.ViaticoPermanente,System.Globalization.CultureInfo.InvariantCulture)));
                            row.GetCell(11).SetCellValue(string.Format("$ {0:N2}", double.Parse(item.ViaticoOcasional,System.Globalization.CultureInfo.InvariantCulture)));
                            row.GetCell(12).SetCellValue(string.Format("$ {0:N2}", double.Parse(item.SaldoAnticipo,System.Globalization.CultureInfo.InvariantCulture)));
                            row.GetCell(13).SetCellValue(string.Format("$ {0:N2}", double.Parse(item.DescAlim,System.Globalization.CultureInfo.InvariantCulture)));
                            row.GetCell(14).SetCellValue(string.Format("$ {0:N2}", double.Parse(item.DescTran,System.Globalization.CultureInfo.InvariantCulture)));
                            j++;

                           
                       // }
                    }


                    IRow rowTotal = excelSheet.GetRow(8+j+1);
                            rowTotal.GetCell(10).SetCellValue(string.Format("$ {0:N2}", totalViaticoPermanente));
                            rowTotal.GetCell(11).SetCellValue(string.Format("$ {0:N2}", totalViaticoOcasional));
                            rowTotal.GetCell(12).SetCellValue(string.Format("$ {0:N2}", totalSaldoAnticipo));
                            rowTotal.GetCell(13).SetCellValue(string.Format("$ {0:N2}", totalDescAlim));
                            rowTotal.GetCell(14).SetCellValue(string.Format("$ {0:N2}", totalDescTran));

                   
                   // rowTotal.GetCell(14).SetCellValue(totalDescTran);


                    workbook.Write(wstr);
                    wstr.Close();
                    rstr.Close();
                }
            }

       ///  archivo=file.OpenRead();
         

            }

            //var archivo=file.OpenRead();
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            
        }


       


        
        [HttpGet]
        [Route("GetPdfAutorizacionExample")] 
        public async Task<FileResult>  GetPdfAutorizacionExample()
        {

            var requestPma= new ResponseApiPma();
            requestPma.Email="enu-44@hotmail.com";
            requestPma.Formato="P135-PYC-ADM-16-13-011";
            requestPma.Version="Version";

            var cliente= new Cliente();
            cliente.Nombre="Nombre";
            cliente.Contrato="Contrato";
            cliente.Proyecto="Proyecto";
            

            var historias= new List<Historias>();
            for(var i =0;i<3;i++){
                var historia1= new Historias();
                historia1.Alimentacion=i;
                historia1.Alojamiento=i;
                historia1.Destino="Destino";
                historia1.Estado="Estado";
                historia1.Fecha="Fecha";
                historia1.Miscelaneos=i;
                historia1.Orden="Orden";
                historia1.Origen="Origen";
                historia1.Tarifa="Tarifa";
                historia1.Transporte=i;
                historia1.Valor=i;
                historias.Add(historia1);
            }

            var dataPdf= new List<DataRequest>();
            for(var i =0;i<2;i++){

                  var empleado= new Empleado();
                    empleado.Apellidos="Apellidos "+i;
                    empleado.Cargo="Cargo";
                    empleado.CC=1;
                    empleado.CECO="CECO";
                    empleado.Nombres="Nombres";
                    empleado.Sucursal=2;
                
                var dataRequest1= new DataRequest();
                dataRequest1.Fecha="Fecha";
                dataRequest1.Objeto="Objeta"+i;
                dataRequest1.FechaInicio="FechaInicio";
                dataRequest1.FechaFin="FechaFin";
                dataRequest1.Consecutivo="Consecutivo"+i;
                dataRequest1.SumAloj=1;
                dataRequest1.SumAlim=2;
                dataRequest1.SumMisc=3;
                dataRequest1.SumTran=4;
                dataRequest1.SumValor=4500;
                dataRequest1.Cliente= cliente;
                dataRequest1.Empleado= empleado;
                dataRequest1.Historias= historias;
                dataPdf.Add(dataRequest1);
            }

            requestPma.Data=dataPdf;


            var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 6 },
                    DocumentTitle = "Formato Autorizacion"
                };

                var templatePdfAutorizacion= new TemplatePdfAutorizacion();

                // var employees = DataStorage.GetAllEmployess();
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                   // Page= "http://interedes.co/",
                    HtmlContent = await templatePdfAutorizacion.GetHTMLString(requestPma,_hostingEnvironment.WebRootPath),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
                };
            
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                var file = _converter.Convert(pdf);
                return File(file, "application/pdf", "Formato_Legalizacion.pdf");
                /*var ruta= await postUploadImage(file);

                return  TestDownload(_hostingEnvironment.WebRootPath+ruta,"Example.pdf");
                */

   
               /* var stream = new FileStream(_hostingEnvironment.WebRootPath+ruta,FileMode.Open);
                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "Example.pdf"
                };*/
            /* 
            
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };
            response.Content = new ByteArrayContent(file);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
            {
                FileName = String.Format("AgencyID" + "userName" + DateTime.Now.ToString("MMMddyyyy_HHmmss"))
                
            };
           
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
            };*/

           
            //return response;
            // return File(file, "application/pdf", "EmployeeReport.pdf");
            // return File(file, "application/pdf");
            //var outputStream = new MemoryStream();
            
            
            //HtmlCellTemplatePdfReport.CreateHtmlHeaderPdfReportStream(_hostingEnvironment.WebRootPath, outputStream);
           // var stream =  new FileStreamResult(outputStream, "application/pdf")
            //{
           //     FileDownloadName = "report.pdf"
           // };
           // return stream;
            //return for view online
            //var bytepdf= ReadToEnd(stream.FileStream);
            //return File(bytepdf, "application/pdf");

    }


    public FileResult TestDownload(string path, string filename)
    {
        HttpContext.Response.ContentType = "application/pdf";
        FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(path), "application/pdf")
        {
            FileDownloadName = filename
        };

        return result;                                
    }
private async Task<string> postUploadImage(byte[] imageArray)
        {
            string rutaFoto=string.Empty;
            if (imageArray != null && imageArray.Length > 0)
            {
                var streamRemote = new MemoryStream(imageArray);
                var guid = Guid.NewGuid().ToString();
                var guid2 = Guid.NewGuid().ToString();
                var file = string.Format("{0}.pdf", guid+guid2);
                var folder = "/Reports";
              
                //rutaFoto= string.Format("{0}/{1}",folder,file);
               /// var folder_fotos = "/Fotos";
                rutaFoto= string.Format("{0}/{1}",folder,file);

                string pathExist= _hostingEnvironment.WebRootPath+folder;// _hostingEnvironment.WebRootPath es Igual a "wwwroot"
                if(!Directory.Exists(pathExist)){
                   Directory.CreateDirectory(pathExist);
                  //string message="No existe el directorio, se creo";
                }else{
                  //string message="Si existe el directorio"; 
                }
                var formFile = new FormFile(streamRemote , 0, streamRemote.Length, "name", file);
                if (formFile == null || formFile.Length == 0)
                    return "file not selected";

                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), pathExist, 
                        Path.GetFileName(formFile.FileName));
                //Ruta foto
                //var fullPath = string.Format("{0}", path);
                //foto.Ruta= rutaFoto;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return rutaFoto;
        }
    public FileResult TestDownload(string ruta)
    {
        HttpContext.Response.ContentType = "application/pdf";
        FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(ruta), "application/pdf")
        {
            FileDownloadName = "test.pdf"
        };

        return result;                                
    }
        
    public static byte[] ReadToEnd(System.IO.Stream stream)
    {
        long originalPosition = 0;

        if(stream.CanSeek)
        {
             originalPosition = stream.Position;
             stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        finally
        {
            if(stream.CanSeek)
            {
                 stream.Position = originalPosition; 
            }
        }
    }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    public class eBookResult : IActionResult  
    {  
        MemoryStream bookStuff;  
        string PdfFileName;  
        HttpRequestMessage httpRequestMessage;  
        HttpResponseMessage httpResponseMessage;  
        public eBookResult(MemoryStream data, HttpRequestMessage request, string filename)  
        {  
            bookStuff = data;  
            httpRequestMessage = request;  
            PdfFileName = filename;  
        }  
       
        public Task ExecuteResultAsync(ActionContext context)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);  
            httpResponseMessage.Content = new StreamContent(bookStuff);  
            //httpResponseMessage.Content = new ByteArrayContent(bookStuff.ToArray());  
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");  
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = PdfFileName;  
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");  
  
            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);  
        }
    }  
    
}
