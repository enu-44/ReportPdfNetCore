using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using pmacore_api.Models;


namespace pmacore_api.Controllers.Pdfreport.PdfTin
{
    public class TemplatePdfLegalizacion
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

            var total_rows_historias= detalle_data.Historias.Count+32;
            main.AppendFormat(@" <tr>
                <td rowspan='{0}' width='20px'><div class='traslate_text_vertical'>{1}</div></td>
            </tr>
			<tr>
				<td  colspan='3' rowspan='3'><img style='width:120px;' src='{2}'></td>
				<td  colspan='8' rowspan='3'><strong>FORMATO DE LEGALIZACION GASTOS DE VIAJE (VIATICOS)</strong></td>
				<td class='content_table'  colspan='6'>Version 2.0</td>
			</tr>
			<tr>
				<td class='content_table' colspan='6'>Código: {3}</td>
			</tr>
			<tr>
				<td class='content_table' colspan='6'>Consecutivo: {4} </td>
			</tr>",total_rows_historias,request.Version,image,request.Formato,detalle_data.Consecutivo);

        
            main.AppendFormat(@"
            <tr>
				<td class='title_table' colspan='3'>PROYECTO:</td>
				<td class='content_table' colspan='14'>{0}</td>
			</tr>
			<tr style='text-align: center;'>
				<td class='title_table' colspan='2'>CLIENTE</td>
				<td class='content_table' colspan='5'>{1}</td>
				<td class='title_table' colspan='3'>CONTRATO</td>
				<td class='content_table' colspan='4'>{2}</td>
				<td class='title_table' >FECHA</td>
				<td class='content_table' colspan='2'>{3}</td>
			</tr>
			<tr >
				<td class='title_table'>CODIGO</td>
				<td class='title_table' colspan='5'>NOMBRE DEL EMPLEADO</td>
				<td class='title_table' colspan='2'>CEDULA</td>
				<td class='title_table' colspan='4'>CARGO</td>
				<td class='title_table' colspan='4'>CECOS</td>
				<td class='title_table' >SUCURSAL</td>
			</tr>
			<tr style='text-align: center;'>
				<td class='content_table' >{4}</td>
				<td class='content_table' colspan='5'>{5}</td>
				<td class='content_table' colspan='2'>{6}</td>
				<td class='content_table' colspan='4'>{7}</td>
				<td class='content_table' colspan='4'>{8}</td>
				<td class='content_table' >{9}</td>
			</tr>
			<tr style='text-align: center; background-color: #48d267;'>
				<td class='title_table' colspan='10'>OBJETO DE LA COMISION</td>
				<td class='title_table' colspan='3'>FECHA INICIO</td>
				<td class='title_table' colspan='4'>FECHA FIN</td>
			</tr>
			<tr style='text-align:center'>
				<td class='content_table' colspan='10'>{10}</td>
				<td class='content_table' colspan='3'>{11}</td>
				<td class='content_table' colspan='4'>{12}</td>
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
				<td class='content_list_small_table''>Miscelaneos</td>
				<td class='content_list_small_table'>Transporte</td>
                <td  class='content_list_small_table'>Terminal</td>
				<td  class='content_list_small_table'>TiquetePor</td>
				<td  class='content_list_small_table'>Tiquete</td>
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
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Terminal)+"</td>"+
                "<td class='content_list_small_table'>"+historias.TiquetesPor+"</td>"+
                "<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Tiquetes)+"</td>"+
				"<td class='content_list_small_table'>"+String.Format("&#36 {0:N0}", historias.Valor)+"</td>"+
			    "</tr>");
            }

			var currencyFormatPma  = Convert.ToDouble(detalle_data.ViaticosPMA);
			var currencyFormatTrabajador  = Convert.ToDouble(detalle_data.ViaticosTrab);
            var stringValorPma= MontoFormat.NumWordsWrapper(currencyFormatPma);
			var stringValorTrabajador= MontoFormat.NumWordsWrapper(currencyFormatTrabajador);

            // var traslate=Task.Run(async()=>await TranslateThisAsync(stringValor));
            var traslatePma=await Traductor.TranslateThisAsync(stringValorPma);
            var replacevalor_string_pma =traslatePma.Replace("milln","millón");

			var traslateTrabajador=await Traductor.TranslateThisAsync(stringValorTrabajador);
            var replacevalor_string_trabajador =traslateTrabajador.Replace("milln","millón");

            main.AppendFormat(@"
                <tr style='text-align: center;'>
                    <td  colspan='17'>&nbsp;</td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='width: 76.8889px; font-size: 11px;'>Son:</td>
                    <td style='width: 76.8889px; font-size: 11px;' colspan='10'>{0}</td>
                    <td style='width: 79.5556px; font-size: 9px;' colspan='3'>Total legalizacion Viaticos PMA</td>
                    <td style='width: 79.5556px; font-size: 10px;' colspan='3'>{1}</td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='width: 76.8889px; font-size: 11px;'>Son:</td>
                    <td style='width: 76.8889px; font-size: 11px;' colspan='10'>{2}</td>
                    <td style='width: 79.5556px; font-size: 9px;' colspan='3'>Total legalizacion Viaticos Trabajador</td>
                    <td style='width: 79.5556px; font-size: 10px;' colspan='3'>{3}</td>
                </tr>
            ",string.Format("{0} Pesos MTC",replacevalor_string_pma),String.Format("&#36 {0:N0}", detalle_data.ViaticosPMA),string.Format("{0} Pesos MTC",replacevalor_string_trabajador),String.Format("&#36 {0:N0}", detalle_data.ViaticosTrab));

            main.AppendFormat(@"
            <tr style='text-align: center;'>
				<td style='width: 76.8889px; font-size: 11px; text-align: left; vertical-align: top;' colspan='7' rowspan='5'>Observaciones: {7}</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='10'>Menos: Anticipo a Viaticos</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 78.6667px; font-size: 10px; background-color: #48d267;' colspan='3'>Pernocta/Retorna</td>
				<td style='width: 78.6667px; font-size: 11px; background-color: #48d267;'>No. Dias</td>
				<td style='width: 79.5556px; font-size: 9px; background-color: #48d267;'>Valor Anticipo</td>
				<td style='width: 79.5556px; font-size: 9px; background-color: #48d267;'>Valor Total</td>
				<td style='width: 79.5556px; font-size: 11px; background-color: #48d267;' colspan='4'>Valor Pagado</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 78.6667px; font-size: 11px;' colspan='3'>Pernocto</td>
				<td style='width: 78.6667px; font-size: 11px;'>{0}</td>
				<td style='width: 79.5556px; font-size: 11px;'> {1}</td>
				<td style='width: 79.5556px; font-size: 11px;' rowspan='2'> {2}</td>
				<td style='width: 79.5556px; font-size: 11px;' colspan='4' rowspan='2'> {3}</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 79.5556px; font-size: 11px;' colspan='3'>Retorno</td>
				<td style='width: 79.5556px; font-size: 11px;'>{4}</td>
				<td style='width: 79.5556px; font-size: 11px;'> {5}</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 79.5556px; font-size: 11px; background-color: #48d267;' colspan='6'>Saldo de Viaticos (+ a Favor; - a cargo)</td>
				<td style='width: 79.5556px; font-size: 11px;' colspan='4'> {6}</td>
			</tr>",detalle_data.DiasPernoc,
            String.Format("&#36 {0:N0}", detalle_data.PernocValor), 
            String.Format("&#36 {0:N0}", detalle_data.ValorTotal),
            String.Format("&#36 {0:N0}", detalle_data.ValorTotal),
            detalle_data.DiasRetorn,
            String.Format("&#36 {0:N0}", detalle_data.RetornValor),
            String.Format("&#36 {0:N0}", detalle_data.SaldoFavor),
			detalle_data.Observaciones);
            

            main.AppendFormat(@"<tr style='text-align: center;'>
                <td colspan='17'>
                        <p>Importante:</p>
                        <p>*Autorizo a la empresa para descontar de mis salarios y prestaciones sociales el valor de los vi&aacute;ticos, anticipos para gastos y pasajes recibidos para esta comisi&oacute;n, en caso de no presentar la legalizaci&oacute;n correspondiente dentro de los cinco (5) d&iacute;as h&aacute;biles al t&eacute;rmino de la comisi&oacute;n.</p>
                    </td>
                </tr>
                <tr>
                    <td  colspan='17'><p style='color:red;'> *Manifiesto que he le&iacute;do y entendido en su integridad el presente formato, por lo cual firmo en calidad de aceptaci&oacute;n </p></td>
                </tr>
                <tr >
                    <td style=' font-size: 11px;' colspan='17'>&nbsp;</td>
                </tr>
                <tr style=' height: 50px'>
                    <td style=' font-size: 11px;' colspan='5'>&nbsp;</td>
                    <td style=' font-size: 11px;' colspan='5'>&nbsp;</td>
                    <td style=' font-size: 11px;' colspan='7'>&nbsp;</td>
                </tr>
                <tr >
                    <td class='title_firmas' colspan='5'>
                        <p>FIRMA DEL EMPLEADO</p>
                        <p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
                    </td>
                    <td class='title_firmas' colspan='5'>
                        <p>FIRMA DEL INGENIERO RESIDENTE</p>
                        <p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
                    </td>
                    <td class='title_firmas' colspan='7'>
                        <p>FIRMA </p>
                    </td>
                </tr>
                <tr >
                    <td class='title_firmas' colspan='17'>
                        CON SU FIRMA EL TRABAJADOR CERTIFICA HABER RECIBIDO LOS VALOES INDICADOS EN ESTA AUTORIZACIÓN DE VIAJE
                    </td>
                </tr>");

        

            main.Append(@"
                    </tbody>
              </table>");

            main.AppendFormat(@"<p>Nota de propiedad: Los derechos de propiedad intelectual sobre este documento y su contenido le pertenecen exclusivamente al CONSORCIO PIPELINE MAINTENANCE ALLIANCE(PMA). Por lo tanto queda estrictamente prohibido el uso divulgaci&oacute;n, distribuci&oacute;n, reproducci&oacute;n, modificaci&oacute;n y/o alteraci&oacute;n de los mencionados derechos, con fines distintos a los previstos en este documento, sin la autorizaci&oacute;n previa y escrita del consorcio.</p>
                <p style='text-align:right;'>Generado por <strong>{0}</strong> el {1} a las {2}</p>
            ",request.Email,request.Fecha,request.Hora);

            main.Append(@"
            </div>");

            }

            main.Append(@"</body>
                        </html>");
        
            return main.ToString();
        }
    }
}