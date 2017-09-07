Imports System.Xml
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.String
Imports System.Data.SqlClient


Public Class Form1

    Public dict As New Dictionary(Of String, Dictionary(Of String, String))
    Public Tables() As String = {"RECIPE", "FERMENTABLE", "HOP", "MISC", "YEAST", "STYLE", "MASH", "MASH_STEPS", "EQUIPMENT", "INGREDIENTS", "PREFERENCES"}

    Private Sub btnNewDatabaseTables_Click(sender As Object, e As EventArgs) Handles btnNewDatabaseTables.Click
        text1.Text = ""
        deleteTables()
        Dim sql As String
        Dim listTable As New List(Of String)(dict.Keys)
        For Each tableName In listTable
            sql = "CREATE TABLE [dbo].[" & tableName & "]  ([Id] INT IDENTITY(1, 1) Not NULL,"
            Dim fields As New List(Of String)(dict.Item(tableName).Keys)
            For Each fieldName In fields
                sql &= "[" & fieldName & "] " & dict(tableName)(fieldName) & " NULL, " '& Environment.NewLine
            Next
            sql &= "LAST_UPDATED DATETIME NULL, PRIMARY KEY CLUSTERED([Id] ASC))"
            sqlExecute(sql)
            Me.text1.Text &= sql & Environment.NewLine
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConnectToDatabase()
        Dim n As Integer = dgv1.Rows.Add()
        Try
            RecipeNameFill("")
        Catch ex As Exception
        End Try
        'populate the dictionary with table definitions
        For Each Table In Me.Tables
            Me.dict(Table) = getTableFields(Table)
        Next
        For Each tableName In Me.dict.Keys
            Me.text1.Text &= tableName & Environment.NewLine
            For Each fieldName In dict(tableName).Keys
                Me.text1.Text &= "  " & fieldName & " " & dict(tableName)(fieldName) & Environment.NewLine
            Next
        Next
        EquipmentPageCreate()       'located in Equipment module
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRecipeNames.SelectedIndexChanged
        Recipe_Fill()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        RecipeNameFill(txtSearch.Text)
    End Sub

    Private Sub btnFileOpen_Click(sender As Object, e As EventArgs) Handles btnFileOpen.Click
        Dim doc As New XmlDocument()

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Me.text1.Text = ""
            Try
                doc.Load(OpenFileDialog1.FileName)
                For Each Table In Me.Tables
                    Dim elemList As XmlNodeList = doc.GetElementsByTagName(Table)
                    For Each nodeTable As XmlElement In elemList
                        Try
                            PopulateTables(Table, nodeTable)
                        Catch ex As Exception
                            Me.text1.Text &= "Error: " & ex.ToString
                        End Try
                    Next
                Next
            Catch ex As Exception
                Me.text1.Text &= "Error: " & ex.ToString
            End Try
        End If
    End Sub

    Sub PopulateTables(ByRef TableName As String, ByRef nodeTable As XmlElement)
        ' Called by btnFileOpen_Click
        If Not (RecordExists(TableName, nodeTable("NAME").InnerText) > 0) Then
            Dim id As Int32 = PerformInsert(TableName, nodeTable)
            Select Case TableName
                Case "RECIPE"
                    Me.text1.Text &= "Recipe ID: " & id.ToString & Environment.NewLine
                    Dim IngredientTables As Array = {"FERMENTABLE", "HOP", "MISC", "YEAST"}
                    Dim Ingredients As XmlNodeList
                    For Each IngredientTable In IngredientTables
                        Me.text1.Text &= IngredientTable & Environment.NewLine
                        Ingredients = nodeTable.GetElementsByTagName(IngredientTable)
                        For Each Ingredient As XmlElement In Ingredients
                            AddIngredientToRecipe(IngredientTable, id, Ingredient)
                        Next
                    Next
                Case Else
                    Exit Select
            End Select
        End If
    End Sub

    Sub AddIngredientToRecipe(ByRef TableName, ByRef RecipeID, ByRef nodeTable)
        ' Called by PopulateTables
        Dim sql As String
        'Add one ingredient to this recipe
        'ingredient will be XmlElement, TableName is FERMENTABLE,HOP,MISC,YEAST...
        text1.Text &= $"Adding {TableName} named {nodeTable.SelectSingleNode("NAME").innertext} to recipe {RecipeID}" & Environment.NewLine
        ' Add this ingredient to correct table if it doesn't already exist
        Dim id = RecordExists(TableName, nodeTable("NAME").InnerText)
        If Not id > 0 Then
            id = PerformInsert(TableName, nodeTable)
        End If
        sql = "INSERT INTO INGREDIENTS ([AMOUNT],[TABLE_NAME],[TABLE_ID],[RECIPE_ID],[TIME_FIELD],[FORM],[USE]) VALUES " _
        & "(" & CDbl(TryGetSingleNodeFromXML(nodeTable, "AMOUNT", 0)) & "," _
        & "'" & TableName & "' ," _
        & id & "," & RecipeID & "," _
        & TryGetSingleNodeFromXML(nodeTable, "TIME", 0) & ",'" _
        & TryGetSingleNodeFromXML(nodeTable, "FORM", "") & "','" _
        & TryGetSingleNodeFromXML(nodeTable, "USE", "") & "')"
        Me.text1.Text &= sql & Environment.NewLine
        sqlExecute(sql)
    End Sub

    Function TryGetSingleNodeFromXML(ByRef nodeTable, ByRef find, ByRef deflt)
        Try
            Return nodeTable.SelectSingleNode(find).innertext
        Catch ex As Exception
            Return deflt
        End Try
    End Function

    Function PerformInsert(ByRef TableName As String, ByRef nodeTable As XmlElement)
        Dim insertFields As String = ""                                'used to build insert sql command
        Dim insertValues As String = ""                                'used to build insert sql command
        Dim sql As String

        For Each field In dict(TableName).Keys
            insertFields &= "[" & field & "],"
            insertValues &= GetAndFormatValue(field, dict(TableName)(field), nodeTable) & ","
        Next
        sql = "INSERT INTO " & TableName & " (" & insertFields _
                & "LAST_UPDATED) VALUES (" & insertValues & "GETDATE()); Select Scope_Identity();"

        Me.text1.Text &= "."
        Me.Update()
        Return SqlExecuteReturnID(sql)
    End Function

    Function RecordExists(ByRef TableName As String, ByRef ItemName As String) As Int16
        Return SqlExecuteReturnID($"Select Id FROM {TableName} WHERE NAME='{ItemName}'")
    End Function

    Function GetAndFormatValue(ByRef fieldName As String, ByVal fieldType As String, ByRef xmlNode As XmlNode) As String
        'fieldType is a dictionary and when i did the substring funtion it messed with d value is whiy byVal above
        Dim temp As String
        Try
            temp = xmlNode(fieldName).InnerText.Replace("'", "''")
        Catch ex As Exception
            temp = ""
        End Try
        Return QuoteMarks(temp, fieldType)
    End Function

    Function isNumericField(ByVal dataType As String) As Boolean
        If InStr(dataType, "VARCHAR") Or dataType = "BIT" Then
            Return False
        End If
        Return True
    End Function

    Private Sub btnDeleteTables_Click(sender As Object, e As EventArgs) Handles btnDeleteTables.Click
        deleteTables()
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim xmldoc = New XmlDocument()
        Dim Text = XmlConvert.EncodeName("Hello, I am text &alpha; &nbsp; &ndash; &mdash;")
        xmldoc.InnerXml = "<p>" & Text & "</p>"
        text1.Text = Text & "Hello, I am text &alpha; &nbsp; &ndash; &mdash;" & Environment.NewLine
    End Sub

    Private Sub numericOnly(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'single routine for any textbox to allow only numeric entry
        Me.txtLastTouched.Text = sender.name
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso e.KeyChar <> "." AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

End Class

'see this about datagridview
'https://stackoverflow.com/questions/14578123/how-to-use-a-progressbar-properly-in-vb-net