using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using pmacore_api.Models.pma;
using System.IO;
using pmacore_api.Controllers.Pdfreport;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Internal;
using pmacore_api.Models.datatake;
using pmacore_api.services;
using pmacore_api.Controllers.Pdfreport.PdfTin.datatake;
using pmacore_api.DataContext;
using System.Linq;
using AutoMapper;

namespace pmacore_api.Controllers
{
    [Route("api/[controller]")]
    public class GeneratePdfInventarioController:Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConverter _converter;
        private readonly IMapper _mapper;
        public GeneratePdfInventarioController(   IMapper mapper,IHostingEnvironment hostingEnvironment,IConverter converter){
            _hostingEnvironment = hostingEnvironment;
             _converter = converter;
              _mapper = mapper;
        }


        
        /* [HttpGet]
        [Route("GetPdfLegalizacion")] 
        public async Task<IActionResult>  GetPdfLegalizacion()*/
        [HttpPost]
        [Route("PostPdfCables")]
        public async Task<FileResult> PostPdfAutorizacion([FromBody] RequestCables requestPma)
        {
            /*var list=requestPma;
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
                return File(file, "application/pdf", "Formato_Autorizacion.pdf");
                */
            //return File(file, "application/pdf");
            return null;
        }

        [HttpGet]
        [Route("GetPdfExampleEquipos")]
        public async Task<IActionResult> GetPdfExampleEquipos()
        {
            var dataContext = MyAppContext.GetInstance();
            var equipos =dataContext.ViewEquipos.ToList();
            return Ok(equipos);
        }


        [HttpGet]
        [Route("GetPdfExampleCables")]
        public async Task<IActionResult> GetPdfExampleCables()
        {
            var listElementosCables = new ResponseElementoCables();
            var prefix = "";
            var controller = "";
            var URL_BASE = "";
            prefix = "odata/datatakedev/";
            controller = string.Format("{0}", "Viewfromcablesms?$top=500&$skip=20000&$count=true");
            var route = RouteService.GetInstance();
            URL_BASE = route.RouteBaseAddress;

            var response = await ApiService.GetList<ResponseElementoCables>(URL_BASE, prefix, controller);

            if (!response.IsSuccess)
            {
                return Ok(response);
            }

            var cables = (ResponseElementoCables)response.Result;
            var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 6 },
                    DocumentTitle = "Formato General Inventario"
                };


                var templatePdfGeneral= new TemplatePdfGeneral();
                //var template= await templatePdfAutorizacion.GetHTMLString(requestPma,_hostingEnvironment.WebRootPath);
            
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                   // Page= "http://interedes.co/",
                    HtmlContent = await templatePdfGeneral.GetHTMLString(cables,_hostingEnvironment.WebRootPath),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "style_inventario.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = false,Spacing=1 },
                    //FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, HtmUrl =Path.Combine(Directory.GetCurrentDirectory(), "assets", "footer/footer.html"), Left = ""  }
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false,Center="" }
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

            return File(file, "application/pdf", "Formato_Inventario_General.pdf");
            ///return Ok(response);
        }




        
        [HttpGet]
        [Route("GetPdfExampleDetailCables")]
        public async Task<IActionResult> GetPdfExampleElementos()
        {
            var listElementosCables = new ResponseElementoCables();
            var prefix = "";
            var controller = "";
            var URL_BASE = "";
            prefix = "odata/datatakedev/";
            controller = string.Format("{0}", "Viewfromcablesms?$top=100&$skip=30000&$count=true");
            var route = RouteService.GetInstance();
            URL_BASE = route.RouteBaseAddress;

            var response = await ApiService.GetList<ResponseElementoCables>(URL_BASE, prefix, controller);

            if (!response.IsSuccess)
            {
                return Ok(response);
            }

            var cables = (ResponseElementoCables)response.Result;
            var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 6 },
                    DocumentTitle = "Formato General Inventario"
                };


                var templatePdfGeneral= new TemplatePdfDetallado();
                //var template= await templatePdfAutorizacion.GetHTMLString(requestPma,_hostingEnvironment.WebRootPath);
            
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                   // Page= "http://interedes.co/",
                    HtmlContent = await templatePdfGeneral.GetElementosHTMLString(cables,_hostingEnvironment.WebRootPath,_mapper),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "bootstrap.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = false,Spacing=1 },
                    //FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, HtmUrl =Path.Combine(Directory.GetCurrentDirectory(), "assets", "footer/footer.html"), Left = ""  }
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false,Center="" }
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

            return File(file, "application/pdf", "Formato_Inventario_Detallado.pdf");
            ///return Ok(response);
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
}