Imports System.Data.SqlClient

Module Database
    Public connection As SqlConnection

    Sub ConnectToDatabase()
        'Had to add the new item to project first - Data -> service-based database
        connection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\brew.mdf;Integrated Security=True;")
        connection.Open()
        'Form1.text1.Text &= "State: " & connection.State & Environment.NewLine
    End Sub

    Sub DisconnectFromDatabase()
        connection.Close()
    End Sub

    Sub deleteTables()
        Dim sql As String
        Form1.text1.Text = ""
        For Each Table In Form1.Tables
            sql = "IF OBJECT_ID('dbo." & Table & "', 'U') IS NOT NULL DROP TABLE [dbo].[" & Table & "];"
            Form1.text1.Text &= sql & Environment.NewLine
            Dim cmd = New SqlCommand(sql, connection)
            cmd.ExecuteNonQuery()
        Next
    End Sub

    Sub sqlExecute(ByRef sql)
        Dim cmd = New SqlCommand(sql, connection)
        cmd.ExecuteNonQuery()
    End Sub

    Function SqlExecuteReturnID(ByRef sql)
        'Requires appended to the SQL query: SELECT Scope_Identity();
        Dim cmd = New SqlCommand(sql, connection)
        Return CInt(cmd.ExecuteScalar())
    End Function

    Sub RecipeNameFill(ByVal search)

        Dim reader As SqlDataReader
        Dim where As String
        If search <> "" Then
            where = " WHERE NAME LIKE '%" & search.Replace("'", "''") & "%'"
        Else
            where = ""
        End If

        Dim cmd As New SqlCommand("Select ID, NAME FROM RECIPE" & where, connection)

        Form1.lstRecipeNames.Items.Clear()
        reader = cmd.ExecuteReader()
        While reader.Read()
            Form1.lstRecipeNames.Items.Add(reader(1))
        End While
        reader.Close()

    End Sub

    Function getIDFromTable(ByRef TableName, ByRef LookupName) As Int32
        Dim reader As SqlDataReader
        Dim sql As String = $"SELECT ID FROM {TableName} WHERE NAME='{LookupName.replace("'", "''")}'"
        Dim cmd As New SqlCommand(sql, connection)
        Try
            reader = cmd.ExecuteReader()
            reader.Read()
            reader.Close()
            Return CInt(reader("ID"))
        Catch ex As Exception
            reader.Close()
            Return -1
        End Try

    End Function


    Sub Recipe_Fill()
        Dim reader As SqlDataReader
        Dim selName As String = Form1.lstRecipeNames.SelectedItem.ToString
        Dim RecipeID As Int32

        Dim cmd As New SqlCommand("SELECT ID, NAME, TYPE, BREWER, ASST_BREWER, BATCH_SIZE FROM RECIPE WHERE NAME = '" & selName.Replace("'", "''") & "'", connection)
        reader = cmd.ExecuteReader()

        'Try
        reader.Read()
        RecipeID = reader(0)
        Form1.txtName.Text = reader(1)
        Form1.txtType.Text = reader(2)
        Form1.txtBrewer.Text = reader(3)
        Form1.txtAsstBrewer.Text = reader(4)
        Dim vol = ConvertUnits(reader(5), "VOLUME", "READ")
        Form1.txtBatchSize.Text = FormatNumber(vol, 2,,, True)
        reader.Close()
        Form1.dgv1.Rows.Clear()

        Dim IngredientTables As Array = {"FERMENTABLE", "HOP", "MISC", "YEAST"}
        'Dim Ingredients As XmlNodeList
        For Each IngredientTable In IngredientTables
            DataGridFill(RecipeID, IngredientTable)
        Next

    End Sub

    Sub DataGridFill(ByVal RecipeID, TableName)
        ' Called by Recipe_Fill
        Dim reader As SqlDataReader
        Dim cmd As SqlCommand

        Select Case TableName
            Case "FERMENTABLE"
                cmd = New SqlCommand($"SELECT SUM(AMOUNT) FROM INGREDIENTS WHERE INGREDIENTS.TABLE_NAME = '{TableName}' AND RECIPE_ID={RecipeID}", connection)
                reader = cmd.ExecuteReader()
                reader.Read()
                Dim AmountTotal = reader.Item(0)
                reader.Close()
                cmd = New SqlCommand($"Select INGREDIENTS.AMOUNT, {TableName}.TYPE, {TableName}.NAME,{TableName}.COLOR FROM INGREDIENTS INNER JOIN {TableName} ON INGREDIENTS.TABLE_ID = {TableName}.ID WHERE INGREDIENTS.TABLE_NAME = '{TableName}' AND RECIPE_ID={RecipeID} ORDER BY AMOUNT DESC", connection)
                reader = cmd.ExecuteReader()
                Dim n As Integer
                While reader.Read()
                    n = Form1.dgv1.Rows.Add()
                    Form1.dgv1.Rows.Item(n).Cells(0).Value = KiloToPoundsOunces(CDbl(reader.Item("AMOUNT"))).ToString
                    Form1.dgv1.Rows.Item(n).Cells(1).Value = reader.Item("NAME") & " (" & reader.Item("COLOR").ToString & " SRM)"
                    Form1.dgv1.Rows.Item(n).Cells(2).Value = reader.Item("TYPE")
                    Form1.dgv1.Rows.Item(n).Cells(3).Value = Math.Round((CDbl(reader.Item("AMOUNT")) * 100 / CDbl(AmountTotal)), 2).ToString & "%"
                End While
                reader.Close()

            Case "HOP"
                cmd = New SqlCommand($"SELECT SUM(AMOUNT) FROM INGREDIENTS WHERE INGREDIENTS.TABLE_NAME = '{TableName}' AND RECIPE_ID={RecipeID}", connection)
                reader = cmd.ExecuteReader()
                reader.Read()
                Dim AmountTotal = reader.Item(0)
                reader.Close()
                cmd = New SqlCommand($"Select INGREDIENTS.AMOUNT, {TableName}.TYPE, {TableName}.NAME, HOP.ALPHA, INGREDIENTS.[USE], INGREDIENTS.TIME_FIELD FROM INGREDIENTS INNER JOIN {TableName} ON INGREDIENTS.TABLE_ID = {TableName}.ID WHERE INGREDIENTS.TABLE_NAME = '{TableName}' AND RECIPE_ID={RecipeID} ORDER BY INGREDIENTS.[USE], TIME_FIELD DESC", connection)
                reader = cmd.ExecuteReader()
                Dim n As Integer
                While reader.Read()
                    n = Form1.dgv1.Rows.Add()
                    Dim HopUse As String = reader.Item("USE")
                    Dim HopTime As String = reader.Item("TIME_FIELD")
                    Dim HopPeriod As String = " min"
                    If HopUse = "DRY HOP" Then
                        HopTime = (CDbl(HopTime) / 1440).ToString
                        HopPeriod = " days"
                    End If
                    ' Form1.dgv1.Rows.Item(n).Cells(0).Value = ConvertUnits(CDbl(reader.Item("AMOUNT")), "WEIGHT_SMALL", "READ").ToString
                    Dim Alpha As String = "[" & (reader.Item("ALPHA") * 100).ToString & "%]"
                    Form1.dgv1.Rows.Item(n).Cells(0).Value = KiloToPoundsOunces(CDbl(reader.Item("AMOUNT"))).ToString
                    Form1.dgv1.Rows.Item(n).Cells(1).Value = reader.Item("NAME") & " [" & (reader.Item("ALPHA")).ToString & "%] " & HopUse & " " & reader.Item("TIME_FIELD") & HopPeriod
                    Form1.dgv1.Rows.Item(n).Cells(2).Value = "Hop"
                    Form1.dgv1.Rows.Item(n).Cells(3).Value = "calc IBU"
                End While
                reader.Close()

        End Select

    End Sub


    Function KiloToPoundsOunces(ByVal Kg) As String
        ' allows to display weight as for instance "14 lbs, 8 oz"
        ' if less than a pound it doesn't display pounds, only oz
        ' if even pounds, no oz shown.
        Dim lbs As Double = Math.Round(Kg * 2.20462262, 2)
        Dim FullPounds = Int(lbs)
        Dim Ounces As Double
        Dim Answer As String = ""
        If FullPounds >= 1 Then
            Kg -= (FullPounds / 2.20462262)
            Answer = FullPounds.ToString & " lbs"
        End If
        Ounces = Math.Round(Kg * 35.27396195, 3)
        If Ounces > 0 Then
            If Answer <> "" Then    'need comma after pounds
                Answer &= ", "
            End If
            Answer &= Ounces.ToString & " oz"
        End If
        Return Answer
    End Function



    Function getTableFields(ByVal TableName) As Dictionary(Of String, String)
        'These are used to create the tables

        Dim d As New Dictionary(Of String, String)

        Select Case TableName
            Case "FERMENTABLE"
                d.Add("NAME", "VARCHAR(50)")
                d.Add("TYPE", "VARCHAR(50)")
                d.Add("YIELD", "FLOAT(53)")
                d.Add("COLOR", "FLOAT(53)")
                d.Add("ADD_AFTER_BOIL", "BIT")
                d.Add("ORIGIN", "VARCHAR(50)")
                d.Add("SUPPLIER", "VARCHAR(50)")
                d.Add("NOTES", "VARCHAR(MAX)")
                d.Add("COARSE_FINE_DIFF", "FLOAT(53)")
                d.Add("MOISTURE", "FLOAT(53)")
                d.Add("DIASTATIC_POWER", "FLOAT(53)")
                d.Add("PROTEIN", "FLOAT(53)")
                d.Add("MAX_IN_BATCH", "FLOAT(53)")
                d.Add("RECOMMEND_MASH", "BIT")
                d.Add("IBU_GAL_PER_LB", "FLOAT(53)")
                d.Add("POTENTIAL", "FLOAT(53)")
                d.Add("EXTRACT_SUBSTITUTE", "VARCHAR(250)")

            Case "HOP"
                d.Add("NAME", "VARCHAR(50)")
                d.Add("ORIGIN", "VARCHAR(50)")
                d.Add("ALPHA", "FLOAT(53)")
                d.Add("USE", "VARCHAR(50)")
                d.Add("NOTES", "VARCHAR(MAX)")
                d.Add("TYPE", "VARCHAR(50)")
                d.Add("BETA", "FLOAT(53)")
                d.Add("HSI", "FLOAT(53)")
                d.Add("SUBSTITUTES", "VARCHAR(250)")
                d.Add("HUMULENE", "FLOAT(53)")
                d.Add("CARYOPHYLLENE", "FLOAT(53)")

            Case "MISC"
                d.Add("NAME", "VARCHAR(50)")
                d.Add("TYPE", "VARCHAR(50)")
                d.Add("USE", "VARCHAR(50)")
                d.Add("AMOUNT", "FLOAT")
                d.Add("TIME", "INT")
                d.Add("AMOUNT_IS_WEIGHT", "BIT")
                d.Add("USE_FOR", "VARCHAR(250)")
                d.Add("NOTES", "VARCHAR(MAX)")
                d.Add("DISPLAY_AMOUNT", "VARCHAR(50)")
                d.Add("DISPLAY_TIME", "VARCHAR(50)")

            Case "YEAST"
                d.Add("NAME", "VARCHAR(50)")
                d.Add("TYPE", "VARCHAR(50)")
                d.Add("FORM", "VARCHAR(50)")
                d.Add("LABORATORY", "VARCHAR(50)")
                d.Add("PRODUCT_ID", "VARCHAR(50)")
                d.Add("MIN_TEMPERATURE", "FLOAT")
                d.Add("MAX_TEMPERATURE", "FLOAT")
                d.Add("FLOCCULATION", "VARCHAR(50)")
                d.Add("ATTENUATION", "VARCHAR(50)")
                d.Add("NOTES", "VARCHAR(MAX)")
                d.Add("BEST_FOR", "VARCHAR(MAX)")
                d.Add("MAX_REUSE", "INT")

            Case "STYLE"
                d.Add("NAME", "VARCHAR(50)")
                d.Add("CATEGORY", "VARCHAR(50)")
                d.Add("CATEGORY_NUMBER", "INT")
                d.Add("STYLE_LETTER", "VARCHAR(5)")
                d.Add("STYLE_GUIDE", "VARCHAR(20)")
                d.Add("TYPE", "VARCHAR(50)")
                d.Add("NOTES", "VARCHAR(MAX)")
                d.Add("PROFILE", "VARCHAR(MAX)")
                d.Add("INGREDIENTS", "VARCHAR(MAX)")
                d.Add("EXAMPLES", "VARCHAR(MAX)")
                d.Add("OG_MIN", "FLOAT(53)")
                d.Add("OG_MAX", "FLOAT(53)")
                d.Add("FG_MIN", "FLOAT(53)")
                d.Add("FG_MAX", "FLOAT(53)")
                d.Add("IBU_MIN", "FLOAT(53)")
                d.Add("IBU_MAX", "FLOAT(53)")
                d.Add("COLOR_MIN", "FLOAT(53)")
                d.Add("COLOR_MAX", "FLOAT(53)")
                d.Add("CARB_MIN", "FLOAT(53)")
                d.Add("CARB_MAX", "FLOAT(53)")
                d.Add("ABV_MIN", "FLOAT(53)")
                d.Add("ABV_MAX", "FLOAT(53)")

            Case "RECIPE"
                d.Add("NAME", "VARCHAR(100)")
                d.Add("TYPE", "VARCHAR(20)")
                d.Add("BREWER", "VARCHAR(100)")
                d.Add("ASST_BREWER", "VARCHAR(100)")
                d.Add("BATCH_SIZE", "FLOAT")
                d.Add("BOIL_SIZE", "FLOAT")
                d.Add("BOIL_TIME", "FLOAT")
                d.Add("EFFICIENCY", "FLOAT")
                d.Add("NOTES", "VARCHAR(MAX)")
                d.Add("TASTE_NOTES", "VARCHAR(MAX)")
                d.Add("TASTE_RATING", "FLOAT")
                d.Add("OG", "FLOAT")
                d.Add("FG", "FLOAT")
                d.Add("CARBONATION", "FLOAT")
                d.Add("FERMENTATION_STAGES", "TINYINT")
                d.Add("PRIMARY_AGE", "INT")
                d.Add("PRIMARY_TEMP", "FLOAT")
                d.Add("SECONDARY_AGE", "INT")
                d.Add("SECONDARY_TEMP", "FLOAT")
                d.Add("TERTIARY_AGE", "INT")
                d.Add("TERTIARY_TEMP", "FLOAT")
                d.Add("AGE", "INT")
                d.Add("AGE_TEMP", "FLOAT")
                d.Add("CARBONATION_USED", "VARCHAR(50)")
                d.Add("FORCED_CARBONATION", "BIT")
                d.Add("PRIMING_SUGAR_NAME", "VARCHAR(50)")
                d.Add("PRIMING_SUGAR_EQUIV", "FLOAT")
                d.Add("DATE", "DATETIME")
                d.Add("EST_OG", "VARCHAR(20)")
                d.Add("EST_FG", "VARCHAR(20)")
                d.Add("EST_COLOR", "VARCHAR(20)")
                d.Add("IBU", "VARCHAR(20)")
                d.Add("IBU_METHOD", "VARCHAR(20)")
                d.Add("EST_ABV", "VARCHAR(20)")
                d.Add("ABV", "VARCHAR(20)")
                d.Add("STYLE_ID", "INT")
                d.Add("MASH_ID", "INT")
                d.Add("EQUIPMENT_ID", "INT")

            Case "MASH"
                d.Add("NAME", "VARCHAR(50)")
                d.Add("GRAIN_TEMP", "FLOAT")
                d.Add("TUN_TEMP", "FLOAT")
                d.Add("SPARGE_TEMP", "FLOAT")
                d.Add("PH", "FLOAT")
                d.Add("TUN_WEIGHT", "FLOAT")
                d.Add("TUN_SPECIFIC_HEAT", "FLOAT")
                d.Add("EQUIP_ADJUST", "BIT")
                d.Add("NOTES", "VARCHAR(MAX)")

            Case "MASH_STEPS"
                d.Add("MASH_ID", "INT")
                d.Add("STEP_ORDER", "INT")
                d.Add("NAME", "VARCHAR(100)")
                d.Add("TYPE", "VARCHAR(100)")
                d.Add("INFUSE_AMOUNT", "FLOAT")
                d.Add("STEP_TIME", "INT")
                d.Add("STEP_TEMP", "FLOAT")
                d.Add("RAMP_TIME", "INT")
                d.Add("END_TEMP", "FLOAT")
                d.Add("DESCRIPTION", "VARCHAR(MAX)")
                d.Add("WATER_GRAIN_RATIO", "FLOAT")
                d.Add("DECOCTION_AMT", "VARCHAR(100)")
                d.Add("INFUSE_TEMP", "VARCHAR(50)")

            Case "EQUIPMENT"
                d.Add("NAME", "VARCHAR(80)")
                d.Add("BOIL_SIZE", "FLOAT")
                d.Add("BATCH_SIZE", "FLOAT")
                d.Add("TUN_VOLUME", "FLOAT")
                d.Add("TUN_WEIGHT", "FLOAT")
                d.Add("TUN_SPECIFIC_HEAT", "FLOAT")
                d.Add("TOP_UP_WATER", "FLOAT")
                d.Add("TRUB_CHILLER_LOSS", "FLOAT")
                d.Add("EVAP_RATE", "FLOAT")
                d.Add("BOIL_TIME", "INT")
                d.Add("CALC_BOIL_VOLUME", "BIT")
                d.Add("LAUTER_DEADSPACE", "FLOAT")
                d.Add("TOP_UP_KETTLE", "FLOAT")
                d.Add("HOP_UTILIZATION", "FLOAT")
                d.Add("NOTES", "VARCHAR(256)")
                d.Add("GRAIN_ABSORPTION", "FLOAT")
                d.Add("TUN_MATERIAL", "VARCHAR(30)")
                d.Add("DEFAULT", "BIT")
                d.Add("BOILOFF_HOUR", "FLOAT")
                d.Add("HOP_ABSORPTION", "FLOAT")
                d.Add("BREWHOUSE_EFFICIENCY", "INT")
                d.Add("BREWERY_ALTITUDE", "FLOAT")
                d.Add("FERMENTER_LOSS", "FLOAT")
                d.Add("DISPLAY_BOIL_SIZE", "VARCHAR(10)")
                d.Add("DISPLAY_BATCH_SIZE", "VARCHAR(10)")
                d.Add("DISPLAY_TUN_VOLUME", "VARCHAR(10)")
                d.Add("DISPLAY_TUN_WEIGHT", "VARCHAR(10)")
                d.Add("DISPLAY_TOP_UP_WATER", "VARCHAR(10)")
                d.Add("DISPLAY_TRUB_CHILLER_LOSS", "VARCHAR(10)")
                d.Add("DISPLAY_LAUTER_DEADSPACE", "VARCHAR(10)")
                d.Add("DISPLAY_TOP_UP_KETTLE", "VARCHAR(10)")
                d.Add("COOLING_LOSS_PCT", "FLOAT")

            Case "INGREDIENTS"
                d.Add("AMOUNT", "FLOAT")
                d.Add("AMOUNT_UNITS", "VARCHAR(10)")
                d.Add("AMOUNT_IS_WEIGHT", "BIT")
                d.Add("TABLE_NAME", "VARCHAR(25)")
                d.Add("TABLE_ID", "INT")
                d.Add("RECIPE_ID", "INT")
                d.Add("TIME_FIELD", "INT")
                d.Add("FORM", "VARCHAR(10)")
                d.Add("USE", "VARCHAR(10)")

            Case "PREFERENCES"
                d.Add("FILEPATH", "VARCHAR(200)")
                d.Add("LAST_RECIPE", "INT")
                d.Add("IMPERIAL_UNITS", "BIT")

        End Select
        Return (d)

    End Function


End Module
