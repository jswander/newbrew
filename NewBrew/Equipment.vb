Imports System.Data.SqlClient

Module Equipment

    'key, tuple(type, label, right-label,{set of choices for comboboxes only}, validate (bool), max val, min val)
    Public EquipDict As New Dictionary(Of String, Tuple(Of String, String, String, String(), Boolean, Integer, Integer))
    Dim tp As TabPage
    Private SQLDataReaderOpen As Boolean = False

    Public Sub EquipmentPageCreate()
        'Form1.TabControl1.TabPages.Add("More Great Stuff")
        'Form1.TabControl1.TabPages.Add("Tools")
        Form1.TabControl1.TabPages.Add("Equipment")
        tp = Form1.TabControl1.TabPages(Form1.TabControl1.TabCount() - 1)
        Dim rows As Integer = 0
        Dim RowSpace As Int16 = 26
        Dim startY = 100
        Dim height = 20
        Dim x_gap = 5
        'type, label, right-label,{set of choices for comboboxes only}, validate (bool), max val, min val
        EquipDict.Add("BREWHOUSE_EFFICIENCY", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Brewhouse Efficiency", "Percent (Should be in range of 70-80 or better)", {}, True, 100, 0))
        EquipDict.Add("TUN_VOLUME", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Mash Tun Volume", "Gallons or Liters", {}, False, 100000, 0))
        EquipDict.Add("TUN_WEIGHT", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Mash Tun Weight", "Pounds or Kg", {}, False, 100000, 0))
        EquipDict.Add("TUN_MATERIAL", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Combobox", "Mash Tun Material", "Select from the choices", {"Stainless Steel", "Plastic", "Aluminum", "Copper", "Pyrex", "Unobtainium"}, False, 0, 0))
        EquipDict.Add("TUN_SPECIFIC_HEAT", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Mash Tun Specific Heat", "This is a calculated value based on material, but you can over-ride.", {}, True, 100, 0))
        EquipDict.Add("BOILOFF_HOUR", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Boil Off Rate", "Gal / hour", {}, True, 100, 0))
        EquipDict.Add("COOLING_LOSS_PCT", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Cooling Loss", "Volume lost due to cooling from boiling to room temp (typically 4%)", {}, False, 100, 0))
        EquipDict.Add("LAUTER_DEADSPACE", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "MLT Dead space", "Amount of liquid left in the MLT after transferring to Boil Kettle", {}, False, 100, 0))
        EquipDict.Add("HOP_ABSORPTION", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Hop Absorption Constant", "(oz/oz) This is the amount of wort lost to hop absorption. Typical 5-6", {}, False, 100, 0))
        EquipDict.Add("GRAIN_ABSORPTION", New Tuple(Of String, String, String, String(), Boolean, Integer, Integer) _
            ("Textbox", "Grain Absorption", "Water lost to the grain in the mash process", {}, False, 100, 0))

        Dim Column1Point As Point
        Column1Point.X = 30 'initial X coordinate
        Column1Point.Y = 0
        Dim Column2Point As Point
        Column2Point.Y = 0
        Dim Column3Point As Point
        Column3Point.Y = 0

        'First a label for the chooser for which profile
        Column1Point.Y = rows * RowSpace + startY

        Dim thisLabel = New Label
        With thisLabel
            .Text = "Choose Equipment Profile"
            .Height = height
            .Width = 200
            .Location = Column1Point
            .TextAlign = ContentAlignment.MiddleRight
            Column2Point.X = .Location.X + .Width + x_gap    'sets x-coordinate for next column +5 from end of this
        End With
        tp.Controls.Add(thisLabel)

        'now the chooser combobox
        Column2Point.Y = Column1Point.Y
        Dim chooser = New ComboBox
        With chooser
            .Height = height
            .Width = 250
            .Location = Column2Point
            .Name = "NAME"
            AddHandler chooser.SelectionChangeCommitted, AddressOf EquipmentSelectedChanged
            'SelectionChangeCommitted does not trigger when change is programmatical
        End With
        tp.Controls.Add(chooser)

        rows += 1

        '# Column 1 (labels)
        For Each EquipField In EquipDict.Keys
            'All columns have same Y coordinate value
            Column1Point.Y = rows * RowSpace + startY
            Column2Point.Y = Column1Point.Y
            Column3Point.Y = Column1Point.Y

            thisLabel = New Label
            With thisLabel
                .Text = EquipDict(EquipField).Item2
                .Height = height
                .Width = 200
                .Location = Column1Point
                .TextAlign = ContentAlignment.MiddleRight
                Column2Point.X = .Location.X + .Width + x_gap    'sets x-coordinate for next column +5 from end of this
            End With
            tp.Controls.Add(thisLabel)

            '# Column 2     Determine which field type this is, usual is text but we have choices
            Select Case EquipDict(EquipField).Item1
                Case "Textbox"
                    Dim Thing = New TextBox
                    With Thing
                        .Height = height
                        .Width = 50
                        .Location = Column2Point
                        .Name = EquipField
                        Column3Point.X = .Location.X + .Width + x_gap    'sets x-coordinate for next column +5 from end of this
                        AddHandler Thing.Validating, AddressOf ValidateEq
                    End With
                    tp.Controls.Add(Thing)
                Case "Combobox"
                    Dim Thing = New ComboBox
                    With Thing
                        .Height = height
                        .Width = 150
                        .Location = Column2Point
                        Column3Point.X = .Location.X + .Width + x_gap    'sets x-coordinate for next column +5 from end of this
                        .Name = EquipField
                        'Add pre-defined ComboBox options
                        For Each x In EquipDict(EquipField).Item4
                            Thing.Items.Add(x)
                        Next
                        AddHandler Thing.TextChanged, AddressOf ValidateEq
                    End With
                    tp.Controls.Add(Thing)
            End Select
            '# Column 3
            thisLabel = New Label   'shouldn't have to re-dim a new var here. re-use previous
            With thisLabel
                .Text = EquipDict(EquipField).Item3
                .Height = height
                .Width = 500
                .Location = Column3Point
                .TextAlign = ContentAlignment.MiddleLeft
            End With
            tp.Controls.Add(thisLabel)
            rows += 1
        Next
        FillEquipmentPage()
    End Sub

    Private Sub EquipmentSelectedChanged(sender As Object, e As EventArgs)
        Dim sql As String = "UPDATE EQUIPMENT SET [DEFAULT]='FALSE'; UPDATE EQUIPMENT SET [DEFAULT]='TRUE' WHERE NAME='" _
                   & CType(tp.Controls("NAME"), ComboBox).SelectedItem.replace("'", "''") & "'"
        sqlExecute(sql)
        FillEquipmentPage()
    End Sub




    Function CntrlExistsIn(ctrlName As String, parent As Control) As Boolean
        Dim bResult As Boolean = False
        For Each elem As Control In parent.Controls
            If elem.Name = ctrlName Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Sub FillEquipmentPage()
        Dim reader As SqlDataReader
        Dim cmd As New SqlCommand("SELECT * FROM EQUIPMENT ORDER BY NAME", connection)
        Dim isDefault As Boolean
        CType(tp.Controls("NAME"), ComboBox).Items.Clear()
        reader = cmd.ExecuteReader()
        SQLDataReaderOpen = True
        While reader.Read()
            CType(tp.Controls("NAME"), ComboBox).Items.Add(reader("NAME"))
            Try
                isDefault = reader("DEFAULT")
                If isDefault Then
                    CType(tp.Controls("NAME"), ComboBox).SelectedItem = reader("NAME")
                    For Each field In EquipDict.Keys
                        'Fill in all the textboxes, etc.
                        If CntrlExistsIn(field, tp) Then
                            If Microsoft.VisualBasic.Right(tp.Controls(field).GetType.ToString, 8) = "ComboBox" Then
                                CType(tp.Controls(field), ComboBox).SelectedItem = reader(field)
                            Else
                                tp.Controls(field).Text = reader(field)
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                MsgBox("Error in FillEquipment Try-Catch: " & ex.ToString)
                isDefault = False
            End Try
        End While
        reader.Close()
        SQLDataReaderOpen = False
    End Sub

    Private Sub ValidateEq(sender As Object, e As EventArgs)
        Dim sql As String
        'If validation is set to true (item5 is boolean) then check value.
        If EquipDict(sender.name).Item5 Then
            Try
                Dim val = CDbl(sender.text)
                If Not InRange(val, CDbl(EquipDict(sender.name).Item6), CDbl(EquipDict(sender.name).Item7)) Then
                    sender.text = "0"
                    'MsgBox("Value should be between " & EquipDict(sender.name).Item4 & " and " & EquipDict(sender.name).Item3)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
            End Try
        End If
        'Make sure the reader is not open before trying to update database. If it is open, we are
        'currently reading the equipment table and populating the equipment tab, therefor no changes
        'are being made by the user that need to update the database. The validating event still
        'fired due to code setting off the event.
        If Not SQLDataReaderOpen Then
            'update TUN_SPECIFIC_HEAT then TUN_MATERIAL when TUN_MATERIAL ComboBox changes
            If sender.name = "TUN_MATERIAL" Then
                Dim SpecHeatValue As String = SpecificHeat(tp.Controls("TUN_MATERIAL").Text)
                tp.Controls("TUN_SPECIFIC_HEAT").Text = SpecHeatValue
                sql = $"UPDATE EQUIPMENT SET TUN_SPECIFIC_HEAT = {SpecHeatValue}"
                sql &= $" WHERE NAME='{tp.Controls("NAME").Text.Replace("'", "''")}'"
                sqlExecute(sql)
            End If
            sql = $"UPDATE EQUIPMENT SET {sender.name} = {QuoteMarks(sender.text, Form1.dict.Item("EQUIPMENT").Item(sender.name))}"
            sql &= $" WHERE NAME='{tp.Controls("NAME").Text.Replace("'", "''")}'"
            sqlExecute(sql)
        End If
    End Sub

    Function SpecificHeat(ByRef material) As Double
        Dim ReturnValue As Double = 0
        Select Case material
            Case "Stainless Steel"
                ReturnValue = 0.12
            Case "Plastic"
                ReturnValue = 0.3
            Case "Copper"
                ReturnValue = 0.092
            Case "Aluminum"
                ReturnValue = 0.215
            Case "Pyrex"
                ReturnValue = 0.2
            Case "Unobtainium"
                ReturnValue = 0
        End Select
        Return ReturnValue
    End Function


    Function QuoteMarks(ByRef value, ByRef type) As String
        'If this data type needs quoted in a sql statement this adds the quote marks
        Select Case type.Substring(0, 3)
            Case "VAR", "BIT", "DAT"
                Return "'" & value.replace("'", "''") & "'"
            Case "INT"
                Try
                    Return Int(value).ToString
                Catch ex As Exception
                    Return "0"
                End Try
            Case "FLO"
                Try
                    Return CDbl(value).ToString
                Catch ex As Exception
                    Return "0.0"
                End Try
            Case Else
                Return value
        End Select
    End Function


    Function InRange(ByVal value As Double, ByVal max As Double, Optional ByVal min As Double = 0) As Boolean
        Return (value >= min AndAlso value <= max)
    End Function

End Module
