Module Utility




    Function ConvertUnits(ByVal item As Single, ByVal units As String, ByVal direction As String)
        'Metric-ize or Imerial-ize units. In a few cases it is called where we know the math is correct but the units may seem odd at first glance.
        Dim temp As Single
        Dim myItem As String
        Dim ImperialUnits As Boolean
        myItem = Trim(item)

        Dim isImperial As Boolean = True

        ImperialUnits = isImperial                                      'should redo below so no new var
        Select Case UCase(direction)
            Case "READ"
                Select Case UCase(units)
                    Case "GRAINABSORPTION"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 5)    'c 
                        Else
                            ConvertUnits = Math.Round(Val(myItem) * 0.479306, 3) 'liters per kilogram to quarts per pound
                        End If
                    Case "DEGREES"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 5)    'c 
                        Else
                            ConvertUnits = Math.Round(Val(myItem) * 9 / 5 + 32, 5)   'c to f
                        End If
                    Case "FLOAT"
                        ConvertUnits = Math.Round(CSng(myItem), 5)
                    Case "INTEGER"
                        ConvertUnits = CInt(myItem)
                    Case "VOLUME"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 5)        'L 
                        Else
                            ConvertUnits = Math.Round(Val(myItem) / 3.78541, 5)      'L to Gal
                        End If
                    Case "WEIGHT"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 6)        'kg 
                        Else
                            ConvertUnits = Math.Round(Val(myItem) * 2.2046226, 6)        'kg to lbs
                        End If
                    Case "QT_OR_LITER"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 5)        'L
                        Else
                            ConvertUnits = Math.Round(Val(myItem) * 1.05669, 5)      'L to qt
                        End If
                    Case "WEIGHT_SMALL"
                        If myItem = "" Then
                            myItem = 0  'workamath.round for empty value
                        End If
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem) * 1000, 3)     'grams
                        Else
                            ConvertUnits = Math.Round(Val(myItem) * 35.2739619, 3) 'kg to oz
                        End If
                    Case "RATIO1"
                        If Not ImperialUnits Then
                            'this is one that Beersmith likes to save as qts/lb
                            ConvertUnits = Math.Round(CSng(myItem) * 2.086351, 5)        'L/kg to qts/lb
                        Else
                            myItem = Replace(myItem, "qt/lb", "")
                            temp = CSng(Trim(myItem))
                            ConvertUnits = Math.Round(temp, 5)
                        End If
                    Case "DAYS"
                        ConvertUnits = CSng(myItem) / 1440      'minutes to days
                    Case "PERCENT"
                        ConvertUnits = Math.Round(CSng(myItem) / 100, 3)
                    Case "METERS/FEET"
                        ConvertUnits = Math.Round(CSng(myItem) * 3.28084, 0)
                    Case "TEXT"
                        'ConvertUnits = replace(myItem,"\n",chr(10))
                        'ConvertUnits = ampersandConvert(myItem, "read")
                    Case Else
                        ConvertUnits = myItem
                End Select
            Case "WRITE"
                On Error GoTo badvalue
                Select Case UCase(units)
                    Case "GRAINABSORPTION"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 3)    'c 
                        Else
                            ConvertUnits = Math.Round(Val(myItem) / 0.479306, 3) 'liters per kilogram to quarts per pound
                        End If
                    Case "DEGREES"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(CSng(myItem), 5)
                        Else
                            ConvertUnits = Math.Round((CSng(myItem) - 32) * 5 / 9, 3)    'f to c		'kg to oz
                        End If
                    Case "FLOAT"
                        ConvertUnits = Math.Round(Val(myItem), 4)
                    Case "INTEGER"
                        ConvertUnits = CInt(myItem)
                    Case "VOLUME"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(CSng(myItem), 5)
                        Else
                            ConvertUnits = Math.Round(CSng(myItem) * 3.78541, 5)     'gal to L
                        End If
                    Case "WEIGHT"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem), 7)
                        Else
                            ConvertUnits = Math.Round(Val(myItem) / 2.2046226, 5)    'lbs to kg
                        End If
                    Case "WEIGHT_SMALL"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(Val(myItem) / 1000, 5) 'g to kg
                        Else
                            ConvertUnits = Math.Round(Val(myItem) * 0.0283495, 5)    'oz to kg
                        End If
                    Case "RATIO1"
                        If Not ImperialUnits Then
                            ConvertUnits = Math.Round(CSng(myItem) / 2.086351, 5)        'qts/lb to L/kg
                        Else
                            ConvertUnits = Math.Round(CSng(myItem), 3)
                        End If
                    'ConvertUnits = math.round(CSng(myItem),3)		'qts/lb to L/kg
                    'ConvertUnits = math.round(CSng(myItem) * 2.086351,3)		'qts/lb to L/kg
                    Case "DAYS"
                        ConvertUnits = CSng(myItem) * 1440      'days to minutes
                    Case "PERCENT"
                        ConvertUnits = Math.Round(CSng(myItem) * 100, 3)
                    Case "METERS/FEET"
                        ConvertUnits = Math.Round(CSng(myItem) / 3.28084, 3)
                    Case "TEXT"
                        'ConvertUnits = ampersandConvert(myItem, "write")
                    Case Else
                        ConvertUnits = myItem
                End Select
                GoTo endline
badvalue:
                MsgBox(myItem + "can't convert to " + units)
endline:
        End Select
errorHandler:
    End Function

    '    Sub dlgRecipeUpdate(Optional XrecipeID)
    '        Dim sql As String
    '        Dim ResultSet As Object
    '        Dim rs As Object        'I should double check I need both ResultSet and rs ....
    '        Dim myName As String
    '        Dim boilTime As Integer
    '        Dim fermentationStages As Integer
    '        Dim srm As Double
    '        Dim lngColor As Double


    '        Dim eqTrubChillerLoss As Double
    '        Dim eqLauterDeadSpace As Double
    '        Dim eqBOILOFFHOUR As Double
    '        Dim eqHOPABSORPTION As Double
    '        'dim eqGRAINABSORPTION as single 			defined globally
    '        Dim eqFERMENTERLOSS As Double
    '        Dim eqTOPUPWATER As Double
    '        '	eqGrainSpecificHeat as single
    '        '		efficiency as single
    '        Dim hopAbsorptionBoil As Double


    '        Dim hopAbsorptionDry As Double
    '        Dim hopAbsorption As Double
    '        Dim litersLostInFermenter As Double
    '        Dim litersRequiredIntoFermenter As Double
    '        Dim litersCoolingLoss As Double
    '        Dim gravityPPG As Double
    '        Dim degreesPlato As Double
    '        Dim yeastMfgTargetPitch As Double
    '        Dim recommendedPitch As Double
    '        Dim finalGravity As Double
    '        Dim recipeABV As Double

    '        'Next set of vars are for the style "graph"
    '        Dim labels()
    '        Dim dotwidth As Integer
    '        Dim i As Integer
    '        Dim ps As Object
    '        Dim psY As Integer
    '        Dim xyz As Object
    '        Dim lblLength As Integer
    '        Dim labeloffset As Integer
    '        Dim fraction As Double


    '        '	on error goto errorExit
    '        recipeLoading = True

    '        'these are the column widths for ingredients
    '        colWidths = Array(15, 50, 15, 15, 10, 10) ' for the ingredients display area
    '        'xray dlg.getControl("ListBoxChooseRecipe")

    '        Debug(1, "dlgRecipeUpdate XrecipeID=" & XrecipeID)

    '        dlgRecipeClear
    '        sql = "SELECT NAME,ID,NOTES,BREWER,ASST_BREWER,""DATE"",BATCH_SIZE,STYLE_ID,BOIL_TIME,EFFICIENCY,TYPE,MASH_ID,"_
    '	& "PRIMARY_AGE,SECONDARY_AGE,TERTIARY_AGE,PRIMARY_TEMP,SECONDARY_TEMP,TERTIARY_TEMP,FERMENTATION_STAGES " _
    '	& "from RECIPE WHERE ID = '" & recipeID & "'"

    '	'recipeID = XrecipeID
    '	ResultSet = dbGetResultSet(sql)
    '        If (ResultSet.next) Then
    '            myName = ResultSet.getString(1)
    '            recipeID = ResultSet.getInt(2)
    '            dlg.getControl("RECIPE_ID").text = recipeID
    '            dlg.getControl("NOTES").text = ampersandConvert(ResultSet.getString(3), "read")
    '            dlg.getControl("BREWER").text = ResultSet.getString(4)
    '            dlg.getControl("ASST_BREWER").text = ResultSet.getString(5)
    '            dlg.getControl("DATE").text = ResultSet.getString(6)
    '            batchSize = ResultSet.getFloat(7)
    '            dlg.getControl("BATCH_SIZE").value = ConvertUnits(batchSize, "VOLUME", "READ")
    '            dlg.getControl("lblBatchSize").text = getUnits("VOLUME")
    '            styleID = ResultSet.getInt(8)
    '            boilTime = ResultSet.getInt(9)
    '            efficiency = ResultSet.getFloat(10)
    '            dlg.getControl("numEfficiency").value = efficiency
    '            dlg.getControl("TYPE").selectItem(ResultSet.getString(11), True)
    '            mashID = ResultSet.getInt(12)
    '            dlg.getControl("numBoilTime").value = boilTime
    '            dlg.getControl("EQUIPMENT.BOIL_TIME").value = boilTime  'displayed here too
    '            dlg.getControl("PRIMARY_AGE").value = ResultSet.getInt(13)
    '            dlg.getControl("SECONDARY_AGE").value = ResultSet.getInt(14)
    '            dlg.getControl("TERTIARY_AGE").value = ResultSet.getInt(15)
    '            dlg.getControl("PRIMARY_TEMP").value = ConvertUnits(ResultSet.getFloat(16), "DEGREES", "read")
    '            dlg.getControl("SECONDARY_TEMP").value = ConvertUnits(ResultSet.getFloat(17), "DEGREES", "read")
    '            dlg.getControl("TERTIARY_TEMP").value = ConvertUnits(ResultSet.getFloat(18), "DEGREES", "read")
    '            If batchSize <= 0 Then
    '                MsgBox("Recipe has invalid batch size. Please correct.")
    '                batchSize = 1
    '            End If
    '            'xray dlg.getControl("FERMENTATION_STAGES")
    '            fermentationStages = ResultSet.getInt(19)
    '            Select Case fermentationStages
    '                Case 1
    '                    dlg.getControl("SECONDARY_AGE").model.enabled = False
    '                    dlg.getControl("SECONDARY_TEMP").model.enabled = False
    '                    dlg.getControl("TERTIARY_AGE").model.enabled = False
    '                    dlg.getControl("TERTIARY_TEMP").model.enabled = False
    '                    dlg.getControl("lblSecondaryAge").model.enabled = False
    '                    dlg.getControl("lblSecondaryTemp").model.enabled = False
    '                    dlg.getControl("lblTertiaryAge").model.enabled = False
    '                    dlg.getControl("lblTertiaryTemp").model.enabled = False
    '                Case 2
    '                    dlg.getControl("TERTIARY_AGE").model.enabled = False
    '                    dlg.getControl("TERTIARY_TEMP").model.enabled = False
    '                    dlg.getControl("lblTertiaryAge").model.enabled = False
    '                    dlg.getControl("lblTertiaryTemp").model.enabled = False
    '            End Select
    '            dlg.getControl("FERMENTATION_STAGES").selectItem(fermentationStages, True)
    '        Else
    '            'msgbox "No results returned: " & sql
    '        End If

    '        'COLUMN HEADINGS 
    '        dlg.getControl("lblIngredients").setText(pad("Amount", colWidths(0)) & pad("Item", colWidths(1)) _
    '    & pad("Type", colWidths(2)) & pad("% / IBU", colWidths(3)) & "   Cost      Inventory")

    '        dlg.getControl("lstIngredients").removeItems(0, dlg.getControl("lstIngredients").getItemCount())

    '        ReDim alstIngredientsID()   'holds ingredients table id for each item in main ingredients listbox
    '        ReDim arrIngredients()  'this one holds ingredient name. May revisit to see that i really need this

    '        ' FERMENTABLES
    '        dlgRecipeGetFermentables(recipeID)  'Need fermentables done before hops so I can calc IBU
    '        'xray alstIngredientsID

    '        dlg.getControl("totalGrains").value = ConvertUnits(totalGrain, "WEIGHT", "READ")

    '        ' Morey's Formula : SRM = 1.4922 * (MCU ^ 0.6859).
    '        srm = totalMCU ^ 0.6859 * 1.4922
    '        dlg.getControl("lblColor").text = Format(srm, "#0.0")
    '        dlg.getControl("lblColor").Model.helptext = CStr(totalMCU) & "^0.6859 * 1.4922"

    '        'RGB values for SRM between 0 and 40 (black) are stored in table
    '        'with one decimal point accuracy (but values are stored x10)
    '        'Using Float datatype didn't work well in this table as the lookup
    '        'done on float value often failed because values stored with tiny rounding
    '        'errors. 
    '        Dim srm2 As Long

    '        srm2 = CLng(srm * 10)
    '        If srm2 > 400 Then
    '            srm2 = 400 ' black is black
    '        End If
    '        If srm2 = 0 Then
    '            srm2 = 1 ' 0 is not in DB table
    '        End If
    '        sql = "SELECT R,G,B FROM SRM2RGB WHERE SRM = " & CStr(srm2)
    '        ResultSet = dbGetResultSet(sql)
    '        ResultSet.next
    '        'oMeta = ResultSet.getMetaData()
    '        lngColor = RGB(ResultSet.getInt(1), ResultSet.getInt(2), ResultSet.getInt(3))
    '        'oSheet.getCellRangeByName("ColorSample").CellBackColor = lngColor
    '        dlg.getControl("lblColorSample").Model.BackgroundColor = lngColor

    '        'MISC
    '        dlgRecipeGetMiscs(recipeID)

    '        'STYLE
    '        dlgRecipeGetStyle(recipeID)
    '        If styleID = 0 Then
    '            GoTo errorExit
    '        End If

    '        'EQUIPMENT
    '        sql = "SELECT ID,NAME,TRUB_CHILLER_LOSS,LAUTER_DEADSPACE,BOILOFF_HOUR,HOP_ABSORPTION,GRAIN_ABSORPTION,FERMENTER_LOSS,TOP_UP_WATER,GRAIN_SPECIFIC_HEAT,BREWHOUSE_EFFICIENCY "_
    '		& "FROM EQUIPMENT WHERE DEFAULT=TRUE"
    '	rs = dbGetResultSet(sql)
    '        If rs.next Then
    '            equipmentID = rs.getInt(1)
    '            dlg.getControl("txtEquipment").text = rs.getString(2)
    '            eqTrubChillerLoss = rs.getFloat(3)
    '            eqLauterDeadSpace = rs.getFloat(4)
    '            eqBOILOFFHOUR = rs.getFloat(5)
    '            eqHOPABSORPTION = rs.getFloat(6)            'This should be liters per kg.
    '            eqGRAINABSORPTION = rs.getFloat(7)
    '            eqFERMENTERLOSS = rs.getFloat(8)
    '            eqTOPUPWATER = rs.getFloat(9)
    '            eqGrainSpecificHeat = rs.getFloat(10)
    '            efficiency = rs.getFloat(11)
    '            dlg.getControl("numEfficiency").value = efficiency
    '        End If

    '        '	Set Units depending on Imperial / Metric
    '        If isImperial Then dlg.getControl("lblTotalGrains").text = "Total Grain (lb)" Else dlg.getControl("lblTotalGrains").text = "Total Grain (kg)"
    '        If isImperial Then dlg.getControl("lblTotalHops").text = "Total Hops (oz)" Else dlg.getControl("lblTotalHops").text = "Total Hops (grams)"

    '        'HOPS
    '        'NOTE - This selects all hops EXCEPT dry hop... notice the <> vs. = operator (includes boil, first wort)
    '        sql = "SELECT SUM(AMOUNT) FROM INGREDIENTS WHERE TABLE_NAME = 'HOP' AND USE <> 'Dry Hop' AND RECIPE_ID = " & CStr(recipeID)
    '        rs = dbGetResultSet(sql)
    '        If rs.next Then
    '            boilHops = rs.getFloat(1)   'in kg
    '        End If

    '        sql = "SELECT SUM(AMOUNT) FROM INGREDIENTS WHERE TABLE_NAME = 'HOP' AND USE = 'Dry Hop' AND RECIPE_ID = " & CStr(recipeID)
    '        rs = dbGetResultSet(sql)
    '        If rs.next Then
    '            dryHops = rs.getFloat(1)    'in kg
    '        End If

    '        totalHops = dryHops + boilHops

    '        hopAbsorptionBoil = eqHOPABSORPTION * boilHops

    '        hopAbsorptionDry = eqHOPABSORPTION * dryHops
    '        hopAbsorption = eqHOPABSORPTION * totalHops

    '        'I should be pulling this from DB or it should be a global variable instead of pulling from sheet.
    '        litersLostInFermenter = ConvertUnits(dlg.getControl("EQUIPMENT.FERMENTER_LOSS").value, "VOLUME", "write")
    '        dlg.getControl("EQUIPMENT.DRYHOP_LOSS").value = ConvertUnits(hopAbsorptionDry, "VOLUME", "read")
    '        litersRequiredIntoFermenter = batchSize + litersLostInFermenter + hopAbsorptionDry

    '        'gallonsRequiredIntoFermenter = convertUnits(litersRequiredIntoFermenter,"VOLUME","READ")
    '        dlg.getControl("EQUIPMENT.REQUIRED_IN_FERMENTER").value = ConvertUnits(litersRequiredIntoFermenter, "VOLUME", "read")

    '        'Losses prior to fermentor
    '        'Boiloff
    '        boilOff = boilTime / 60 * eqBOILOFFHOUR
    '        dlg.getControl("EQUIPMENT.BOILOFF_TOTAL").value = ConvertUnits(boilOff, "VOLUME", "READ")
    '        'Trub / Chiller
    '        dlg.getControl("EQUIPMENT.TRUB_CHILLER_LOSS").value = ConvertUnits(eqTrubChillerLoss, "VOLUME", "READ")

    '        'Boil Hops Absorption
    '        '	dlg.getControl("EQUIPMENT.HOP_ABSORPTION_VOL").value =  convertUnits(hopAbsorptionBoil,"VOLUME","READ") 


    '        'Cooling


    '        'litersCoolingLoss = ( (dlg.getControl("EQUIPMENT.COOLING_LOSS_PCT").value + 100) / 100 * litersRequiredIntoFermenter )  - litersRequiredIntoFermenter

    '        'This is the more simple formula
    '        litersCoolingLoss = dlg.getControl("EQUIPMENT.COOLING_LOSS_PCT").value * litersRequiredIntoFermenter / 100

    '        dlg.getControl("NumericEq_COOLING_LOSS_VOL").value = ConvertUnits(litersCoolingLoss, "VOLUME", "READ")

    '        'Calculate boilSize accounting for losses but subtracting eqeqTOPUPWATER that some may use
    '        boilSize = litersRequiredIntoFermenter + boilOff + eqTrubChillerLoss + hopAbsorption + litersCoolingLoss - eqTOPUPWATER

    '        'galboilSize = convertUnits(boilSize,"VOLUME","READ")

    '        dlg.getControl("numBoilSize").value = ConvertUnits(boilSize, "VOLUME", "READ")
    '        dlg.getControl("EQUIPMENT.BOIL_TIME").value = boilTime  'Display Boil Time on equipment page

    '        'YEAST
    '        dlgRecipeGetYeast(recipeID)
    '        'CALCULATE ESTIMATED GRAVITY, ABV
    '        'OG
    '        'ogPostBoil = totalPoints / convertUnits(batchSize,"VOLUME","READ") * efficiency / 100 / 1000 + 1

    '        'ogPostBoil = totalPoints / batchSize * 0.264172 * efficiency / 100 / 1000 + 1
    '        ogPostBoil = totalPoints / (batchSize / 3.78541) * efficiency / 100 / 1000 + 1

    '        dlg.getControl("lblOG").text = Format(ogPostBoil, "#.##0")

    '        'pitching rate, plato
    '        gravityPPG = (totalPoints * efficiency / 100) / (batchSize / 3.78541)
    '        degreesPlato = gravityPPG / 4       'not displayed, but could be easily

    '        '.35M cells / mL / deg Plato
    '        'recommendedPitch = 350000 * batchSize * 1000 * degreesPlato / 10^9
    '        yeastMfgTargetPitch = 35
    '        'removed some 0's from formula, math is same
    '        recommendedPitch = yeastMfgTargetPitch * batchSize * degreesPlato / 100

    '        dlg.getControl("lblPitch").text = Format(recommendedPitch, "##########") & " billion cells"
    '        If recommendedPitch > 100 Then
    '            dlg.getControl("lblPitchStarter").text = "Starter recommended"
    '        Else
    '            dlg.getControl("lblPitchStarter").text = "Starter optional"
    '        End If
    '        'FG
    '        finalGravity = ogPostBoil + (1 - ogPostBoil) * yeastAttenuation / 100
    '        dlg.getControl("lblFG").text = Format(finalGravity, "#.##0")

    '        'ABV
    '        recipeABV = ((76.08 * (ogPostBoil - finalGravity) / (1.775 - finalGravity)) * (finalGravity / 0.794))
    '        dlg.getControl("lblABV").text = Format(recipeABV, "##.0")

    '        'HOPS
    '        dlgRecipeGetHops(recipeID)
    '        dlg.getControl("totalHops").value = ConvertUnits(totalHops, "WEIGHT_SMALL", "READ")
    '        dlg.getControl("lblIBU").text = Format(totalIBU, "##0.#")

    '        'MASH
    '        dlgRecipeGetMash(recipeID)

    '        'Cost
    '        dlg.getControl("totalCost").value = totalCost

    '        'Toying with a graphical scale for Style values og,fg,abv,etc

    '        labels = Split(",lblOG,lblFG,lblABV,lblIBU,lblColor", ",")
    '        Dim a(5)
    '        a(1) = (ogPostBoil - styleOGmin) / (styleOGMax - styleOGmin)
    '        a(2) = (finalGravity - styleFGmin) / (styleFGMax - styleFGmin)
    '        a(3) = (recipeABV - styleABVmin) / (styleABVMax - styleABVmin)
    '        a(4) = (totalIBU - styleIBUmin) / (styleIBUMax - styleIBUMin)
    '        a(5) = (srm - styleColormin) / (styleColorMax - styleColorMin)
    '        '	xray a
    '        ' 	should also change to use index 0-4
    '        dotwidth = 10
    '        For i = 1 To 5
    '            xyz = dlg.getControl("txtLine" & CStr(i)).getPosSize        'x, y, width, height
    '            dlg.getControl("txtLine" & CStr(i)).text = "-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|-----|"
    '            ps = dlg.getControl("txtLine" & CStr(i)).getPosSize
    '            dlg.getControl("txtDot" & CStr(i)).Model.BackgroundColor = RGB(128, 128, 128)
    '            fraction = a(i)
    '            psY = ps.Y
    '            If a(i) > 1 Or a(i) < 0 Then
    '                dlg.getControl("txtDot" & CStr(i)).Model.BackgroundColor = RGB(255, 0, 0)
    '                If a(i) > 1 Then
    '                    a(i) = 1
    '                Else
    '                    a(i) = 0
    '                End If
    '            End If

    '            lblLength = Len(dlg.getControl(labels(i)).text)
    '            If a(i) > 0.5 Then                  'which side of the "dot" do we place the text value
    '                labeloffset = -9 * lblLength        'all this bit of nonsense is simply for asthetics
    '            Else                                'to position the value label a reasonable distance from
    '                labeloffset = 10                    'the "dot" on the green graph of "this recipe" values
    '            End If
    '            dlg.getControl(labels(i)).setPosSize(ps.X + Int(a(i) * ps.Width) + labeloffset, ps.Y - 1, 0, 0, 3)
    '            dlg.getControl("txtDot" & CStr(i)).setPosSize(ps.X + Int(a(i) * ps.Width) - 7, ps.Y + 2, 7, ps.Height - 4, 15)
    '            dlg.getControl("txtDot" & CStr(i)).Model.Enabled = 0
    '            dlg.getControl("txtDot" & CStr(i)).Model.Tabstop = False
    '            dlg.getControl("txtDot" & CStr(i)).Model.HelpText = "OK"
    '            'dlg.getControl("txtDot" & cStr(i)).Model.Border = 0
    '        Next i

    '        'xray dlg.getControl("lblOG")
    '        sql = "UPDATE RECIPE SET "_
    '	& "BOIL_SIZE=" & boilSize & ","_
    '	& "EST_OG=" & dlg.getControl("lblOG").text & ","_
    '	& "EST_FG=" & dlg.getControl("lblFG").text & ","_
    '	& "EST_ABV=" & dlg.getControl("lblABV").text & ","_
    '	& "IBU=" & dlg.getControl("lblIBU").text & ","_
    '	& "EST_COLOR=" & dlg.getControl("lblColor").text _
    '	& " WHERE ID=" & recipeID
    '	sqlx(sql)
    '        recipeLoading = False
    '        Exit Sub
    'errorExit:
    '        MsgBox "Error loading this recipe. After the next prompt, you might have to hit the Esc key to terminate the program, then restart." 
    '    If MsgBox("Do you want to delete this recipe?", 4 + 32 + 256, "Delete Recipe") = 6 Then dlgRecipeDelete(recipeID)
    '        recipeID = 0

    '    End Sub


End Module
