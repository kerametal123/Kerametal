Module DataGridExtensions
    <Runtime.CompilerServices.Extension()>
    Function GetRow(ByVal grid As DataGrid, ByVal index As Integer) As DataGridRow
        Dim row As DataGridRow = DirectCast(grid.ItemContainerGenerator.ContainerFromIndex(index), DataGridRow)
        If row Is Nothing Then
            grid.UpdateLayout()
            grid.ScrollIntoView(grid.Items(index))
            row = DirectCast(grid.ItemContainerGenerator.ContainerFromIndex(index), DataGridRow)
        End If 'RAČUNARSKE KOMPONENT 16015 487 Niv. 1072
        'Niv. 1072 2013-03-05 00:00:00
        '100	100 100 100 100    100 84254
        '31392 31392 '31392 '31392
        '31392 31392 31392 31392
        'MUX815 ADELMO 30X30 SCRIPTOR MARAZZI MUX815 ADELMO 30X30 SCRIPTOR MARAZZI
        ' 88249	1	6236	11
        '	88293	1	6280	11	 SY 2 RS EL.RADIJATOR 2512 OMAS	kom	1	RADIJATOR	355	1	91.38	96	118			0		17	860	2006-11-16 00:00:00	10212		0	0		100	100
        '	88275	1	6262	11	UGRADBENA PEĆNICA  M 245W	kom	1	UGRADBENA PEĆNICA	328	1	386.4	420			0		17	273 2010-01-19 00:00:00	10194		0	0		100	100
        Return row
    End Function
End Module