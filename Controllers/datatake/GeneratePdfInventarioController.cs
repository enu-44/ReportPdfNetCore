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

namespace pmacore_api.Controllers
{
    [Route("api/[controller]")]
    public class GeneratePdfInventarioController:Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConverter _converter;
        public GeneratePdfInventarioController(  IHostingEnvironment hostingEnvironment,IConverter converter){
            _hostingEnvironment = hostingEnvironment;
             _converter = converter;
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
        [Route("GetPdfExampleCables")]
        public async Task<ActionResult> GetPdfAutorizacionExample()
        {
            var listElementosCables = new ResponseElementoCables();
            var prefix = "";
            var controller = "";
            var URL_BASE = "";
            prefix = "odata/datatakedev/";
            controller = string.Format("{0}", "Viewfromcablesms?$top=2&$skip=0&$count=true");
            var route = RouteService.GetInstance();
            URL_BASE = route.RouteBaseAddress;

            var response = await ApiService.GetList<ResponseElementoCables>(URL_BASE, prefix, controller);

            if (!response.IsSuccess)
            {
                return Ok(response);
            }

            var cables = (ResponseElementoCables)response.Result;
            return Ok(response);
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