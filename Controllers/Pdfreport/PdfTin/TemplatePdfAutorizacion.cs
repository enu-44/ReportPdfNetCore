using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pmacore_api.Models;

namespace pmacore_api.Controllers.Pdfreport.PdfTin
{
    public class TemplatePdfAutorizacion
    {
        public async Task<string> GetHTMLString(ResponseApiPma request,String wwwroot)
        {
            var image= Path.Combine(wwwroot, "Images","_pma.png");
			//var image = string.Format("<img  width='40'  src='{0}' />", encabezado);
            

            var main = new StringBuilder();
            main.Append(@"<html>
            
            <head>
            <meta charset='utf-8'>
            </head>
            <body>");

            foreach(var detalle_data in request.Data){

            
            var list = new StringBuilder();
            main.Append(@"
            <div class='formato'>
            <table  border='1' width='100%'>
		    <tbody>");

            var total_rows_historias= detalle_data.Historias.Count+22;
            main.AppendFormat(@" <tr>
                <td rowspan='{0}' width='20px'><div class='traslate_text_vertical'>{1}</div></td>
            </tr>
			<tr>
				<td  colspan='3' rowspan='3'><img style='width:120px; ' src='{2}'></td>
				<td  colspan='8' rowspan='3'><strong>FORMATO DE AUTORIZACION GASTOS DE VIAJE (VIATICOS)</strong></td>
				<td class='content_table'  colspan='3'>Version 2.0</td>
			</tr>
			<tr>
				<td class='content_table' colspan='3'>Código: {3}</td>
			</tr>
			<tr>
				<td class='content_table' colspan='3'>Consecutivo: {4} </td>
			</tr>",total_rows_historias,request.Version,image,request.Formato,detalle_data.Consecutivo);

            

            main.AppendFormat(@"<tr >
				<td class='title_table' colspan='3'>PROYECTO:</td>
				<td class='content_table' colspan='11'>{0}</td>
			</tr>
			<tr style='text-align: center;'>
				<td class='title_table' colspan='2'>CLIENTE</td>
				<td class='content_table' colspan='2'>{1}</td>
				<td class='title_table' colspan='2'>CONTRATO</td>
				<td class='content_table' colspan='4'>{2}</td>
				<td class='title_table' colspan='2'>FECHA</td>
				<td class='content_table' colspan='2'>{3}</td>
			</tr>
			<tr >
				<td class='title_table'>CODIGO</td>
				<td class='title_table' colspan='4'>NOMBRE DEL EMPLEADO</td>
				<td class='title_table'>CEDULA</td>
				<td class='title_table' colspan='4'>CARGO</td>
				<td class='title_table' colspan='3'>CECOS</td>
				<td class='title_table' >SUCURSAL</td>
			</tr>
			<tr style='text-align: center;'>
				<td class='content_table' >{4}</td>
				<td class='content_table' colspan='4'>{5}</td>
				<td class='content_table' >{6}</td>
				<td class='content_table' colspan='4'>{7}</td>
				<td class='content_table' colspan='3'>{8}</td>
				<td class='content_table' >{9}</td>
			</tr>
			<tr style='text-align: center; background-color: #48d267;'>
				<td class='title_table' colspan='10'>OBJETO DE LA COMISION</td>
				<td class='title_table' colspan='2'>FECHA INICIO</td>
				<td class='title_table' colspan='2'>FECHA FIN</td>
			</tr>
			<tr style='text-align:center'>
				<td class='content_table' colspan='10'>{10}</td>
				<td class='content_table' colspan='2'>{11}</td>
				<td class='content_table' colspan='2'>{12}</td>
			</tr>
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
						detalle_data.FechaFin
						);
           
			
            main.Append(@"
            <tr style='text-align: center; background-color: #48d267;'>
				<td class='content_list_small_table'>Fecha</td>
				<td class='content_list_small_table' colspan='2'>Origen</td>
				<td class='content_list_small_table' colspan='2'>Destino</td>
				<td class='content_list_small_table'>Tarifa</td>
				<td class='content_list_small_table' colspan='2'>Orden de Trabajo</td>
				<td class='content_list_small_table'>Estado</td>
				<td class='content_list_small_table'>Alojamiento</td>
				<td class='content_list_small_table'>Alimentacion</td>
				<td class='content_list_small_table'>Miscelaneos</td>
				<td class='content_list_small_table'>Transporte</td>
				<td class='content_list_small_table'>Valor</td>
			</tr>
            ");

        
           foreach(var historias in detalle_data.Historias){
                 main.Append("<tr style='text-align: center;'>"+
				"<td class='content_list_small_table'>"+historias.Fecha+"</td>"+
				"<td class='content_list_small_table' colspan='2'>"+historias.Origen+"</td>"+
				"<td class='content_list_small_table' colspan='2'>"+historias.Destino+"</td>"+
				"<td class='content_list_small_table'>"+historias.Tarifa+"</td>"+
				"<td class='content_list_small_table' colspan='2'>"+historias.Orden+"</td>"+
                "<td class='content_list_small_table'>"+historias.Estado+"</td>"+
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Alojamiento)+"</td>"+
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Alimentacion)+"</td>"+
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Miscelaneos)+"</td>"+
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Transporte)+"</td>"+
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Valor)+"</td>"+
			    "</tr>");
            }

          
            var currencyFormat  = Convert.ToDouble(detalle_data.SumValor);
            var stringValor= MontoFormat.NumWordsWrapper(currencyFormat);

            // var traslate=Task.Run(async()=>await TranslateThisAsync(stringValor));
            var traslate=await Traductor.TranslateThisAsync(stringValor);
            var replacevalor_string =traslate.Replace("milln","millón");

            main.AppendFormat(@"
                <tr style='text-align: center;'>
                    <td  colspan='14'>&nbsp;</td>
                </tr>
                    <tr style='text-align: center;'>
                    <td class='title_total' colspan='9'>TOTAL VI&Aacute;TICOS A PAGAR</td>
                    <td class='content_table'>{0}</td>
                    <td class='content_table'>{1}</td>
                    <td class='content_table'>{2}</td>
                    <td class='content_table'>{3}</td>
                    <td class='content_table'>{4}</td>
                </tr>
                <tr style='text-align: center;'>
                    <td class='content_table'>Son:</td>
                    <td class='content_table' colspan='13'>{5}</td>
                </tr>
            ",
            String.Format("&#36 {0:N0}", detalle_data.SumAloj),
            String.Format("&#36 {0:N0}", detalle_data.SumAlim),
            String.Format("&#36 {0:N0}",  detalle_data.SumMisc),
            String.Format("&#36 {0:N0}",  detalle_data.SumTran),
            String.Format("&#36 {0:N0}",  detalle_data.SumValor),
            String.Format("{0} Pesos MTC",replacevalor_string));

          

            main.AppendFormat(@"<tr style='text-align: center;'>
                <td colspan='14'>
                        <p>Importante:</p>
                        <p>*Autorizo a la empresa para descontar de mis salarios y prestaciones sociales el valor de los vi&aacute;ticos, anticipos para gastos y pasajes recibidos para esta comisi&oacute;n, en caso de no presentar la legalizaci&oacute;n correspondiente dentro de los cinco (5) d&iacute;as h&aacute;biles al t&eacute;rmino de la comisi&oacute;n.</p>
                    </td>
                </tr>
                <tr>
                    <td  colspan='14'><p style='color:red;'> *Manifiesto que he le&iacute;do y entendido en su integridad el presente formato, por lo cual firmo en calidad de aceptaci&oacute;n </p></td>
                </tr>
                <tr >
                    <td style=' font-size: 11px;' colspan='14'>&nbsp;</td>
                </tr>
                <tr style=' height: 50px'>
                    <td style=' font-size: 11px;' colspan='4'>&nbsp;</td>
                    <td style=' font-size: 11px;' colspan='5'>&nbsp;</td>
                    <td style=' font-size: 11px;' colspan='5'>&nbsp;</td>
                </tr>
                <tr >
                    <td class='title_firmas' colspan='4'>
                        <p>FIRMA DEL EMPLEADO</p>
                        <p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
                    </td>
                    <td class='title_firmas' colspan='5'>
                        <p>FIRMA DEL INGENIERO RESIDENTE</p>
                        <p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
                    </td>
                    <td class='title_firmas' colspan='5'>
                        <p>FIRMA </p>
                    </td>
                </tr>
                <tr >
                    <td class='title_firmas' colspan='14'>
                        CON SU FIRMA EL TRABAJADOR CERTIFICA HABER RECIBIDO LOS VALOES INDICADOS EN ESTA AUTORIZACIÓN DE VIAJE
                    </td>
                </tr>");

            main.Append(@"
                    </tbody>
              </table>");

            main.AppendFormat(@"<p>Nota de propiedad: Los derechos de propiedad intelectual sobre este documento y su contenido le pertenecen exclusivamente al CONSORCIO PIPELINE MAINTENANCE ALLIANCE(PMA). Por lo tanto queda estrictamente prohibido el uso divulgaci&oacute;n, distribuci&oacute;n, reproducci&oacute;n, modificaci&oacute;n y/o alteraci&oacute;n de los mencionados derechos, con fines distintos a los previstos en este documento, sin la autorizaci&oacute;n previa y escrita del consorcio.</p>
                <p style='text-align:right;'>Generado por <strong>{0}</strong> el {1} a las {2}</p>
            ",request.Email,request.Fecha, request.Hora);

            main.Append(@"
            </div>");

            }

            main.Append(@"</body>
                        </html>");
        
            return main.ToString();
        }
    }        
}