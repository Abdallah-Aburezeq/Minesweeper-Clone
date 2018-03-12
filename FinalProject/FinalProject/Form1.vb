
'GAME CODE BY: ABDALLAH R ABU-REZEQ


Public Class Form1
    '****************************************************************************************************************************************************************
    '*Known Bugs:                                                                                                                                                   *
    '*     FIXED 1.Flagging a tile then revealing it makes the counter show an incorrect number of remaining mines left.                                            *
    '*     FIXED 2.Flagging a chokepoint of empty squares prevents floodFill revealing anything past the choke point.                                               *
    '*           3.Clicking the form background throws an invalid cast exception (type form to type picturbox).                                                     *
    '*           4.The player can lose at the very first click, because mines are seeded before the first click not after.                                          * 
    '*           5.Mashing the mouse buttons during a floodFill messes up the counter.                                                                              *
    '*           6.In some cases mashing the mouse buttons over other squares during a floodFill incorrectly reveals squares. (Possible input pause required?)      *
    '****************************************************************************************************************************************************************


    Const TILESET_LENGTH As Integer = 61
    Const TILESET_WIDTH As Integer = 61


    'The grid of 9x9 buttons in an array
    'These two arrays are parrallel in that index 0 of both arrays hold data for the first (top left) control, the picturebox holds the actual control, and the array integer holds what should..
    '...be "behind" that control
    Dim gameGrid(80) As PictureBox
    Dim mineMap(80) As Integer

    '10 flags to imply how many mines, and allow player to flag them
    Dim flags As Integer = 10

    Sub creatMineMapArray()

        'Now I create the array that will hold which buttons on the grid are mines

        Dim mineRandom As New Random() 'need random for random mine placement

        'Initialize all values to 0, which is a clear space on the map, 99+ is a mine
        For i = 0 To 80
            mineMap(i) = 0
        Next

        'Seed map with 10 random mines
        For i = 0 To 9
            Dim index As Integer = mineRandom.Next(0, 80)

            If mineMap(index) = 99 Then      'if there already exists a mine in that spot, then redo index until a spot without a mine
                Do Until mineMap(index) <> 99
                    index = mineRandom.Next(0, 80)
                Loop
                mineMap(index) = 99

            Else
                mineMap(index) = 99
            End If
        Next

        'Iterate through mineMap and add 1 to all squares adjacent to a mine
        For i = 0 To 80

            'Add 1 to any adjacent squares if it is a mine
            If mineMap(i) >= 99 Then

                'These are the edges and must be dealt with differtly than normal cases
                If i = 0 Or i = 8 Or i = 72 Or i = 80 Then
                    Select Case i
                        Case 0
                            mineMap(i + 1) += 1 'RIGHT
                            mineMap(i + 9) += 1 'BELOW
                            mineMap(i + 10) += 1 'LOWER RIGHT
                        Case 8
                            mineMap(i - 1) += 1 'LEFT
                            mineMap(i + 9) += 1 'BELOW
                            mineMap(i + 8) += 1 'LOWER LEFT
                        Case 72
                            mineMap(i - 9) += 1 'ABOVE
                            mineMap(i + 1) += 1 'RIGHT
                            mineMap(i - 8) += 1 'UPPER RIGHT
                        Case 81
                            mineMap(i - 1) += 1 'LEFT
                            mineMap(i - 9) += 1 'ABOVE
                            mineMap(i - 10) += 1 'UPPER LEFT
                    End Select

                ElseIf (i Mod 9) = 0 Then 'This would be the LEFT border and must be dealt with differently
                    mineMap(i - 9) += 1 'ABOVE
                    mineMap(i - 8) += 1 'UPPER RIGHT
                    mineMap(i + 1) += 1 'RIGHT
                    mineMap(i + 10) += 1 'LOWER RIGHT
                    mineMap(i + 9) += 1 'BELOW

                ElseIf ((i + 1) Mod 9) = 0 Then 'This would be the RIGHT border and must be dealt with differently
                    mineMap(i - 9) += 1 'ABOVE
                    mineMap(i - 10) += 1 'UPPER LEFT
                    mineMap(i - 1) += 1 'LEFT
                    mineMap(i + 8) += 1 'LOWER LEFT
                    mineMap(i + 9) += 1 'BELOW

                ElseIf i > 0 And i < 8 Then 'This would be the TOP border and must be dealt with differently
                    mineMap(i + 1) += 1 'RIGHT
                    mineMap(i + 10) += 1 'LOWER RIGHT
                    mineMap(i + 9) += 1 'BELOW
                    mineMap(i + 8) += 1 'LOWER LEFT
                    mineMap(i - 1) += 1 'LEFT

                ElseIf i > 72 And i < 80 Then 'This would be the BOTTOM border and must be dealt with differently
                    mineMap(i + 1) += 1 'RIGHT
                    mineMap(i - 8) += 1 'UPPER RIGHT
                    mineMap(i - 9) += 1 'ABOVE
                    mineMap(i - 10) += 1 'UPPER LEFT
                    mineMap(i - 1) += 1 'LEFT

                Else                            'All other cases, the controls within the borders
                    mineMap(i + 1) += 1 'RIGHT
                    mineMap(i - 1) += 1 'LEFT
                    mineMap(i - 8) += 1 'UPPER RIGHT
                    mineMap(i + 8) += 1 'LOWER LEFT
                    mineMap(i + 9) += 1 'BELOW
                    mineMap(i - 9) += 1 'ABOVE
                    mineMap(i + 10) += 1 'LOWER RIGHT
                    mineMap(i - 10) += 1 'UPPER LEFT
                End If
            End If
        Next
    End Sub

    Sub creatGameControls()

        Dim BTN_Coordinate_X As Integer = 62  'Initial button X location / The top left control
        Dim BTN_Coordinate_Y As Integer = 106  'Initial button X location / The top left control

        Dim BTN_LOCATION_X_OFFSET As Integer = TILESET_LENGTH 'The X Coordinate offest / where next button will be
        Dim BTN_LOCATION_Y_OFFSET As Integer = TILESET_WIDTH 'THE Y Coordinate offset / where next button will be

        Dim BtnCoordinate As New Point 'The point that will be passed to the button control for its position
        BtnCoordinate.X = BTN_Coordinate_X
        BtnCoordinate.Y = BTN_Coordinate_Y

        'Here I create, position, and place new buttons within the array and form
        For i = 0 To 80
            If (i Mod 9) = 0 And i <> 0 Then
                BtnCoordinate.Y += BTN_LOCATION_Y_OFFSET
            End If

            If BtnCoordinate.X = (BTN_Coordinate_X + (BTN_LOCATION_X_OFFSET * 9)) Then
                BtnCoordinate.X = BTN_Coordinate_X
            End If

            Dim gameButton As New PictureBox
            gameGrid(i) = gameButton
            gameGrid(i).Width = TILESET_WIDTH
            gameGrid(i).Height = TILESET_WIDTH
            gameGrid(i).Location = BtnCoordinate
            gameGrid(i).Image = My.Resources.Minesweeper_LAZARUS_61x61_unexplored
            '***********************************************************************
            ''UNCOMMENT THIS CODE IN ORDER TO HAVE ALL MINES BE VISIBLE
            'If mineMap(i) >= 99 Then
            '    gameGrid(i).Image = My.Resources.Minesweeper_LAZARUS_61x61_mine
            'End If
            '***********************************************************************
            gameGrid(i).Tag = "NUMBER"

            If mineMap(i) > 90 Then       'if anything is over the value 90, it is a mine
                gameGrid(i).Tag = "MINE"  'Set a tag so program will know that control is a mine
            ElseIf mineMap(i) = 0 Then
                gameGrid(i).Tag = "EMPTY" 'Need a tag for empty controls for floodFill to work properly
            End If

            Controls.Add(gameButton)
            AddHandler gameGrid(i).MouseDown, AddressOf gameControl_mouseDown

            BtnCoordinate.X += BTN_LOCATION_X_OFFSET
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        creatMineMapArray()

        creatGameControls()

    End Sub

    Sub gameControl_mouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        'Was having issues with errors if the user clicked anything other than Picturbox controls(ie. the form BG), this Try/Catch is a viable fix but not optimal, preferable to create-
        'some way to find what type of control was clicked before setting it as a picturebox.
        Try
            Dim gameControl As PictureBox = sender

            If e.Button = MouseButtons.Right Then       'if the event that was triggered was a right click do flagGameControl
                flagGameControl(gameControl)

            ElseIf e.Button = MouseButtons.Left Then    'if the event that was triggered was a left click reveal and check for win/loss conditions
                revealControl(gameControl)
                winCheck()

            End If

        Catch ex As Exception
            'When an error occurs in assigning the sender as a picture box it will throw an exception and not crash instead.
        End Try

    End Sub

    Private Sub flagGameControl(ByRef gameControl As PictureBox)
        'In this subroutine, the gameControl tag is checked for the Flagged tag and is dealt with by changing from undiscovered to flag and vice versa.

        If gameControl.Tag = "FLAGGED" Then
            gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_unexplored
            gameControl.Tag = ""
            flags += 1

        ElseIf gameControl.Tag = "REVEALED" Then
            'Do nothing, you cannot flag revealed squares

        Else
            gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_flag
            gameControl.Tag = "FLAGGED"
            flags -= 1
        End If

        flagLBL.Text = flags.ToString()
    End Sub

    Private Sub revealControl(ByRef gameControl As PictureBox)
        'This subroutine retrives the index of the game control, cross refrences its index in the mine map (what value is behind the control).
        'Then if its a mine, the player loses, if its a number that number will be revealed, and if its 0 then it will try to floodFill accordingly.

        Dim indexOfControl As Integer = Array.IndexOf(gameGrid, gameControl)
        Dim controlValue = mineMap(indexOfControl)

        'Catch that it was flagged and ensure correct number of mines stays consistent.
        If gameControl.Tag = "FLAGGED" Then
            flags += 1
            flagLBL.Text = flags.ToString()
        End If


        If controlValue > 0 And controlValue < 99 Then
            setControlAsset(controlValue, gameControl)

        ElseIf controlValue = 0 Then
            floodFillGrid(gameControl)


        ElseIf controlValue >= 99 Then
            loseGame(gameControl)

        End If

    End Sub

    Private Sub floodFillGrid(ByRef gameControl As PictureBox)
        'This is an altered unoptimized algorithm based on the flood fill/seed fill algorithm.
        'It checks for surrounding empty squares to fill in, but is recurssive and returns to the parent node once it has filled in its own surrouinding squares.


        If gameControl.Tag = "REVEALED" Then
            Return
        End If
        If gameControl.Tag = "FLAGGED" Then
            flags += 1
            flagLBL.Text = flags.ToString()
        End If
        If gameControl.Tag = "NUMBER" Then
            Dim outlineIndex As Integer = Array.IndexOf(gameGrid, gameControl)
            Dim outlineAssetValue As Integer = mineMap(outlineIndex)
            setControlAsset(outlineAssetValue, gameControl)
            Return
        End If

        Dim index As Integer = Array.IndexOf(gameGrid, gameControl)
        Dim assetValue As Integer = mineMap(index)
        setControlAsset(assetValue, gameControl)


        If index = 0 Or index = 8 Or index = 72 Or index = 80 Then
            Select Case index
                Case 0
                    floodFillGrid(gameGrid(index + 9))  'one square to the south
                    floodFillGrid(gameGrid(index + 1))  'one square to the east
                Case 8
                    floodFillGrid(gameGrid(index + 9))  'one square to the south
                    floodFillGrid(gameGrid(index - 1))  'one square to the west
                Case 72
                    floodFillGrid(gameGrid(index - 9))  'one square to the north
                    floodFillGrid(gameGrid(index + 1))  'one square to the east
                Case 80
                    floodFillGrid(gameGrid(index - 9))  'one square to the north
                    floodFillGrid(gameGrid(index - 1))  'one square to the west
            End Select
        ElseIf index > 0 And index < 8 Then
            floodFillGrid(gameGrid(index + 9))  'one square to the south
            floodFillGrid(gameGrid(index - 1))  'one square to the west
            floodFillGrid(gameGrid(index + 1))  'one square to the east

        ElseIf index Mod 9 = 0 Then
            floodFillGrid(gameGrid(index + 9))  'one square to the south
            floodFillGrid(gameGrid(index - 9))  'one square to the north
            floodFillGrid(gameGrid(index + 1))  'one square to the east

        ElseIf (index + 1) Mod 9 = 0 Then
            floodFillGrid(gameGrid(index + 9))  'one square to the south
            floodFillGrid(gameGrid(index - 9))  'one square to the north
            floodFillGrid(gameGrid(index - 1))  'one square to the west

        ElseIf index > 72 And index < 80 Then
            floodFillGrid(gameGrid(index - 9))  'one square to the north
            floodFillGrid(gameGrid(index - 1))  'one square to the west
            floodFillGrid(gameGrid(index + 1))  'one square to the east

        Else
            floodFillGrid(gameGrid(index + 9))  'one square to the south
            floodFillGrid(gameGrid(index - 9))  'one square to the north
            floodFillGrid(gameGrid(index - 1))  'one square to the west
            floodFillGrid(gameGrid(index + 1))  'one square to the east
        End If

    End Sub

    Private Sub setControlAsset(ByVal value As Integer, ByRef gameControl As PictureBox)
        'This subroutine is called when we want to reveal a square and will set the appropriate picture to its corresponding number

        If gameControl.Tag = "REVEALED" Then
            Return
        End If
        Select Case value
            Case 0
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_0
            Case 1
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_1
            Case 2
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_2
            Case 3
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_3
            Case 4
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_4
            Case 5
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_5
            Case 6
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_6
            Case 7
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_7
            Case 8
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_8
            Case 99 To 150
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_mine
            Case 999
                gameControl.Image = My.Resources.Minesweeper_LAZARUS_61x61_mine_hit
        End Select

        gameControl.Tag = "REVEALED"

    End Sub

    Private Sub winCheck()
        'This subroutine checks to see that the playes has revealed all the squares except for the mines, which is 71 squares (9 by 9 = 81 - 10(mines) = 71)

        Dim numberRevealed As Integer = 0

        For x = 0 To 80
            If mineMap(x) < 99 And gameGrid(x).Tag = "REVEALED" Then
                numberRevealed += 1
            End If
        Next

        If numberRevealed = 71 Then
            MessageBox.Show("THE MISSION WAS A SUCCESS! GREAT JOB CLEARING THE FIELD!")
            Me.Close()
        End If

        numberRevealed = 0
    End Sub

    Private Sub loseGame(ByRef gameControl As PictureBox)
        'This subroutine is only called when the player has already hit a mine, it sets the mine that was set off as 999(for the mine hit asset)
        'Then it will reveal where the remaining mines where, show a message that the player lost, and force close the program. 

        mineMap(Array.IndexOf(gameGrid, gameControl)) = 999

        For x = 0 To 80
            If mineMap(x) >= 99 Then
                setControlAsset(mineMap(x), gameGrid(x))
            End If
        Next

        MessageBox.Show("MISSION FAILED! BETTER LUCK NEXT TIME!")

        Me.Close()
    End Sub

    Private Sub Credits_Click(sender As Object, e As EventArgs) Handles Credits.Click
        MessageBox.Show("Designed and Programmed by: Abdallah R Abu-Rezeq;                                             
Tileset by: Eugene Loza from (http://opengameart.org/content/minesweeper-tile-set-lazarus);    
Created for CS1408 (Cyril Harris) UHD")
    End Sub
End Class
