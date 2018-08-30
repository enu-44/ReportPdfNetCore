using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using pmacore_api.Models;

using System.ComponentModel;
using System.Globalization;

using iTextSharp.text;

namespace pmacore_api.Controllers.Pdfreport
{
   
    public class LegalizacionPdfReport
    {
        public static byte[] CreateInMemoryPdfReport(string wwwroot, List<DataRequest> listData,ResponseApiPma requestData)
        {
            return CreateHtmlHeaderPdfReport(wwwroot,listData,requestData).GenerateAsByteArray(); // creating an in-memory PDF file
        }
       
        public static IPdfReportData CreateHtmlHeaderPdfReportStream(string wwwroot, Stream stream, List<DataRequest> listData,ResponseApiPma requestData)
        {
            return CreateHtmlHeaderPdfReport(wwwroot,listData,requestData).Generate(data => data.AsPdfStream(stream, closeStream: false));
        }

        public  static PdfReport CreateHtmlHeaderPdfReport(String wwwroot, List<DataRequest> listData,ResponseApiPma requestData)
		{
			/*var margin= new DocumentMargins();
			margin.Bottom=2;
			margin.Left=3;
			margin.Right=3;
			margin.Top=2;*/
		
			return new PdfReport().DocumentPreferences(doc =>
			{
				doc.RunDirection(PdfRunDirection.LeftToRight);
				 //     Sets the new document's margins. Its predefined values are Bottom = 60, Left
                //     = 36, Right = 36, Top = 36.
		
				//doc.DocumentMargins(margin );
				//doc.Orientation(PageOrientation.Portrait);
				doc.Orientation(PageOrientation.Landscape);
				doc.PageSize(PdfPageSize.A4);
				doc.DocumentMetadata(new DocumentMetadata { Author = "PMA", Application = "PMA REPORT", Keywords = "Fomato Atorizacion", Subject = "Atorizacion Viaticos", Title = "Fomato Atorizacion" });
				doc.Compression(new CompressionSettings
				{
					EnableCompression = true,
					EnableFullCompression = true
				});
			})
			 .DefaultFonts(fonts =>
			 {
				// fonts.Path(TestUtils.GetVerdanaFontPath(),
                           // TestUtils.GetTahomaFontPath());
				 //fonts.Size(9);
				 ///fonts.Color(System.Drawing.Color.Black);
				 fonts.Path(Path.Combine(wwwroot, "fonts", "verdana.ttf"),
                        Path.Combine(wwwroot, "fonts", "tahoma.ttf"));
				 fonts.Size(5);
				 fonts.Color(System.Drawing.Color.White);
			 })
			 
			 .PagesFooter(footer =>
			 {
				 footer.HtmlFooter(rptFooter =>
				 {
					 rptFooter.PageFooterProperties(new FooterBasicProperties
					 {
						 RunDirection = PdfRunDirection.LeftToRight,
						 ShowBorder = true,
						 PdfFont = footer.PdfFont,
						 TotalPagesCountTemplateHeight = 50,
						 TotalPagesCountTemplateWidth = 100
					 });
					 rptFooter.AddPageFooter(pageFooter =>
					 {
						 var email = requestData.Email;
						 return string.Format(@"<table style='font-size:7.5pt;font-family:tahoma;'>
														<tr>
															<td align='center'>
															<p style='text-align: center;'>Nota de propiedad: Los derechos de propiedad intelectual sobre este documento y su contenido le pertenecen exclusivamente al CONSORCIO PIPELINE MAINTENANCE ALLIANCE(PMA).
															 Por lo tanto queda estrictamente prohibido el uso divulgaci&oacute;n, distribuci&oacute;n, reproducci&oacute;n, modificaci&oacute;n y/o alteraci&oacute;n de los mencionados derechos, con fines distintos a los previstos en este documento,
															  sin la autorizaci&oacute;n previa y escrita del consorcio.</p>
															  <p style='text-align:right; margin:0px'>Generado por <strong>{0}</strong> el Fecha a las Hora</p></td>
														 </tr>
												</table>", email);

					 });
				 });
			 })
			 .PagesHeader(header =>
			 {
				 header.HtmlHeader(rptHeader =>
				 {
					 rptHeader.PageHeaderProperties(new HeaderBasicProperties
					 {
						 TableWidthPercentage=100,
						 RunDirection = PdfRunDirection.LeftToRight,
						 ShowBorder = false,
                         PdfFont = header.PdfFont
                     });
					 rptHeader.AddPageHeader(pageHeader =>
					 {
						var headerr= "";
						return string.Format(@"{0}",headerr);
					 });

					 rptHeader.GroupHeaderProperties(new HeaderBasicProperties
					 {
						 RunDirection = PdfRunDirection.LeftToRight,
						 ShowBorder = true,
						 SpacingBeforeTable = 1f,
                         PdfFont = header.PdfFont,
						 TableWidthPercentage=100
                     });
					 
					 rptHeader.AddGroupHeader(groupHeader =>
					 {
						var dataGroup = groupHeader.NewGroupInfo;

						var message = "Grouping employees by department and age.";
					    
						
						//var dateNow = DateTime.Now.ToString("MM/dd/yyyy");
						var dateNow = "12/12/2018";
						//var encabezado= TestUtils.GetImagePath("logo_electrohuila.png");
						
						var encabezado= Path.Combine(wwwroot, "Images","_pma.png");
						var image = string.Format("<img  width='40'  src='{0}' />", encabezado);

						

						var consecutivo = dataGroup.GetSafeStringValueOf<DataRequest>(x => x.Consecutivo);
						
						var detalle_data= listData.Where(a=>a.Consecutivo==consecutivo).FirstOrDefault();

					

					var cliente = dataGroup.GetSafeStringValueOf<DataRequest>(x => x.Cliente);


						var currencyFormat  = Convert.ToDouble(detalle_data.SumValor).ToString();
						///var msg = long.Parse(currencyFormat, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.English);
						var string_valor= string.Format(" {0}{1}",ConvertToWords(currencyFormat)," Pesos MCT");

					
					    var footer=string.Format(@"<tr>
												<td align='center' colspan='11'>
												<p style='text-align: center; font-size:6px;'>Nota de propiedad: Los derechos de propiedad intelectual sobre este documento y su contenido le pertenecen exclusivamente al CONSORCIO PIPELINE MAINTENANCE ALLIANCE(PMA).
															 Por lo tanto queda estrictamente prohibido el uso divulgaci&oacute;n, distribuci&oacute;n, reproducci&oacute;n, modificaci&oacute;n y/o alteraci&oacute;n de los mencionados derechos, con fines distintos a los previstos en este documento,
															  sin la autorizaci&oacute;n previa y escrita del consorcio.</p>
												<p style='text-align:right; margin:0px;  font-size:6px;'>Generado por <strong>{0}</strong> el Fecha a las Hora</p></td>
											</tr>",requestData.Email);
								
						var table_totals= string.Format(@"
								<tr style='text-align: center;'>
									<td colspan='14'><br></td>
									
								</tr>
								<tr style='text-align: center;'>
									<td style='width: 78.6667px; font-size: 11px;'>Son:</td>
									<td style='width: 78.6667px; font-size: 11px;' colspan='7'>{5}</td>
                                    <td style='width: 78.6667px; font-size: 11px;' colspan='3'>Total legalizacion Viaticos PMA</td>
									<td style='width: 78.6667px; font-size: 11px;' colspan='3'>{6}</td>
								</tr>
                                <tr style='text-align: center;'>
									<td style='width: 78.6667px; font-size: 11px;'>Son:</td>
									<td style='width: 78.6667px; font-size: 11px;' colspan='7'>{7}</td>
                                    <td style='width: 78.6667px; font-size: 11px;' colspan='3'>Total legalizacion Viaticos Trabajador</td>
									<td style='width: 78.6667px; font-size: 11px;' colspan='3'>{8}</td>
								</tr>







                                 
            <tr style='text-align: center;'>
				<td style='font-size: 11px;' colspan='4'>Observaciones:</td>
				<td style='font-size: 11px;' colspan='10'>
                    <table   style='width: 100%;'>
                            <tr>
                                <td style='font-size: 6px;' bordercolor='#B2B2B2'>Pernocta/Retorna</td>
                                <td style='font-size: 6px;' bordercolor='#B2B2B2'>No. Dias</td>
                                <td style=' font-size: 6px;' bordercolor='#B2B2B2'>Valor Anticipo</td>
                                <td style=' font-size: 6px;' bordercolor='#B2B2B2'>Valor Total</td>
                                <td style=' font-size: 6px;' bordercolor='#B2B2B2'>Valor Pagado</td>
                            </tr>
                    </table>
                </td>
			</tr>










								<tr style='text-align: center;'>
									<td style='width: 78.6667px; font-size: 10px; vertical-align: top;' colspan='14'>
										<p>Importante:</p>
										<p>*Autorizo a la empresa para descontar de mis salarios y prestaciones sociales el valor de los vi&aacute;ticos, anticipos para gastos y pasajes recibidos para esta comisi&oacute;n, en caso de no presentar la legalizaci&oacute;n correspondiente dentro de los cinco (5) d&iacute;as h&aacute;biles al t&eacute;rmino de la comisi&oacute;n.</p>
									</td>
								</tr>
								<tr style='text-align: center;'>
									<td style='width: 78.6667px; font-size: 10px; color: #ff0000;' colspan='14'>*Manifiesto que he le&iacute;do y entendido en su integridad el presente formato, por lo cual firmo en calidad de aceptaci&oacute;n</td>
								</tr>
								<tr style='text-align: center;'>
												<td style='width: 78.6667px; font-size: 11px;' colspan='14'><br></td>
								</tr>
								<tr  style='text-align: center; height: 50px'>
										<td style='width: 78.6667px; font-size: 11px;' colspan='5'><br><br></td>
										<td style='width: 78.6667px; font-size: 11px;' colspan='5'><br><br></td>
										<td style='width: 78.6667px; font-size: 11px;' colspan='4'><br><br></td>
								</tr>
								<tr style='text-align: center'>
									<td style='width: 78.6667px; font-size: 5px'  colspan='5'>
													<p>FIRMA DEL EMPLEADO</p>
					                                <p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
									</td>
									<td style='width: 78.6667px; font-size: 5px;'  colspan='5'>
													<p>FIRMA DEL INGENIERO RESIDENTE</p>
					                                <p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
									</td>
									<td style='width: 78.6667px; font-size: 5px;'   colspan='4'>
													<p>FIRMA </p>
									</td>
								</tr>
								<tr style='text-align: center;'>
									<td style='width: 78.6667px; font-size: 10px;' colspan='14'>
										CON SU FIRMA EL TRABAJADOR CERTIFICA HABER RECIBIDO LOS VALOES INDICADOS EN ESTA AUTORIZACIÓN DE VIAJE
									</td>
								</tr>
								{9}	
								",detalle_data.SumAloj,
								detalle_data.SumAlim,
								detalle_data.SumMisc,
								detalle_data.SumTran,
								detalle_data.SumValor,
								string_valor,"","","",footer);



						var viaticos= string.Format(@"
																				
														<tr align='center' bgcolor='#48d267'>
															<td><strong>Fecha.</strong></td>
															<td><strong>Origen</strong></td>
															<td><strong>Destino</strong></td>
															<td><strong>Tarifa</strong></td>
															<td><strong>Orden de trabajo</strong></td>
															<td><strong>Estado</strong></td>
															<td><strong>Alojamiento</strong></td>
															<td><strong>Alimentacion</strong></td>
															<td><strong>Miscelaneos</strong></td>
															<td><strong>Transporte</strong></td>
                                                            <td><strong>Terminal</strong></td>
                                                            <td><strong>TiquetePor</strong></td>
                                                            <td><strong>Tiquete</strong></td>
															<td><strong>Valor</strong></td>
														</tr>");
														
														foreach(var item in detalle_data.Historias){
															viaticos += @"<tr  align='center'>
																		<td>"+item.Fecha+"</td>"+
																		"<td>"+item.Origen+"</td>"+
																		"<td>"+item.Destino+"</td>"+
																		"<td>"+item.Tarifa+"</td>"+
																		"<td>"+item.Orden+"</td>"+
																		"<td>"+item.Estado+"</td>"+
																		"<td>$ "+item.Alojamiento+"</td>"+
																		"<td>$ "+item.Alimentacion+"</td>"+
																		"<td>$ "+item.Miscelaneos+"</td>"+
																		"<td>$ "+item.Transporte+"</td>"+

                                                                        "<td>$ </td>"+
																		"<td></td>"+
																		"<td>$ </td>"+


																		"<td>$ "+item.Valor+"</td>"+
																		"</tr>";
															}
													viaticos += table_totals;
													//viaticos += @"</table>";

													
					var detalle_solicitud= string.Format(@"
						<table  border='1' style='border-collapse: collapse; width: 100%;font-size:7pt;'>
						
							<tr>
								<td align='center' colspan='14'> 
									<span style='color:#000000; font-weight: normal;'> MODELO  P135-GI-ADM-16-14-007 V1.0 (13/01/2017) </span><br>
								</td>
							</tr>
							<tr>
								<td align='center' bgcolor='#48d267'> 
									<span style='color:#000000; font-weight: normal;'> Proyecto  </span><br>
								</td>
								<td align='center' colspan='13'> 
									<span style='color:#000000; font-weight: normal;'> {0} </span><br>
								</td>
							</tr>

							<tr>
								<td align='center'  bgcolor='#48d267'> 
									<span style='color:#000000; background-color: #48d267; font-weight: normal;'> CLIENTE  </span><br>
								</td>
								<td align='center' colspan='5'> 
									<span style='color:#000000; font-weight: normal;'>  {1}</span><br>
								</td>
								<td align='center' bgcolor='#48d267'> 
									<span style='color:#000000; background-color: #48d267; font-weight: normal;'> CONTRATO  </span><br>
								</td>
								<td align='center' colspan='5'> 
									<span style='color:#000000; font-weight: normal;'> {2} </span><br>
								</td>
								<td align='center' bgcolor='#48d267'> 
									<span style='color:#000000; background-color: #48d267; font-weight: normal;'> FECHA  </span><br>
								</td>
								<td align='center' > 
									<span style='color:#000000; font-weight: normal;'> {3} </span><br>
								</td>
							</tr>

							<tr bgcolor='#48d267'>
								<td align='center'> 
									<span style='color:#000000; background-color: #48d267; font-weight: normal;'> CODIGO  </span><br>
								</td>
								<td align='center' colspan='5' > 
									<span style='color:#000000; font-weight: normal;'>  NOMBRE EMPLEADO</span><br>
								</td>
								<td align='center'> 
									<span style='color:#000000; background-color: #48d267; font-weight: normal;'> CEDULA  </span><br>
								</td>
								<td align='center'  colspan='3'> 
									<span style='color:#000000; font-weight: normal;'> CARGO </span><br>
								</td>
								<td align='center' colspan='3'> 
									<span style='color:#000000; background-color: #48d267; font-weight: normal;'> CECOS  </span><br>
								</td>
								<td align='center' > 
									<span style='color:#000000; font-weight: normal;'> SUCURSAL </span><br>
								</td>
							</tr>

							<tr>
								<td align='center'> 
									<span style='color:#000000; font-weight: normal;'> {4}  </span><br>
								</td>
								<td align='center' colspan='5' > 
									<span style='color:#000000; font-weight: normal;'>  {5}</span><br>
								</td>
								<td align='center'> 
									<span style='color:#000000; font-weight: normal;'> {6}  </span><br>
								</td>
								<td align='center' colspan='3'> 
									<span style='color:#000000; font-weight: normal;'> {7} </span><br>
								</td>
								<td align='center' colspan='3'> 
									<span style='color:#000000;font-weight: normal;'> {8}  </span><br>
								</td>
								<td align='center'> 
									<span style='color:#000000; font-weight: normal;'> {9} </span><br>
								</td>
							</tr>

							<tr bgcolor='#48d267'>
								<td align='center' colspan='12' > 
									<span style='color:#000000; font-weight: normal;'> OBJETO DE LA COMISION  </span><br>
								</td>
								<td align='center' > 
									<span style='color:#000000; font-weight: normal;'>  FECJA INICIO</span><br>
								</td>
								<td align='center'> 
									<span style='color:#000000; font-weight: normal;'> FECHA FIN  </span><br>
								</td>
							</tr>

							<tr>
								<td align='center' colspan='12' > 
									<span style='color:#000000; font-weight: normal;'> {10} </span><br>
								</td>
								<td align='center' > 
									<span style='color:#000000; font-weight: normal;'>  {11}</span><br>
								</td>
								<td align='center'> 
									<span style='color:#000000; font-weight: normal;'> {12}  </span><br>
								</td>
							</tr>

							 {13}
							 
						</table>

						",detalle_data.Cliente.Proyecto, 
						detalle_data.Cliente.Nombre, 
						detalle_data.Cliente.Contrato,
						detalle_data.Fecha,
						"",
						detalle_data.Empleado.Nombres+" "+detalle_data.Empleado.Apellidos,
						detalle_data.Empleado.CC,
						detalle_data.Empleado.Cargo,
						detalle_data.Empleado.CECO,
						detalle_data.Empleado.Sucursal,
						detalle_data.Objeto,
						detalle_data.FechaInicio,
						detalle_data.FechaFin,
						viaticos
						);


						/*var headerr= string.Format(@"
						<table  border='1' style='border-collapse: collapse; width: 100%;font-size:7pt;'>
						<tr>
							<td align='center' rowspan='3'>{0}</td>
							<td align='center' rowspan='3'><strong>FORMATO DE AUTORIZACION GASTOS DE VIAJE (VIATICOS)</strong></td>
							<td>Version 0.2
							
						
							</td>
						</tr>
					   	<tr >
							<td style='border: 1px solid black !important;'>
								{1}
							</td>
						</tr>
						<tr>
							<td bordercolor='#48d267'>
								{2}
							</td>
						</tr>
						
						</table>
						",image,requestData.Formato,consecutivo);*/

						var headerr= string.Format(@"
						<table  border='1' style='border-collapse: collapse; width: 100%;font-size:7pt;'>
					
						<tr>
							<td align='center' colspan='4' rowspan='3'>{0}</td>
							<td align='center' colspan='4' rowspan='3'><strong>FORMATO DE LEGALIZACION GASTOS DE VIAJE (VIATICOS)</strong></td>
							<td colspan='3'>
								<table   style='width: 100%;'>
									<tr>
										<td bordercolor='#B2B2B2'>Version 3.0</td>
									</tr>
									<tr>
										<td bordercolor='#B2B2B2'>{1}</td>
									</tr>
									<tr>
										<td bordercolor='#B2B2B2'>{2}</td>
									</tr>
								</table>
							</td>
						</tr>
						</table>
						",image,requestData.Formato,consecutivo);

						/*var headerr= string.Format(@"
						
 <table border='1'>
  <tr>
    <th>col 1</th>
    <th>col 2</th>
    <th>col 3</th>
    <th>col 4</th>
  </tr>
  <tr>
    <td rowspan='4' width='10%'>a</td>
    <td rowspan='2'>b</td>
    <td rowspan='2'>c</td>
    <td>f</td>
  </tr>
  <tr>
    <td>g</td>
  </tr>
  <tr>
    <td>h</td>
    <td>i</td>
    <td>j</td>
  </tr>
</table>
						");*/

					    // return string.Format(@"{0}",headerr);
						return string.Format(@"{0}{1}",headerr,detalle_solicitud);
						

							//return general;
					 });
				 });
			 })
			 
			 .MainTableTemplate(template =>
			 {
				 template.BasicTemplate(BasicTemplate.SilverTemplate);
			 })
			 .MainTablePreferences(table =>
			 {
				 table.ColumnsWidthsType(TableColumnWidthType.Relative);
				 table.GroupsPreferences(new GroupsPreferences
				 {
					 GroupType = GroupType.HideGroupingColumns,
					 
					 RepeatHeaderRowPerGroup = false,
					 ShowOneGroupPerPage = false,
					 
					 SpacingBeforeAllGroupsSummary = 5f,
					 NewGroupAvailableSpacingThreshold = 150
				 });

				//table.NumberOfDataRowsPerPage(35);
			 })
			 
			 .MainTableDataSource(dataSource =>
			 {
				
				
				/* var listOfRows = new List<Employee>();
				 var rnd = new Random();
				 for (int i = 0; i < 170; i++)
				 {
					 listOfRows.Add(
						 new Employee
						 {
							 Age = rnd.Next(25, 35),
							 Id = i + 1000,
							 Salary = rnd.Next(1000, 4000),
							 Name = "Employee " + i,
							 Department = "Department " + rnd.Next(1, 3)
						 });
				 }

				 listOfRows = listOfRows.OrderBy(x => x.Department).ThenBy(x => x.Age).ToList();
				 dataSource.StronglyTypedList(listOfRows);

				 */
				 

				 var listOfRows = new List<DataRequest>();
				 listOfRows=listData;
				 //listOfRows=reportElementos;
				 listOfRows = listOfRows.OrderBy(x => x.Consecutivo).ThenBy(x=>x.Objeto).ToList();
                 /// listOfRows = listOfRows.OrderBy(x => x.Department).ThenBy(x => x.Age).ToList();
				 dataSource.StronglyTypedList(listOfRows);

			 })
			 .MainTableSummarySettings(summarySettings =>
			 {
				// summarySettings.PreviousPageSummarySettings("Cont.");
				// summarySettings.OverallSummarySettings("Sum");
				// summarySettings.AllGroupsSummarySettings("Groups Sum");
			 })
			 .MainTableColumns(columns =>
			 {
				 columns.AddColumn(column =>
				 {
					 column.PropertyName("rowNo");
					 column.IsRowNumber(true);
					 column.CellsHorizontalAlignment(HorizontalAlignment.Center);
					 column.IsVisible(true);
					 column.Order(0);
					 column.Width(0);
					 column.HeaderCell("#");
				 });

				 columns.AddColumn(column =>
				 {
					  column.IsVisible(false);
					 column.PropertyName<DataRequest>(x => x.Consecutivo);
					 column.CellsHorizontalAlignment(HorizontalAlignment.Center);
					 column.Order(1);
					 column.Width(20);
					 column.HeaderCell("Consecutivo");
					 column.Group(
					 (val1, val2) =>
					 {
						 return val1.ToString() == val2.ToString();
					 });
				 });

				 columns.AddColumn(column =>
				 {
					  column.IsVisible(false);
					 column.PropertyName<DataRequest>(x => x.Objeto);
					 column.CellsHorizontalAlignment(HorizontalAlignment.Center);
					 column.Order(2);
					 column.Width(20);
					 column.HeaderCell("Objeto");
					 /*column.Group(
					 (val1, val2) =>
					 {
						 //return (int)val1 == (int)val2;
						 return val1.ToString() == val2.ToString();
					 });*/
				 });

				 columns.AddColumn(column =>
				 {
					  column.IsVisible(false);
					 column.PropertyName<DataRequest>(x => x.Fecha);
					 column.CellsHorizontalAlignment(HorizontalAlignment.Center);
					 column.Order(3);
					 column.Width(20);
					 column.HeaderCell("Fecha");
				 });
			 })
			 .MainTableEvents(events =>
			 {
                events.DataSourceIsEmpty(message: "داده ای جهت نمایش وجود ندارد.");
                events.CellCreated(args =>
                {
                    args.Cell.BasicProperties.CellPadding = 4f;
                });
                events.MainTableAdded(args =>
                {
                    var taxTable = new PdfGrid(3);  // Create a clone of the MainTable's structure
                    taxTable.RunDirection = 3;
                    taxTable.SetWidths(new float[] { 3, 3, 3 });
                    taxTable.WidthPercentage = 100f;
                    taxTable.SpacingBefore = 10f;

                    taxTable.AddSimpleRow(
                        (data, cellProperties) =>
                        {
                            data.Value = " Enuar";
                            cellProperties.ShowBorder = true;
                            cellProperties.PdfFont = args.PdfFont;
                        },
                        (data, cellProperties) =>
                        {
                            data.Value = " Muñoz";
                            cellProperties.ShowBorder = true;
                            cellProperties.PdfFont = args.PdfFont;
                        },
                        (data, cellProperties) =>
                        {
                            data.Value = " Castillo";
                            cellProperties.ShowBorder = true;
                            cellProperties.PdfFont = args.PdfFont;
                        });
                    args.PdfDoc.Add(taxTable);
                });


				 
				 //events.DataSourceIsEmpty(message: "There is no data available to display.");
			 })
			 .Export(export =>
			 {
				 export.ToExcel();
			 });
		}



		private static String ConvertWholeNumber(String Number)   
      {   
          string word = "";   
          try   
          {   
              bool beginsZero = false;//tests for 0XX   
              bool isDone = false;//test if already translated   
              double dblAmt = (Convert.ToDouble(Number));   
              //if ((dblAmt > 0) && number.StartsWith("0"))   
              if (dblAmt > 0)   
              {//test for zero or digit zero in a nuemric   
                  beginsZero = Number.StartsWith("0");   
   
                  int numDigits = Number.Length;   
                  int pos = 0;//store digit grouping   
                  String place = "";//digit grouping name:hundres,thousand,etc...   
                  switch (numDigits)   
                  {   
                      case 1://ones' range   
                          word = ones(Number);   
                          isDone = true;   
                          break;   
                      case 2://tens' range   
                          word = tens(Number);   
                          isDone = true;   
                          break;   
                      case 3://hundreds' range   
                          pos = (numDigits % 3) + 1;   
                          place = " Cien ";   
                          break;   
                      case 4://thousands' range   
                      case 5:   
                      case 6:   
                          pos = (numDigits % 4) + 1;   
                          place = " Mil ";   
                          break;   
                      case 7://millions' range   
                      case 8:   
                      case 9:   
                          pos = (numDigits % 7) + 1;   
                          place = " Millon ";   
                          break;   
                      case 10://Billions's range   
                      case 11:   
                      case 12:   
                         
                          pos = (numDigits % 10) + 1;   
                          place = " Billon ";   
                          break;   
                      //add extra case options for anything above Billion...   
                      default:   
                          isDone = true;   
                          break;   
                  }                      
                  if (!isDone)   
                  {//if transalation is not done, continue...(Recursion comes in now!!)   
                      if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")   
                      {   
                          try   
                          {   
                              word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));   
                          }   
                          catch { }   
                      }   
                      else   
                      {   
                          word = ConvertWholeNumber(Number.Substring(0, pos))  + ConvertWholeNumber(Number.Substring(pos));   
                      }   
                  }   
                  //ignore digit grouping names   
                  if (word.Trim().Equals(place.Trim())) word = "";   
              }   
          }   
          catch { }   
          return word.Trim();   
      }  


	  private static String ones(String Number)   
            {   
                int _Number = Convert.ToInt32(Number);   
                String name = "";   
                switch (_Number)   
                {   
                   
                    case 1:   
                        name = "Uno";   
                        break;   
                    case 2:   
                        name = "Dos";   
                        break;   
                    case 3:   
                        name = "Tres";   
                        break;   
                    case 4:   
                        name = "Cuatro";   
                        break;   
                    case 5:   
                        name = "Cinco";   
                        break;   
                    case 6:   
                        name = "Seis";   
                        break;   
                    case 7:   
                        name = "Siete";   
                        break;   
                    case 8:   
                        name = "Ocho";   
                        break;   
                    case 9:   
                        name = "Nueve";   
                        break;   
                }   
                return name;   
       }  

	   private static String tens(String Number)   
            {   
                int _Number = Convert.ToInt32(Number);   
                String name = null;   
                switch (_Number)   
                {   
                    case 10:   
                        name = "Diez";   
                        break;   
                    case 11:   
                        name = "Once";   
                        break;   
                    case 12:   
                        name = "Doce";   
                        break;   
                    case 13:   
                        name = "Trece";   
                        break;   
                    case 14:   
                        name = "Catorce";   
                        break;   
                    case 15:   
                        name = "Quince";   
                        break;   
                    case 16:   
                        name = "Dieciseis";   
                        break;   
                    case 17:   
                        name = "Diecisiete";   
                        break;   
                    case 18:   
                        name = "Dieciocho";   
                        break;   
                    case 19:   
                        name = "Diecinueve";   
                        break;   
                    case 20:   
                        name = "Veinte";   
                        break;   
                    case 30:   
                        name = "Treinta";   
                        break;   
                    case 40:   
                        name = "Cuarenta";   
                        break;   
                    case 50:   
                        name = "Cincuenta";   
                        break;   
                    case 60:   
                        name = "Sesenta";   
                        break;   
                    case 70:   
                        name = "Setenta";   
                        break;   
                    case 80:   
                        name = "Ochenta";   
                        break;   
                    case 90:   
                        name = "Noventa";   
                        break;   
                    default:   
                        if (_Number > 0)   
                        {   
                            name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));   
                        }   
                        break;   
                }   
                return name;   
            }   


			 private static String ConvertToWords(String numb)   
            {   
                String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";   
                String endStr = "";   
                try   
                {   
                    int decimalPlace = numb.IndexOf(".");   
                    if (decimalPlace > 0)   
                    {   
                        wholeNo = numb.Substring(0, decimalPlace);   
                        points = numb.Substring(decimalPlace + 1);   
                        if (Convert.ToInt32(points) > 0)   
                        {   
                            andStr = "and";// just to separate whole numbers from points/cents   
                            endStr = "Paisa " + endStr;//Cents   
                            pointStr = ConvertDecimals(points);   
                        }   
                    }   
                    val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);   
                }   
                catch { }   
                return val;   
            }   

			 private static String ConvertDecimals(String number)   
            {   
                String cd = "", digit = "", engOne = "";   
                for (int i = 0; i < number.Length; i++)   
                {   
                    digit = number[i].ToString();   
                    if (digit.Equals("0"))   
                    {   
                        engOne = "Zero";   
                    }   
                    else   
                    {   
                        engOne = ones(digit);   
                    }   
                    cd += " " + engOne;   
                }   
                return cd;   
            }  
       
    }
}