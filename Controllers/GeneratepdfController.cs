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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using pmacore_api.Models;
using System.IO;
using pmacore_api.Controllers.Pdfreport;
using DinkToPdf.Contracts;
using DinkToPdf;
using pmacore_api.Controllers.Pdfreport.PdfTin;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Internal;

namespace pmacore_api.Controllers
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
