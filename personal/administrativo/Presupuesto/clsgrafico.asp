<%
Class clsGrafico

	Sub mostrargrafico(ByRef arrValores, ByRef arrEtiquetas, ByRef strTituloGrafico, ByRef EtiquetaX, ByRef EtiquetaY,ByRef rutaimg,ByVal IncluyeBorde)
		' Some user changable graph defining constants
		' All units are in screen pixels
		Const anchoGrafico  = 350  ' The width of the body of the graph
		Const altoGrafico = 150  ' The heigth of the body of the graph
		Const bordeGrafico = 3    ' The size of the black border
		Const espacioGrafico = 1    ' The size of the space between the bars

		' Declare our variables
		Dim I
		Dim iMaxValue
		Dim iBarWidth
		Dim iBarHeight

		' Get the maximum value in the data set
		iMaxValue = 0
		
		if Ubound(arrValores)>0 then
		
			For I = 0 To UBound(arrValores)
				If cdbl(iMaxValue) < cdbl(arrValores(I)) Then
					iMaxValue = cdbl(arrValores(I))
				end if
			Next 'I
			'Response.Write iMaxValue ' Debugging line

			' Calculate the width of the bars
			' Take the overall width and divide by number of items and round down.
			' I then reduce it by the size of the spacer so the end result
			' should be anchoGrafico or less!
			iBarWidth = (anchoGrafico \ (UBound(arrValores) + 1)) - espacioGrafico
			'Response.Write iBarWidth ' Debugging line

			' Start drawing the graph
			
			if incluyeborde="S" then
				propborde="style=""border-collapse: collapse; border: 1px solid #808080"" bordercolor=""#111111"" "
			end if
			%>
	<TABLE border="0" cellpadding="0" cellspacing="3" <%=propborde%> width="80%" align="center">
		<TR>
			<TD COLSPAN="3" ALIGN="center"><H5><%= strTituloGrafico %></H5>
			</TD>
		</TR>
		<TR>
			<TD VALIGN="center"><B><%= EtiquetaY %></B></TD>
			<TD VALIGN="top">
				<TABLE BORDER="0" CELLSPACING="0" CELLPADDING="0">
					<TR>
						<TD VALIGN="top" ALIGN="right"><%= iMaxValue %>&nbsp;</TD>
					</TR>
				</TABLE>
			</TD>
			<TD>
				<TABLE BORDER="0" CELLSPACING="0" CELLPADDING="0">
					
					<TR>
						<TD VALIGN="bottom"><IMG SRC="<%=rutaimg%>" BORDER="0" WIDTH="<%= bordeGrafico %>" HEIGHT="<%= altoGrafico %>"></TD>
						<%
							' We're now in the body of the chart.  Loop through the data showing the bars!
							For I = 0 To UBound(arrValores)
								if Int(arrValores(I))> 0 then
									iBarHeight = Int((arrValores(I) / iMaxValue) * altoGrafico)
								else
									iBarHeight =0
								end if
								
								

								' This is a hack since browsers ignore a 0 as an image dimension!
								If iBarHeight = 0 Then iBarHeight = 1
							%>
			
							<TD VALIGN="bottom"><IMG SRC="spacer.gif" BORDER="0" WIDTH="<%= GRAPH_SPACER %>" HEIGHT="1"></TD>
								<% if (I mod 2)=0 then %>
									<TD VALIGN="bottom"><IMG SRC="bgtabla.gif" BORDER="0" WIDTH="<%= iBarWidth %>" HEIGHT="<%= iBarHeight %>" ALT="<%= arrValores(I) %>"></TD>
								<% else%>
									<TD VALIGN="bottom"><IMG SRC="R.gif" BORDER="0" WIDTH="<%= iBarWidth %>" HEIGHT="<%= iBarHeight %>" ALT="<%= arrValores(I) %>"></TD>
						
								<% end if%>							

							
						<!--<TD VALIGN="bottom"><IMG SRC="<%=rutaimg%>" BORDER="0" WIDTH="<%= espacioGrafico %>" HEIGHT="1"></TD>
						<TD VALIGN="bottom"><IMG SRC="<%=rutaimg%>" BORDER="0" WIDTH="<%= iBarWidth %>" HEIGHT="<%= iBarHeight %>" ALT="<%= arrValores(I) %>"></TD>-->
											
						<%
							Next 'I
							%>
					</TR>
					<!-- I was using bordeGrafico + anchoGrafico but it was moving the last x axis label -->
					<TR>
						<TD COLSPAN="<%= (2 * (UBound(arrValores) + 1)) + 1 %>"><IMG SRC="<%=rutaimg%>" BORDER="0" WIDTH="<%= bordeGrafico + ((UBound(arrValores) + 1) * (iBarWidth + espacioGrafico)) %>" HEIGHT="<%= bordeGrafico %>"></TD>
					</TR>
					<% ' The label array is optional and is really only useful for small data sets with very short labels! %>
					<% If IsArray(arrEtiquetas) Then %>
					<TR>
						<TD><!-- Spacing for Left Border Column --></TD>
						<% For I = 0 To UBound(arrValores)  %>
						<TD><!-- Spacing for Spacer Column --></TD>
						<TD ALIGN="center"><FONT SIZE="1"><%= arrEtiquetas(I) %></FONT></TD>
						<% Next 'I %>
					</TR>
					<% End If %>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><!-- Place holder for X Axis label centering--></TD>
			<TD ALIGN="center"><BR>
				<B>
					<%= EtiquetaX %>
				</B>
			</TD>
		</TR>
	</TABLE>
	<%end if
	
	End Sub
End Class
%>