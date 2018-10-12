using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using pmacore_api.DataContext;
using pmacore_api.Models.datatake;
using pmacore_api.Models.datatake.equipos;
using pmacore_api.services;

namespace pmacore_api.Controllers.Pdfreport.PdfTin.datatake
{
    public class TemplatePdfDetallado
    {
         public async Task<string> GetElementosHTMLString(ResponseElementoCables response , String wwwroot,IMapper _mapper)
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


			var listGroup= response.List.GroupBy(a=> new {a.elemento_id, a.ciudad_id}).ToList();
            var countElements=0;
            foreach (var item in listGroup)
            {

                //region HEADER
                /*--------------------------------------------------------------------------------------------------------*/
                countElements++;

                
                 
                main.Append(@"<div class='formato'>");

                //Encabezado
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
											</table>", image, dateNow, response.List.FirstOrDefault().nombre_proyecto);

                                          

                //show totales for first page
                if(countElements==1){

										    var totalPostes= response.List.GroupBy(a=>a.elemento_id).Count();
											long ocupacion_total=response.List.AsEnumerable().Sum(a=>a.cantidad_cable);
										
											var longitudes= response.List.GroupBy(a=>a.longitud).ToList();


                                            string combindedString = string.Join( ",", response.List.GroupBy(a=>a.ciudad_id).Select(a=>a.FirstOrDefault().ciudad).ToArray() );
                                            main.AppendFormat(@"<table >
																		<tr >
																			<th width='5%'  rowspan='2'>Ciudad:</th>
																			<td width='20%' align='center'  rowspan='2'>{0}</td>
																			<td width='10%'>Postes</td>
																			<td width='10%' align='center'>{1}</td>
																			<td width='20%' rowspan='2'>Ocupaciones Long.</td>
																			<td width='10%' rowspan='2' 
																			>",combindedString,totalPostes);
											var lonOcupaciones=0;
											foreach (var longitud in longitudes)
											{
												lonOcupaciones++;
												if(lonOcupaciones>1){
													var totallongitud= response.List.Where(
														a=>a.longitud==longitud.FirstOrDefault().longitud).AsEnumerable().Sum(a=>a.cantidad_cable);
												    main.AppendFormat(@"<br>{0} m:   {1}",longitud.FirstOrDefault().longitud,totallongitud);
												}else{
													var totallongitud= response.List.Where(
														a=>a.longitud==longitud.FirstOrDefault().longitud).AsEnumerable().Sum(a=>a.cantidad_cable);
												    main.AppendFormat(@"{0} m:   {1}",longitud.FirstOrDefault().longitud,totallongitud);
												}
											}

                                            //Elementos
											main.AppendFormat(@"</td>
                                                                <td width='15%' rowspan='2'>Elemento Long.</td>
																<td width='10%' rowspan='2'>");

                                            var lonEleemntos=0;
											foreach (var longitudelemento in longitudes)
											{
												lonEleemntos++;
												if(lonEleemntos>1){
													var totallongitudElemento= response.List.Where(
														a=>a.longitud==longitudelemento.FirstOrDefault().longitud).GroupBy(a=>a.elemento_id).Count();
												    main.AppendFormat(@"<br>{0} m:   {1}",longitudelemento.FirstOrDefault().longitud,totallongitudElemento);
												}else{
													var totallongitudElemento= response.List.Where(
														a=>a.longitud==longitudelemento.FirstOrDefault().longitud).GroupBy(a=>a.elemento_id).Count();
												    main.AppendFormat(@"{0} m:   {1}",longitudelemento.FirstOrDefault().longitud,totallongitudElemento);
												}
											}                  

                                            main.AppendFormat(@"
                                            </td>
																		</tr>
																		<tr>
																			<td width='10%'>Ocupaciones:</td>
																			<td width='10%' align='center'>{1}</td>
																		</tr>
																		</table>", "Todas las empresas",ocupacion_total);
                }

               

                //region BODY
                /*--------------------------------------------------------------------------------------------------------*/
                main.AppendFormat(@"
                <table border='1' align='center' bordercolor='#C6C6C6'>	
                                <tr class='title_table_orange'   align='center'><td colspan='11' width='100 %'  align='center'>DETALLE POSTE ({11})</td></tr>											
					            <tr class='title_table_bold'  align='center'>
							    <td width='5%'  align='center'>Numero Apoyo</td>
								<td width='10%'  align='center'>Codigo Apoyo</td>
								<td width='5%'  align='center'>Long. Poste</td>
								<td width='10%' align='center'>Estado</td>
								<td width='5%' align='center'>Nivel Tension</td>
								<td width='10%' align='center'>Altura Disponible</td>
								<td width='10%' align='center'>Resistencia Mecanica</td>
								<td width='10%' align='center'>Material</td>
								<td width='10%' align='center'>Retenidas</td>
								<td width='10%' align='center'>Direccion</td>
								<td width='20%' align='center'>Coordenadas</td>
								</tr>
								<tr  align='center'>
								    <td width='5%' align='center'>{0}</td>
									<td width='5%' align='center'>{1}</td>
									<td width='5%' align='center'>{2}</td>
									<td width='10%' align='center'>{3}</td>
									<td width='10%' align='center'>{4}</td>
									<td width='10%' align='center'>{5}</td>
									<td width='10%' align='center'>{6}</td>
									<td width='10%' align='center'>{7}</td>
									<td width='10%' align='center'>{8}</td>
									<td width='10%' align='center'>{9}</td>
									<td width='20%' align='center'>{10}</td>
								</tr>
								</table>",item.FirstOrDefault().elemento_id,item.FirstOrDefault().codigoapoyo, item.FirstOrDefault().longitud,
                                item.FirstOrDefault().nombre_estado, item.FirstOrDefault().valor_nivel_tension,item.FirstOrDefault().alturadisponible,
                                item.FirstOrDefault().resistenciamecanica,item.FirstOrDefault().nombre_material, item.FirstOrDefault().retenidas,
                                item.FirstOrDefault().direccion_elemento,item.FirstOrDefault().coordenadas_elemento,
                                item.FirstOrDefault().ciudad);

                //CABLES
                /*--------------------------------------------------------------------------------------------------------*/ 
                 main.AppendFormat(@"<table border='1' align='center' bordercolor='#C6C6C6'>
                                    <tr class='title_table_orange'   align='center'>
                                        <td colspan='7' width='100 %'  align='center'>DETALLE CABLES</td>
                                    </tr>						
									<tr align='center' class='title_table_bold'>
                                        <td  align='center'>Num.</td>
                                        <td  align='center'>Operador</td>
                                        <td  align='center'>Tipo</td>
                                        <td  align='center'>Detalle</td>
                                        <td  align='center'>Nivel Ocupacion</td>
                                        <td  align='center'>Esta el cable sobre RBT ?</td>
                                        <td  align='center'>Cable cuenta con marquilla ?</td>
									</tr>");

               
                 var listCables=response.List.Where(a=>a.elemento_id==item.FirstOrDefault().elemento_id).ToList();
                 var countCables=0;
                 foreach (var itemCables in listCables)
                 {
                     countCables++;
                      main.AppendFormat(@"					
							<tr align='center'>
                                    <td  align='center'>{0}</td>
                                    <td  align='center'>{1}</td>
                                    <td  align='center'>{2}</td>
                                    <td  align='center'>{3}</td>
                                    <td  align='center'>{4}</td>
                                    <td  align='center'>{5}</td>
                                    <td  align='center'>{6}</td>
							</tr>",countCables,itemCables.nombre_empresa,itemCables.nombre_tipo_cable,itemCables.nombre_cable,
                            itemCables.cantidad_cable,itemCables.sobrerbt,itemCables.tiene_marquilla);
                 }

				 main.Append(@"</table>");
                 

                //EQUIPOS 
                /*--------------------------------------------------------------------------------------------------------*/

                var dataContext = MyAppContext.GetInstance();

                var equipos= dataContext.ViewEquipos.Where(a=>a.numero_apoyo==item.FirstOrDefault().elemento_id).ToList();
                //var equiposMap= 

                var equiposMap = _mapper.Map<IEnumerable<ViewEquipos>, IEnumerable<ViewEquiposMap>>(equipos).ToList();
                

                 main.AppendFormat(@"<table border='1' align='center' bordercolor='#C6C6C6'>
                                    <tr class='title_table_orange'   align='center'>
                                        <td colspan='5' width='100 %'  align='center'>DETALLE EQUIPOS</td>
                                    </tr>						
									<tr align='center' class='title_table_bold'>
                                        <td  align='center'>Num.</td>
                                        <td  align='center'>Operador</td>
                                        <td  align='center'>Tipo</td>
                                        <td  align='center'>Conectado red Electrica ?</td>
                                        <td  align='center'>Medidor</td>
									</tr>");
                    var countEquipos= 0;      
                    if(equiposMap.Count>0){
                        foreach (var itemEquipos in equiposMap)
                        {
                            countEquipos++;
                            main.AppendFormat(@"					
                                <tr align='center'>
                                        <td  align='center'>{0}</td>
                                        <td  align='center'>{1}</td>
                                        <td  align='center'>{2}</td>
                                        <td  align='center'>{3}</td>
                                        <td  align='center'>{4}</td>
                                </tr>",countEquipos,itemEquipos.empresa_nombre,
                                itemEquipos.tipo_equipo,itemEquipos.ConectadoRbt,
                                itemEquipos.MedidorBt);
                        }  
                    }else{
                            main.AppendFormat(@"					
                                <tr  align='center'>
                                       <td colspan='5' width='100 %'  align='center'>Sin equipos</td>
                                </tr>","");
                    }
                    main.Append(@"</table><br>");


                    var fotos= dataContext.Fotos.Where(a=>a.Elemento_Id==item.FirstOrDefault().elemento_id).ToList();

                    main.Append(@"<table>
							<tr class='title_table_orange' >
								<td align='center'>
									FOTOS</td>
							</tr>
					</table>");

                    var  RouteFotoRepository="http://181.60.56.39:89";
					var countfotos=fotos.Count;
					var recuadro_empty= Path.Combine(wwwroot, "Images","recuaadro.png");

                    var take=4;
                    var skip=0;
                    main.Append(@"<table>");
                    if(countfotos<=take){
                         main.Append(@"<tr>");	
                                foreach(var foto in fotos.OrderBy(a=>a.Id))
                                {
                                    if(foto.Ruta.ToUpper().Contains("Foto Nula".ToUpper())){
                                            foto.Ruta="/Images/recuaadro.png";
                                    }else if(foto.Ruta.ToUpper().Contains("/Fotos1".ToUpper())){
                                            string replaceFoto =foto.Ruta.Replace("/Fotos1", "");
                                            foto.Ruta=replaceFoto;
                                    }
                                    main.AppendFormat("<td align='center'><img  width='140'  src='{0}{1}' /><p class='title_table_orange'>Titulo: {2}</p><p>Descripcion: {3}</p></td>",RouteFotoRepository,foto.Ruta,foto.Titulo,foto.Descripcion);
                                }
                        main.Append(@"</tr>");
                    }else{


                        double promediopaginas= ((double)countfotos/take);
                        int promedioint = (int)Math.Ceiling(promediopaginas / 1) * 1;
                        for (int t = 0; t < promedioint; t++)
                            {
                                var paginates=fotos.Page(skip, take).ToList();
                                var list =fotos.OrderBy(a=>a.Id).Skip(skip).Take(take).ToList();
                                main.Append(@"<tr>");	
                                foreach(var foto in list)
                                {
                                    if(foto.Ruta.ToUpper().Contains("Foto Nula".ToUpper())){
                                            foto.Ruta="/Images/recuaadro.png";
                                    }else if(foto.Ruta.ToUpper().Contains("/Fotos1".ToUpper())){
                                            string replaceFoto =foto.Ruta.Replace("/Fotos1", "");
                                            foto.Ruta=replaceFoto;
                                    }
                                    main.AppendFormat("<td align='center'><img  width='140'  src='{0}{1}' /><p class='title_table_orange'>{2}</p><p> {3}</p></td>",RouteFotoRepository,foto.Ruta,foto.Titulo,foto.Descripcion);
                                }
                                main.Append(@"</tr>");
                                skip=list.Count();
                        }
                        
                    }
                    main.Append(@"</table>");
                   
                    
                    /* 
                    main.AppendFormat(@"<div class='row'>");
                    foreach (var foto in fotos)
                    {
                         main.AppendFormat(@"<div class='col-xs-6 col-md-3'>
                        <div class='thumbnail'>
                        <img src='...' alt='...'>
                        <div class='caption'>
                            <h3>Thumbnail label</h3>
                            <p>...</p>
                            <p><a href='#' class='btn btn-primary' role='button'>Button</a> <a href='#' class='btn btn-default' role='button'>Button</a></p>
                        </div>
                        </div>
                    </div>");
                    }
                    main.AppendFormat(@"</div>");
                    */
              
                 ///var equiposContext= dataContext.Equipos.Where(a=>a.Elemento_Id==item.FirstOrDefault().elemento_id).Join().ToList();
                 /* 
                var listEquipos = new ResponseEquipos();
                var prefix = "";
                var controller = "";
                var URL_BASE = "";
                prefix = "odata/datatakedev/";
                controller = string.Format("{0}", string.Format("EquipoElementos?$expand=TipoEquipo,UbicacionEmpresa($expand=Empresa, Ubicacion)&$filter=Elemento_Id eq {0}",item.FirstOrDefault().elemento_id));
                var route = RouteService.GetInstance();
                URL_BASE = route.RouteBaseAddress;

                var responseEquipos = await ApiService.GetList<ResponseFormatEquipos>(URL_BASE, prefix, controller);

                if (responseEquipos.IsSuccess)
                {
                    var equipos = (ResponseFormatEquipos)responseEquipos.Result;
                 
                   
                }else{
                    main.AppendFormat(@"<table border='1' align='center' bordercolor='#C6C6C6'>
                                    <tr class='title_table_orange'   align='center'>
                                        <td colspan='7' width='100 %'  align='center'>NO SE CARGARON LOS EQUIPOS</td>
                                    </tr>						
									");
                    main.Append(@"</table>");
                }
                */

                main.Append(@"
                </div>");
            }

            main.Append(@"</body>
                        </html>");

            return main.ToString();
        }

    }
}