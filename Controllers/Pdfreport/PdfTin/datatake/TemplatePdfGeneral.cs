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
            var image = string.Format("<img  width='170'  src='{0}' />", encabezado);
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

                main.AppendFormat(@"<table >
													<tr>
													<td rowspan='2' align='center'  width='25%'> 
															{0}
													</td>
													<td colspan='2' align='center'>
															<span class='title'>  
															INVENTARIO CABLE OPERADORES 
															</span>
													</td>
													</tr>
													<tr>
													<td align='left' width='50%'> 
															<span class='title'>  FECHA(DD/MM/AAAA):  </span>{1}<br />
															<span class='title'>  CIUDAD DE EJECUCUION:  </span><br />
															<span class='title'>  PROYECTO:  </span>{2}<br />
													</td>
													<td align='left'  width='25%'> 
															<span class='title'>   EMPRESA OPERADORA  </span><br />
															<span class='title'>   ORDEN DE TRABAJO  </span>01<br />
															<span class='title'>   VERSION:  </span>01<br />
													</td>
													</tr>
											</table>", image, dateNow, item.nombre_proyecto);

                main.AppendFormat(@"<p></p><table>
																		<tr >
																			<th width='10%'><strong>Ciudad:</strong></th>
																			<td width='20%' align='center'>{0}</td>
																			<td width='10%'><strong>Postes</strong></td>
																			<td width='20%' align='center'>{1}</td>
																			<td width='20%' rowspan='2'><strong>Ocupaciones por longitud</strong></td>
																			<td width='20%' rowspan='2' 
																			>
																			<strong>6 m:  </strong> {4}
																			<br><strong>8 m:  </strong> {5}
																			<br><strong>10 m:  </strong> {6}
																			<br> <strong>12 m: </strong> {7}
																			<br> <strong>14 m: </strong> {8} 
																			<br> <strong>16 m: </strong> {9} 
																			</td>
																		</tr>
																		<tr>
																			<td width='10%'><strong>Operador:</strong></td>
																			<td width='20%' align='center'>{2}</td>
																			<td width='10%'><strong>Ocupaciones:</strong></td>
																			<td width='10%' align='center'>{3}</td>
																	
																		</tr>
																		</table>", 1,2,3,4,5,6,7,8,9,10);


                                                                        main.AppendFormat(@"
                                                                        <table style='width: 100%;font-size:7pt;font-family:tahoma;'>
																		    <tr align='center'>
                                                                                <th ><strong>#</strong></th>
                                                                                <th ><strong>Numero apoyo</strong></th>
                                                                                <th ><strong>Codigo Apoyo</strong></th>
                                                                                <th ><strong>Long. Poste</strong></th>
                                                                                <th ><strong>Estado</strong></th>
                                                                                <th ><strong>Nivel Tension</strong></th>
                                                                                <th ><strong>Altura Disponible</strong></th>
                                                                                <th ><strong>Resistencia Mecanica</strong></th>
                                                                                <th ><strong>Material</strong></th>
                                                                                <th ><strong>Retenidas</strong></th>
                                                                                <th ><strong>Direccion</strong></th>
                                                                                <th ><strong>Cable</strong></th>
                                                                                <th ><strong>Ocupaciones</strong></th>
                                                                                <th ><strong>Coordenadas</strong></th>
																			</tr>
																		</table>");





                main.Append(@"
                </div>");
            }

            main.Append(@"</body>
                        </html>");

            return main.ToString();
        }

    }
}