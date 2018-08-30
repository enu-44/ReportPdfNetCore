using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pmacore_api.Models;

namespace pmacore_api.Controllers.Pdfreport.PdfTin.example
{ 
    public  class TemplateGenerator
    {
		
 	

        public static string GetHTMLString(ResponseApiPma employees,String wwwroot)
        {
            var encabezado= Path.Combine(wwwroot, "Images","_pma.png");
			var image = string.Format("<img  width='40'  src='{0}' />", encabezado);
            

             var main = new StringBuilder();

            var list = new StringBuilder();


            for(var i=0;i<100;i++){
                 list.Append(@"<tr style='text-align: center;'>
				<td style='width: 76.8889px; font-size: 11px'></td>
				<td style='width: 76.8889px; font-size: 11px' colspan='2'></td>
				<td style='width: 77.7778px; font-size: 11px' colspan='2'></td>
				<td style='width: 78.6667px; font-size: 11px'></td>
				<td style='width: 78.6667px; font-size: 11px' colspan='2'></td>
				<td style='width: 78.6667px; font-size: 11px'></td>
				<td style='width: 79.5556px; font-size: 11px'>$</td>
				<td style='width: 79.5556px; font-size: 11px'>$</td>
				<td style='width: 79.5556px; font-size: 11px'>$</td>
				<td style='width: 79.5556px; font-size: 11px'>$</td>
				<td style='width: 79.5556px; font-size: 11px'>$</td>
			</tr>");

            }
            


            main.Append(@"<html>
            <head>
            </head>
            <body>");


            main.Append(@"

            <table style='border-collapse: collapse' border='1' width='100%'>
		    <tbody>
            <tr>
                <td rowspan='22' width='20px'><div style='font-size:10px;
                -webkit-transform: rotate(-90deg);
                -moz-transform: rotate(-90deg);
                -ms-transform: rotate(-90deg);
                -o-transform: rotate(-90deg);
                transform: rotate(-90deg);
                margin: 0 -13em; '>MODELO  P135-GI-ADM-16-14-007 V1.0 (13/01/2017)</div></td>
            </tr>
			<tr>
				<td style='width: 76.8889px; text-align:center' colspan='3' rowspan='3'><img style='width:120px; height:100px' src='https://s3.amazonaws.com/agriculturapp/PMA.png'></td>
				<td style='width: 77.7778px; text-align: center; vertical-align: middle;' colspan='8' rowspan='3'><strong>FORMATO DE AUTORIZACION GASTOS DE VIAJE (VIATICOS)</strong></td>
				<td style='width: 79.5556px; text-align: center; font-size: 11px' colspan='3'>Version 2.0</td>
			</tr>
			<tr>
				<td style='width: 79.5556px; text-align: center; font-size: 11px' colspan='3'>Código: P135-PYC-ADM-16-13-011</td>
			</tr>
			<tr>
				<td style='width: 79.5556px; text-align: center; font-size: 11px' colspan='3'>Consecutivo </td>
			</tr>
			<tr style='font-size: 10px; text-align: center;'>
				<td style='width: 76.8889px; background-color: #48d267; font-size: 11px;' colspan='3'>PROYECTO:</td>
				<td style='width: 77.7778px;' colspan='11'></td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 76.8889px; font-size: 11px; background-color: #48d267;' colspan='2'>CLIENTE</td>
				<td style='width: 76.8889px; font-size: 11px;' colspan='2'></td>
				<td style='width: 77.7778px; font-size: 11px; background-color: #48d267;' colspan='2'>CONTRATO</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='4'></td>
				<td style='width: 79.5556px; font-size: 11px; background-color: #48d267;' colspan='2'>FECHA</td>
				<td style='width: 79.5556px; font-size: 11px;' colspan='2'></td>
			</tr>
			<tr style='font-size: 11px; background-color: #48d267; text-align: center;'>
				<td style='width: 76.8889px; font-size: 11px;'>CODIGO</td>
				<td style='width: 76.8889px; font-size: 11px;' colspan='4'>NOMBRE DEL EMPLEADO</td>
				<td style='width: 78.6667px; font-size: 11px;'>CEDULA</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='4'>CARGO</td>
				<td style='width: 79.5556px; font-size: 11px;' colspan='3'>CECOS</td>
				<td style='width: 79.5556px; font-size: 11px;'>SUCURSAL</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 76.8889px; font-size: 11px;'>&nbsp;</td>
				<td style='width: 76.8889px; font-size: 11px;' colspan='4'></td>
				<td style='width: 78.6667px; font-size: 11px;'></td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='4'></td>
				<td style='width: 79.5556px; font-size:11px' colspan='3'></td>
				<td style='width: 79.5556px; font-size: 11px;'></td>
			</tr>
			<tr style='text-align: center; background-color: #48d267;'>
				<td style='width: 76.8889px; font-size: 11px;' colspan='10'>OBJETO DE LA COMISION</td>
				<td style='width: 79.5556px; font-size: 11px;' colspan='2'>FECHA INICIO</td>
				<td style='width: 79.5556px; font-size: 11px;' colspan='2'>FECHA FIN</td>
			</tr>
			<tr style='text-align:center'>
				<td style='width: 76.8889px; font-size:11px' colspan='10'></td>
				<td style='width: 79.5556px; font-size:11px' colspan='2'></td>
				<td style='width: 79.5556px; font-size:11px' colspan='2'></td>
			</tr>
			<tr style='text-align: center; background-color: #48d267;'>
				<td style='width: 76.8889px; font-size: 11px;'>Fecha</td>
				<td style='width: 76.8889px; font-size: 11px;' colspan='2'>Origen</td>
				<td style='width: 77.7778px; font-size: 11px;' colspan='2'>Destino</td>
				<td style='width: 78.6667px; font-size: 11px;'>Tarifa</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='2'>Orden de Trabajo</td>
				<td style='width: 78.6667px; font-size: 11px;'>Estado</td>
				<td style='width: 79.5556px; font-size: 9px;'>Alojamiento</td>
				<td style='width: 79.5556px; font-size: 9px;'>Alimentacion</td>
				<td style='width: 79.5556px; font-size: 9px;'>Miscelaneos</td>
				<td style='width: 79.5556px; font-size: 9px;'>Transporte</td>
				<td style='width: 79.5556px; font-size: 11px;'>Valor</td>
			</tr>");


			 main.Append(list);
			
            
             main.Append(@"<tr>
				<td style='width: 76.8889px;' colspan='14'>&nbsp;</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 78.6667px; font-size: 11px; background-color: #909497;' colspan='9'>TOTAL VI&Aacute;TICOS A PAGAR</td>
				<td style='width: 78.6667px; font-size: 11px;'>$</td>
                <td style='width: 78.6667px; font-size: 11px;'>$</td>
                <td style='width: 78.6667px; font-size: 11px;'>$</td>
				<td style='width: 79.5556px; font-size: 11px;'>$</td>
                <td style='width: 78.6667px; font-size: 11px;'>$</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 78.6667px; font-size: 11px;'>Son:</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='13'></td>
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
				<td style='width: 78.6667px; font-size: 11px;' colspan='14'>&nbsp;</td>
			</tr>
			<tr style='text-align: center; height: 50px'>
				<td style='width: 78.6667px; font-size: 11px;' colspan='4'>&nbsp;</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='5'>&nbsp;</td>
				<td style='width: 78.6667px; font-size: 11px;' colspan='5'>&nbsp;</td>
			</tr>
			<tr style='text-align: center'>
				<td style='width: 78.6667px; font-size: 10px' colspan='4'>
					<p>FIRMA DEL EMPLEADO</p>
					<p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
				</td>
				<td style='width: 78.6667px; font-size: 10px;' colspan='5'>
					<p>FIRMA DEL INGENIERO RESIDENTE</p>
					<p>CONSORCIO PIPELINE MAINTENANCE ALLIANCE</p>
				</td>
				<td style='width: 78.6667px; font-size: 10px;' colspan='5'>
					<p>FIRMA </p>
				</td>
			</tr>
			<tr style='text-align: center;'>
				<td style='width: 78.6667px; font-size: 10px;' colspan='14'>
					CON SU FIRMA EL TRABAJADOR CERTIFICA HABER RECIBIDO LOS VALOES INDICADOS EN ESTA AUTORIZACIÓN DE VIAJE
				</td>
			</tr>
		</tbody>
	</table>
	<p style='text-align: center; font-size: 10px'>Nota de propiedad: Los derechos de propiedad intelectual sobre este documento y su contenido le pertenecen exclusivamente al CONSORCIO PIPELINE MAINTENANCE ALLIANCE(PMA). Por lo tanto queda estrictamente prohibido el uso divulgaci&oacute;n, distribuci&oacute;n, reproducci&oacute;n, modificaci&oacute;n y/o alteraci&oacute;n de los mencionados derechos, con fines distintos a los previstos en este documento, sin la autorizaci&oacute;n previa y escrita del consorcio.</p>
    <p style='text-align:right; font-size: 11px; margin:0px'>Generado por <strong></strong> el  a las [$-es-ES]hh:mm:ss AM/PM</p>




            ");


            main.Append(@"</body>
                        </html>");

            /* 
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");
 
            foreach (var emp in employees)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
            }
 
            sb.Append(@"
                                </table>
                            </body>
                        </html>");*/
 
            return main.ToString();
        }
    }
}
