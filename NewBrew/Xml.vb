Imports System.Xml

Module Xml

    Sub Testit()
        Dim i As Int16 = 0
        ConnectToDatabase()
        'Concept here is to build required tables based on an XML template provided as masterTableDefinitionXML
        Dim doc As New XmlDocument()
        doc.Load("tables.xml")
        Dim elemList As XmlNodeList = doc.GetElementsByTagName("table")         'list of all the table elements in the tables.xml file
        For Each nodeTable As XmlElement In elemList
            Form1.ProgressBar1.Maximum = 100
            i += 1
            Form1.ProgressBar1.Value = Int((i / elemList.Count) * 100)
            Form1.text1.Text &= "Processing collection: " & nodeTable("collection").InnerText & Environment.NewLine
            Form1.Refresh()
            'Console.WriteLine("Processing collection: " & nodeTable("collection").InnerText)
            CreateTables(nodeTable, 0)
            Try
                CreateTables(nodeTable("sub_table"), 1)
            Catch ex As Exception
            End Try
            PopulateTables_defunct(nodeTable)
        Next
        'Listbox1_Fill()
        DisconnectFromDatabase()
    End Sub

    Dim subTableFields As String
    Dim subTableTypes As String

    Sub CreateTables(ByRef nodeTable, ByRef IsSubTable)
        'Has passed in the XML defining a table, name, collection (of) and fields.
        MsgBox("Create tabels entered")
        Dim sql As String
        Dim XML_SourceData As New XmlDocument()
        subTableFields = ""
        subTableTypes = ""
        Form1.text1.Text &= "Creating table: " & nodeTable("name").InnerText & Environment.NewLine

        'Console.WriteLine("Creating table: " & nodeTable("name").InnerText)
        sql = "CREATE TABLE[dbo].[" & nodeTable("name").InnerText & "] ([Id] INT IDENTITY(1, 1) NOT NULL, "
        Dim fields As XmlNodeList = nodeTable("fields").GetElementsByTagName("field")
        For Each field As XmlElement In fields
            If IsSubTable Then
                subTableFields = subTableFields & field("name").InnerText.ToUpper & ","
                subTableTypes = subTableTypes & field("type").InnerText.ToUpper & ","
            End If
            'Build a sql statement here to create these tables.
            sql &= "[" & field("name").InnerText.ToUpper & "] " & field("type").InnerText & " NULL, "
        Next
        sql &= " PRIMARY KEY CLUSTERED([Id] ASC))"
        sqlExecute(sql)                                                     'Create the table here
    End Sub

    Sub PopulateTables_defunct(ByRef nodeTable)
        Dim listOfFieldNames As String = ""
        Dim listOfFieldTypes As String = ""

        Dim XML_SourceData As New XmlDocument()

        Form1.text1.Text &= "Populating table: " & nodeTable("name").InnerText & " " & nodeTable("file").InnerText & Environment.NewLine
        'Console.WriteLine("Populating table: " & nodeTable("name").InnerText & " " & nodeTable("file").InnerText)

        Dim fields As XmlNodeList = nodeTable("fields").GetElementsByTagName("field")
        For Each field As XmlElement In fields
            listOfFieldNames &= field("name").InnerText & ","
            listOfFieldTypes &= field("type").InnerText & ","
        Next

        'Create two arrays, one with Field names other with Field data types to pass to next routine
        Dim arrNames() As String = listOfFieldNames.Split(","c)
        Dim arrTypes() As String = listOfFieldTypes.Split(","c)
        XML_SourceData.Load(nodeTable("file").InnerText)
        insertIntoTables(arrNames, arrTypes, nodeTable("name").InnerText, XML_SourceData, nodeTable("collection").InnerText)
    End Sub


    Sub insertIntoTables(ByRef arrNames, ByRef arrTypes, ByRef tableName, ByRef XML_SourceData, ByRef collection)
        'Passed in: list of table column names, data type, table name, XML file to parse, and what we are parsing from the XML
        Dim insertFields As String                                  'used to build insert sql command
        Dim insertValues As String                                  'used to build insert sql command
        Dim id As Int32
        Dim elemList As XmlNodeList = XML_SourceData.GetElementsByTagName(collection)

        Dim arrNamesSubTable() As String
        Dim arrTypesSubTable() As String

        If subTableFields.Length > 0 Then
            arrNamesSubTable = subTableFields.Split(","c)
            arrTypesSubTable = subTableTypes.Split(","c)
        End If

        Dim sql As String

        For Each node As XmlElement In elemList
            insertFields = ""                                       'Not zeroing these out caused a bit of troubleshooting.
            insertValues = ""                                       'Not zeroing these out caused a bit of troubleshooting.
            For i As Integer = 0 To arrNames.length - 1
                Try                                                 'See if the database item exists in this XML record, but don't error if not, that is to be expected often
                    insertFields &= "[" & node.SelectSingleNode(arrNames(i).ToString.ToUpper).Name & "],"       'Escape with [brackets] all field names in case a SQL reserved word
                    If InStr(arrTypes(i), "VARCHAR") Or arrTypes(i) = "BIT" Then
                        insertValues &= "'" & node.SelectSingleNode(arrNames(i).ToString.ToUpper).InnerText.Replace("'", "''") & "',"   'Adding outer quote marks and escape any single quotes with double
                    Else                                            ' Values that shouldn't be quoted in the insert sql
                        If IsNumeric(node.SelectSingleNode(arrNames(i).ToString.ToUpper).InnerText) Then
                            insertValues &= node.SelectSingleNode(arrNames(i).ToString.ToUpper).InnerText & ","     'value is numeric, build without single quotes
                        Else
                            insertValues &= "0,"                    'In case someone put in an invalid value in a numeric field. Might want to remove the 0
                        End If
                    End If
                Catch ex As Exception                               'Didn't find what we were looking for. No big deal. No action.
                End Try
            Next
            id = SqlExecuteReturnID("INSERT INTO " & tableName & " (" & insertFields.TrimEnd(CChar(","c)) &
                                    ") VALUES (" & insertValues.TrimEnd(CChar(","c)) & "); SELECT Scope_Identity();")       'calls the execute routine in DB module


            Dim count As Int16 = 0
            '##### something has to happen here
            If collection = "MASH" Then
                For Each node1 As XmlElement In node.SelectSingleNode("MASH_STEPS")
                    count += 1
                    insertFields = ""
                    insertValues = ""
                    For i As Integer = 0 To arrNamesSubTable.Length - 1
                        Try                                                 'See if the database item exists in this XML record, but don't error if not, that is to be expected often
                            insertFields &= "[" & node1.SelectSingleNode(arrNamesSubTable(i).ToString.ToUpper).Name & "],"       'Escape with [brackets] all field names in case a SQL reserved word
                            If InStr(arrTypesSubTable(i), "VARCHAR") Or arrTypesSubTable(i) = "BIT" Then
                                insertValues &= "'" & node1.SelectSingleNode(arrNamesSubTable(i).ToString.ToUpper).InnerText.Replace("'", "''") & "',"   'Adding outer quote marks and escape any single quotes with double
                            ElseIf arrTypesSubTable(i) = "INT" Then
                                insertValues &= Int(node1.SelectSingleNode(arrNamesSubTable(i).ToString.ToUpper).InnerText).ToString & ","
                            Else                                            ' Values that shouldn't be quoted in the insert sql
                                If IsNumeric(node1.SelectSingleNode(arrNamesSubTable(i).ToString.ToUpper).InnerText) Then
                                    insertValues &= node1.SelectSingleNode(arrNamesSubTable(i).ToString.ToUpper).InnerText & ","     'value is numeric, build without single quotes
                                Else
                                    insertValues &= "0,"                    'In case someone put in an invalid value in a numeric field. Might want to remove the 0
                                End If
                            End If
                        Catch ex As Exception                               'Didn't find what we were looking for. No big deal. No action.
                        End Try
                    Next
                    sql = "INSERT INTO MASH_STEPS (MASH_ID,STEP_ORDER," & insertFields.TrimEnd(CChar(","c)) &
                        ") VALUES (" & id.ToString & "," & count.ToString & "," & insertValues.TrimEnd(CChar(","c)) & ")"
                    sqlExecute(sql)
                Next
            End If

        Next
    End Sub

End Module
