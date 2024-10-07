namespace WebApiUdApp.Utilities
{
    public class EnviarCorreo
    {
        public async Task CorreoInicioSesion(string destinatario, string nombreUsuario)
        {
            try
            {
                destinatario = "angipaola3321@gmail.com"; // agregado para pruebas xd
                string asunto = "Bienvenido a UdApp";
                string imageUrl = "https://i.ibb.co/Kr5WhCp/Ud-App-sign.png"; // URL directa de la imagen
                string body = $@"
                <!DOCTYPE html>

				<html lang=""en"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml"">

				<head>
					<title></title>
					<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
					<meta content=""width=device-width, initial-scale=1.0"" name=""viewport"" />
					<!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]--><!--[if !mso]><!-->
					<link href=""https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700&display=swap"" rel=""stylesheet""
						type=""text/css"" /><!--<![endif]-->
					<style>
						* {{
							box-sizing: border-box;
						}}

						body {{
							margin: 0;
							padding: 0;
						}}

						a[x-apple-data-detectors] {{
							color: inherit !important;
							text-decoration: inherit !important;
						}}

						#MessageViewBody a {{
							color: inherit;
							text-decoration: none;
						}}

						p {{
							line-height: inherit
						}}

						.desktop_hide,
						.desktop_hide table {{
							mso-hide: all;
							display: none;
							max-height: 0px;
							overflow: hidden;
						}}

						.image_block img+div {{
							display: none;
						}}

						sup,
						sub {{
							line-height: 0;
							font-size: 75%;
						}}

						@media (max-width:670px) {{
							.desktop_hide table.icons-outer {{
								display: inline-table !important;
							}}

							.desktop_hide table.icons-inner {{
								display: inline-block !important;
							}}

							.icons-inner {{
								text-align: center;
							}}

							.icons-inner td {{
								margin: 0 auto;
							}}

							.image_block div.fullWidth {{
								max-width: 100% !important;
							}}

							.mobile_hide {{
								display: none;
							}}

							.row-content {{
								width: 100% !important;
							}}

							.stack .column {{
								width: 100%;
								display: block;
							}}

							.mobile_hide {{
								min-height: 0;
								max-height: 0;
								max-width: 0;
								overflow: hidden;
								font-size: 0px;
							}}

							.desktop_hide,
							.desktop_hide table {{
								display: table !important;
								max-height: none !important;
							}}

							.reverse {{
								display: table;
								width: 100%;
							}}

							.reverse .column.first {{
								display: table-footer-group !important;
							}}

							.reverse .column.last {{
								display: table-header-group !important;
							}}

							.row-5 td.column.first .border {{
								padding: 5px 0;
								border-top: 0;
								border-right: 0px;
								border-bottom: 0;
								border-left: 0;
							}}

							.row-5 td.column.last .border {{
								padding: 0;
								border-top: 0;
								border-right: 0px;
								border-bottom: 0;
								border-left: 0;
							}}
						}}
					</style>
					<!--[if mso ]><style>sup, sub {{ font-size: 100% !important; }} sup {{ mso-text-raise:10% }} sub {{ mso-text-raise:-10% }}</style> <![endif]--><!--[if true]><style>.forceBgColor{{background-color: white !important}}</style><![endif]-->
				</head>

				<body class=""body forceBgColor""
					style=""background-color: transparent; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation""
						style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: transparent;"" width=""100%"">
						<tbody>
							<tr>
								<td>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-1""
										role=""presentation""
										style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f6f6fc;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<div class=""spacer_block block-1""
																		style=""height:45px;line-height:45px;font-size:1px;""> </div>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""icons_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; text-align: center; line-height: 0;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""vertical-align: middle; color: #000000; font-family: inherit; font-size: 14px; text-align: center;"">
																				<table cellpadding=""0"" cellspacing=""0""
																					class=""icons-outer"" role=""presentation""
																					style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; display: inline-table;"">
																					<tr>
																						<td
																							style=""vertical-align: middle; text-align: center; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;"">
																							<img align=""center"" alt=""Logo"" class=""icon""
																								height=""auto""
																								src=""https://i.ibb.co/9NLC8p9/7c313584485ed5765c8642ef1f3e96bc.png""
																								style=""display: block; height: auto; margin: 0 auto; border: 0;""
																								width=""117"" /></td>
																					</tr>
																				</table>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""15"" cellspacing=""0""
																		class=""heading_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"">
																				<h1
																					style=""margin: 0; color: #131313; direction: ltr; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 38px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 45.6px;"">
																					Bienvenido a UdApp</h1>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""paragraph_block block-4"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"">
																				<div
																					style=""color:#393d47;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Hola {nombreUsuario}, te has
																						registrado satisfactoriamente en UdApp. Gracias
																						por elegirnos, esperamos que disfrutes de todo
																						lo que nuestra plataforma tiene para ofrecerte.
																					</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""button_block block-5"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"">
																				<div align=""center"" class=""alignment""><!--[if mso]>
				<v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" href=""www.example.com"" style=""height:44px;width:118px;v-text-anchor:middle;"" arcsize=""10%"" strokeweight=""0.75pt"" strokecolor=""#5046C7"" fillcolor=""#5046c7"">
				<w:anchorlock/>
				<v:textbox inset=""0px,0px,0px,0px"">
				<center dir=""false"" style=""color:#ffffff;font-family:Arial, sans-serif;font-size:16px"">
				<![endif]--><a href=""www.example.com"" style=""background-color:#213B44;border-bottom:1px solid #213B44;border-left:1px solid #213B44;border-radius:4px;border-right:1px solid #213B44;border-top:1px solid #213B44;color:#ffffff;display:inline-block;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;mso-border-alt:none;padding-bottom:5px;padding-top:5px;text-align:center;text-decoration:none;width:auto;word-break:keep-all;""
																						target=""_blank""><span
																							style=""word-break: break-word; padding-left: 20px; padding-right: 20px; font-size: 16px; display: inline-block; letter-spacing: normal;""><span
																								style=""word-break: break-word; line-height: 32px;"">Ir
																								a
																								UdApp</span></span></a><!--[if mso]></center></v:textbox></v:roundrect><![endif]-->
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""image_block block-6"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""width:100%;padding-right:0px;padding-left:0px;"">
																				<div align=""center"" class=""alignment""
																					style=""line-height:10px"">
																					<div style=""max-width: 293px;""><img
																							alt=""Girl With Laptop"" height=""auto""
																							src=""https://i.ibb.co/G2tYyMm/Feedback.png""
																							style=""display: block; height: auto; border: 0; width: 100%;""
																							title=""Girl With Laptop"" width=""293"" />
																					</div>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-2""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<div class=""spacer_block block-1""
																		style=""height:55px;line-height:55px;font-size:1px;""> </div>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-3""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"" style=""text-align:center;width:100%;"">
																				<h1
																					style=""margin: 0; color: #131313; direction: ltr; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 38px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 45.6px;"">
																					Actualízate</h1>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:5px;padding-left:10px;padding-right:10px;padding-top:10px;"">
																				<div
																					style=""color:#131313;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Mantente al día con todo lo
																						con todo tu campus virtual en la red</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:10px;padding-left:10px;padding-right:10px;"">
																				<div
																					style=""color:#a9a9a9;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Para ti</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
																<td class=""column column-2""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"" style=""text-align:center;width:100%;"">
																				<h1
																					style=""margin: 0; color: #131313; direction: ltr; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 38px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 45.6px;"">
																					<span class=""tinyMce-placeholder""
																						style=""word-break: break-word;"">Exprésate</span>
																				</h1>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:5px;padding-left:10px;padding-right:10px;padding-top:10px;"">
																				<div
																					style=""color:#131313;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Publica, comparte y muestra lo
																						que tengas para la comunidad</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:10px;padding-left:10px;padding-right:10px;"">
																				<div
																					style=""color:#a9a9a9;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Para los demás</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
																<td class=""column column-3""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"" style=""text-align:center;width:100%;"">
																				<h1
																					style=""margin: 0; color: #131313; direction: ltr; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 38px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 45.6px;"">
																					<span class=""tinyMce-placeholder""
																						style=""word-break: break-word;"">Descubre</span>
																				</h1>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:5px;padding-left:10px;padding-right:10px;padding-top:10px;"">
																				<div
																					style=""color:#131313;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Toda tu comunidad
																						universitaria en un solo lugar</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:10px;padding-left:10px;padding-right:10px;"">
																				<div
																					style=""color:#a9a9a9;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Para todos</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-4""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<div class=""spacer_block block-1""
																		style=""height:50px;line-height:50px;font-size:1px;""> </div>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-5""
										role=""presentation""
										style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #001a22; background-size: auto;""
										width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-size: auto; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr class=""reverse"">
																<td class=""column column-1 first""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""50%"">
																	<div class=""border"">
																		<div class=""spacer_block block-1""
																			style=""height:40px;line-height:40px;font-size:1px;""> </div>
																		<table border=""0"" cellpadding=""0"" cellspacing=""0""
																			class=""paragraph_block block-2"" role=""presentation""
																			style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																			width=""100%"">
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:15px;padding-right:10px;padding-top:10px;"">
																					<div
																						style=""color:#a59ee5;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:15px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:left;mso-line-height-alt:22.5px;"">
																						<p style=""margin: 0;"">Sobre nosotros</p>
																					</div>
																				</td>
																			</tr>
																		</table>
																		<table border=""0"" cellpadding=""0"" cellspacing=""0""
																			class=""heading_block block-3"" role=""presentation""
																			style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																			width=""100%"">
																			<tr>
																				<td class=""pad""
																					style=""padding-left:15px;padding-right:15px;padding-top:5px;text-align:center;width:100%;"">
																					<h2
																						style=""margin: 0; color: #ffffff; direction: ltr; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 30px; font-weight: 700; letter-spacing: normal; line-height: 150%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 45px;"">
																						<strong>UdApp es el nuevo Boom!</strong></h2>
																				</td>
																			</tr>
																		</table>
																		<table border=""0"" cellpadding=""0"" cellspacing=""0""
																			class=""divider_block block-4"" role=""presentation""
																			style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																			width=""100%"">
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:15px;padding-right:10px;"">
																					<div align=""left"" class=""alignment"">
																						<table border=""0"" cellpadding=""0""
																							cellspacing=""0"" role=""presentation""
																							style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																							width=""45%"">
																							<tr>
																								<td class=""divider_inner""
																									style=""font-size: 1px; line-height: 1px; border-top: 2px solid #5046C7;"">
																									<span
																										style=""word-break: break-word;""> </span>
																								</td>
																							</tr>
																						</table>
																					</div>
																				</td>
																			</tr>
																		</table>
																		<table border=""0"" cellpadding=""0"" cellspacing=""0""
																			class=""paragraph_block block-5"" role=""presentation""
																			style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																			width=""100%"">
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:15px;padding-right:10px;padding-top:10px;"">
																					<div
																						style=""color:#cdcdcd;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:15px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:left;mso-line-height-alt:22.5px;"">
																						<p style=""margin: 0; margin-bottom: 16px;"">
																							¡Bienvenido a UdApp, la plataforma digital
																							diseñada exclusivamente para la comunidad
																							estudiantil de la Universidad de
																							Cundinamarca! Nos complace que estés aquí.
																							En UdApp, buscamos crear un espacio donde
																							cada estudiante pueda sentirse parte de una
																							comunidad vibrante, conectada y llena de
																							oportunidades.</p>
																						<p style=""margin: 0;"">Esta plataforma no solo te
																							facilitará la gestión de tus tareas y
																							actividades académicas, sino que también te
																							permitirá interactuar, colaborar y compartir
																							ideas con compañeros de diferentes carreras,
																							semestres y campus. </p>
																					</div>
																				</td>
																			</tr>
																		</table>
																		<div class=""spacer_block block-6""
																			style=""height:25px;line-height:25px;font-size:1px;""> </div>
																		<table border=""0"" cellpadding=""0"" cellspacing=""0""
																			class=""button_block block-7"" role=""presentation""
																			style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																			width=""100%"">
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:15px;padding-right:10px;padding-top:10px;text-align:left;"">
																					<div align=""left"" class=""alignment""><!--[if mso]>
				<v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" href=""www.example.com"" style=""height:44px;width:160px;v-text-anchor:middle;"" arcsize=""10%"" strokeweight=""0.75pt"" strokecolor=""#5046C7"" fillcolor=""#5046c7"">
				<w:anchorlock/>
				<v:textbox inset=""0px,0px,0px,0px"">
				<center dir=""false"" style=""color:#ffffff;font-family:Arial, sans-serif;font-size:16px"">
				<![endif]--><a href=""www.example.com"" style=""background-color:#5046c7;border-bottom:1px solid #5046C7;border-left:1px solid #5046C7;border-radius:4px;border-right:1px solid #5046C7;border-top:1px solid #5046C7;color:#ffffff;display:inline-block;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;mso-border-alt:none;padding-bottom:5px;padding-top:5px;text-align:center;text-decoration:none;width:auto;word-break:keep-all;""
																							target=""_blank""><span
																								style=""word-break: break-word; padding-left: 20px; padding-right: 20px; font-size: 16px; display: inline-block; letter-spacing: normal;""><span
																									style=""word-break: break-word; line-height: 32px;"">¡Echa
																									un
																									vistazo</span></span></a><!--[if mso]></center></v:textbox></v:roundrect><![endif]-->
																					</div>
																				</td>
																			</tr>
																		</table>
																		<div class=""spacer_block block-8""
																			style=""height:25px;line-height:25px;font-size:1px;""> </div>
																	</div>
																</td>
																<td class=""column column-2 last""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""50%"">
																	<div class=""border"">
																		<table border=""0"" cellpadding=""0"" cellspacing=""0""
																			class=""image_block block-1"" role=""presentation""
																			style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																			width=""100%"">
																			<tr>
																				<td class=""pad""
																					style=""width:100%;padding-right:0px;padding-left:0px;"">
																					<div align=""right"" class=""alignment""
																						style=""line-height:10px"">
																						<div style=""max-width: 325px;""><img
																								alt=""Ecommerce App"" height=""auto""
																								src=""https://i.ibb.co/Bt5XHbj/eb99026a-ad5a-45f1-9796-697405d35c6c.png""
																								style=""display: block; height: auto; border: 0; width: 100%;""
																								title=""Ecommerce App"" width=""325"" />
																						</div>
																					</div>
																				</td>
																			</tr>
																		</table>
																	</div>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-6""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<div class=""spacer_block block-1""
																		style=""height:50px;line-height:50px;font-size:1px;""> </div>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""image_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""width:100%;padding-right:0px;padding-left:0px;"">
																				<div align=""center"" class=""alignment""
																					style=""line-height:10px"">
																					<div class=""fullWidth"" style=""max-width: 422.5px;"">
																						<img alt=""Girl Followers"" height=""auto""
																							src=""https://i.ibb.co/z4jfb0v/social-media-illustration.png""
																							style=""display: block; height: auto; border: 0; width: 100%;""
																							title=""Girl Followers"" width=""422.5"" />
																					</div>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-7""
										role=""presentation""
										style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #001A22; background-size: auto;""
										width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-size: auto; border-radius: 0; color: #001A22; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<div class=""spacer_block block-1""
																		style=""height:40px;line-height:40px;font-size:1px;""> </div>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:10px;padding-left:15px;padding-right:10px;padding-top:10px;"">
																				<div
																					style=""color:#a59ee5;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:15px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:22.5px;"">
																					<p style=""margin: 0;"">Support</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-left:15px;padding-right:15px;padding-top:5px;text-align:center;width:100%;"">
																				<h2
																					style=""margin: 0; color: #ffffff; direction: ltr; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 30px; font-weight: 700; letter-spacing: normal; line-height: 150%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 45px;"">
																					<strong>Comunícate con nosotros</strong></h2>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-4"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:10px;padding-left:15px;padding-right:10px;padding-top:10px;"">
																				<div
																					style=""color:#cdcdcd;direction:ltr;font-family:Arial, Helvetica Neue, Helvetica, sans-serif;font-size:16px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:24px;"">
																					<p style=""margin: 0;"">Tu opinión es importante para
																						nosotros, si tienes dudas, preguntas,
																						inquietudes o quejas puedes comunicarte con
																						nosotros<br /><u><a
																								href=""mailto:udappx@gmail.com""
																								rel=""noopener""
																								style=""text-decoration: underline; color: #8a3b8f;""
																								target=""_blank""><u>udappx@gmail.com</u></a></u>
																					</p>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<div class=""spacer_block block-5""
																		style=""height:25px;line-height:25px;font-size:1px;""> </div>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""image_block block-6"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"" style=""width:100%;"">
																				<div align=""center"" class=""alignment""
																					style=""line-height:10px"">
																					<div style=""max-width: 650px;""><img alt=""App""
																							height=""auto""
																							src=""https://i.ibb.co/pQJN8TV/DALL-E-2024-10-05-16-58-30-A-modern-and-aesthetically-pleasing-mockup-of-a-social-media-website-disp.png""
																							style=""display: block; height: auto; border: 0; width: 100%;""
																							title=""App"" width=""650"" /></div>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
					
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-9""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 30px; padding-left: 20px; padding-right: 20px; padding-top: 30px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""image_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""padding-bottom:10px;width:100%;padding-right:0px;padding-left:0px;"">
																				<div align=""center"" class=""alignment""
																					style=""line-height:10px"">
																					<div style=""max-width: 61px;""><img height=""auto""
																							src=""https://i.ibb.co/9NLC8p9/7c313584485ed5765c8642ef1f3e96bc.png""
																							style=""display: block; height: auto; border: 0; width: 100%;""
																							width=""61"" /></div>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""divider_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"">
																				<div align=""center"" class=""alignment"">
																					<table border=""0"" cellpadding=""0"" cellspacing=""0""
																						role=""presentation""
																						style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																						width=""100%"">
																						<tr>
																							<td class=""divider_inner""
																								style=""font-size: 1px; line-height: 1px; border-top: 1px solid #E4DAFF;"">
																								<span
																									style=""word-break: break-word;""> </span>
																							</td>
																						</tr>
																					</table>
																				</div>
																			</td>
																		</tr>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tr>
																			<td class=""pad"" style=""padding-bottom:5px;padding-top:5px;"">
																				<div
																					style=""color:#444a5b;direction:ltr;font-family:'Inter','Arial';font-size:14px;font-weight:400;letter-spacing:0px;line-height:150%;text-align:center;mso-line-height-alt:21px;"">
																					<p style=""margin: 0; margin-bottom: 16px;"">Copyright
																						© 2024 UdApp, All rights reserved.</p>
																					<p style=""margin: 0; margin-bottom: 16px;"">
																						<br />Universidad de
																						Cundinamarca<br />Ingenieria en Sistemas y
																						Computacion</p>
																					<p style=""margin: 0;""><strong><em>Nota: Este correo
																								se genera automáticamente, por favor no
																								lo responda.</em></strong></p>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-10""
										role=""presentation""
										style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #001A22;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-radius: 0; color: #000000; width: 650px; margin: 0 auto;""
														width=""650"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 15px; padding-top: 15px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""icons_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; text-align: center; line-height: 0;""
																		width=""100%"">
																		<tr>
																			<td class=""pad""
																				style=""vertical-align: middle; color: #ffffff; font-family: inherit; font-size: 14px; text-align: center;"">
																				<!--[if vml]><table align=""center"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;""><![endif]-->
																				<!--[if !vml]><!-->
																				<table cellpadding=""0"" cellspacing=""0""
																					class=""icons-inner"" role=""presentation""
																					style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; display: inline-block; padding-left: 0px; padding-right: 0px;"">
																					<!--<![endif]-->
																					<tr>
																						<td
																							style=""vertical-align: middle; text-align: center; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;"">
																							<img align=""center"" class=""icon""
																								height=""auto""
																								src=""https://i.ibb.co/KN62DjM/heart-white.png""
																								style=""display: block; height: auto; margin: 0 auto; border: 0;""
																								width=""16"" /></td>
																						<td
																							style=""font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 14px; font-weight: undefined; color: #ffffff; vertical-align: middle; letter-spacing: undefined; text-align: center; line-height: normal;"">
																							Made With Love By UdApp</td>
																					</tr>
																				</table>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
					
								</td>
							</tr>
						</tbody>
					</table><!-- End -->
				</body>

				</html>
                ";

                // Instancia de la clase CorreoElectronico
                SmtpCorreos correo = new SmtpCorreos();

                // Llamar al método para enviar el correo con estilo
                await correo.EnviarCorreoConEstilo(destinatario, asunto, body);
            }
            catch (Exception ex)
            {
                // Capturar errores en el envío de correo
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }
    }
}
