using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            main.Append(@"
			<html>
            
            <head>
            <meta charset='utf-8'>
            </head>
            <body>");



			var listGroup= response.List.GroupBy(a=> new {a.empresa_id,a.ciudad_id}).ToList();

            foreach (var item in listGroup)
            {
            
                main.Append(@"<div class='formato'>");
                main.AppendFormat(@"<table>
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
											</table>", image, dateNow, item.FirstOrDefault().nombre_proyecto);

										    var totalPostes= response.List.Where(a=>a.ciudad_id==item.FirstOrDefault().ciudad_id && a.empresa_id==item.FirstOrDefault().empresa_id).GroupBy(a=>a.elemento_id).Count();
											long ocupacion_total=response.List.Where(a=>a.ciudad_id==item.FirstOrDefault().ciudad_id && a.empresa_id==item.FirstOrDefault().empresa_id).AsEnumerable().Sum(a=>a.cantidad_cable);
											
											var longitudes= response.List.GroupBy(a=>a.longitud).ToList();

										
                                            main.AppendFormat(@"<table >
																		<tr >
																			<th width='10%'><strong>Ciudad:</strong></th>
																			<td width='20%' align='center'>{0}</td>
																			<td width='10%'><strong>Postes</strong></td>
																			<td width='20%' align='center'>{1}</td>
																			<td width='20%' rowspan='2'><strong>Ocupaciones por long.</strong></td>
																			<td width='20%' rowspan='2' 
																			>",item.FirstOrDefault().ciudad,totalPostes);
											var lon=0;
											foreach (var longitud in longitudes)
											{
												lon++;
												if(lon>1){
													var totallongitud= response.List.Where(
														a=>a.ciudad_id==item.FirstOrDefault().ciudad_id && a.empresa_id==item.FirstOrDefault().empresa_id &&
														a.longitud==longitud.FirstOrDefault().longitud).AsEnumerable().Sum(a=>a.cantidad_cable);
												    main.AppendFormat(@"<br><strong>{0} m:  </strong> {1}",longitud.FirstOrDefault().longitud,totallongitud);
												}else{
													var totallongitud= response.List.Where(
														a=>a.ciudad_id==item.FirstOrDefault().ciudad_id && a.empresa_id==item.FirstOrDefault().empresa_id &&
														a.longitud==longitud.FirstOrDefault().longitud).AsEnumerable().Sum(a=>a.cantidad_cable);
												    main.AppendFormat(@"<strong>{0} m:  </strong> {1}",longitud.FirstOrDefault().longitud,totallongitud);
												}
											}

											main.AppendFormat(@"			</td>
																		</tr>
																		<tr>
																			<td width='10%'><strong>Operador:</strong></td>
																			<td width='20%' align='center'>{0}</td>
																			<td width='10%'><strong>Ocupaciones:</strong></td>
																			<td width='10%' align='center'>{1}</td>
																	
																		</tr>
																		</table>", item.FirstOrDefault().nombre_empresa,ocupacion_total);


                                                                        main.AppendFormat(@"
                                                                        <table  border='1' align='center' bordercolor='#C6C6C6'>
																		    <tr class='title_table_bold'  align='center'>
                                                                                <th align='center'>#</th>
                                                                                <th align='center'>Numero apoyo</th>
                                                                                <th align='center'>Codigo Apoyo</th>
                                                                                <th align='center'>Long. Poste</th>
                                                                                <th align='center'>Estado</th>
                                                                                <th align='center'>Nivel Tension</th>
                                                                                <th align='center'>Altura Disponible</th>
                                                                                <th align='center'>Resistencia Mecanica</th>
                                                                                <th align='center'>Material</th>
                                                                                <th align='center'>Retenidas</th>
                                                                                <th align='center'>Direccion</th>
                                                                                <th align='center'>Cable</th>
                                                                                <th align='center'>Ocupaciones</th>
                                                                                <th align='center'>Coordenadas</th>
																			</tr>
																		");


																		var listCables=  response.List.Where(
																			a=>a.ciudad_id==item.FirstOrDefault().ciudad_id && 
																			a.empresa_id==item.FirstOrDefault().empresa_id).ToList();
																		var count= 0;
																		foreach (var cable in listCables)
																		{
																		count++;
																		main.AppendFormat(@"
                                                                       
																		    <tr >
                                                                                <td class='content_table' align='center'>{0}</td>
                                                                                <td class='content_table' align='center'>{1}</td>
                                                                                <td class='content_table' align='center'>{2}</td>
                                                                                <td class='content_table' align='center'>{3}</td>
                                                                                <td class='content_table' align='center'>{4}</td>
                                                                                <td class='content_table' align='center'>{5}</td>
                                                                                <td class='content_table' align='center'>{6}</td>
                                                                                <td class='content_table' align='center'>{7}</td>
                                                                                <td class='content_table' align='center'>{8}</td>
                                                                                <td class='content_table' align='center'>{9}</td>
                                                                                <td class='content_table' align='center'>{10}</td>
                                                                                <td class='content_table' align='center'>{11}</td>
                                                                                <td class='content_table' align='center'>{12}</td>
                                                                                <td class='content_table' align='center'>{13}</td>
																			</tr>
																		",count,cable.elemento_id,cable.codigoapoyo,cable.longitud,cable.nombre_estado,
																		cable.valor_nivel_tension,cable.alturadisponible, cable.resistenciamecanica,cable.nombre_material,
																		cable.retenidas,cable.direccion_elemento,cable.nombre_cable,cable.cantidad_cable,cable.coordenadas_elemento);
																			
																		}

																		 main.Append(@"
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