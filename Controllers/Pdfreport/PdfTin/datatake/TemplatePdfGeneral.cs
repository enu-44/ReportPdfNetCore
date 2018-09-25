using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using pmacore_api.Models.datatake;

namespace pmacore_api.Controllers.Pdfreport.PdfTin.datatake
{
    public class TemplatePdfGeneral
    {
        public async Task<string> GetHTMLString(ResponseElementoCables response , String wwwroot)
        {

            var encabezado = Path.Combine(wwwroot, "Images", "logo_electrohuila.png");
            var image = string.Format("<img  width='130'  src='{0}' />", encabezado);
            var dateNow = DateTime.Now.ToString("MM/dd/yyyy");
            //var image = string.Format("<img  width='40'  src='{0}' />", encabezado);


            var main = new StringBuilder();
            main.Append(@"<html>
            
            <head>
            <meta charset='utf-8'>
            </head>
            <body>");


           




            foreach (var item in response.List)
            {
                

               
                main.Append(@"
                <div class='formato'>

                ");

                main.AppendFormat(@"<table style='width: 100%;font-size:7pt;font-family:tahoma;'>
													<tr>
													<td rowspan='2' align='center'> 
															{0}
													</td>
													<td colspan='2' align='center'>
															<span style='padding:0px !important; color:#e26912; font-weight: bold;'>  
															INVENTARIO CABLE OPERADORES 
															</span>
													</td>
													</tr>
													<tr>
													<td> 
															<span style='color:#e26912; font-weight: bold;'>  FECHA(DD/MM/AAAA):  </span>{1}<br />
															<span style='color:#e26912; font-weight: bold;'>  CIUDAD DE EJECUCUION:  </span><br />
															<span style='color:#e26912; font-weight: bold;'>  PROYECTO:  </span>{2}<br />
													</td>
													<td> 
															<span style='color:#e26912; font-weight: bold;'> EMPRESA OPERADORA  </span><br />
															<span style='color:#e26912; font-weight: bold;'>  ORDEN DE TRABAJO  </span>01<br />
															<span style='color:#e26912; font-weight: bold;'>  VERSION:  </span>01<br />
													</td>
													</tr>
											</table>", image, dateNow, item.nombre_proyecto);


                main.Append(@"
                </div>");
            }

            main.Append(@"</body>
                        </html>");

            return main.ToString();
        }

    }
}